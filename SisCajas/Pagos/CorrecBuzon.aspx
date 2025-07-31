<%@ Page Language="vb" AutoEventWireup="false" Codebehind="CorrecBuzon.aspx.vb" Inherits="SisCajas.CorrecBuzon"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>CorrecBuzon</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="JavaScript" src="../librerias/ubigeo.js"></script>
		<script language="JavaScript" src="../librerias/date-picker.js"></script>
		<script language="JavaScript" src="../librerias/Lib_FuncValidacion.js"></script>
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<LINK href="../estilos/est_General.css" type="text/css" rel="styleSheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="frmPrincipal" name="frmPrincipal" method="post" runat="server">
			<div id="overDiv" style="Z-INDEX: 101; WIDTH: 100px; POSITION: absolute"></div>
			<table cellSpacing="0" cellPadding="0" width="800" border="0">
				<tr>
					<td vAlign="top" width="10">&nbsp;</td>
					<td vAlign="top" width="810">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0" name="Contenedor">
							<tr>
								<td align="center" height="10"></td>
							</tr>
						</table>
						<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="100%" align="center"
							border="1" name="Contenedor">
							<tr>
								<td align="center">
									<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
										<tr>
											<td vAlign="top" width="10" height="32"></td>
											<td class="TituloRConsulta" style="PADDING-TOP: 4px" vAlign="top" align="center" width="98%"
												height="32">Eliminación de sobres registrados sin remesar</td>
											<td vAlign="top" width="10" height="32"></td>
										</tr>
									</table>
									<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
										<tr>
											<td vAlign="top" width="14"></td>
											<td style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px; PADDING-BOTTOM: 5px; PADDING-TOP: 5px"
												width="98%">
												<table cellSpacing="0" cellPadding="0" width="770" border="0">
													<tr>
														<td height="4"></td>
													</tr>
												</table>
												<table cellSpacing="0" cellPadding="0" width="770" border="0">
													<TR>
														<TD style="WIDTH: 66px; HEIGHT: 18px" width="66"></TD>
														<TD class="Arial12b" style="WIDTH: 121px; HEIGHT: 18px"></TD>
														<TD class="Arial12b" style="WIDTH: 85px; HEIGHT: 18px"></TD>
														<TD class="Arial12b" style="WIDTH: 60px; HEIGHT: 18px" width="60"></TD>
														<TD style="HEIGHT: 18px" width="300"></TD>
													</TR>
													<TR>
														<TD colSpan="5">
															<div class="frame2" style="BORDER-RIGHT: 1px; BORDER-TOP: 1px; OVERFLOW-Y: scroll; OVERFLOW-X: scroll; BORDER-LEFT: 1px; WIDTH: 775px; BORDER-BOTTOM: 1px; HEIGHT: 350px; TEXT-ALIGN: center"><asp:datagrid id="dgSobres" runat="server" Width="750px" CellSpacing="1" CellPadding="1" BorderWidth="1px"
																	AutoGenerateColumns="False" BorderColor="White">
																	<AlternatingItemStyle BackColor="#E9EBEE"></AlternatingItemStyle>
																	<ItemStyle CssClass="Arial11B" BackColor="#DDDEE2"></ItemStyle>
																	<HeaderStyle HorizontalAlign="Center" Height="22px" CssClass="Arial12B"></HeaderStyle>
																	<Columns>
																		<asp:TemplateColumn HeaderText="Seleccionar">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="40px"></HeaderStyle>
																			<ItemStyle HorizontalAlign="Center"></ItemStyle>
																			<ItemTemplate>
																				<asp:CheckBox id="chkSobre" runat="server"></asp:CheckBox>
																			</ItemTemplate>
																		</asp:TemplateColumn>
																		<asp:BoundColumn DataField="BUZON_BOLSA" HeaderText="N&#176; de sobre">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="90px"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="BUZON_FECHA" HeaderText="Fecha">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="200px"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="BUZON_TIPOVIA" HeaderText="Tipo Via">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="250px"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="BUZON_MONTO" HeaderText="Importe" DataFormatString="{0:0.00}">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="200px"></HeaderStyle>
																			<ItemStyle HorizontalAlign="Right"></ItemStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="BUZON_USUARIO" HeaderText="Usuario">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="120px"></HeaderStyle>
																			<ItemStyle HorizontalAlign="Center"></ItemStyle>
																		</asp:BoundColumn>
																	</Columns>
																</asp:datagrid></div>
														</TD>
													</TR>
													<tr>
														<td>&nbsp;&nbsp;&nbsp;&nbsp;
														</td>
														<td class="Arial12B">Total en&nbsp;Soles
														</td>
														<td><input class="clsInputDisable" id="txtSoles" readOnly type="text" name="txtSoles">
														</td>
														<td class="Arial12B">Total en Dolares
														</td>
														<td><input class="clsInputDisable" id="txtDolares" readOnly type="text" name="txtDolares">
														</td>
													</tr>
												</table>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
							<tr>
								<td height="10"></td>
							</tr>
							<tr>
								<td height="10"></td>
							</tr>
						</table>
						<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="400" align="center"
							border="1">
							<TR>
								<TD align="center">
									<TABLE cellSpacing="2" cellPadding="0" border="0">
										<TR>
											<TD align="center" width="28"></TD>
											<TD align="center" width="85">
												<asp:button id="btnEliminar" runat="server" CssClass="BotonOptm" Width="98px" Text="Eliminar"></asp:button>
												<INPUT id="hddEnvio" type="hidden" name="hddEnvio" runat="server">
											</TD>
											<TD align="center" width="28"></TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
						</table>
					</td>
				</tr>
			</table>
			</TD></TR></TABLE></form>
		<script language="javascript" type="text/javascript">


function e_numero(){
	if ( !( (event.keyCode>=48) && (event.keyCode<=57)) && !(event.keyCode==13) && !(event.keyCode==46) )
		event.keyCode=0;
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
         sumaSol+=parseFloat(dgSobres.rows[i].cells[4].innerText);
       else  
         sumaDol+=parseFloat(dgSobres.rows[i].cells[4].innerText);
     }
  }
  document.frmPrincipal.txtSoles.value = sumaSol;
  document.frmPrincipal.txtDolares.value = sumaDol;
}

function f_Valida()
{
  event.returnValue = false;
  blnFound = false;
  if(!ValidaAlfanumerico('document.frmPrincipal.txtBolsa','el campo numero de bolsa ',false)) return false
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
  {   alert('Debe seleccionar por lo menos un sobre para eliminar'); 
      return false;
  }  

  event.returnValue = true;
  return true;
}

f_Suma();

		</script>
	</body>
</HTML>
