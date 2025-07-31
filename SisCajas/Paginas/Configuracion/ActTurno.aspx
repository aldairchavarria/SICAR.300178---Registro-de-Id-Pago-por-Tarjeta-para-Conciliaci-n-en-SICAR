<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ActTurno.aspx.vb" Inherits="SisCajas.ActTurno" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Sistema de Cajas</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../estilos/est_General.css" type="text/css" rel="styleSheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="frmTurno" method="post" runat="server">
			<table borderColor="#336699" cellSpacing="0" cellPadding="4" width="410" align="center"
				border="1">
				<tr>
					<td align="center"><asp:label class="TituloRConsulta" id="lblTitActualizaBIN" runat="server">Actualizacion de Turnos</asp:label></td>
				</tr>
			</table>
			<br>
			<table borderColor="#336699" cellSpacing="0" cellPadding="4" width="375" align="center"
				border="0">
				<tr>
					<td><asp:label class="Arial12b" id="lblCodigoBin" runat="server">Descripción</asp:label></td>
					<td><input class="clsInputEnable" id="txtDescripcion" style="WIDTH: 200px" type="text" maxLength="60"
							size="20" name="txtDescripcion" runat="server"></td>
				</tr>
				<tr>
					<td><asp:label class="Arial12b" id="lblDesBIN" runat="server">Hora Inicio</asp:label></td>
					<td class="Arial12b"><input class="clsInputEnable" id="txtHoraIni" style="WIDTH: 50px" type="text" maxLength="5"
							size="20" name="txtHoraIni" runat="server">&nbsp;&nbsp;&nbsp;(am/pm)</td>
				</tr>
				<tr>
					<td><asp:label class="Arial12b" id="Label2" runat="server">Hora Fin</asp:label></td>
					<td class="Arial12b"><input class="clsInputEnable" id="txtHoraFin" style="WIDTH: 50px" type="text" maxLength="5"
							size="20" name="txtHoraFin" runat="server">&nbsp;&nbsp;&nbsp;(am/pm)</td>
				</tr>
				<tr>
					<td><asp:label class="Arial12b" id="Label3" runat="server">Tolerancia</asp:label></td>
					<td><input class="clsInputEnable" onkeypress="f_Numeros()" id="txtTolerancia" style="WIDTH: 50px"
							type="text" maxLength="2" size="25" name="txtTolerancia" runat="server"></td>
				</tr>
				<tr>
					<td><asp:label class="Arial12b" id="Label1" runat="server">Estado</asp:label></td>
					<td><select class="Arial12b" id="selEstado" runat="server" border="0">
							<option value="0" selected>-- Seleccione --</option>
							<option value="1">Activo</option>
							<option value="2">Inactivo</option>
						</select></td>
				</tr>
			</table>
			<br>
			<br>
			<table borderColor="#336699" cellSpacing="0" cellPadding="4" width="300" align="center"
				border="1">
				<tr>
					<td>
						<table class="Arial10B" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
							<tr>
								<td align="center"><input class="BotonOptm" id="cmdGrabar" style="WIDTH: 100px" type="button" value="Grabar"
										name="btnGrabar" runat="server">&nbsp;&nbsp;</td>
								<td align="center"><input class="BotonOptm" id="cmdCancelar" style="WIDTH: 100px" onclick="f_Cancelar()" type="button"
										value="Cancelar" name="btnCancelar">&nbsp;&nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<input type="hidden" id="txtAccion" name="txtAccion"> <input type="hidden" id="txtIdSession" name="txtIdSession">
		</form>
		<script language="javascript">
			function f_Cancelar(){
				window.close();
			}	
			function f_Numeros(){
				if (window.event.keyCode != "13"){
					if(String.fromCharCode(window.event.keyCode)<"0" ||String.fromCharCode(window.event.keyCode)>"9"){
						window.event.returnValue=0
					}
				}
			}
		</script>
	</body>
</HTML>
