<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Top.aspx.vb" Inherits="SisCajas.Top"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
    <title>Top</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
  </head>
<script language="JavaScript" type="text/JavaScript">
<!--
function MM_reloadPage(init){  //reloads the window if Nav4 resized
	if (init==true) with (navigator) {
		if ((appName=="Netscape")&&(parseInt(appVersion)==4)) {
			document.MM_pgW=innerWidth;
			document.MM_pgH=innerHeight;
			onresize=MM_reloadPage;
		}
	}
	else{
		if (innerWidth!=document.MM_pgW || innerHeight!=document.MM_pgH) {
			location.reload();
		}
	}
}
MM_reloadPage(true);

function cerrarSession() {
	//parent.document.location.href = "home.aspx";
	parent.window.close();
}
function pantallaInicio() {
	//parent.contenido.location.href = "home.aspx";
	parent.document.location.href = "home.aspx";
}
//-->
</script>

<style>
.TituloTIM
{
   font-family:Verdana,Arial,Helvetica; 
   font-size:17px;
   color:#FFFFFF;
   font-weight:bold;
   height:50px;
   padding-left:20px;
   padding-top:5px;
   cursor:default;
}
.txtUp { font-size:12px; font-family:arial,verdana; color:#E7E7E7; font-weight:bold; }
.txtIco { padding-left:0px; padding-bottom:6px; font-family:arial,verdana; font-size:11px; font-weight:bold; color:white; text-decoration:none; padding-top:6px }
</style>

<body leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
<!--<table width="100%" border="0" cellpadding="0" cellspacing="0" background="images/fondos/new_backclaro.gif">-->
<table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#5991bb">
<tr>
    <td valign=top style="padding-top:6px;padding-left:3px" width=300><img border=0 src=images/titulos/new_titcaja.gif></td>
    <td style="padding-top:3px">
	<table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr><td width="25%" class=txtUp>Punto de Venta:</td>
		<td width="75%" class=txtUp><%=Session("ALMACEN")%>&nbsp;-&nbsp;<%=SESSION("OFICINA")%> &nbsp;<%=Session("SUBOFICINADAC")%></td>
		
	</tr>
	<tr><td class=txtUp>Usuario:</td>
		<td class=txtUp><%=Session("USUARIO")%>&nbsp;-&nbsp;<%=SESSION("NOMBRE_COMPLETO")%></td></tr>
			<tr>
				<td class="txtUp">Nodo:</td>
				<td class="txtUp"><%=Right(System.Net.Dns.GetHostName,2) %></td>
			</tr>
  </table>
  <table width="100%" border="0" cellspacing="0" cellpadding="0">
	<tr><td height="22" valign="bottom" class=txtIco align=left><a href="javascript:pantallaInicio()" class=txtIco>Página Principal</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;<a href="javascript:cerrarSession()" class=txtIco>Cerrar Sesión</a></td></tr>
  </table>
	</td>
	<td width="25%" valign="top"  bgcolor="#5991bb"><img border=0 src=images/botones/new_logotim.gif></td>
  </tr>
</table>
</body>
</html>
