<%@ Page Language="vb" AutoEventWireup="false" Codebehind="SICAR_Reporte_ConsultaCaja_Excel.aspx.vb" Inherits="SisCajas.SICAR_Reporte_ConsultaCaja_Excel"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Reporte_ConsultaCaja</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body leftMargin="0" topMargin="0" marginheight="0" marginwidth="0">
		<form id="frmPrincipal" method="post" runat="server">
			<tr>
				<td colspan="4">
					<table width="100%">
						<TR>
							<td><asp:datagrid style="Z-INDEX: 0" id="DGListaInd" runat="server" AutoGenerateColumns="False" CssClass="Arial12b"
									Visible="False">
									<AlternatingItemStyle HorizontalAlign="Center" Height="21px" BackColor="#DDDEE2"></AlternatingItemStyle>
									<ItemStyle HorizontalAlign="Center" Height="21px" BackColor="#E9EBEE"></ItemStyle>
									<HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderColor="#999999" CssClass="Arial12B"></HeaderStyle>
									<Columns>
										<asp:BoundColumn Visible="False" DataField="ID_T_TI_CAJA_DIARIO" HeaderText="ID"></asp:BoundColumn>
										<asp:BoundColumn DataField="OFICINA" HeaderText="Oficina">
											<HeaderStyle Wrap="False" HorizontalAlign="Left" Width="130px"></HeaderStyle>
											<ItemStyle HorizontalAlign="Left"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="CAJA" HeaderText="Caja">
											<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="50px"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="NOMBRE_CAJA" HeaderText="Nombre de Caja">
											<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="50px"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="CAJERO" HeaderText="Cajero">
											<HeaderStyle Wrap="False" HorizontalAlign="Left" Width="150px"></HeaderStyle>
											<ItemStyle HorizontalAlign="Left"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="FECHA" HeaderText="Fecha de Apertura">
											<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="60px"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="FECHA_CIERRE" HeaderText="Fecha de Cierre">
											<HeaderStyle Wrap="False" HorizontalAlign="Left" Width="120px"></HeaderStyle>
											<ItemStyle HorizontalAlign="Left"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="DIAS_PENDIENTES" HeaderText="Dias Pendientes">
											<HeaderStyle Wrap="False" HorizontalAlign="Left" Width="120px"></HeaderStyle>
											<ItemStyle HorizontalAlign="Left"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="ESTADO" HeaderText="Estado">
											<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="50px"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center"></ItemStyle>
										</asp:BoundColumn>
									</Columns>
								</asp:datagrid></td>
						</TR>
						<tr>
							<td>
                                                                <!--INI-936 - JH - Se quitó la tilde a la letra "i" de la columna DIAS_PENDIENTES -->
								<asp:datagrid style="Z-INDEX: 0" id="DGListaGen" runat="server" CssClass="Arial12b" AutoGenerateColumns="False"
									Visible="False">
									<AlternatingItemStyle HorizontalAlign="Center" Height="21px" BackColor="#DDDEE2"></AlternatingItemStyle>
									<ItemStyle HorizontalAlign="Center" Height="21px" BackColor="#E9EBEE"></ItemStyle>
									<HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderColor="#999999" CssClass="Arial12B"></HeaderStyle>
									<Columns>
										<asp:BoundColumn Visible="False" DataField="ID_TRS_CUADRE_GEN" HeaderText="ID"></asp:BoundColumn>
										<asp:BoundColumn DataField="OFICINA" HeaderText="Oficina">
											<HeaderStyle Wrap="False" HorizontalAlign="Left" Width="130px"></HeaderStyle>
											<ItemStyle HorizontalAlign="Left"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="CAJERO" HeaderText="Cajero">
											<HeaderStyle Wrap="False" HorizontalAlign="Left" Width="150px"></HeaderStyle>
											<ItemStyle HorizontalAlign="Left"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="NOMBRE_CAJA" HeaderText="Nombre Caja">
											<HeaderStyle Wrap="False" HorizontalAlign="Left" Width="150px"></HeaderStyle>
											<ItemStyle HorizontalAlign="Left"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="FECHA" HeaderText="Fecha de Apertura">
											<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="80px"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="FECHA_CIERRE" HeaderText="Fecha de Cierre" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}">
											<HeaderStyle Wrap="False" HorizontalAlign="Left" Width="160px"></HeaderStyle>
											<ItemStyle HorizontalAlign="Left"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="DIAS_PENDIENTES" HeaderText="Dias Pendientes" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}">
											<HeaderStyle Wrap="False" HorizontalAlign="Left" Width="160px"></HeaderStyle>
											<ItemStyle HorizontalAlign="Left"></ItemStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="ESTADO" HeaderText="Estado">
											<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="50px"></HeaderStyle>
											<ItemStyle HorizontalAlign="Center"></ItemStyle>
										</asp:BoundColumn>
									</Columns>
								</asp:datagrid>
							</td>
						</tr>
					</table>
				</td>
			</tr>
			</TABLE></form>
	</body>
</HTML>
