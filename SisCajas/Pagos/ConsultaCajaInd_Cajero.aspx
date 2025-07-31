<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ConsultaCajaInd_Cajero.aspx.vb" Inherits="SisCajas.ConsultaCajaInd_Cajero" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>América Móvil</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<LINK rel="styleSheet" type="text/css" href="../estilos/est_General.css">
		<script language="javascript">

	function f_Cancelar(){
		window.close();
	}
	
	function f_buscar(){
		if(event.keyCode == 13 ){
			frmCCTCajeroB.cmdBuscar.click();
		}
	}
	
		</script>
	</head>
	<body MS_POSITIONING="GridLayout">
		<form id="frmCCTCajeroB" method="post" runat="server">
			<table border="1" cellSpacing="0" borderColor="#336699" cellPadding="4" width="375" align="center">
				<tr>
					<td align="center"><asp:label id="lblTitulo" runat="server" CssClass="TituloConsulta">Buscar cajeros</asp:label></td>
				</tr>
			</table>
			<br>
			<table border="0" cellSpacing="0" borderColor="#336699" cellPadding="4" width="350" align="center">
				<tr>
					<td><asp:label id="lblFiltro" runat="server" CssClass="Arial12b">Descripción :</asp:label></td>
					<td><input style="WIDTH: 200px" id="txtFiltro" class="clsInputEnable" maxLength="30" size="25"
							name="txtFiltro" runat="server"></td>
					<td><input style="WIDTH: 100px" id="cmdBuscar" class="BotonOptm" value="Buscar" type="button"
							name="cmdBuscar" runat="server">&nbsp;&nbsp;</td>
					</TD></tr>
			</table>
			<br>
			<div style="HEIGHT:265px;">
				<TABLE style="WIDTH: 325px; HEIGHT: 133px" id="Table5" class="Arial12b" cellPadding="3"
					align="center">
					<tbody>
						<tr>
							<td style="PADDING-BOTTOM: 0px; PADDING-LEFT: 0px; PADDING-RIGHT: 0px; PADDING-TOP: 0px"
								colSpan="7">
								<div style="WIDTH: 324px; HEIGHT: 114px" class="frame2">
									<asp:datagrid style="Z-INDEX: 0" id="DGLista" runat="server" CssClass="Arial11b" Width="328px"
										AutoGenerateColumns="False" AllowPaging="True" PagerStyle-Mode="NumericPages" PageSize="10"
										EnableViewState="True" DataKeyField="CODIGO">
										<AlternatingItemStyle HorizontalAlign="Center" Height="21px" BackColor="#DDDEE2"></AlternatingItemStyle>
										<ItemStyle HorizontalAlign="Center" Height="21px" BackColor="#E9EBEE"></ItemStyle>
										<HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderColor="#999999" CssClass="Arial12B"></HeaderStyle>
										<Columns>
											<asp:TemplateColumn HeaderText="Opci&#243;n" ItemStyle-HorizontalAlign="Center">
												<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="5%"></HeaderStyle>
												<ItemTemplate>
													<asp:CheckBox id="chkSel" runat="server"></asp:CheckBox>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Cod" ItemStyle-HorizontalAlign="Center" Visible="False">
												<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="5%"></HeaderStyle>
												<ItemTemplate>
													<asp:Label ID="lblCodigo" Runat="server" Text='<%# Container.DataItem("CODIGO")%>'>
													</asp:Label>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn DataField="DESCRIPCION" HeaderText="Cajero">
												<HeaderStyle Wrap="False" Width="250px" HorizontalAlign="Left"></HeaderStyle>
												<ItemStyle HorizontalAlign="Left"></ItemStyle>
											</asp:BoundColumn>
										</Columns>
									</asp:datagrid>
								</div>
							</td>
						</tr>
					</tbody>
				</TABLE>
			</div>
			<br>
			<table style="WIDTH: 301px; HEIGHT: 48px" border="1" cellSpacing="0" borderColor="#336699"
				cellPadding="4" width="301" align="center">
				<tr>
					<td>
						<table class="Arial10B" border="0" cellSpacing="0" cellPadding="0" width="100%" align="center">
							<tr>
								<td align="center"><input style="WIDTH: 100px" id="cmdAceptar" class="BotonOptm" value="Aceptar" type="button"
										name="cmdAceptar" runat="server">&nbsp;&nbsp;</td>
								<td align="center"><input style="WIDTH: 100px" id="cmdCancelar" class="BotonOptm" onclick="javascript:f_Cancelar();"
										value="Cancelar" type="button" name="cmdCancelar">&nbsp;&nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>