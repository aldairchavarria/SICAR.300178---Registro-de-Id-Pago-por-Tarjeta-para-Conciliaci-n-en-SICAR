<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frmValidarUsuario.aspx.vb" Inherits="SisCajas.frmValidarUsuario" aspcompat=true %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>SICAR - Autorizar Transacción</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<LINK href="../estilos/est_General.css" type="text/css" rel="styleSheet">
	</HEAD>
	<body onunload="fin();">
		<script language="javascript" type="text/javascript">
			function fin()
			{	
				if(document.getElementById("hddActionAutorizador").value == "FormaPago"){	
			
					if(document.getElementById("hddActionFpago").value == "FormaPago"){	
						document.getElementById("hddActionFpago").value = "X";
					}else{
						window.opener.frmPrincipal.hddComboAutorizador.value = document.getElementById("hddCombo").value
						window.opener.frmPrincipal.hddEnvioAutorizador.value = "ERROR";
						window.opener.frmPrincipal.submit();
						window.close();	
					}
				}
				else
				{
					window.close();
				}	
			}
			function validarAutorizacion(){
				
				var control;
				var expresion;
				
				control = document.getElementById("txtValUsuario");
				expresion = control.value
				
				if (expresion == null){
					control.focus();
					return alert("Ingrese el Usuario");
				}
				if (expresion == ""){
					control.focus();
					return alert("Ingrese el Usuario");
				}
				
				control = document.getElementById("txtValPassword");
				expresion = control.value
				
				if (expresion == null){
					control.focus();
					return alert("Ingrese el Password");
				}
				if (expresion == ""){
					control.focus();
					return alert("Ingrese el Password");
				}
				document.getElementById("hddActionFpago").value = "FormaPago"
				document.getElementById("hddAction").value = "OK"
				document.frmPrincipal.submit();
				
				
				
		}
			
		function cerrarPagina(){
			
				if(document.getElementById("hddActionAutorizador").value == "FormaPago"){	
					window.opener.frmPrincipal.hddComboAutorizador.value = document.getElementById("hddCombo").value
					window.opener.frmPrincipal.hddEnvioAutorizador.value = 'ERROR';
					window.opener.frmPrincipal.submit();
				window.close();
		}
				else
				{
					window.close();
				}
		}
			
		</script>
		<form id="frmPrincipal" name="frmPrincipal" method="post" runat="server">
			<table style="WIDTH: 290px; HEIGHT: 100px">
				<tr>
					<td valign="middle" align="center">
						<table width="98%" height="100%">
							<TR>
								<TD class="Arial12b" bgColor="#5991bb" vAlign="top" colSpan="3" align="center"><ASP:LABEL id="Label1" runat="server" forecolor="White" font-bold="True" font-size="12pt">AUTORIZACIÓN</ASP:LABEL></TD>
							</TR>
							<tr>
								<td class="TituloRConsulta" align="center" colspan="2" height="32">
									<asp:Label id="lblTitulo" runat="server"></asp:Label>
								</td>
							</tr>
							<tr>
								<td class="Arial12B" style="WIDTH:150px">
									Ingrese Usuario :
								</td>
								<td style="WIDTH:150px">
									<asp:TextBox id="txtValUsuario" runat="server" Width="120px" MaxLength="20"></asp:TextBox>
								</td>
							</tr>
							<tr>
								<td class="Arial12B" style="WIDTH:150px">
									Ingrese Password :
								</td>
								<td style="WIDTH:150px">
									<asp:TextBox id="txtValPassword" runat="server" Width="120px" MaxLength="20" TextMode="Password"></asp:TextBox>
								</td>
							</tr>
							<tr>
								<td align="center" colspan="2" height="32">
									<INPUT id="btnAceptar" name="btnAceptar" type="button" value="Aceptar" class="BotonOptm"
										style="WIDTH: 104px; HEIGHT: 19px" onclick="validarAutorizacion()"> &nbsp; <INPUT id="btnCancelar" name="btnCancelar" type="button" value="Cancelar" class="BotonOptm"
										style="WIDTH: 106px; HEIGHT: 19px" onclick="cerrarPagina()">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<asp:Label id="lblID" runat="server" Visible="False"></asp:Label>
			<INPUT runat="server" type="hidden" id="hddAction" name="hddAction" style="DISPLAY: none">
			<INPUT runat="server" type="hidden" id="hddActionAutorizador" name="hddActionAutorizador"
				style="DISPLAY: none"> <INPUT runat="server" type="hidden" id="hddCombo" name="hddCombo" style="DISPLAY: none">
			<INPUT runat="server" type="hidden" id="hddActionFpago" name="hddActionFpago" style="DISPLAY: none">
		</form>
	</body>
</HTML>
