<%@ Page Language="vb" AutoEventWireup="false" Codebehind="recBusNomAp.aspx.vb" Inherits="SisCajas.recBusNomAp"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>recBusNomAp</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../estilos/est_General.css" type="text/css" rel="styleSheet">
		<script language="JavaScript" src="../librerias/Lib_FuncValidacion.js"></script>
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<script language="javascript">
			
		</script>
	</HEAD>
	<body vLink="#ceefff" aLink="#ceefff" link="#ceefff">
		<form id="frmRecauda" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="600" border="0">
				<tr>
					<td vAlign="top" width="10">&nbsp;</td>
					<td vAlign="top" width="600">
						<table height="14" cellSpacing="0" cellPadding="0" width="600" border="0" name="Contenedor">
							<tr>
								<td align="center"></td>
							</tr>
						</table>
						<table cellSpacing="0" cellPadding="0" width="600" align="center" border="0" name="Contenedor">
							<tr>
								<td align="center">
									<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="550" align="center"
										border="1">
										<tr>
											<td align="center">
												<table class="Arial10B" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
													<tr>
														<td width="10" height="4" border="0"></td>
														<td class="TituloRConsulta" align="center" width="98%" height="32">Recaudación - 
															Búsqueda de Documentos por Nombre</td>
														<td vAlign="top" width="14" height="32"></td>
													</tr>
												</table>
												<table cellSpacing="0" cellPadding="0" width="550" align="center" border="0">
													<tr>
														<td style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px; PADDING-BOTTOM: 5px; PADDING-TOP: 5px"
															width="98%">
															<table cellSpacing="0" cellPadding="0" width="500" border="0">
																<tr>
																	<td height="4"></td>
																</tr>
																<tr>
																	<td>
																		<table cellSpacing="2" cellPadding="2" width="500" border="0">
																			<tr>
																				<td class="Arial12b" width="120">&nbsp;Tipo de Identificador:</td>
																				<td class="Arial12b" width="125" colSpan="3"><asp:dropdownlist class="Arial12b" id="cboTipoIdentificador" runat="server" name="selTipo" Width="144px">
																						<asp:ListItem Value="01" Selected="True">Nombres y Apellidos</asp:ListItem>
																						<asp:ListItem Value="02">Raz&#243;n Social</asp:ListItem>
																					</asp:dropdownlist></td>
																			</tr>
																			<TR>
																				<TD class="Arial12b" id="tdNombre" width="120">&nbsp;Nombre:</TD>
																				<TD class="Arial12b" width="125" colSpan="3"><asp:textbox id="txtNomRaz" runat="server" name="txtNomRaz" Width="344px" MaxLength="200" CssClass="clsInputEnable"></asp:textbox></TD>
																			</TR>
																			<TR id="trApellido">
																				<TD class="Arial12b" width="120">&nbsp;Apellido</TD>
																				<TD class="Arial12b" width="125" colSpan="3"><asp:textbox id="txtApellido" runat="server" name="txtApellido" Width="344px" MaxLength="200"
																						CssClass="clsInputEnable"></asp:textbox></TD>
																			</TR>
																			<tr>
																				<td class="Arial12b" colSpan="4">&nbsp;</td>
																			</tr>
																			<tr>
																				<td class="Arial12b" align="center" colSpan="4"><asp:button id="cmdBuscar" runat="server" Width="100px" CssClass="BotonOptm" Text="Buscar"></asp:button></td>
																			</tr>
																		</table>
																	</td>
																</tr>
															</table>
														</td>
													</tr>
												</table>
											</td>
										</tr>
									</table>
									<br>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
		<script language="javascript">
function e_mayuscula() {
	if ((event.keyCode>96&&event.keyCode<123)||(event.keyCode==241)||(event.keyCode==250)||(event.keyCode==243)||(event.keyCode==237)||(event.keyCode==233)||(event.keyCode==225))
		event.keyCode=event.keyCode-32;
}

window.document.frmRecauda.txtNomRaz.onkeypress=e_mayuscula;
window.document.frmRecauda.txtApellido.onkeypress=e_mayuscula;


  function f_Cambiar()
  {
     if (document.frmRecauda.cboTipoIdentificador.value == '01')
       //document.frmRecauda.trApellido.style.display = "block";
     {  
       document.getElementById("trApellido").style.display = "block";
       document.getElementById("tdNombre").innerText = " Nombre:"
     }  
     else  
     {
       //document.frmRecauda.trApellido.style.display = "none";
       document.getElementById("trApellido").style.display = "none";
       document.getElementById("tdNombre").innerText = " Razón Social:";
       document.frmRecauda.txtApellido.value = "";
     }
  }
  
  function f_Verificar()
  {
    if (document.frmRecauda.cboTipoIdentificador.value == '01')
    {
       if (!ValidaAlfanumerico('document.frmRecauda.txtNomRaz','el campo Nombre ',false)) return false;
       if (!ValidaAlfanumerico('document.frmRecauda.txtApellido','el campo Apellido ',false)) return false;
    }
    else
       if (!ValidaAlfanumerico('document.frmRecauda.txtNomRaz','el campo Razón Social ',false)) return false;
    
    return true;   
  }
  
  function f_Buscar()
  { 
    if (f_Verificar())
    {
       event.returnValue = true;
    }   
    else
       event.returnValue = false;
  }

  f_Cambiar();
  
		</script>
	</body>
</HTML>
