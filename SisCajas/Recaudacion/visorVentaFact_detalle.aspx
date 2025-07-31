<%@ Page Language="vb" AutoEventWireup="false" Codebehind="visorVentaFact_detalle.aspx.vb" Inherits="SisCajas.visorVentaFact_detalle"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
    <title>América Móvil</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
    <script type=text/javascript src="../librerias/Lib_FuncGenerales.js"></script>
    <LINK rel=styleSheet type=text/css href="../estilos/est_General.css" >
	<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
	<script language=JavaScript>
	</script>
  </head>
  <body MS_POSITIONING="GridLayout">
    <form id="frmVisVtaFactDet" method="post" runat="server">
		<input id="hdnOrdenacion" runat="server" type="hidden" name="hdnOrdenacion">
		<table border=0 cellSpacing=0 cellPadding=0>
		<TBODY>
		<tr>
			<td>
			<TABLE style="BORDER-BOTTOM: #336699 1px solid; BORDER-LEFT: #336699 1px solid; WIDTH: 974px; HEIGHT: 225px; BORDER-TOP: #336699 1px solid; BORDER-RIGHT: #336699 1px solid" 
			id=Table1 border=0 cellSpacing=0 cellPadding=5 width=974 align=center>
			<TR>
				<TD class=TituloRConsulta align=center>Facturación Detallada</TD></TR>
			<TR>
				<TD>
					<TABLE id=Table5 class=Arial12b cellPadding=3><tbody>
					<tr>
						<td style="PADDING-BOTTOM: 0px; PADDING-LEFT: 0px; PADDING-RIGHT: 0px; PADDING-TOP: 0px">
						<div style="WIDTH: 1024px; OVERFLOW-Y:scroll;OVERFLOW-X:scroll; Height:400px;" class=frame2>
							<asp:datagrid style="Z-INDEX: 0" id=DGLista runat="server"
								CssClass="Arial12b" AutoGenerateColumns="False" AllowSorting="True">
							<AlternatingItemStyle CssClass="RowEven"></AlternatingItemStyle>
							<ItemStyle CssClass="RowOdd"></ItemStyle>
							<HeaderStyle CssClass="Arial12b"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="Oficina de Venta" ItemStyle-HorizontalAlign="Center" SortExpression="DESC_OFICINA">
									<HeaderStyle Wrap="False" Width="150px" HorizontalAlign="Center"></HeaderStyle>
									<ItemTemplate>
										<%# DataBinder.Eval(Container,"DataItem.DESC_OFICINA") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Sub Oficina" ItemStyle-HorizontalAlign="Center" SortExpression="SUB_OFICINA_DESC">
									<HeaderStyle Wrap="False" Width="150px" HorizontalAlign="Center"></HeaderStyle>
									<ItemTemplate>
										<%# DataBinder.Eval(Container,"DataItem.SUB_OFICINA_DESC") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Cod. Cajero" ItemStyle-HorizontalAlign="Center" SortExpression="CAJERO">
									<HeaderStyle Wrap="False" Width="80px" HorizontalAlign="Center"></HeaderStyle>
									<ItemTemplate>
										<%# DataBinder.Eval(Container,"DataItem.CAJERO") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Cajero" ItemStyle-HorizontalAlign="Center" SortExpression="NOM_CAJERO">
									<HeaderStyle Wrap="False" Width="200px" HorizontalAlign="Center"></HeaderStyle>
									<ItemTemplate>
										<%# DataBinder.Eval(Container,"DataItem.NOM_CAJERO") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Fecha" ItemStyle-HorizontalAlign="Center" SortExpression="FECHA">
									<HeaderStyle Wrap="False" Width="80px" HorizontalAlign="Center"></HeaderStyle>
									<ItemTemplate>
										<%# DataBinder.Eval(Container,"DataItem.FECHA")  %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Tipo Documento" ItemStyle-HorizontalAlign="Center" SortExpression="TIPO_DOCUMENTO">
									<HeaderStyle Wrap="False" Width="220px" HorizontalAlign="Center"></HeaderStyle>
									<ItemTemplate>
										<%# DataBinder.Eval(Container,"DataItem.TIPO_DOCUMENTO") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Documento" ItemStyle-HorizontalAlign="Center" SortExpression="DESC_DOCUMENTO">
									<HeaderStyle Wrap="False" Width="210px" HorizontalAlign="Center"></HeaderStyle>
									<ItemTemplate>
										<%# DataBinder.Eval(Container,"DataItem.DESC_DOCUMENTO") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Nro. de Pedido" ItemStyle-HorizontalAlign="Center" SortExpression="FACTURA_FICTICIA">
									<HeaderStyle Wrap="False" Width="140px" HorizontalAlign="Center"></HeaderStyle>
									<ItemTemplate>
										<%# DataBinder.Eval(Container,"DataItem.FACTURA_FICTICIA") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Doc Sunat" ItemStyle-HorizontalAlign="Center" SortExpression="REFERENCIA">
									<HeaderStyle Wrap="False" Width="130px" HorizontalAlign="Center"></HeaderStyle>
									<ItemTemplate>
										<%# DataBinder.Eval(Container,"DataItem.REFERENCIA") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Cod. Vendedor" ItemStyle-HorizontalAlign="Center" SortExpression="COD_VENDEDOR">
									<HeaderStyle Wrap="False" Width="90px" HorizontalAlign="Center"></HeaderStyle>
									<ItemTemplate>
										<%# DataBinder.Eval(Container,"DataItem.COD_VENDEDOR") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Vendedor" ItemStyle-HorizontalAlign="Center" SortExpression="VENDEDOR">
									<HeaderStyle Wrap="False" Width="200px" HorizontalAlign="Center"></HeaderStyle>
									<ItemTemplate>
										<%# DataBinder.Eval(Container,"DataItem.VENDEDOR") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Moneda" ItemStyle-HorizontalAlign="Center" SortExpression="MONEDA">
									<HeaderStyle Wrap="False" Width="70px" HorizontalAlign="Center"></HeaderStyle>
									<ItemTemplate>
										<%# DataBinder.Eval(Container,"DataItem.MONEDA") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Clase Factura" ItemStyle-HorizontalAlign="Center" SortExpression="CLASE_FACTURA_COD">
									<HeaderStyle Wrap="False" Width="90px" HorizontalAlign="Center"></HeaderStyle>
									<ItemTemplate>
										<%# DataBinder.Eval(Container,"DataItem.CLASE_FACTURA_COD") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Cuotas" ItemStyle-HorizontalAlign="Right" SortExpression="CUOTA">
									<HeaderStyle Wrap="False" Width="60px" HorizontalAlign="Center"></HeaderStyle>
									<ItemTemplate>
										<%# Format(DataBinder.Eval(Container,"DataItem.CUOTA"),"###0.00") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Total Doc." ItemStyle-HorizontalAlign="Right" SortExpression="TOTFA">
									<HeaderStyle Wrap="False" Width="90px" HorizontalAlign="Center"></HeaderStyle>
									<ItemTemplate>
										<%# Format(DataBinder.Eval(Container,"DataItem.TOTFA"),"###0.00") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Efectivo" ItemStyle-HorizontalAlign="Right" SortExpression="ZEFE">
									<HeaderStyle Wrap="False" Width="60px" HorizontalAlign="Center"></HeaderStyle>
									<ItemTemplate>
										<%# Format(DataBinder.Eval(Container,"DataItem.ZEFE"),"###0.00") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="American Exp." ItemStyle-HorizontalAlign="Right" SortExpression="ZAEX">
									<HeaderStyle Wrap="False" Width="60px" HorizontalAlign="Center"></HeaderStyle>
									<ItemTemplate>
										<%# Format(DataBinder.Eval(Container,"DataItem.ZAEX"),"###0.00") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="NetCard" ItemStyle-HorizontalAlign="Right" SortExpression="ZCAR">
									<HeaderStyle Wrap="False" Width="60px" HorizontalAlign="Center"></HeaderStyle>
									<ItemTemplate>
										<%# Format(DataBinder.Eval(Container,"DataItem.ZCAR"),"###0.00") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Cheque" ItemStyle-HorizontalAlign="Right" SortExpression="ZCHQ">
									<HeaderStyle Wrap="False" Width="60px" HorizontalAlign="Center"></HeaderStyle>
									<ItemTemplate>
										<%# Format(DataBinder.Eval(Container,"DataItem.ZCHQ"),"###0.00") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Cob. Interbank" ItemStyle-HorizontalAlign="Right" SortExpression="ZCIB">
									<HeaderStyle Wrap="False" Width="60px" HorizontalAlign="Center"></HeaderStyle>
									<ItemTemplate>
										<%# Format(DataBinder.Eval(Container,"DataItem.ZCIB"),"###0.00") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Electron" ItemStyle-HorizontalAlign="Right" SortExpression="ZDEL">
									<HeaderStyle Wrap="False" Width="60px" HorizontalAlign="Center"></HeaderStyle>
									<ItemTemplate>
										<%# Format(DataBinder.Eval(Container,"DataItem.ZDEL"),"###0.00") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Dinners" ItemStyle-HorizontalAlign="Right" SortExpression="ZDIN">
									<HeaderStyle Wrap="False" Width="60px" HorizontalAlign="Center"></HeaderStyle>
									<ItemTemplate>
										<%# Format(DataBinder.Eval(Container,"DataItem.ZDIN"),"###0.00") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Maestro" ItemStyle-HorizontalAlign="Right" SortExpression="ZDMT">
									<HeaderStyle Wrap="False" Width="60px" HorizontalAlign="Center"></HeaderStyle>
									<ItemTemplate>
										<%# Format(DataBinder.Eval(Container,"DataItem.ZDMT"),"###0.00") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="MasterCard" ItemStyle-HorizontalAlign="Right" SortExpression="ZMCD">
									<HeaderStyle Wrap="False" Width="60px" HorizontalAlign="Center"></HeaderStyle>
									<ItemTemplate>
										<%# Format(DataBinder.Eval(Container,"DataItem.ZMCD"),"###0.00") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Ripley" ItemStyle-HorizontalAlign="Right" SortExpression="ZRIP">
									<HeaderStyle Wrap="False" Width="60px" HorizontalAlign="Center"></HeaderStyle>
									<ItemTemplate>
										<%# Format(DataBinder.Eval(Container,"DataItem.ZRIP"),"###0.00") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="CMR" ItemStyle-HorizontalAlign="Right" SortExpression="ZSAG">
									<HeaderStyle Wrap="False" Width="60px" HorizontalAlign="Center"></HeaderStyle>
									<ItemTemplate>
										<%# Format(DataBinder.Eval(Container,"DataItem.ZSAG"),"###0.00") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Visa" ItemStyle-HorizontalAlign="Right" SortExpression="ZVIS">
									<HeaderStyle Wrap="False" Width="60px" HorizontalAlign="Center"></HeaderStyle>
									<ItemTemplate>
										<%# Format(DataBinder.Eval(Container,"DataItem.ZVIS"),"###0.00") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Carsa" ItemStyle-HorizontalAlign="Right" SortExpression="ZCRS">
									<HeaderStyle Wrap="False" Width="60px" HorizontalAlign="Center"></HeaderStyle>
									<ItemTemplate>
										<%# Format(DataBinder.Eval(Container,"DataItem.ZCRS"),"###0.00") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Curacao" ItemStyle-HorizontalAlign="Right" SortExpression="ZCZO">
									<HeaderStyle Wrap="False" Width="60px" HorizontalAlign="Center"></HeaderStyle>
									<ItemTemplate>
										<%# Format(DataBinder.Eval(Container,"DataItem.ZCZO"),"###0.00") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="ACE Home Center" ItemStyle-HorizontalAlign="Right" SortExpression="ZACE">
									<HeaderStyle Wrap="False" Width="60px" HorizontalAlign="Center"></HeaderStyle>
									<ItemTemplate>
										<%# Format(DataBinder.Eval(Container,"DataItem.ZACE"),"###0.00") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Trans. deuda post" ItemStyle-HorizontalAlign="Right" SortExpression="TDPP">
									<HeaderStyle Wrap="False" Width="60px" HorizontalAlign="Center"></HeaderStyle>
									<ItemTemplate>
										<%# Format(DataBinder.Eval(Container,"DataItem.TDPP"),"###0.00") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Nota credito" ItemStyle-HorizontalAlign="Right" SortExpression="ZNCR">
									<HeaderStyle Wrap="False" Width="60px" HorizontalAlign="Center"></HeaderStyle>
									<ItemTemplate>
										<%# Format(DataBinder.Eval(Container,"DataItem.ZNCR"),"###0.00") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Cuotas Empl. Claro" ItemStyle-HorizontalAlign="Right" SortExpression="ZEAM">
									<HeaderStyle Wrap="False" Width="60px" HorizontalAlign="Center"></HeaderStyle>
									<ItemTemplate>
										<%# Format(DataBinder.Eval(Container,"DataItem.ZEAM"),"###0.00") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Saldo" ItemStyle-HorizontalAlign="Right" SortExpression="SALDO">
									<HeaderStyle Wrap="False" Width="60px" HorizontalAlign="Center"></HeaderStyle>
									<ItemTemplate>
										<%# Format(DataBinder.Eval(Container,"DataItem.SALDO"),"###0.00") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="1 Cuota" ItemStyle-HorizontalAlign="Right" SortExpression="CUO1">
									<HeaderStyle Wrap="False" Width="60px" HorizontalAlign="Center"></HeaderStyle>
									<ItemTemplate>
										<%# Format(DataBinder.Eval(Container,"DataItem.CUO1"),"###0.00") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="6 Cuotas" ItemStyle-HorizontalAlign="Right" SortExpression="CUO6">
									<HeaderStyle Wrap="False" Width="60px" HorizontalAlign="Center"></HeaderStyle>
									<ItemTemplate>
										<%# Format(DataBinder.Eval(Container,"DataItem.CUO6"),"###0.00") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="12 Cuotas" ItemStyle-HorizontalAlign="Right" SortExpression="CUO12">
									<HeaderStyle Wrap="False" Width="60px" HorizontalAlign="Center"></HeaderStyle>
									<ItemTemplate>
										<%# Format(DataBinder.Eval(Container,"DataItem.CUO12"),"###0.00") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="18 Cuotas" ItemStyle-HorizontalAlign="Right" SortExpression="CUO18">
									<HeaderStyle Wrap="False" Width="60px" HorizontalAlign="Center"></HeaderStyle>
									<ItemTemplate>
										<%# Format(DataBinder.Eval(Container,"DataItem.CUO18"),"###0.00") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="24 Cuotas" ItemStyle-HorizontalAlign="Right" SortExpression="CUO24">
									<HeaderStyle Wrap="False" Width="60px" HorizontalAlign="Center"></HeaderStyle>
									<ItemTemplate>
										<%# Format(DataBinder.Eval(Container,"DataItem.CUO24"),"###0.00") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Estado" ItemStyle-HorizontalAlign="Center" SortExpression="DES_ESTADO">
									<HeaderStyle Wrap="False" Width="70px" HorizontalAlign="Center"></HeaderStyle>
									<ItemTemplate>
										<%# DataBinder.Eval(Container,"DataItem.DES_ESTADO") %>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							</asp:datagrid>
							</DIV>
						</TD>
					</TR>
				</TBODY>
				</TABLE>
				</TD>
				</TR>
				</TABLE>
				<br>
				<div id=divComandos>
					<div style="float:left; padding-left: 400px;">
					<TABLE style="BORDER-BOTTOM: #336699 1px solid; BORDER-LEFT: #336699 1px solid; WIDTH: 245px; HEIGHT: 48px; BORDER-TOP: #336699 1px solid; BORDER-RIGHT: #336699 1px solid" 
						id=Table2 border=0 cellSpacing=0 cellPadding=2 align=center>
						<TR>
							<TD align=center>
								<TABLE style="WIDTH: 250px; HEIGHT: 40px" id=Table4 align=center>
									<TR>
										<TD style="WIDTH: 250px">
										&nbsp;&nbsp;&nbsp; 
										<input style="WIDTH: 100px" id=btnExcel class=BotonOptm onclick=f_Exportar() value="Exportar Excel" type=button name=btnExcel>
										&nbsp;&nbsp;&nbsp;<asp:button style="Z-INDEX: 0" id=btnCancelar runat="server" CssClass="BotonOptm" Width="100px" Text="Regresar"></asp:button>
										</TD>
									</TR>
								</TABLE>
							</TD>
						</TR>
					</TABLE>
					</div>
					<div style="clear:both;"></div>
				</DIV>
				
				</TD>
				</TR>
			</TBODY>
		</TABLE>
	</FORM>
		<script language=JavaScript>
			function f_Exportar()
			{
				document.frmTmp.action = 'visorVentaFact_excel.aspx';
				document.frmTmp.submit();
			}
		</script>
		<form method=post name=frmTmp action="" target=_blank>
		</form>
	</body>
</html>
