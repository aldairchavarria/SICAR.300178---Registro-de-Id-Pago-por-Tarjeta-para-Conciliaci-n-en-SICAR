<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ConfPosRegistrar.aspx.vb" Inherits="SisCajas.ConfPosRegistrar" %>
<!DOCTYPE HTML PUBLIC "-//W3C//Dtd HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Transacciones POS</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../estilos/est_General.css" type="text/css" rel="styleSheet">
		<LINK href="../../estilos/est_General.css" type="text/css" rel="styleSheet">
		<style>
		</style>
		<script language="javascript">
		
		function f_CambioComboInt(){
		
		var valorInt = document.getElementById('cboIntegra').selectedIndex;
		
			if(valorInt == 1){
			
				document.getElementById('cboPagoAuto').selectedIndex = 1;
			
			}
		
		}
		
		function onkeypressSoloNumeros(e)
		{
			var key = (e ? e.keyCode || e.which : window.event.keyCode);
			return ((key >= 48 && key <= 57) || key == 46);
		}

		
		function f_CambioComboPAG(){
		
			var valorPagoAuto = document.getElementById('cboPagoAuto').selectedIndex;
					
			if(valorPagoAuto == 1){
			
				document.getElementById('cboIntegra').selectedIndex = 1;
			
			}
		
		}
	
	             
		</script>
	</HEAD>
	<BODY>
		<div style="POSITION: absolute; WIDTH: 10px; HEIGHT: 10px; TOP: 8px; LEFT: 18px" ms_positioning="text2D">
			<form id="frmPrincipal" name="frmPrincipal" method="post" runat="server">
				<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="800" border="1">
					<tr>
						<td>
							<table class="Arial10B" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
								<tr>
									<td width="10" height="4"></td>
									<td class="TituloRConsulta" align="center" width="98%" height="32">Mantenimiento de 
										Oficinas - POS</td>
									<td width="14" height="32"></td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td>
							<table>
								<tr>
									<td class="Arial12b" width="200">&nbsp;&nbsp;&nbsp;Oficina de Venta :
									</td>
									<td class="Arial12b" width="450">&nbsp;
										<asp:label id="lblOficinaVenta" runat="server">Label</asp:label></td>
								</tr>
								<tr>
									<td class="Arial12b" width="200">&nbsp;&nbsp; Numero de Caja :
									</td>
									<td class="Arial12b" width="450">&nbsp;
										<asp:label id="lblNumeroCaja" runat="server">Label</asp:label></td>
								</tr>
								<tr>
									<td class="Arial12b" width="200">&nbsp;&nbsp; Código Establecimiento :</td>
									<td class="Arial12b" width="450">&nbsp;&nbsp;<asp:label id="lblCodigoEsta" runat="server">Label</asp:label></td>
								</tr>
								<tr>
									<td class="Arial12b" style="HEIGHT: 21px" width="200">&nbsp;&nbsp;&nbsp;IP 
										Caja&nbsp;:
									</td>
									<td class="Arial12b" style="HEIGHT: 21px" width="450">&nbsp;
										<asp:textbox id="txtIpCaja" runat="server" maxLength="15" onkeypress="return onkeypressSoloNumeros(event)"
											CssClass="clsInputEnable"></asp:textbox></td>
								</tr>
								<tr>
									<td class="Arial12b" width="200">&nbsp;&nbsp;&nbsp;Nombre&nbsp;Equipo&nbsp;:</td>
									<td class="Arial12b" width="450">&nbsp;
										<asp:textbox id="txtNombreEquipo" runat="server" maxLength="30" CssClass="clsInputEnable"></asp:textbox></td>
								</tr>
								<tr>
									<td class="Arial12b" style="HEIGHT: 16px" width="200">&nbsp;&nbsp;&nbsp;Estado 
										:&nbsp;&nbsp;
									</td>
									<td>&nbsp;<select class="clsSelectEnable" id="cboEstado" name="cboEstado" runat="server" border="0">
											<option value="0" selected>Inactivo</option>
											<option value="1">Activo</option>
										</select></td>
								</tr>
								<tr>
									<td class="Arial12b" width="200">&nbsp;&nbsp;&nbsp;Tipo&nbsp;de POS&nbsp;:</td>
									<td class="Arial12b" width="450">&nbsp;
										<asp:checkbox id="chkVisa" runat="server" Text="VisaNet" AutoPostBack="True"></asp:checkbox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
										<asp:checkbox id="chkMCProcesos" runat="server" Text="MC Procesos" AutoPostBack="True"></asp:checkbox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
								</tr>
							
								<tr>
									<td class="Arial12b" width="200">&nbsp;&nbsp;&nbsp;Pago Automático&nbsp;:</td>
									<td>&nbsp;<select class="clsSelectEnable" id="cboPagoAuto" onchange="" style="WIDTH: 40px"
											name="cboPagoAuto" runat="server" border="0">
											<option value="0" selected>NO</option>
											<option value="1">SI</option>
										</select></td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td style="HEIGHT: 90px">
							<table style="MARGIN-TOP: 20px; MARGIN-BOTTOM: 20px" borderColor="#336699" cellSpacing="0"
								cellPadding="4" width="550" align="center" border="1">
								<tr id="trOpciones">
									<td>
										<table class="Arial10B" id="Table10" cellSpacing="0" cellPadding="0" width="100%" align="center"
											border="0">
											<tr>
												<td style="TEXT-ALIGN: center"><asp:button id="btnGuardar" CssClass="BotonOptm" Text="Guardar" Runat="server" Width="100"></asp:button></td>
												<td style="TEXT-ALIGN: center"><asp:button id="btnCancelar" CssClass="BotonOptm" Text="Cancelar" Runat="server" Width="100"></asp:button></td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr align="center">
						<td></td>
					</tr>
				</table>
				<asp:TextBox ID="txtIpOculto" Width="0px" Height="0px" runat="server"></asp:TextBox>
			</form>
			<input id="hidTramaPOS" type="hidden" name="hidTramaPOS" runat="server"> <input id="hidIPAntigua" type="hidden" name="hidIPAntigua" runat="server">
		</div>
	</BODY>
</HTML>
