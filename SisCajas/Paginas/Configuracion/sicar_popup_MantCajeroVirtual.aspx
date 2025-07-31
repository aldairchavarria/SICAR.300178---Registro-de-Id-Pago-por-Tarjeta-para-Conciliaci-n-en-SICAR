<%@ Page Language="vb" AutoEventWireup="false" Codebehind="sicar_popup_MantCajeroVirtual.aspx.vb" Inherits="SisCajas.sicar_popup_MantRecargaVirtual"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Mantenimiento de Códigos de Tarjetas</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta http-equiv="pragma" content="no-cache">
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
		
		function f_Retorno()
		{		
			window.opener.f_respuesta('Registro grabado correctamente');
			window.close();
			
		}
		function Inicio()
		{
			var hidValor = document.getElementById("hidRpta").value;
			if (hidValor == "OK"){
				window.returnValue = "Registro grabado correctamente";
				window.close();
			}
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" onload="Inicio()">
		<form id="Form1" method="post" runat="server" >
			<table style="POSITION: relative; TOP: 5px; LEFT: 5px" class="tabla_borde" cellSpacing="0"
				cellPadding="0" width="390" align="center">
				<tr>
					<td class="TituloRConsulta" height="30" colSpan="4" align="center">Mantenimiento de 
						Cajeros Virtuales
					</td>
				</tr>
			</table>
			<br>
			<table style="POSITION: relative; TOP: 5px; LEFT: 5px" class="Arial12B" cellSpacing="5"
				cellPadding="0" width="390" align="center">
				<tr>
					<td style="WIDTH: 112px">&nbsp;Oficina Venta:&nbsp;&nbsp;&nbsp;&nbsp;
					</td>
					<td><asp:dropdownlist id="ddlOficinaVenta" runat="server" AutoPostBack="true" CssClass="clsSelectEnable"
							Width="240px"></asp:dropdownlist></td>
				</tr>
			</table>
			<table style="POSITION: relative; TOP: 5px; LEFT: 5px" class="Arial12B" cellSpacing="5"
				cellPadding="0" width="390" align="center">
				<tr>
					<td style="WIDTH: 112px">&nbsp;Cajero:&nbsp;&nbsp;&nbsp;&nbsp;
					</td>
					<td><asp:dropdownlist id="ddlCajero" runat="server" CssClass="clsSelectEnable" Width="240px"></asp:dropdownlist></td>
				</tr>
			</table>
			<table style="POSITION: relative; TOP: 5px; LEFT: 5px" class="Arial12B" cellSpacing="5"
				cellPadding="0" width="390" align="center">
				<tr>
					<td style="WIDTH: 112px">&nbsp;Caja:&nbsp;&nbsp;&nbsp;&nbsp;
					</td>
					<td><asp:dropdownlist id="ddlCaja" runat="server" CssClass="clsSelectEnable" Width="240px"></asp:dropdownlist></td>
				</tr>
			</table>
			<table style="POSITION: relative; TOP: 5px; LEFT: 5px" class="Arial12B" cellSpacing="5"
				cellPadding="0" width="390" align="center">
				<tr>
					<td style="WIDTH: 112px">&nbsp;Activo:&nbsp;&nbsp;&nbsp;&nbsp;
					</td>
					<td><asp:checkbox id="chkActivo" runat="server" Text="" Checked="False"></asp:checkbox></td>
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
								<td style="WIDTH: 160px">Usuario Creacion:&nbsp;&nbsp;&nbsp;&nbsp;
								</td>
								<td><asp:textbox style="Z-INDEX: 0" id="txtUsuarioCreacion" runat="server" CssClass="clsInputEnable"
										Width="185px" ReadOnly="True" Enabled="False"></asp:textbox></td>
							</tr>
						</table>
						<table style="POSITION: relative; TOP: 5px; LEFT: 5px" class="Arial12B" cellSpacing="5"
							cellPadding="0" width="370" align="center">
							<tr>
								<td style="WIDTH: 160px">Fecha Creacion:&nbsp;&nbsp;&nbsp;&nbsp;
								</td>
								<td><asp:textbox style="Z-INDEX: 0" id="txtFechaCreacion" runat="server" CssClass="clsInputEnable"
										Width="185px" ReadOnly="True" Enabled="False"></asp:textbox></td>
							</tr>
						</table>
						<table style="POSITION: relative; TOP: 5px; LEFT: 5px" class="Arial12B" cellSpacing="5"
							cellPadding="0" width="370" align="center">
							<tr>
								<td style="WIDTH: 160px">Usuario Modificacion:&nbsp;&nbsp;&nbsp;&nbsp;
								</td>
								<td><asp:textbox style="Z-INDEX: 0" id="txtUsuarioModificacion" runat="server" CssClass="clsInputEnable"
										Width="185px" ReadOnly="True" Enabled="False"></asp:textbox></td>
							</tr>
						</table>
						<table style="POSITION: relative; TOP: 5px; LEFT: 5px" class="Arial12B" cellSpacing="5"
							cellPadding="0" width="370" align="center">
							<tr>
								<td style="WIDTH: 160px">Fecha Modificacion:&nbsp;&nbsp;&nbsp;&nbsp;
								</td>
								<td><asp:textbox style="Z-INDEX: 0" id="txtFechaModificacion" runat="server" CssClass="clsInputEnable"
										Width="185px" ReadOnly="True" Enabled="False"></asp:textbox></td>
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
					<td align="center"><input style="WIDTH: 100px" id="cmdAceptar" class="BotonOptm" value="Aceptar" type="button"
							name="cmdAceptar" runat="server">
					</td>
					<td align="center"><input style="WIDTH: 100px" id="cmdCancelar" class="BotonOptm" onclick="javascript:f_Cancelar();"
							value="Cancelar" type="button" name="cmdCancelar"></td>
				</tr>
			</table>
			<input type="hidden" id="hidRpta" runat="server" name="hidRpta">
		</form>
	</body>
</HTML>
