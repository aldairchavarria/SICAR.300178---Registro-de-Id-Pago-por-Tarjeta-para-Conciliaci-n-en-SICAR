<%@ Page Language="vb" AutoEventWireup="false" Codebehind="sicar_popup_MantCodigoTarjeta.aspx.vb" Inherits="SisCajas.sicar_popup_MantCodigoTarjeta"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Mantenimiento de Códigos de Tarjetas</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="styleSheet" type="text/css" href="../../estilos/est_General.css">
		<script language="JavaScript" src="../../librerias/date-picker.js"></script>
		<script language="JavaScript" src="../../librerias/Lib_FuncValidacion.js"></script>
		<script language="JavaScript" src="../../librerias/msrsclient.js"></script>
		<script language="JavaScript" src="../../librerias/jquery-1.8.2.min.js"></script>
		<base target="_self">
		<script>
		
		/*var ClosingVar = true;
		window.onbeforeunload = ExitCheck;
		
		function ExitCheck()
		{
			if(ClosingVar == true)
			{
				ExitCheck = false;
				return "Existe transacciones";
			
			}		
		}*/
		
		
		function f_Cancelar()
		{		
		window.close();
		}
		
		
		
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table style="POSITION: relative; TOP: 5px; LEFT: 5px" class="tabla_borde" cellSpacing="0"
				cellPadding="0" width="390" align="center">
				<tr>
					<td class="TituloRConsulta" height="30" colSpan="4" align="center">Mantenimiento de 
						Códigos de Tarjetas
					</td>
				</tr>
			</table>
			<br>
			<table style="POSITION: relative; TOP: 5px; LEFT: 5px" class="Arial12B" cellSpacing="5"
				cellPadding="0" width="390" align="center">
				<tr>
					<td style="WIDTH: 112px">&nbsp;Tarjeta:&nbsp;&nbsp;&nbsp;&nbsp;
					</td>
					<td><asp:dropdownlist id="cboMantTarjeta" runat="server" Width="240px" CssClass="clsSelectEnable"></asp:dropdownlist></td>
				</tr>
			</table>
			<table style="POSITION: relative; TOP: 5px; LEFT: 5px" class="Arial12B" cellSpacing="5"
				cellPadding="0" width="390" align="center">
				<tr>
					<td style="WIDTH: 112px">&nbsp;Código:&nbsp;&nbsp;&nbsp;&nbsp;
					</td>
					<td><asp:textbox style="Z-INDEX: 0" id="txtCodigo" runat="server" Width="140px" CssClass="clsInputEnable"></asp:textbox></td>
				</tr>
			</table>
			<table style="POSITION: relative; TOP: 5px; LEFT: 5px" class="Arial12B" cellSpacing="5"
				cellPadding="0" width="390" align="center">
				<tr>
					<td style="WIDTH: 112px">&nbsp;Comentario:&nbsp;&nbsp;&nbsp;&nbsp;
					</td>
					<td><asp:textbox id="txtComentario" runat="server" Width="240px" CssClass="clsInputEnable"></asp:textbox></td>
				</tr>
			</table>
			<table style="POSITION: relative; TOP: 5px; LEFT: 5px" class="Arial12B" cellSpacing="5"
				cellPadding="0" width="390" align="center">
				<tr>
					<td style="WIDTH: 112px">&nbsp;Estado:&nbsp;&nbsp;&nbsp;&nbsp;
					</td>
					<td><select class="clsSelectEnable" id="cboEstado" name="cboEstado" runat="server" border="0">
							<option value="0">Inactivo</option>
							<option value="1" selected>Activo</option>
						</select></td>
				</tr>
			</table>
			<br>
			<table style="POSITION: relative; TOP: 5px; LEFT: 5px" class="tabla_borde" cellSpacing="0"
				cellPadding="0" width="390" align="center">
				<tr>
					<td>
						<table style="POSITION: relative; TOP: 5px; LEFT: 5px" class="Arial12B" cellSpacing="5"
							cellPadding="0" width="370" align="center">
							<tr>
								<td style="WIDTH: 160px">Usuario Creaciòn:&nbsp;&nbsp;&nbsp;&nbsp;
								</td>
								<td><asp:textbox Enabled="False" style="Z-INDEX: 0" id="txtUsuarioCreacion" runat="server" Width="185px"
										CssClass="clsInputEnable" ReadOnly="True"></asp:textbox></td>
							</tr>
						</table>
						<table style="POSITION: relative; TOP: 5px; LEFT: 5px" class="Arial12B" cellSpacing="5"
							cellPadding="0" width="370" align="center">
							<tr>
								<td style="WIDTH: 160px">Fecha Creaciòn:&nbsp;&nbsp;&nbsp;&nbsp;
								</td>
								<td><asp:textbox style="Z-INDEX: 0" id="txtFechaCreacion" Enabled="False" runat="server" Width="185px"
										CssClass="clsInputEnable" ReadOnly="True"></asp:textbox></td>
							</tr>
						</table>
						<table style="POSITION: relative; TOP: 5px; LEFT: 5px" class="Arial12B" cellSpacing="5"
							cellPadding="0" width="370" align="center">
							<tr>
								<td style="WIDTH: 160px">Usuario Modificaciòn:&nbsp;&nbsp;&nbsp;&nbsp;
								</td>
								<td><asp:textbox style="Z-INDEX: 0" id="txtUsuarioModificacion" Enabled="False" runat="server" Width="175px"
										CssClass="clsInputEnable" ReadOnly="True"></asp:textbox></td>
							</tr>
						</table>
						<table style="POSITION: relative; TOP: 5px; LEFT: 5px" class="Arial12B" cellSpacing="5"
							cellPadding="0" width="370" align="center">
							<tr>
								<td style="WIDTH: 160px">Fecha Modificaciòn:&nbsp;&nbsp;&nbsp;&nbsp;
								</td>
								<td><asp:textbox style="Z-INDEX: 0" Enabled="False" id="txtFechaModificacion" runat="server" Width="175px"
										CssClass="clsInputEnable" ReadOnly="True"></asp:textbox></td>
							</tr>
						</table>
						<br>
					</td>
				</tr>
			</table>
			<br>
			<br>
			<table class="tabla_borde" cellSpacing="0" cellPadding="0" width="240" align="center" height="50">
				<tr>
					<td align="center">
						<input style="WIDTH: 100px" id="cmdAceptar" class="BotonOptm" value="Aceptar" type="button"
							name="cmdAceptar" runat="server">
					</td>
					<td align="center"><input style="WIDTH: 100px" id="cmdCancelar" class="BotonOptm" onclick="javascript:f_Cancelar();"
							value="Cancelar" type="button" name="cmdCancelar"></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
