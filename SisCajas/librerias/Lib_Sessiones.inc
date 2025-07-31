<%
	Response.buffer = true
	Response.clear
	Response.expires = 0       
	Server.ScriptTimeout = 10000000
	Session.Timeout = 360
	
	if Session("USUARIO")="" then
%>				
		<script	language="JavaScript">
		alert('se perdio la conexión al servidor, vuelva a logearse porfavor');
		parent.document.location.href='../index.asp'
		</script>				
<%				
		response.end
	End if
	
%>

<script language=javascript>
	servidor = window.location.hostname ;
	wparent = new String(window.parent.location.href);
	if (window.parent.frames[0]==null) {
		if (eval("window.opener") == null) {
			window.location.replace('../../index.asp');
		}
		else {
			if (window.opener.parent.frames[0]==null) {
				window.location.replace('../../index.asp');
			}
		}
	}
</script>