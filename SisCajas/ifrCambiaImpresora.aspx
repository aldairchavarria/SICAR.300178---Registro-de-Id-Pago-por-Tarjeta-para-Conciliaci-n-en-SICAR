<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ifrCambiaImpresora.aspx.vb" Inherits="SisCajas.ifrCambiaImpresora"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ifrCambiaImpresora</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="estilos/est_General.css" type="text/css" rel="styleSheet">
		<script language="jscript">
		
		window.onload = window_onload;
		
		function window_onload()
		{
			
			var txtCanal = document.getElementById("txtCanal");
			var txtRows = document.getElementById("txtRows");
					
			if (txtCanal.value !="MT" || txtRows.value=="" )
			{			
				parent.document.getElementById("txtFlag").value="NV";
				document.all.tbImp.style.display="none";
			}

			if (txtCanal.value == "MT" && txtRows.value=="" )
			{			
				parent.document.getElementById("txtFlag").value="NV";
				document.all.tbImp.style.display="none";
			}
			
			if ( txtRows.value !="" )
			{			
				parent.document.getElementById("txtFlag").value="SV";
		}
			
		}
			
		function ValidaUsuarioImp()
		{
			var arr;
			var breturn = true;
			
			var ocbImpresora = document.getElementById("cbImpresora");
			var otxtUsuario = document.getElementById("txtUsuario");
			var otxtFecha = document.getElementById("txtFecha");
			
			if (ocbImpresora.value != "")
			{
				parent.document.getElementById("txtFlag").value="NV";
				parent.document.getElementById("txtImpresora").value = ocbImpresora.value;
			
				arr = ocbImpresora.value.split(";");
				
				if (arr[2] != "")
				{						
					if (otxtUsuario.value != arr[2] && otxtFecha.value == arr[3])
					{
						var mensaje="Esta impresora ya se encuentra asiganda al Usuario = " + arr[2] + " ¿Esta seguro que desea usar la misma Impresora?";
						breturn = confirm(mensaje);
						
						if (! breturn)
						{
							parent.document.getElementById("txtFlag").value="SV";
							parent.document.getElementById("txtImpresora").value= "";
							parent.document.getElementById("txtMensajeImp").value= "";
							
							event.returnValue = false;
						}
						else
						{
							parent.document.getElementById("txtFlag").value="NV";
							parent.document.getElementById("txtImpresora").value= ocbImpresora.value;
							parent.document.getElementById("txtMensajeImp").value= mensaje;
						}
					}
				}
			}
			return breturn;
		}
			
					
		</script>
	</HEAD>
	<body leftMargin="0" topMargin="0">
		<form id="frmPrincipal" method="post" runat="server">
			<table ID="tbImp" cellSpacing="0" cellPadding="0" width="395" border="0">
				<tr>
					<td class="login01" vAlign="middle" align="left" width="95">&nbsp;&nbsp;Impresora:</td>
					<td align="left" width="250">&nbsp;&nbsp;
						<asp:dropdownlist id="cbImpresora" runat="server" CssClass="clsSelectEnable" Width="175px"></asp:dropdownlist></td>
				</tr>
			</table>
			<br>
			<br>
			&nbsp; 
			<INPUT id="txtUsuario" type="hidden" runat="server">
			<INPUT id="txtFecha" type="hidden" runat="server">
			<INPUT id="txtMensaje" type="hidden" NAME="txtMensaje">
			<INPUT id="txtCanal" type="hidden" NAME="txtCanal" runat="server">
			<INPUT id="txtRows" type="hidden" NAME="txtRows" runat="server">
		</form>
	</body>
</HTML>
