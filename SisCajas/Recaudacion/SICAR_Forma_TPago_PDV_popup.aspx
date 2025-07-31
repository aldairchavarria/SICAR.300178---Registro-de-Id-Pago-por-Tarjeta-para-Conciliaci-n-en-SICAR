<%@ Page Language="vb" AutoEventWireup="false" Codebehind="SICAR_Forma_TPago_PDV_popup.aspx.vb" Inherits="SisCajas.SICAR_Forma_TPago_PDV_popup" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Registrar Nueva Forma de Pago por PDV</title> 
		<!-- Inicio - INI-1019 - YGP - Nueva pagina para registrar una forma de pago por PDV-->
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta content="no-cache" http-equiv="pragma">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="styleSheet" type="text/css" href="../estilos/est_General.css">
		<script language="JavaScript" src="../librerias/date-picker.js"></script>
		<script language="JavaScript" src="../librerias/Lib_FuncValidacion.js"></script>
		<script language="JavaScript" src="../librerias/msrsclient.js"></script>
		<script language="JavaScript" src="../librerias/jquery-1.8.2.min.js"></script>
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<base target="_self">
		<script>
		function mensaje(mensaje) {
			alert(mensaje);
		}
		
		function cerrarPopup(codPDV, codMedioPago){
			window.opener.refrescarGrid(codPDV, codMedioPago);
			window.close();	
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table style="Z-INDEX: 101; POSITION: relative; WIDTH: 368px; HEIGHT: 26px; TOP: 16px; LEFT: 4px"
				class="tabla_borde">
				<tr>
					<td class="TituloRConsulta"><div style="TEXT-ALIGN: center">Mantenimiento de Formas de 
							Pago por PDV</div>
					</td>
				</tr>
			</table>
			<br>
			<table style="Z-INDEX: 102; POSITION: relative; TOP: 5px; LEFT: 5px" class="Arial12B">
				<tr>
					<td style="WIDTH: 112px">&nbsp;PDV:&nbsp;&nbsp;&nbsp;&nbsp;
					</td>
					<td><asp:dropdownlist id="ddlPDV" runat="server" Width="240px" CssClass="clsSelectEnable" AutoPostBack="true"></asp:dropdownlist></td>
				</tr>
			</table>
			<table style="Z-INDEX: 103; POSITION: relative; TOP: 5px; LEFT: 5px" class="Arial12B">
				<tr>
					<td style="WIDTH: 112px">&nbsp;Forma de Pago:&nbsp;&nbsp;&nbsp;&nbsp;
					</td>
					<td><asp:dropdownlist id="ddlFP" runat="server" Width="240px" CssClass="clsSelectEnable"></asp:dropdownlist></td>
				</tr>
			</table>
			<br>
			<br>
			<table style="Z-INDEX: 104; POSITION: absolute; WIDTH: 360px; HEIGHT: 32px; TOP: 136px; LEFT: 16px">
				<TR>
					<td style="WIDTH: 90px"></td>
					<TD style="WIDTH: 204px" class="tabla_borde">
						<asp:button id="btnRegistrar" runat="server" CssClass="BotonOptm" Width="100px" Text="Registrar"></asp:button>
						<asp:button id="btnCancelar" runat="server" CssClass="BotonOptm" Width="100px" Text="Cancelar"
							style="Z-INDEX: 0"></asp:button></TD>
					<td style="WIDTH: 50px"></td>
				</TR>
			</table>
			<br>
		</form>
		<!-- Fin - INI-1019 - YGP -->
	</body>
</HTML>
