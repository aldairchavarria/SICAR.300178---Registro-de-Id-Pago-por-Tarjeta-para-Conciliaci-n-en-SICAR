<%@ Page Language="vb" AutoEventWireup="false" aspcompat=true Codebehind="reniec_Aproximacion.aspx.vb" Inherits="SisCajas.reniec_Aproximacion"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
    <title>reniec_Aproximacion</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="../estilos/est_General.css" rel="styleSheet" type="text/css">
    <script language="JavaScript" src="../librerias/Lib_FuncValidacion.js"></script>
  </head>
  
   
  
  <script LANGUAGE="JavaScript">
<!--

	document.oncontextmenu = function()
	{
		//return false
	}

	if(document.layers)
	{
		window.onmousedown = function(e)
		{
			if(e.target==document)
				return false;
		}
	}

//-->
</script>

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

	function f_Regresar() {
		document.frmPrincipal.hidAccion.value = <%=K_REGRESAR%>;
		document.frmPrincipal.submit();
	}

	function f_NavBloquePagina(p_intNav) {
		var lintNroBloquePagina = document.frmPrincipal.hidNroBloquePagina.value;
		if ( p_intNav == <%=K_PAGINA_INICIO%> ) { lintNroBloquePagina = 1; }
		if ( p_intNav == <%=K_PAGINA_ANTERIOR%> ) { if (lintNroBloquePagina > 1) lintNroBloquePagina--; }
		if ( p_intNav == <%=K_PAGINA_SIGUIENTE%> ) { if (lintNroBloquePagina < <%=intNroMaxBloquePagina%>) lintNroBloquePagina++; }
		if ( p_intNav == <%=K_PAGINA_FINAL%> ) { lintNroBloquePagina = <%=intNroMaxBloquePagina%> }
		document.frmPrincipal.hidNroBloquePagina.value = lintNroBloquePagina;
		f_CargaBloquePagina(lintNroBloquePagina);
	}

	function f_CargaBloquePagina(p_intNroBloquePagina) {
		var lintPagina = 1;
		var lstrhtml = '';
		for(var i=1; i<=15; i++) {
			lintPagina = ((p_intNroBloquePagina-1)*15) + i;
			if ( lintPagina == <%=intNroPagina%>)
				lstrhtml += "<a href='javascript:f_IrPagina(" + lintPagina.toString() + ");' style='text-decoration: none;'><b>" + lintPagina.toString() + "</b></a>&nbsp;";
			else
				lstrhtml += "<a href='javascript:f_IrPagina(" + lintPagina.toString() + ");' style='text-decoration: none;'>" + lintPagina.toString() + "</a>&nbsp;";
			if (lintPagina >= <%=intNroMaxPagina%>) break;
		}
		document.all.tdBloquePagina.innerHTML = lstrhtml;
	}

	function f_IrPagina(p_intNroPagina) {
		if ( <%=intNroPagina%> != p_intNroPagina) {
			document.frmPrincipal.hidNroPagina.value = (p_intNroPagina);
			document.frmPrincipal.hidAccion.value = <%=K_PAGINAR%>;
			document.frmPrincipal.submit();
		}
	}

	function f_Buscar(p_strNroDNI) {
		ifraCargaDatos.document.frm.hidNumDoc.value = p_strNroDNI;
		ifraCargaDatos.document.frm.hidTXReniec.value = <%=K_TX_NOMBRE%>;
		ifraCargaDatos.document.frm.hidAccion.value = <%=K_BUSCAR%>;
		ifraCargaDatos.document.frm.submit();
	}

	function DoScroll()
	{
		document.all("tblGridHeader").style.pixelLeft = divGridRow.scrollLeft * -1;
	}

//-->
</script>

<body topmargin="0" leftmargin="0" marginwidth="0" marginheight="0" >
<form name="frmPrincipal" id="frmPrincipal" method="post">
<input type=hidden id="hidAccion" name="hidAccion" value=0>
<input type=hidden id="hidNroMaxBloquePagina" name="hidNroMaxBloquePagina" value=<%=intNroMaxBloquePagina%>>
<input type=hidden id="hidNroBloquePagina" name="hidNroBloquePagina" value=<%=intNroBloquePagina%>>
<input type=hidden id="hidNroMaxPagina" name="hidNroMaxPagina" value=<%=intNroMaxPagina%>>
<input type=hidden id="hidNroPagina" name="hidNroPagina" value=<%=intNroPagina%>>
<input type=hidden id="hidApePaterno" name="hidApePaterno" value="<%=strApePaterno%>">
<input type=hidden id="hidApeMaterno" name="hidApeMaterno" value="<%=strApeMaterno%>">
<input type=hidden id="hidNombre" name="hidNombre" value="<%=strNombre%>">

<table width="820" cellpadding="0" cellspacing="0" border="0">
<tr>
	<td width="10" valign="top">&nbsp;</td>
	<td width="820" align="center" valign="top">

		<table width="820" height="14" border="0" cellspacing="0" cellpadding="0">
			<tr>
				<td align="center"></td>
			</tr>
		</table>

        <table width="810" cellspacing="0" cellpadding="0" class="tabla_borde">
		<tr>
			<td width="98%" style="padding:5">

				<table width="100%" border="0" cellspacing="0" cellpadding="0">
			    <tr>
					<td height="30" align="center" class="TituloRConsulta">RESULTADOS</td>
				</tr>
				</table>

			</td>
		</tr>
		<tr><td><img src="" border=0 width=1 height=8></td></tr>
		</table>
		<br>

		<table width="810" cellspacing="0" cellpadding="0" class="tabla_borde">
        <tr>
			<td>
				<div class="clsGridHeader" style="width:805px;" id="divGridHeader">
					<table id="tblGridHeader" width="805">
						<tr height="21">
						  <td height=22 width="80" align=center>NroDNI</td>
						  <td height=22 width="200" align=center>Nombre</td>
						  <td height=22 width="265" align=center>Apellido Paterno</td>
						  <td height=22 width="265" align=center>Apellido Materno</td>
						</tr>
					</table>
				</div>

				<div class="clsGridRow" style="width:805px; height:238px;" id="divGridRow" onscroll="DoScroll()">
					<table id="tblGridGridRow" width="788">
				    <%
				    dim objRegistro as object
				    dim intIndice as integer
					If Not objValores Is Nothing Then
					  If objValores("CodigoError") = 0 Then
						If (objValores("RegEnviados") > 0) Then
							objRegistro = objValores("Registro")
							For intIndice = 1 To objValores("RegEnviados") %>
								<%If (intIndice mod 2) = 0 Then%>
								<tr class="clsRowOn">
								<%Else%>
								<tr class="clsRowOff">
								<%End If%>
									<td height=22 width="80" align=center style="cursor:hand;text-decoration:underline" onClick="f_Buscar('<%=objRegistro(intIndice)("NumDoc")%>');"
										title="Ver consolidado de datos"><%=objRegistro(intIndice)("NumDoc")%></td>
									<td height=22 width="200" align=left><%=objRegistro(intIndice)("Nombres")%></td>
									<td height=22 width="265" align=left><%=objRegistro(intIndice)("ApePaterno")%></td>
									<td height=22 width="265" align=left><%=objRegistro(intIndice)("ApeMaterno")%></td>
								</tr>
							<%Next
						End If
					  end if	
					End If

					objRegistro = Nothing
					objValores = Nothing
					%>
					</table>
				</div>
			</td>
		</tr>
		</table >

		<table border=0 width=100 cellpadding=0 cellspacing=0><tr><td><img src="" border=0 width=1 height=8></td></tr></table>

		<table width="810" cellspacing="0" cellpadding="0" class="tabla_borde">
		<tr>
			<td width="810" align="center">
				<table bordercolor="#FFFFFF" border="1" width="100%" cellspacing="1" cellpadding="1">
				<tr class="Arial11B">
					<td width="100%" colspan="11" align="right"><b>Total de Registros:</b>
					<%=intNroReg%> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Total de Páginas:</b>
					<%=intNroMaxPagina%></td>
				</tr>
				</table>
			</td>
		</tr>
		</table>

		<table border=0 width=100 cellpadding=0 cellspacing=0><tr><td><img src="" border=0 width=1 height=8></td></tr></table>

		<table width="580" cellspacing="0" cellpadding="4" class="tabla_borde">
		<tr>
			<td align="center">
				<table width="90%" border='0' cellspacing='2' cellpadding='0'>
				<tr>
					<td width="40" align="left"><input name="btnInicio" type="button" class=BotonOptm style="width:30px" onClick="f_NavBloquePagina(<%=K_PAGINA_INICIO%>);" value="<<" title='Ir al inicio'></td>
					<td width="40" align="left"><input name="btnAnterior" type="button" class=BotonOptm style="width:30px" onClick="f_NavBloquePagina(<%=K_PAGINA_ANTERIOR%>);" value="<" title='15 Páginas Anteriores'></td>
					<td width="362" align=center style="font-family:arial,Verdana; font-size:13px;" id="tdBloquePagina">
					<script language=JavaScript type='text/javascript'>f_NavBloquePagina(<%=intNroBloquePagina%>);</script>
					</td>
					<td width="40" align="right"><input name="btnSiguiente" type="button" class=BotonOptm style="width:30px" onClick="f_NavBloquePagina(<%=K_PAGINA_SIGUIENTE%>);" value=">" title='15 Páginas Siguientes'></td>
					<td width="40" align="right"><input name="btnFinal" type="button" class=BotonOptm style="width:30px" onClick="f_NavBloquePagina(<%=K_PAGINA_FINAL%>);" value=">>" title='Ir al final'></td>
				</tr>
				</table>
			</td>
		</tr>
		</table>

		<table border=0 width=100 cellpadding=0 cellspacing=0><tr><td><img src="" border=0 width=1 height=8></td></tr></table>

		<table width="260" cellspacing="0" cellpadding="4" class="tabla_borde">
		<tr>
			<td align="center">
				<input name="btnRegresar" type="button" class=BotonOptm style="width:100px" onClick="f_Regresar();" value="Regresar">
			</td>
		</tr>
		</table>

	</td>
</tr>
</table>

<div STYLE="visibility:hidden;">
	<iframe id="ifraCargaDatos" width="0" height="0" src="./reniec_CargaDatos.aspx">
	</iframe>
<div>

</form>

</body>
  
  
  
</html>


