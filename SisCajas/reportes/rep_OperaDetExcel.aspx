<%@ Page Language="vb" AutoEventWireup="false" Codepage="1252" Codebehind="rep_OperaDetExcel.aspx.vb" Inherits="SisCajas.rep_OperaDetExcel" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>rep_OperaDetExcel</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body leftMargin="0" topMargin="0" marginwidth="0" marginheight="0">
		<form id="frmPrincipal" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="1000" border="0">
				<tr>
					<td colspan="4">Cuadre de Caja - Detalle de Operaciones Procesadas</td>
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
								<asp:datagrid id="DgOpera" runat="server" BorderColor="Black" CssClass="Arial11B" AutoGenerateColumns="False"
									CellSpacing="1" Height="30px" Width="100%">
									<AlternatingItemStyle BackColor="#DDDEE2"></AlternatingItemStyle>
									<ItemStyle BackColor="#E9EBEE"></ItemStyle>
									<HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderColor="#999999" CssClass="Arial12B"
										VerticalAlign="Top"></HeaderStyle>
									<Columns>
										<asp:BoundColumn DataField="ID_CONFTRAN" HeaderText="Transacci&#243;n">
											<HeaderStyle Wrap="False" BorderWidth="1px" Width="50px"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="OPER_TIP_DOCUMENTO" HeaderText="Tipo Doc.">
											<HeaderStyle Wrap="False" BorderWidth="1px" Width="80px"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="OPER_COD_ARTICULO" HeaderText="Art&#237;culo">
											<HeaderStyle Wrap="False" BorderWidth="1px" Width="150px"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="OPER_NOM_CLIENTE" HeaderText="Nombre Cliente">
											<HeaderStyle Wrap="False" BorderWidth="1px" Width="180px"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="OPER_NRO_FACT_SAP" HeaderText="Nro Fact. SAP">
											<HeaderStyle Wrap="False" BorderWidth="1px" Width="100px"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="OPER_NRO_FAC_SUNAT" HeaderText="Doc. Sunat">
											<HeaderStyle Wrap="False" BorderWidth="1px" Width="120px"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="OPER_NRO_TELEFONO" HeaderText="Tel&#233;fono">
											<HeaderStyle Wrap="False" BorderWidth="1px" Width="70px"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="OPER_CANTIDAD" HeaderText="Cantidad">
											<HeaderStyle Wrap="False" BorderWidth="1px" Width="30px"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="OPER_MEDIO_PAGO" HeaderText="Medio de Pago">
											<HeaderStyle Wrap="False" BorderWidth="1px" Width="120px"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="OPER_MONTO" HeaderText="Monto">
											<HeaderStyle Wrap="False" BorderWidth="1px" Width="60px"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="OPER_DOCUMENTO_CLTE" HeaderText="Nro Doc. Cliente">
											<HeaderStyle Wrap="False" BorderWidth="1px" Width="100px"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="oper_nro_referencia" HeaderText="Nro de Referencia">
											<HeaderStyle Wrap="False" BorderWidth="1px" Width="130px"></HeaderStyle>
										</asp:BoundColumn>
										<asp:BoundColumn DataField="COD_CAJERO" HeaderText="Cajero">
											<HeaderStyle Wrap="False" BorderWidth="1px" Width="80px"></HeaderStyle>
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
