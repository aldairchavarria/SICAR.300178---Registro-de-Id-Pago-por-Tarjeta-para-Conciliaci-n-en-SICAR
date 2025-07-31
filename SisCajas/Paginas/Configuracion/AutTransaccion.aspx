<%@ Page Language="vb" AutoEventWireup="false" Codebehind="AutTransaccion.aspx.vb" Inherits="SisCajas.AutTransaccion"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>America Movil</title>
		<meta content="False" name="vs_snapToGrid">
		<meta content="False" name="vs_showGrid">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../estilos/est_General.css" type="text/css" rel="styleSheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="frmConfig" name="frmConfig" method="post" runat="server">
			<table>
				<tr>
					<td>
						<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="810" align="left" border="1">
							<TBODY>
								<tr>
									<td>
										<table class="Arial10B" cellSpacing="0" cellPadding="0" width="475" align="center" border="0">
											<tr>
												<td width="10" height="4" border="0"></td>
												<td class="TituloRConsulta" align="center" width="90%" height="32">Autoriza 
													Transacción</td>
												<td vAlign="top" width="14" height="32"></td>
											</tr>
										</table>
										<br>
										<br>
										<asp:datagrid class="Arial12B" id="DGAutorizaTran" runat="server" AutoGenerateColumns="False">
											<ItemStyle Wrap="False"></ItemStyle>
											<HeaderStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
											<Columns>
												<asp:BoundColumn Visible="False" DataField="ID_CONFTRAN"></asp:BoundColumn>
												<asp:BoundColumn Visible="False" DataField="ID_AUTTRAN"></asp:BoundColumn>
												<asp:BoundColumn DataField="TRANS_DESC" HeaderText="Transaccion">
													<HeaderStyle Width="100px"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="AUTRAN_NOMCLIENTE" HeaderText="Nom Cliente">
													<HeaderStyle Width="100px"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="AUTRAN_NOMCAJERO" HeaderText="Cajero">
													<HeaderStyle Width="50px"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="AUTRAN_DOC_SAP" HeaderText="Doc. SAP">
													<HeaderStyle Width="75px"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="AUTRAN_FACT_SUNAT" HeaderText="Num.SUNAT">
													<HeaderStyle Width="100px"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="AUTRAN_FECHA" HeaderText="Fecha Trxn">
													<HeaderStyle Width="135px"></HeaderStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="Autorizacion">
													<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
													<ItemTemplate>
														<asp:DropDownList class="Arial11b" id="DropDownList1" runat="server"></asp:DropDownList>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Motivo">
													<ItemTemplate>
														<asp:DropDownList class="Arial11b" id="cboMotivo" runat="server" Width="216px"></asp:DropDownList>
														<asp:TextBox id="txtOtroMot" runat="server" Width="216px" CssClass="clsInputEnable"></asp:TextBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="AUTRAN_MONTDEV" HeaderText="Monto Autorizado" DataFormatString="{0:N2}">
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
											</Columns>
										</asp:datagrid><br>
									</td>
								</tr>
							</TBODY>
						</table>
					</td>
				</tr>
				<tr>
					<td><br>
						<table borderColor="#336699" cellSpacing="0" cellPadding="4" width="360" align="center"
							border="1">
							<tr>
								<td>
									<table class="Arial10B" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
										<tr>
											<td align="center">&nbsp; <input class="BotonOptm" id="cmdGrabar" style="WIDTH: 100px" type="button" value="Grabar"
													name="cmdGrabar" runat="server">&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<input class="BotonOptm" id="btnCancelar" style="WIDTH: 100px" type="button" value="Cancelar"
													name="btnCancelar" runat="server"></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<br>
			</TD></TR><tr>
				<td><br>
					<br>
				</td>
			</tr>
		</form>
		</TBODY></TABLE>
		<script language="javascript"> 



function e_mayuscula() {
	if ((event.keyCode>96&&event.keyCode<123)||(event.keyCode==241)||(event.keyCode==250)||(event.keyCode==243)||(event.keyCode==237)||(event.keyCode==233)||(event.keyCode==225))
		event.keyCode=event.keyCode-32;
}

  function f_OtroMotivo(fila)
  {
    var Valor;
    var Txt;
    //for (i=1;i<DGAutorizaTran.rows.length;i++)
    //{
    
       control = "DGAutorizaTran.rows[fila].cells[7].children.DGAutorizaTran__ctl" + (fila+1) + "_cboMotivo.value";
       otcontrol = "DGAutorizaTran.rows[fila].cells[7].children.DGAutorizaTran__ctl" + (fila+1) + "_txtOtroMot";
	   eval("Valor=" + control +";");
	   eval("Txt=" + otcontrol + ";");
	   
	   if (parseFloat(Valor) == 8)
	   {
	     Txt.style.display = "block"
	   }
	   else
	   {
	     Txt.style.display= "none"
	   }
	   
	   
   // }
  }
		</script>
	</body>
</HTML>
