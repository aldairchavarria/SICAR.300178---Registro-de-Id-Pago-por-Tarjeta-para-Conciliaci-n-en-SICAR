<% @ language="vbscript" %>
<%	Option Explicit %>
	<!-- #include file="Constantes_site.asp" -->
	<!-- #include file="GeneralFunctions.asp" -->
<%
	' Objectos de conexion
	Dim objComponente,objComponenteA,objRecordSet,sValorA,sValorB,sValorC,sAntValorA
	Set objComponente = Server.CreateObject("COM_PVU_General.SAPGeneral")
%>
<script language="JavaScript" type="text/JavaScript">
<!--
function MM_reloadPage(init) {  //reloads the window if Nav4 resized
	if (init==true) with (navigator) {
		if ((appName=="Netscape")&&(parseInt(appVersion)==4)) {
			document.MM_pgW=innerWidth;
			document.MM_pgH=innerHeight;
			onresize=MM_reloadPage;
		}
	} else {
		if (innerWidth!=document.MM_pgW || innerHeight!=document.MM_pgH) {
			location.reload();
		}
	}
}		
MM_reloadPage(true);

function cerrarSession() {
	parent.document.location.href = "../index.asp";
}

function pantallaInicio() {
	parent.contenido.location.href = "../paginas/Inicio/inicial.asp";
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
.txtIco { padding-left:0px; padding-bottom:6px; font-family:arial,verdana; font-size:11px; font-weight:bold; color:#666666; text-decoration:none; padding-top:6px }
</style>

<body leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">

<table width="100%" border="0" cellpadding="0" cellspacing="0" background="../images/fondos/new_backtim.gif">
  <tr>
    <td valign=top style="padding-top:7px;padding-left:4px" width=400><img border=0 src=../images/titulos/new_titpvu.gif></td>
    <td style="padding-top:3px">
	<table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
      <td width="20%" class=txtUp>Tienda:</td>
      <td width="80%" class=txtUp><%=Session("ALMACEN")%>&nbsp;-&nbsp;<%=SESSION("MUNDOTIM")%></td>
    </tr>
    <tr>
      <td class=txtUp>Usuario:</td>
      <td class=txtUp><%=Session("USUARIO")%>&nbsp;-&nbsp;<%=SESSION("NOMBRE_COMPLETO")%></td>
    </tr>
  </table>
  <table width="100%" border="0" cellspacing="0" cellpadding="0">
	<tr> 	
      <td height="22" valign="bottom" class=txtIco><a href="javascript:pantallaInicio()" class=txtIco>Página
          Principal</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;<a href="javascript:cerrarSession()" class=txtIco>Cerrar Sesión</a></td>
	</tr>
  </table>
	</td>
    <td width="25%" valign="top" background="../images/fondos/new_backgris.gif" bgcolor="#DADADA"><img border=0 src=../images/principal/new_logotim.gif></td>
  </tr>
</table>

<form name="frmUbigeo" id="frmUbigeo">
<!-- #Include File="Obti_DptoI.asp" -->

<!-- #Include File="Obti_Prov.asp" -->

<!-- #Include File="Obti_Dist.asp" -->

<!-- #Include File="Obti_CPost.asp" -->

<!-- #Include File="Obti_GrpArti.asp" -->
</form>

<%
	Set objComponente = Nothing
%>
</body>
