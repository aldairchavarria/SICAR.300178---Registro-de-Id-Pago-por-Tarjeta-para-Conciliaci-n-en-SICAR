<%@ Page Language="vb" AutoEventWireup="false" Codebehind="visorRecTrsPago_cajero.aspx.vb" Inherits="SisCajas.visorRecTrsPago_cajero" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
    <title>América Móvil</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
    <LINK rel="styleSheet" type="text/css" href="../estilos/est_General.css">
	<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
    <script language="javascript">	
		function f_Cancelar(){
			window.close();
		}
		
		function f_buscar(){
			if(event.keyCode == 13 ){
				frmVTPCajero.cmdBuscar.click();
			}
		}
	</script>
    
  </head>
  <body MS_POSITIONING="GridLayout">
    <form id="frmVTPCajero" method="post" runat="server">
		<table width="375" border="1" align="center" cellpadding="4" cellspacing="0" bordercolor="#336699">
			<tr>
				<td align="center">
					<asp:Label id="lblTitulo" runat="server" CssClass="TituloConsulta">Buscar Cajero</asp:Label>
				</td>
			</tr>
		</table>
		<br>
		<table width="350" border="0" align="center" cellpadding="4" cellspacing="0" bordercolor="#336699">
			<tr>
				<td><asp:Label id="lblFiltro" runat="server" CssClass="Arial12b">Descripción :</asp:Label></td>
				<td><input name="txtFiltro" style="WIDTH:200px" class="clsInputEnable" id="txtFiltro" size="25"
						maxlength="30" runat="server"></td>
				<td><input style="WIDTH: 100px" id="cmdBuscar" class="BotonOptm" value="Buscar" type="button"
						name="cmdBuscar" runat="server">&nbsp;&nbsp;</td>
				</TD>
			</tr>
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
									EnableViewState="True">
									<AlternatingItemStyle HorizontalAlign="Center" Height="21px" BackColor="#DDDEE2"></AlternatingItemStyle>
									<ItemStyle HorizontalAlign="Center" Height="21px" BackColor="#E9EBEE"></ItemStyle>
									<HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderColor="#999999" CssClass="Arial12B"></HeaderStyle>
									<Columns>
										<asp:TemplateColumn HeaderText="Opci&#243;n" ItemStyle-HorizontalAlign="Center">
											<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="5%"></HeaderStyle>
											<ItemTemplate>
												<INPUT id="rbSel" type="radio" value='<%# DataBinder.Eval(Container,"DataItem.CODIGO") %>' name="rbSel">
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
