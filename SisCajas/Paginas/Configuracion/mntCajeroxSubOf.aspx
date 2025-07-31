<%@ Page Language="vb" AutoEventWireup="false" Codebehind="mntCajeroxSubOf.aspx.vb" Inherits="SisCajas.mntCajeroxSubOf" %>
<%@ OutputCache Location="None" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
    <title>mntCajeroxSubOf</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
    <LINK href="../../estilos/est_General.css" type="text/css" rel="styleSheet">
		<base target="_self">
		<style>
		.tbl_Cajas { BORDER-BOTTOM: #999999 1px solid; BORDER-LEFT: #999999 1px solid; FONT-FAMILY: Arial; FONT-SIZE: 10px; BORDER-TOP: #999999 1px solid; BORDER-RIGHT: #999999 1px solid; TEXT-DECORATION: none }
		.tbl_Cajas TH { TEXT-ALIGN: center; PADDING-BOTTOM: 5px; BACKGROUND-COLOR: #21618c; HEIGHT: 22px; COLOR: #ffffff; PADDING-TOP: 5px }
		.tbl_Cajas TD { BORDER-BOTTOM: #999999 1px solid; TEXT-ALIGN: center; BORDER-LEFT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-RIGHT: #999999 1px solid }
		.listBozCssClass { BORDER-BOTTOM: #bfbee9 1px solid; BORDER-LEFT: #bfbee9 1px solid; BACKGROUND-COLOR: #bfbee9; BORDER-TOP: #bfbee9 1px solid; BORDER-RIGHT: #bfbee9 1px solid }
	</style>
  </head>
  <body MS_POSITIONING="GridLayout">

    <form id="Form1" method="post" runat="server">
		<div style="width:100%;">
			<div style="width:90%; border:3px #336699 solid; padding:7px; margin-top:15px; margin-left:5%; text-align:center;">
				<label class="TituloRConsulta">Mantenimiento de Cajero por Sub Oficina</label>
			</div>
			<div style="width:90%; padding-top:10px; margin-left:5%; text-align:letf;">
				<table style="position:relative; top:5px; left:5px; width:100%;" class="Arial12B" cellSpacing="5" cellPadding="0"> 
				<tr>
					<td style="width:35%;">Sub Oficina:</td>
					<td style="width:50%;">
						<select id="cboSubCodOficina" class="clsSelectEnable" style="width:100%;" runat="server" disabled >
						</select>
					</td>
				</tr>
				<tr>
					<td style="width:35%;">Cajero:</td>
					<td style="width:50%;">
						<select id="cboCodCajero" class="clsSelectEnable" style="width:100%;" runat="server" >
						</select>
					</td>
				</tr>
				<tr>
					<td style="width:35%;">Comentario:</td>
					<td style="width:50%;">
						<input type="text" id="txtComentario" class="clsInputEnable" style="width:100%;" runat="server" NAME="txtComentario" maxlength="100">
					</td>
				</tr>
				<tr>
					<td style="width:35%;">Estado:</td>
					<td style="width:50%;">
						<select id="cboEstado" class="clsSelectEnable" style="width:50%;" runat="server" NAME="cboEstado">
							<option value="">Seleccionar</option>
							<option value="0">Inactivo</option>
							<option value="1" selected>Activo</option>
						</select>
					</td>
				</tr>
				</table>
			</div>
			<div style="width:90%; border:3px #336699 solid; margin-top:15px; padding-top:7px;padding-bottom:14px; margin-left:5%; text-align:center;">
				<table style="position:relative; width:90%; margin-left:2.5%;" class="Arial12B" cellSpacing="5" cellPadding="0">
				<tr>
					<td style="width:35%;">Usuario Creación:</td>
					<td style="width:50%;">
						<input type="text" id="txtCreaUsuario" class="clsInputEnable" style="width:100%;" runat="server" disabled NAME="txtCreaUsuario"/>
					</td>
				</tr>
				<tr>
					<td style="width:35%;">Fecha Creación:</td>
					<td style="width:50%;">
						<input type="text" id="txtCreaFecha" class="clsInputEnable" style="width:100%;" runat="server" disabled NAME="txtCreaFecha"/>
					</td>
				</tr>
				<tr>
					<td style="width:35%;">Usuario Modificación:</td>
					<td style="width:50%;">
						<input type="text" id="txtModUsuario" class="clsInputEnable" style="width:100%;" runat="server" disabled NAME="txtModUsuario"/>
					</td>
				</tr>
				<tr>
					<td style="width:35%;">Fecha Modificación:</td>
					<td style="width:50%;">
						<input type="text" id="txtModFecha" class="clsInputEnable" style="width:100%;" runat="server" disabled NAME="txtModFecha"/>
					</td>
				</tr>
				</table>
			</div>
			<div style="width:80%; border:3px #336699 solid; margin-top:15px; margin-left:10%;">
				<div style="width:80%; padding-left:20%; padding-top:7px; padding-bottom:7px;">
					<asp:Button Runat="server" ID="btnGrabar" CssClass="BotonOptm" style="width:48%;" Text="Aceptar"></asp:Button>
					<input type="button" id="btnSalir" class="BotonOptm" value="Cancelar" style="width:48%;" onclick="FN_Close()"  />
				</div>
			</div>
		</div>
		
		<input type="hidden" id="hdnCodOficina" runat="server" NAME="hdnCodOficina">
		<input type="hidden" id="hdnDesOficina" runat="server" NAME="hdnDesOficina">
		<input type="hidden" id="hdnCodSubOficina" runat="server" NAME="hdnCodSubOficina">
		<input type="hidden" id="hdnOpcion" runat="server" NAME="hdnOpcion">
		<input type="hidden" id="hdnUsuario" runat="server" NAME="hdnUsuario">
		<input type="hidden" id="hdnID" runat="server" NAME="hdnID">
    </form>
	<script>	
	function FN_Close(){
		var oResponse = {
			blnRefresh:false
		}
		
		window.returnValue = oResponse;
		window.close();
	}
	</script>
  </body>
</html>
