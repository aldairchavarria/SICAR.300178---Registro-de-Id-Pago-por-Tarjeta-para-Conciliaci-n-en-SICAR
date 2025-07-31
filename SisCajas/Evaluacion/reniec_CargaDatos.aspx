<%@ Page Language="vb" AutoEventWireup="false" Codebehind="reniec_CargaDatos.aspx.vb" Inherits="SisCajas.reniec_CargaDatos"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Aplicativo CLARO<</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="JavaScript" src="../librerias/Lib_FuncValidacion.js"></script>
		<script LANGUAGE="JavaScript">
<!--
	function f_VerDetalle(p_strDNI) {
		var url = 'reniec_consolidado.aspx?strDNI=' + p_strDNI;
		openwindowdiagolo(url,'Impresión',660,580,250,10);
	}

	window.onload = function()
	{
		<%If (Len(strMsgError)>0) Then%>
			alert('<%=strMsgError%>');
		<%Else%>
			<%If blnVerConsolidado Then%>
				f_VerDetalle('<%=Request("hidNumDoc")%>');
			<%End If%>
		<%End If%>
		if (window.parent.document.frmPrincipal.btnBuscar != null) {
			window.parent.document.frmPrincipal.btnBuscar.disabled=false;
			window.parent.document.frmPrincipal.btnLimpiar.disabled=false;
		}
	}
//-->
		</script>
	</HEAD>
	<body topmargin="0" leftmargin="0" marginwidth="0" marginheight="0">
		<form name="frm" id="frm" method="post" runat="server">
			<input type="hidden" id="hidAccion" name="hidAccion" value="0"> 
			<input type="hidden" id="hidNumDoc" name="hidNumDoc">
			<input type="hidden" id="hidTXReniec" name="hidTXReniec">
		</form>
	</body>
</HTML>
