<%@ Page Language="vb" AutoEventWireup="false" Codebehind="docMontoDisputa.aspx.vb" Inherits="SisCajas.docMontoDisputa"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>resDocumentos</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../Estilos/est_General.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="../../librerias/date-picker.js"></script>
		<script language="JavaScript" src="../../librerias/Lib_FuncValidacion.js"></script>
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<script language="javascript">
				
		function Imprimir(){
		var objIframe = document.getElementById("IfrmImpresion");
		window.open(objIframe.contentWindow.location);
	}
	
	    <% if len(request.item("NombreCliente")) > 0 then %>
	    function f_Imprimir(){
	    
			var NombreCliente = "<%=request.item("NombreCliente")%>";
			var MontoTotalPagado = <%=request.item("MontoTotalPagado")%>;			
			var strTrama = "<%=request.item("strTrama")%>";
			var strRecibos = "<%=request.item("strRecibos")%>";
			
			var objIframe = document.getElementById("IfrmImpresion");
						
			objIframe.style.visibility = "visible";
			//objIframe.style.width = 0;
			//objIframe.style.height = 0;
			objIframe.src = "impRecBusiness.aspx?NombreCliente=" + NombreCliente + "&MontoTotalPagado=" + MontoTotalPagado +  "&strTrama=" + strTrama + "&strRecibos=" + strRecibos;
		}
		
		<% end if %>
		
		
	
		
		
		
		
		</script>
	</HEAD>
	<body>
		<form id="frmPrincipal" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="750" border="0">
				<tr>
					<td>
						<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
							<tr>
								<td height="10"></td>
							</tr>
						</table>
						<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="100%" align="center"
							border="1">
							<tr>
								<td>
									<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
										<tr>
											<td class="TituloRConsulta" align="center">Consulta de Montos en Disputa
											</td>
										</tr>
									</table>
									<br>
									<div style="BORDER-RIGHT: 1px; BORDER-TOP: 1px; OVERFLOW-Y: scroll; OVERFLOW-X: scroll; BORDER-LEFT: 1px; WIDTH: 740px; BORDER-BOTTOM: 1px; HEIGHT: 330px; TEXT-ALIGN: center"><asp:datagrid class="Arial11B" id="dgMontosDisputa" runat="server" Width="700px" AutoGenerateColumns="False"
											CellPadding="0" CellSpacing="1" BorderColor="White">
											<AlternatingItemStyle CssClass="RowOdd"></AlternatingItemStyle>
											<ItemStyle Height="22px" CssClass="RowEven"></ItemStyle>
											<HeaderStyle Wrap="False" HorizontalAlign="Center" CssClass="Arial12B"></HeaderStyle>
											<Columns>
												<asp:BoundColumn DataField="CODIGO_FACTURA" HeaderText="Recibo">
													<HeaderStyle Width="110px" CssClass="ColumnHeader"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="MONTO_FACTURA" HeaderText="Monto Recibo">
													<HeaderStyle Width="110px" CssClass="ColumnHeader"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="FEC_VENCIMIENTO" HeaderText="Fecha de Vencimiento" DataFormatString="{0:d}">
													<HeaderStyle Width="110px" CssClass="ColumnHeader"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="MONTO_DEUDA" HeaderText="Monto Deuda">
													<HeaderStyle Width="110px" CssClass="ColumnHeader"></HeaderStyle>
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="MONTO_RECLAMADO" HeaderText="Monto Reclamado">
													<HeaderStyle Width="110px" CssClass="ColumnHeader"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
											</Columns>
										</asp:datagrid></div>
								</td>
							</tr>
						</table>
						<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
							<tr>
								<td height="10"></td>
							</tr>
						</table>
						<div id="Botones">
							<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="400" align="center"
								border="1">
								<tr>
									<td align="center">
										<table cellSpacing="2" cellPadding="0" border="0">
											<tr>
												<td align="center" width="28"></td>
												<td align="center" width="60"><asp:button id="btnCancelar" runat="server" CssClass="BotonOptm" Width="100px" Text="Cancelar"></asp:button></td>
												<td align="center" width="28"></td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
						</div>
					</td>
				</tr>
			</table>
			<iframe id="IfrmImpresion" style="VISIBILITY: hidden; WIDTH: 0px; HEIGHT: 0px" name="IfrmImpresion"
				src="#"></iframe>
		</form>
	</body>
</HTML>
