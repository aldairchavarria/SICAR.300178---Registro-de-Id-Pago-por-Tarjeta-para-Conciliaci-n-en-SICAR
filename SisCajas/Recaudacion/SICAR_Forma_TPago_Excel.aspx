<%@ Page Language="vb" AutoEventWireup="false" Codebehind="SICAR_Forma_TPago_Excel.aspx.vb" Inherits="SisCajas.SICAR_Forma_TPago_Excel" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>SICAR_Forma_TPago_Excel</title> 
		<!-- Inicio - INI-936 - YGP - Nueva pàgina para exportar las formas de pago -->
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:DataGrid style="Z-INDEX: 101; POSITION: absolute; TOP: 128px; LEFT: 104px" id="dgFormaPago"
				runat="server" AutoGenerateColumns="False" Width="368px">
				<HeaderStyle Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" BackColor="Silver"></HeaderStyle>
				<Columns>
					<asp:TemplateColumn HeaderText="ID">
						<ItemTemplate>
							<asp:Label ID="idTipoPago" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.idTipoPago") %>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="FORMA DE PAGO">
						<ItemTemplate>
							<asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.descripcionTipoPago") %>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="CCINS">
						<ItemTemplate>
							<asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.codigoTipoPago") %>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="ESTADO">
						<ItemTemplate>
							<asp:Label ID="Label3" runat="server" Text='<%# IIf(DataBinder.Eval(Container,"DataItem.estadoTipoPago").equals("1"), "Habilitado", "Deshabilitado") %>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
			</asp:DataGrid>
		</form>
		<!-- Fin - INI-936 - YGP -->
	</body>
</HTML>
