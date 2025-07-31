<%@ Page Language="vb" AutoEventWireup="false" Codebehind="AnulPrevent.aspx.vb" Inherits="SisCajas.AnulPrevent" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>AnulPreven</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="../estilos/est_General.css" rel="styleSheet" type="text/css">
		<script language="JavaScript" src="../librerias/Lib_FuncValidacion.js"></script>
		<script language="JavaScript" src="../librerias/date-picker.js"></script>
		<script language="JavaScript">
			
			document.onclick = document_onclick;

			function document_onclick() {
				var obj = event.srcElement;
				switch (obj.tagName) {			
					case "INPUT":
						switch (obj.id) {
							case "btnGrabar":						
								if (!f_Grabar()) event.returnValue = false;
								break;							
						}
						break;
				}
			}

			function f_Grabar()
			{				
				if (frmPrincipal.txtClaseDoc.value=="")
				{
					alert("No ha ingresado la Clase del Documento..!");
					return false;
				}
				if (frmPrincipal.txtSerie.value=="")
				{
					alert("No ha ingresado la Serie SUNAT..!");
					return false;
				}
				if (frmPrincipal.txtCorrelativo.value=="")
				{
					alert("No ha ingresado el Correlativo..!");
					return false;
				}				
				return true;
			}
		
		
		</script>
	</HEAD>
	<body>
		<form id="frmPrincipal" method="post" runat="server">
			<table borderColor="#336699" width="790" border="1" cellspacing="0" cellpadding="0" name="Contenedor">
				<tr>
					<td align="center">
						<table width="800" border="0" cellspacing="0" cellpadding="0" align="center">
							<tr>
								<td height="14"></td>
							</tr>
						</table>
						<table width="800" border="0" cellspacing="0" cellpadding="0" align="center">
							<tr>
								<td width="10" valign="top" height="32"></td>
								<td width="98%" height="32" align="center" class="TituloRConsulta" valign="top" style="PADDING-TOP:4px">Anulación 
									Definitiva</td>
								<td valign="top" width="14" height="32"></td>
							</tr>
						</table>
						<table width="800" border="0" cellspacing="0" cellpadding="0" align="center">
							<tr>
								<td valign="top" width="14"></td>
								<td width="98%" style="PADDING-RIGHT:5px; PADDING-LEFT:5px; PADDING-BOTTOM:5px; PADDING-TOP:5px">
									<table border="0" cellspacing="2" cellpadding="0" align="center" class="Arial12B" width="80%">
										<tr>
											<td width="150">&nbsp;&nbsp;&nbsp;Clase de Documento :</td>
											<td><input class="clsInputEnable" type="text" name="txtClaseDoc" id="txtClaseDoc" size="30"
													maxlength="2" runat="server"></td>
										</tr>
										<tr>
											<td>&nbsp;&nbsp;&nbsp;Serie Sunat :</td>
											<td><input class="clsInputEnable" type="text" name="txtSerie" id="txtSerie" size="30" maxlength="5"
													runat="server"></td>
										</tr>
										<tr>
											<td>&nbsp;&nbsp;&nbsp;Correlativo A Anular :</td>
											<td><input class="clsInputEnable" type="text" name="txtCorrelativo" id="txtCorrelativo" size="30"
													maxlength="7" runat="server"></td>
										</tr>
										<tr>
											<td>&nbsp;&nbsp;&nbsp;Fecha de Contabilización :</td>
											<td><input name="txtFecha" type="text" class="clsInputEnable" id="txtFecha" tabindex="34" size="10"
													maxlength="10" runat="server"> <a href="javascript:show_calendar('frmPrincipal.txtFecha');" onMouseOut="window.status='';return true;"
													onMouseOver="window.status='Date Picker';return true;"><img border="0" src="../../images/botones/btn_Calendario.gif"></a></td>
										</tr>
									</table>
								</td>
								<td valign="top" width="14" align="right"></td>
							</tr>
						</table>
						<table width="800" border="0" cellspacing="0" cellpadding="0" align="center">
							<tr>
								<td height="17"></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<br>
			<table width="450" border="1" cellspacing="0" cellpadding="0" align="center" borderColor="#336699">
				<tr>
					<td align="center">
						<table border="0" cellspacing="2" cellpadding="0">
							<tr>
								<td align="center" width="28"></td>
								<td align="center" width="85">
									<asp:Button id="btnGrabar" runat="server" Text="Grabar" CssClass="BotonOptm" Width="80px"></asp:Button></td>
								<td align="center" width="28"></td>
								<td align="center" width="85">
									<asp:Button id="btnCancelar" runat="server" Text="Cancelar" CssClass="BotonOptm" Width="80px"></asp:Button></td>
								<td align="center" width="28"></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<br>
			<br>
			</TD></TR></TABLE>
		</form>
	</body>
</HTML>
