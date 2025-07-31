<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ActBin.aspx.vb" Inherits="SisCajas.ActBin" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Sistema de Cajas</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="../../estilos/est_General.css" rel="styleSheet" type="text/css">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table width="375" border="1" align="center" cellpadding="4" cellspacing="0" bordercolor="#336699">
				<tr>
					<td align="center">
						<asp:Label id="lblTitActualizaBIN" runat="server" class="TituloRConsulta">Actualizacion de datos BIN</asp:Label>
					</td>
				</tr>
			</table>
			<br>
			<table width="350" border="0" align="center" cellpadding="4" cellspacing="0" bordercolor="#336699">
				<tr>
					<td><asp:Label id="lblCodigoBin" runat="server" class="Arial12b">Numero</asp:Label></td>
					<td><input name="txtNumero" type="text" style="WIDTH:75px" class="clsInputEnable" id="txtNumero"
							size="25" maxlength="15" runat="server"></td>
				</tr>
				<tr>
					<td><asp:Label id="lblDesBIN" runat="server" class="Arial12b">Descripcion</asp:Label></td>
					<td><input name="txtDescripcion" type="text" style="WIDTH:200px" class="clsInputEnable" id="txtDescripcion"
							size="25" maxlength="150" runat="server"></td>
				</tr>
				<tr>
					<td><asp:Label id="Label1" runat="server" class="Arial12b">Estado</asp:Label></td>
					<td><select id="selEstado" border="0" class="Arial12b" runat="server">
							<option value="0" selected>-- Seleccione --</option>
							<option value="1">Activo</option>
							<option value="2">Inactivo</option>
						</select></td>
				</tr>
			</table>
			<br>
			<br>
			<table width="300" border="1" align="center" cellpadding="4" cellspacing="0" bordercolor="#336699">
				<tr>
					<td>
						<table width="100%" border="0" cellspacing="0" cellpadding="0" align="center" class="Arial10B">
							<tr>
								<td align="center">
									<input name="btnGrabar" type="button" class="BotonOptm" style="WIDTH:100px" value="Grabar"
										id="cmdGrabar" runat="server">&nbsp;&nbsp;</td>
								<td align="center">
									<input name="btnCancelar" type="button" class="BotonOptm" style="WIDTH:100px" onClick="javascript:f_Cancelar();"
										value="Cancelar" id="cmdCancelar">&nbsp;&nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
		<script language="javascript">
	function f_Cancelar(){
		window.close();
	}
		</script>
	</body>
</HTML>
