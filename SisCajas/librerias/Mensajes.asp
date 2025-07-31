<!-- #includes File="../include/constantes_site.asp" -->
<html>
<head>
<title>Aplicativo TIM</title>
<link rel="stylesheet" href="<%=strRutaSite%>/Estilos/estilos.css" type="text/css">
</head>

<body topmargin="0" leftmargin="0" marginwidth="0" marginheight="0">
<table width="100%" cellpadding="0" cellspacing="0" border="0">
<tr><td colspan="2" background="<%=strRutaSite%>/../images/bg_tdtop.gif">
<!-- includes File="../include/top.asp" -->
</td></tr>
<tr>
<td width="170" valign="top"><!-- #include File="../include/menu.asp" --></td>
    <td valign="top" width="90%" style="padding-top:14; padding-left:4">
      <table width="550" border="0" cellspacing="0" cellpadding="0" align="center">
        <tr><td style="padding-left:100"><img src="../images/mensajes/msj_MensjSistema.gif" border="0" width="266" height="50"></td></tr>
        <tr><td height="2" bgcolor="#FFFFFF"></td></tr>
        <tr><td> 
            <table width="100%" border="1" cellspacing="0" cellpadding="1" align="center" bordercolor=<%=strColorResult%>>
              <tr><td bordercolor="#ffffff"> 
                  <table width="100%" border="2" cellspacing="0" cellpadding="0" bordercolor=<%=strBrColorResult%> >
                    <tr><td bordercolor="#ffffff" bgcolor=<%=strColorResult%> align="center" style="padding:4"> 
                        <table width="85%" border="0" cellspacing="6" cellpadding="0">
							<tr><td class="Arial12B"><%Response.Write(Request.QueryString("TextoMensaje"))%></td>
								<td width="100" align="right" style="padding-right:10"><img src="../images/iconos/ico_Alerta.gif" border="0" width="70" height="75"></td></tr>
                        </table>
                      </td></tr>
                  </table>
                </td></tr>
            </table>
          </td></tr>
        <tr><td height="2" bgcolor="#FFFFFF"></td></tr>
        <tr><td align="right" style="padding-right:70" background="../images/fondos/fnd_TrazoMensj.gif" height="96"> 
				<%if request("PVU")="OK" then%>
            <a href="../paginas/Debito/RegDomiciliacion.asp"><img src="../images/botones/btn_Anterior.gif" border="0" width="82" height="23"></a> 
				<%else%>
            <a href="../paginas/Debito/ConsultaDomiciliacion.asp"><img src="../images/botones/btn_Anterior.gif" border="0" width="82" height="23"></a> 
				<%end if%>
          </td></tr>
      </table>
    </td></tr>
</table>
</body>
</html>
