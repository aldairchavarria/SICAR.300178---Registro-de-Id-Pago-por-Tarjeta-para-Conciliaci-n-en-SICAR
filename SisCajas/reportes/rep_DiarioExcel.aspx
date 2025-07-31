<%@ Page Language="vb" AutoEventWireup="false" Codebehind="rep_DiarioExcel.aspx.vb" Inherits="SisCajas.rep_DiarioExcel" enableViewState="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>rep_DiarioExcel</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body leftMargin="0" topMargin="0" marginwidth="0" marginheight="0">
		<form id="frmPrincipal" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="1000" border="0">
				<tr>
					<td colspan="4">Detalle de Diario Electrónico</td>
				<tr>
				</tr>
			</table>
			<table>
				<tr>
					<td width="100">Fecha:</td>
					<td width="200">
						<asp:Label id="lblfecha" runat="server"></asp:Label></td>
					<td width="100">Hora:</td>
					<td width="200">
						<asp:Label id="lblHora" runat="server">Label</asp:Label></td>
				</tr>
			</table>
			</TR>
			<tr>
				<td colspan="4">
					<table width="100%">
						<TR>
							<td>
								<asp:datagrid id="dgDiarioE" runat="server" Width="100%" Height="30px" CellSpacing="1" AutoGenerateColumns="False"
									CssClass="Arial11B" BorderColor="Black">
									<HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderColor="#999999" CssClass="Arial12B"
										VerticalAlign="Top"></HeaderStyle>
									<Columns>
										<asp:BoundColumn DataField="COD_CAJERO" HeaderText="Cajero">
											<HeaderStyle Wrap="False" Width="60px"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="TIPO_OPER" HeaderText="Tipo Operaci&#243;n">
											<HeaderStyle Wrap="False" Width="100px"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="TIP_DOCCLI" HeaderText="Tipo Doc. Cliente">
											<HeaderStyle Wrap="False" Width="150px"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="NUM_DOC_CLI" HeaderText="Nro Doc. Cliente">
											<HeaderStyle Wrap="False" Width="150px"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="TIP_DOCVTA" HeaderText="Tipo Doc. Venta">
											<HeaderStyle Wrap="False" Width="120px"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="NUM_DOCSAP" HeaderText="Nro Doc. SAP">
											<HeaderStyle Wrap="False" Width="130px"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="NUM_REF" HeaderText="Nro Referencia">
											<HeaderStyle Wrap="False" Width="130px"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="FECHA_HORA" HeaderText="Fecha Operaci&#243;n">
											<HeaderStyle Wrap="False" Width="160px"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="IMPORTE_PAGADO_PEN" HeaderText="Importe Pagado PEN">
											<HeaderStyle Wrap="False" Width="130px"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="IMPORTE_PAGADO_USD" HeaderText="Importe Pagado USD">
											<HeaderStyle Wrap="False" Width="130px"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="VUELTO" HeaderText="Vuelto">
											<HeaderStyle Wrap="False" Width="85px"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="SUB_TOT" HeaderText="SubTotal">
											<HeaderStyle Wrap="False" Width="100px"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="IMPUESTO" HeaderText="Impuesto">
											<HeaderStyle Wrap="False" Width="80px"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="TOTAL" HeaderText="Total">
											<HeaderStyle Wrap="False" Width="80px"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="COD_ARTICULO" HeaderText="Art&#237;culo">
											<HeaderStyle Wrap="False" Width="100px"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="NUM_SERIE" HeaderText="Nro Serie">
											<HeaderStyle Wrap="False" Width="60px"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="NUM_TELEF" HeaderText="TELEFONO">
											<HeaderStyle Wrap="False" Width="80px"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="CANTIDAD" HeaderText="Cantidad">
											<HeaderStyle Wrap="False" Width="60px"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="COD_VIA" HeaderText="V&#237;a">
											<HeaderStyle Wrap="False" Width="100px"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="NUM_TARJETA" HeaderText="Nro Tarjeta">
											<HeaderStyle Wrap="False" Width="130px"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="MONTO" HeaderText="Monto">
											<HeaderStyle Wrap="False" Width="130px"></HeaderStyle>
										</asp:BoundColumn>
									</Columns>
								</asp:datagrid>
							</td>
						</TR>
					</table>
				</td>
			</tr>
			</TABLE>
		</form>
	</body>
</HTML>
