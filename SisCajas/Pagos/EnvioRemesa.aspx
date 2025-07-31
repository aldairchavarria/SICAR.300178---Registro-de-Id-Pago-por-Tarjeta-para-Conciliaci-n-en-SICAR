<%@ Page Language="vb" AutoEventWireup="false" Codebehind="EnvioRemesa.aspx.vb" Inherits="SisCajas.EnvioRemesa"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>EnvioRemesa</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="JavaScript" src="../librerias/ubigeo.js"></script>
		<script language="JavaScript" src="../librerias/date-picker.js"></script>
		<script language="JavaScript" src="../librerias/Lib_FuncValidacion.js"></script>
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<LINK href="../estilos/est_General.css" type="text/css" rel="styleSheet">
		<script language="javascript">
		
		window.onload = window_onload;	
		
		function window_onload() {
			document.getElementById('divBotones').style.display = 'block';
			document.getElementById('lblCompServ').style.display = 'none'; //INI-936 - CNSO
					
			if (frmPrincipal.txtMonEfSol.value!=""){
			    alert('La Remesa fue generada con éxito');
				f_Imprimir();
				frmPrincipal.action="EnvioRemesa.aspx"
				frmPrincipal.txtMonEfSol.value=""			
				frmPrincipal.txtMonEfDol.value=""			
				frmPrincipal.txtMonChSol.value=""			
				frmPrincipal.txtMonChDol.value=""			
			}					
			if ( document.getElementById('hidFlagPerfil').value == 'S' ) {
				var strPos = document.frmPrincipal.hidPos.value;
				document.getElementById("divSobres").scrollTop = strPos;
				document.frmPrincipal.hidPos.value="";	
			}	
		}
		
		function SetDivPosition(){
			var intY = document.getElementById("divSobres").scrollTop;
			document.frmPrincipal.hidPos.value = intY;	
			return true;
		}
		
		function f_Imprimir(){
			var strMonEfSol = frmPrincipal.txtMonEfSol.value;			
			var strMonEfDol = frmPrincipal.txtMonEfDol.value;
			var strMonChSol = frmPrincipal.txtMonChSol.value;
			var strMonChDol = frmPrincipal.txtMonChDol.value;
			var strNumBol  = frmPrincipal.txtBolsa.value;
			var strCompServ = frmPrincipal.txtCompServ.value;//INICIATIVA - 529
						
			var objIframe = document.getElementById("IfrmImpresion");
			
			objIframe.style.visibility = "visible";
			objIframe.style.width = 0;
			objIframe.style.height = 0;
			
			objIframe.src = "FormatoRemesa.aspx?MonEfSol="+ strMonEfSol + "&MonEfDol=" + strMonEfDol + "&MonChSol=" + strMonChSol + "&MonChDol=" + strMonChDol + "&Bolsa=" +  strNumBol + "&CompServ="+ strCompServ;//INICIATIVA - 529
		}
		
		function Imprimir()
	    {					
			var objIframe = document.getElementById("IfrmImpresion");
			window.open(objIframe.contentWindow.location);
			
	    }
	    
                //INI-936 - CNSO
                function ocultarMensajeCompServ(ocultar) {
                   var display = "block";
                   if(ocultar)
                     display = "none";

                   document.getElementById('lblCompServ').style.display = display;
                }
	    
	    
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="frmPrincipal" method="post" name="frmPrincipal" runat="server">
			<div style="Z-INDEX: 101; WIDTH: 100px; POSITION: absolute" id="overDiv"></div>
			<table border="0" cellSpacing="0" cellPadding="0" width="800">
				<tr>
					<td vAlign="top" width="10">&nbsp;</td>
					<td vAlign="top" width="810">
						<table border="0" cellSpacing="0" cellPadding="0" width="100%">
							<tr>
								<td height="10" align="center"></td>
							</tr>
						</table>
						<table class="tabla_borde" cellSpacing="0" cellPadding="0" width="100%" align="center">
							<tr>
								<td align="center">
									<table border="0" cellSpacing="0" cellPadding="0" width="100%" align="center">
										<tr>
											<td height="32" vAlign="top" width="10"></td>
											<td style="PADDING-TOP: 4px" class="TituloRConsulta" height="32" vAlign="top" width="98%"
												align="center">Envío de Remesa</td>
											<td height="32" vAlign="top" width="10"></td>
										</tr>
									</table>
									<table border="0" cellSpacing="0" cellPadding="0" width="100%" align="center">
										<tr>
											<td vAlign="top" width="14"></td>
											<td style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px; PADDING-BOTTOM: 5px; PADDING-TOP: 5px"
												width="98%">
												<table border="0" cellSpacing="0" cellPadding="0" width="770">
													<tr>
														<td height="4"></td>
													</tr>
												</table>
												<table border="0" cellSpacing="0" cellPadding="0" width="770">
													<tr>
														<td style="WIDTH: 66px; HEIGHT: 18px" width="66"></td>
														<td style="WIDTH: 121px; HEIGHT: 18px" class="Arial12b">&nbsp;&nbsp; Número de 
															Bolsa</td>
														<td style="WIDTH: 85px; HEIGHT: 18px" class="Arial12b"><asp:textbox id="txtBolsa" runat="server" MaxLength="10" CssClass="clsInputEnable"></asp:textbox></td>
														<td style="WIDTH: 60px; HEIGHT: 18px" class="Arial12b" width="60">Fecha :</td>
														<td style="HEIGHT: 18px" width="300"><asp:textbox id="txtFecha" runat="server" MaxLength="10" CssClass="clsInputEnable" Width="80px"></asp:textbox>&nbsp;
															<A onmouseover="window.status='Date Picker';return true;" onmouseout="window.status='';return true;"
																href="javascript:show_calendar('frmPrincipal.txtFecha');"><IMG border="0" src="../../images/botones/btn_Calendario.gif"></A></td>
													</tr>
													<!--INICIATIVA - 529 INI-->
													<tr>
														<td style="WIDTH: 66px; HEIGHT: 18px" width="66"></td>
														<td style="WIDTH: 121px; HEIGHT: 18px" class="Arial12b">Comprobante de Servicio:</td>
														<td style="WIDTH: 85px; HEIGHT: 18px" class="Arial12b">
                                                                                                                        <!-- INI-936 - CNSO - Agregado label lblCompServ y eventos onblur y onfocus al textbox txtCompServ -->
                                                                                                                        <asp:textbox id="txtCompServ" runat="server" MaxLength="10" CssClass="clsInputEnable" onblur="ocultarMensajeCompServ(true)" onfocus="ocultarMensajeCompServ(false)"></asp:textbox>&nbsp;
                                                                                                                        <asp:label style="FONT-SIZE: 10px" id="lblCompServ" runat="server" Width="104px" ForeColor="Red">*Ingrese el número de comprobante correcto</asp:label></td>
														<td style="WIDTH: 60px; HEIGHT: 18px" class="Arial12b" width="60">Tipo Vía</td>
														<td style="HEIGHT: 18px" width="300">
															<asp:dropdownlist id="cboMoneda" runat="server" CssClass="clsSelectEnable" Width="144px" AutoPostBack="True">
																<asp:ListItem Value="0">Todos</asp:ListItem>
																<asp:ListItem Value="1">Efectivo Soles</asp:ListItem>
																<asp:ListItem Value="2">Efectivo Dolares</asp:ListItem>
																<asp:ListItem Value="3">Cheques Soles</asp:ListItem>
																<asp:ListItem Value="4">Cheques Dolares</asp:ListItem>
															</asp:dropdownlist></td>
													</tr>
													<!--INICIATIVA - 529 FIN-->
													<tr>
														<td colspan="5"></td>
													</tr>
													<tr>
														<td colSpan="5">
															<div style="BORDER-RIGHT: 1px; BORDER-TOP: 1px; OVERFLOW-Y: scroll; OVERFLOW-X: scroll; BORDER-LEFT: 1px; WIDTH: 775px; BORDER-BOTTOM: 1px; HEIGHT: 350px; TEXT-ALIGN: center"
																class="frame2" id="divSobres"><asp:datagrid id="dgSobres" runat="server" Width="750px" BorderColor="White" AutoGenerateColumns="False"
																	BorderWidth="1px" CellPadding="1" CellSpacing="1">
																	<AlternatingItemStyle BackColor="#E9EBEE"></AlternatingItemStyle>
																	<ItemStyle CssClass="Arial11B" BackColor="#DDDEE2"></ItemStyle>
																	<HeaderStyle HorizontalAlign="Center" Height="22px" CssClass="Arial12B"></HeaderStyle>
																	<Columns>
																		<asp:TemplateColumn HeaderText="Seleccionar">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="40px"></HeaderStyle>
																			<ItemStyle HorizontalAlign="Center"></ItemStyle>
																			<HeaderTemplate>
																				<P align="center">
																					Seleccionar
																					<BR>
																					<asp:CheckBox id="chkAll" runat="server" Width="71px" Height="7px"></asp:CheckBox></P>
																			</HeaderTemplate>
																			<ItemTemplate>
																				<asp:CheckBox id="chkSobre" runat="server"></asp:CheckBox>
																			</ItemTemplate>
																		</asp:TemplateColumn>
																		<asp:TemplateColumn HeaderText="N&#176; de sobre">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="90px"></HeaderStyle>
																			<EditItemTemplate>
																				<asp:Label ID="lblEditNroSobre" Runat="server" Text='<%# Container.DataItem("BUZON_BOLSA")%>'>
																				</asp:Label>
																			</EditItemTemplate>
																			<ItemTemplate>
																				<asp:Label ID="lblItemNroSobre" Runat="server" Text='<%# Container.DataItem("BUZON_BOLSA")%>'>
																				</asp:Label>
																			</ItemTemplate>
																		</asp:TemplateColumn>
																		<asp:TemplateColumn HeaderText="Fecha">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="180px"></HeaderStyle>
																			<ItemStyle HorizontalAlign="Left"></ItemStyle>
																			<EditItemTemplate>
																				<asp:TextBox CssClass="Arial11B" id="txtFechaSobre" Width="130px" Text='<%# Container.DataItem("BUZON_FECHA")%>' runat="server" ReadOnly="True" />
																				<IMG id="imgFechaSobre" src="../../images/botones/btn_Calendario.gif" style="CURSOR: hand"
																					onmouseover="window.status='Date Picker';return true;" onmouseout="window.status='';return true;"
																					border="0" onclick="javascript:show_calendar('frmPrincipal.' + this.parentNode.getElementsByTagName('input')[0].id, '../');">
																			</EditItemTemplate>
																			<ItemTemplate>
																				<asp:Label ID="lblFecha" Runat="server" Text='<%# Container.DataItem("BUZON_FECHA")%>' >
																				</asp:Label>
																			</ItemTemplate>
																		</asp:TemplateColumn>
																		<asp:TemplateColumn HeaderText="Tipo Via">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="200px"></HeaderStyle>
																			<EditItemTemplate>
																				<%# Container.DataItem("BUZON_TIPOVIA")%>
																			</EditItemTemplate>
																			<ItemTemplate>
																				<asp:Label ID="lblTipoVia" Runat="server" Text='<%# Container.DataItem("BUZON_TIPOVIA")%>'>
																				</asp:Label>
																			</ItemTemplate>
																		</asp:TemplateColumn>
																		<asp:TemplateColumn HeaderText="Importe">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="150px"></HeaderStyle>
																			<ItemStyle HorizontalAlign="Right"></ItemStyle>
																			<EditItemTemplate>
																				<%# Container.DataItem("BUZON_MONTO")%>
																			</EditItemTemplate>
																			<ItemTemplate>
																				<asp:Label ID="lblMonto" Runat="server" Text='<%# Container.DataItem("BUZON_MONTO")%>'>
																				</asp:Label>
																			</ItemTemplate>
																		</asp:TemplateColumn>
																		<asp:TemplateColumn HeaderText="Usuario">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="120px"></HeaderStyle>
																			<ItemStyle HorizontalAlign="Center"></ItemStyle>
																			<EditItemTemplate>
																				<%# Container.DataItem("BUZON_USUARIO")%>
																			</EditItemTemplate>
																			<ItemTemplate>
																				<asp:Label ID="lblUsuario" Runat="server" Text='<%# Container.DataItem("BUZON_USUARIO")%>'>
																				</asp:Label>
																			</ItemTemplate>
																		</asp:TemplateColumn>
																		<asp:TemplateColumn Visible="False" HeaderText="Editar">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="100px"></HeaderStyle>
																			<ItemStyle HorizontalAlign="Center"></ItemStyle>
																			<EditItemTemplate>
																				<asp:ImageButton ID="imgGrabar" runat="server" CausesValidation="True" CommandName="Update" ImageUrl="../images/botones/save.png"
																					ToolTip="Grabar" />
																				<asp:ImageButton ID="imgCancelar" runat="server" CausesValidation="False" CommandName="Cancel" ImageUrl="../images/botones/cancel.png"
																					ToolTip="Cancelar" />
																			</EditItemTemplate>
																			<ItemTemplate>
																				<asp:ImageButton ID="imgModificar" runat="server" CausesValidation="False" CommandName="Edit" ImageUrl="../images/botones/edit.gif"
																					ToolTip="Modificar" />
																			</ItemTemplate>
																		</asp:TemplateColumn>
																	</Columns>
																</asp:datagrid></div>
														</td>
													</tr>
													<tr>
														<td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
														<td class="Arial12B">Total en Soles</td>
														<td><input id="txtSoles" class="clsInputDisable" readOnly name="txtSoles"></td>
														<td class="Arial12B">Total en Dolares</td>
														<td><input id="txtDolares" class="clsInputDisable" readOnly name="txtDolares"></td>
													</tr>
												</table>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<br>
						<br>
						<!-- E75893: Validar Perfil Usuario "Supervisor Recaudaciones -->
						<div id="divBotones" style="DISPLAY: none">
							<table class="tabla_borde" cellSpacing="0" cellPadding="4" width="400" align="center">
								<tr align="center">
									<td>
										<asp:button id="btnGrabar" runat="server" CssClass="BotonOptm" Width="100" Height="20" Text="Grabar"></asp:button>
										&nbsp;&nbsp; <input type="button" Class="BotonOptm" value="Imprimir" onclick="f_ImprimePend()">
									</td>
							</tr>
						</table>
						</div>
					</td>
				</tr>
			</table>
			<table>
				<tr>
					<td>
						<p style="DISPLAY: none"><asp:textbox id="txtMonEfSol" runat="server"></asp:textbox><asp:textbox id="txtMonChSol" runat="server"></asp:textbox><asp:textbox id="txtMonEfDol" runat="server"></asp:textbox><asp:textbox id="txtMonChDol" runat="server"></asp:textbox></p>
					</td>
				</tr>
			</table>
			<iframe style="VISIBILITY: hidden; WIDTH: 0px; HEIGHT: 0px" id="IfrmImpresion" src="#"
				name="IfrmImpresion"></iframe><input style="WIDTH: 16px; HEIGHT: 22px" id="hidFlagPerfil" size="1" type="hidden" name="hidFlagPerfil"
				runat="server"> <input style="WIDTH: 16px; HEIGHT: 22px" id="hidNroSobre" size="1" type="hidden" name="hidNroSobre"
				runat="server"><input style="WIDTH: 16px; HEIGHT: 22px" id="hidPos" size="1" type="hidden" name="hidPos"
				runat="server">
		</form>
		<script language="javascript" type="text/javascript">

function e_numero(){
	if ( !( (event.keyCode>=48) && (event.keyCode<=57)) && !(event.keyCode==13) && !(event.keyCode==46) )
		event.keyCode=0;
}

function f_CheckAll()
{
	for (i=1;i < dgSobres.rows.length; i++){
	   dgSobres.rows[i].cells[0].children[0].checked = !dgSobres.rows[i].cells[0].children[0].checked;
	}
	f_Suma();
}

function f_Suma()
{
  var valor;
  var sumaSol=0;
  var sumaDol=0;
  
  for (i=1;i < dgSobres.rows.length; i++){
     control = "dgSobres__ctl" + (i+1) + "_chkSobre";
     nombre = "dgSobres.rows[i].cells[0].children." + control + ".checked";
     formula = "valor=" + nombre +";"
     eval(formula);
    if (valor)
     {
       if (dgSobres.rows[i].cells[3].innerText.indexOf("SOLES") > -1 )
         sumaSol+=eval(dgSobres.rows[i].cells[4].innerText);
         
       else  
         sumaDol+=eval(dgSobres.rows[i].cells[4].innerText);
     }
  }
  document.frmPrincipal.txtSoles.value = sumaSol;
  document.frmPrincipal.txtDolares.value = sumaDol;
}


function f_ImprimePend()
{
	var objIframe = document.getElementById("IfrmImpresion");
			
	objIframe.style.visibility = "visible";
	objIframe.style.width = 0;
	objIframe.style.height = 0;
		
	objIframe.src = "ImpSobresPendientes.aspx";

}

function f_Valida()
{
  event.returnValue = false;
  blnFound = false;
  if(ContieneEspaciosEnBlanco(document.frmPrincipal.txtBolsa.value)){
		alert("Error al ingresar Nro de Bolsa, no se permite espacios en blanco.");
		return false;  
  }			      
  //INICIATIVA - 529 INI
  if(ContieneEspaciosEnBlanco(document.frmPrincipal.txtCompServ.value)){
		alert("Error al ingresar Nro de Comprobante de Servicio, no se permite espacios en blanco.");
		return false;  
  }			      
  //INICIATIVA - 529 FIN
  if(!ValidaAlfanumerico('document.frmPrincipal.txtBolsa','el campo numero de bolsa ',false)) return false
  if(!ValidaAlfanumerico('document.frmPrincipal.txtCompServ','el campo numero de comprobante de servicio ',false)) return false //INICIATIVA - 529
  if (!ValidaFechaA('document.frmPrincipal.txtFecha',false)) return false;
  
  for (i=0;i<document.frmPrincipal.elements.length;i++)
  {
     pos = document.frmPrincipal.elements[i].name.indexOf("chk")
     //pos = 0
     if(document.frmPrincipal.elements[i].name.substring(pos,pos+8)=="chkSobre"){
			if(document.frmPrincipal.elements[i].checked)
			   blnFound = true;
	}		   
  }
  
  if (!blnFound)
  {   alert('Debe seleccionar por lo menos un sobre para remesar'); 
      return false;
  }  

  event.returnValue = true;
  return true;
}

f_Suma();

		</script>
	</body>
</HTML>
