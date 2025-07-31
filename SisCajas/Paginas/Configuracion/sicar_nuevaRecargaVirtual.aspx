<%@ Page Language="vb" AutoEventWireup="false" Codebehind="sicar_nuevaRecargaVirtual.aspx.vb" Inherits="SisCajas.sicar_nuevaRecargaVirtual" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
		<title>SICAR - Mantenimiento de Recargas Virtuales</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../estilos/est_General.css" type="text/css" rel="styleSheet">
		<script language="JavaScript" src="../../../librerias/Lib_FuncValidacion.js"></script>
		<base target="_self">
		<script language="javascript">
		
			function inicio()
			{		
				if(document.frmPrincipal.hidMensaje.value != ''){
					alert(document.frmPrincipal.hidMensaje.value);
					return false;
				}
				if (document.frmPrincipal.hidAccion.value == 'OK' ){
								
					var sData = dialogArguments;
       				sData.f_AgregarEditarRecarga();       				       				       				
       				window.close();
				}				
			}
			function f_Grabar(){
			
				if(f_Validar()){
					document.getElementById('hidAccion').value='G';				
					document.frmPrincipal.submit();
				}
			}
			function f_Cancelar(){
				var strValores;
				strValores = document.frmPrincipal.txtMonto.value + document.frmPrincipal.txtDescripcion.value + document.frmPrincipal.cboEstado.value;
				if(strValores != document.frmPrincipal.hidValores.value){
					if(confirm ("Se cancelarán los cambios realizados. ¿Continuar?")){
						window.close();
					}
				}else{
					window.close();
				}
												
			}
			function f_Validar() {// valida campos
				if (!ValidaDecimal('document.frmPrincipal.txtMonto','el campo Monto a Aplicar ',false)) return false;							
				if(parseInt(document.frmPrincipal.txtMonto.value) == 0){
					alert('El monto de la recarga virtual debe ser mayor a Cero (0).');
					return false;				
				}
				return true;  
			}
			function ValidaNumero(obj){
				var KeyAscii = window.event.keyCode;

				if (KeyAscii==13) return;	
				if (!(KeyAscii >= 46 && KeyAscii<=57) | (KeyAscii==46 && obj.value.indexOf(".")>=0) ){		
					window.event.keyCode = 0;
				}	
				else
				{	
					if (obj.value.indexOf(".")>=0 ){		
						if (KeyAscii!=46  && obj.value.substring(obj.value.indexOf(".")+1,obj.value.length).length>1)
							window.event.keyCode = 0;	
					}
				}
			}
		</script>
</HEAD>
	<body MS_POSITIONING="GridLayout" onload="inicio()">
		<form id="frmPrincipal" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="450" border="0" align="center">
				<tr>
					<td height="4"></td>
				</tr>
			</table>
			<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="450" align="center"
				border="1" style="WIDTH: 450px; HEIGHT: 55px">
				<tr>
					<td align="center">
						<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
							<tr>
								<td vAlign="top" width="10" height="32"></td>
								<td class="TituloRConsulta" style="PADDING-TOP: 4px" vAlign="top" align="center" width="98%"
									height="32"><asp:Label ID="lblTitulo" Runat="server"></asp:Label></td>
								<td vAlign="top" width="10" height="32"></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<table cellSpacing="0" cellPadding="0" width="450" border="0" align="center">
				<tr>
					<td vAlign="top" width="10">&nbsp;</td>
				</tr>
			</table>
			<table cellSpacing="0" cellPadding="0" width="400" border="0" align="center">
				<tr>
					<td align="center">
						<table align="center" border="0">
							<tr>
								<td vAlign="top" width="14"></td>
								<td style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px; PADDING-BOTTOM: 5px; PADDING-TOP: 5px"
									width="98%">
									<table cellSpacing="0" cellPadding="0" width="450" border="0">
										<tr>
											<td height="4"></td>
										</tr>
									</table>
									<table cellSpacing="0" cellPadding="0" width="450" border="0">
										<TR>
											<TD class="Arial12b" style="WIDTH: 20px; HEIGHT: 28px" width="20"></TD>
											<TD class="Arial12b" style="WIDTH: 83px; HEIGHT: 28px" width="83">Monto S/ :</TD>
											<TD class="Arial12b" style="HEIGHT: 28px" width="250"><asp:textbox id="txtMonto" runat="server" CssClass="clsInputEnable" MaxLength="15" ></asp:textbox></TD>
											<TD class="Arial12b" style="WIDTH: 20px; HEIGHT: 28px" width="20"></TD>
										</TR>
										<TR>
											<TD class="Arial12b" style="WIDTH: 20px; HEIGHT: 28px" width="20"></TD>
											<TD class="Arial12b" style="WIDTH: 83px; HEIGHT: 28px" width="83">Descripción 
												:</TD>
											<TD class="Arial12b" style="HEIGHT: 28px" width="250"><asp:textbox id="txtDescripcion" runat="server" CssClass="clsInputEnable" MaxLength="30" Width="248px"></asp:textbox></TD>
											<TD class="Arial12b" style="WIDTH: 20px; HEIGHT: 28px" width="20"></TD>
										</TR>
										<TR>
											<TD class="Arial12b" style="WIDTH: 20px; HEIGHT: 28px" width="20"></TD>
											<TD class="Arial12b" style="WIDTH: 98px; HEIGHT: 28px" width="98">Estado :</TD>
											<TD class="Arial12b" style="WIDTH: 84px; HEIGHT: 28px" width="84">
												<asp:dropdownlist id="cboEstado" runat="server" CssClass="clsSelectEnable" ></asp:dropdownlist>
											</TD>
											<TD class="Arial12b" style="WIDTH: 20px; HEIGHT: 28px" width="20"></TD>
										</TR>
									</table>
								</td>
							</tr>
						</table>
						<table>
							<tr>
								<td>
									<table cellSpacing="0" cellPadding="4">
										<tr>
											<td>
												<asp:button id="btnGrabar" runat="server" Width="98px" CssClass="BotonOptm" Text="Grabar"></asp:button>
											</td>
											<td>
												<asp:button id="btnCancelar" runat="server" Width="98px" CssClass="BotonOptm" Text="Cancelar"></asp:button>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<INPUT id="hidAccion" style="WIDTH: 10px; HEIGHT: 18px" type="hidden" size="1" name="hidAccion"
				runat="server"> <INPUT id="hidMensaje" style="WIDTH: 10px; HEIGHT: 18px" type="hidden" size="1" name="hidMensaje"
				runat="server"> <INPUT id="hidFlagCambioForm" style="WIDTH: 10px; HEIGHT: 18px" type="hidden" size="1"
				name="hidFlagCambioForm" runat="server">
				 <INPUT id="hidValores" style="WIDTH: 10px; HEIGHT: 18px" type="hidden" size="1"
				name="hidValores" runat="server">
		</form>
	</body>
</HTML>
