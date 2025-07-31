<%@ Page Language="vb" AutoEventWireup="false" Codebehind="visorRecTrsPago_detalle.aspx.vb" Inherits="SisCajas.visorRecTrsPago_detalle"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
	<title>América Móvil</title>
	<meta name=GENERATOR content="Microsoft Visual Studio .NET 7.1">
	<meta name=CODE_LANGUAGE content="Visual Basic .NET 7.1">
	<meta name=vs_defaultClientScript content=JavaScript>
	<meta name=vs_targetSchema content=http://schemas.microsoft.com/intellisense/ie5><LINK rel=styleSheet type=text/css href="../estilos/est_General.css" >
	<script type=text/javascript src="../librerias/Lib_FuncGenerales.js"></script>
	<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
	<script language=JavaScript>
	</script>
</HEAD>
<body MS_POSITIONING="GridLayout">
<form id=frmVisTrsPagDet method=post runat="server">
<input id="hdnOrdenacion" runat="server" type="hidden" name="hdnOrdenacion">
<table border=0 cellSpacing=0 cellPadding=0>
  <TBODY>
  <tr>
    <td>
      <TABLE style="BORDER-BOTTOM: #336699 1px solid; BORDER-LEFT: #336699 1px solid; WIDTH: 974px; HEIGHT: 225px; BORDER-TOP: #336699 1px solid; BORDER-RIGHT: #336699 1px solid" 
      id=Table1 border=0 cellSpacing=0 cellPadding=5 width=974 align=center>
       <TR>
          <TD class=TituloRConsulta align=center>Detalle Recaudaciones - Recau Pag</TD></TR>
       <TR>
          <TD>
            <TABLE id=Table5 class=Arial12b cellPadding=3><tbody>
              <tr>
                <td style="PADDING-BOTTOM: 0px; PADDING-LEFT: 0px; PADDING-RIGHT: 0px; PADDING-TOP: 0px">
                  <div style="WIDTH: 1024px; OVERFLOW-Y:scroll;OVERFLOW-X:scroll; Height:400px;" class=frame2>
					<asp:datagrid style="Z-INDEX: 0" id=DGLista runat="server"
						DataKeyField="ID_T_TRS_REG_DEUDA" CssClass="Arial12b" 
						AutoGenerateColumns="False" AllowSorting="True">
					<AlternatingItemStyle CssClass="RowEven"></AlternatingItemStyle>
					<ItemStyle CssClass="RowOdd"></ItemStyle>
					<HeaderStyle CssClass="Arial12b"></HeaderStyle>
					<Columns>
						<asp:TemplateColumn HeaderText="Nro. Transacción" ItemStyle-HorizontalAlign="Center" SortExpression="NRO_TRANSACCION">
							<HeaderStyle Wrap="False" Width="110px" HorizontalAlign="Center"></HeaderStyle>
							<ItemTemplate>
								<%# DataBinder.Eval(Container,"DataItem.NRO_TRANSACCION") %>
							</ItemTemplate>
							<EditItemTemplate>
								<%# DataBinder.Eval(Container,"DataItem.NRO_TRANSACCION") %>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Oficina de Venta" ItemStyle-HorizontalAlign="Center" SortExpression="NOM_OF_VENTA">
							<HeaderStyle Wrap="False" Width="160px" HorizontalAlign="Center"></HeaderStyle>
							<ItemTemplate>
								<%# DataBinder.Eval(Container,"DataItem.NOM_OF_VENTA") %>
							</ItemTemplate>
							<EditItemTemplate>
								<%# DataBinder.Eval(Container,"DataItem.NOM_OF_VENTA") %>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Sub Oficina" ItemStyle-HorizontalAlign="Center" SortExpression="SUB_OFICINA_DESC">
							<HeaderStyle Wrap="False" Width="160px" HorizontalAlign="Center"></HeaderStyle>
							<ItemTemplate>
							<%# DataBinder.Eval(Container,"DataItem.SUB_OFICINA_DESC") %>
							</ItemTemplate>
							<EditItemTemplate>
							<%# DataBinder.Eval(Container,"DataItem.SUB_OFICINA_DESC") %>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Fecha Trans." ItemStyle-HorizontalAlign="Center" SortExpression="FECHA_TRANSAC">
							<HeaderStyle Wrap="False" Width="80px" HorizontalAlign="Center"></HeaderStyle>
							<ItemTemplate>
								<%# format(DataBinder.Eval(Container,"DataItem.FECHA_TRANSAC"), "dd/MM/yyyy") %>
							</ItemTemplate>
							<EditItemTemplate>
								<%# format(DataBinder.Eval(Container,"DataItem.FECHA_TRANSAC"), "dd/MM/yyyy") %>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Hora Trans." ItemStyle-HorizontalAlign="Center" SortExpression="HORA_TRANSAC">
							<HeaderStyle Wrap="False" Width="80px" HorizontalAlign="Center"></HeaderStyle>
							<ItemTemplate>
								<%# format(DataBinder.Eval(Container,"DataItem.HORA_TRANSAC"), "HH:mm:ss") %>
							</ItemTemplate>
							<EditItemTemplate>
								<%# format(DataBinder.Eval(Container,"DataItem.HORA_TRANSAC"), "HH:mm:ss") %>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Importe Pago" ItemStyle-HorizontalAlign="Right" SortExpression="IMPORTE_PAGO">
							<HeaderStyle Wrap="False" Width="80px" HorizontalAlign="Center"></HeaderStyle>
							<ItemTemplate>
								<%# Format(DataBinder.Eval(Container,"DataItem.IMPORTE_PAGO"),"###0.00") %>
							</ItemTemplate>
							<EditItemTemplate>
								<%# Format(DataBinder.Eval(Container,"DataItem.IMPORTE_PAGO"),"###0.00") %>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Importe Pagado" ItemStyle-HorizontalAlign="Right" SortExpression="IMPORTE_PAGADO">
							<HeaderStyle Wrap="False" Width="80px" HorizontalAlign="Center"></HeaderStyle>
							<ItemTemplate>
								<%# Format(DataBinder.Eval(Container,"DataItem.IMPORTE_PAGADO"),"###0.00") %>
							</ItemTemplate>
							<EditItemTemplate>
								<%# Format(DataBinder.Eval(Container,"DataItem.IMPORTE_PAGADO"),"###0.00") %>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Importe Pago Dólares" ItemStyle-HorizontalAlign="Right" SortExpression="IMPORTE_PAGO_DOL">
							<HeaderStyle Wrap="False" Width="80px" HorizontalAlign="Center"></HeaderStyle>
							<ItemTemplate>
								<%# Format(DataBinder.Eval(Container,"DataItem.IMPORTE_PAGO_DOL"),"###0.00") %>
							</ItemTemplate>
							<EditItemTemplate>
								<%# Format(DataBinder.Eval(Container,"DataItem.IMPORTE_PAGO_DOL"),"###0.00") %>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Importe Pagado Dólares" ItemStyle-HorizontalAlign="Right" SortExpression="IMPORTE_PAGADO_DOL">
							<HeaderStyle Wrap="False" Width="100px" HorizontalAlign="Center"></HeaderStyle>
							<ItemTemplate>
								<%# Format(DataBinder.Eval(Container,"DataItem.IMPORTE_PAGADO_DOL"),"###0.00") %>
							</ItemTemplate>
							<EditItemTemplate>
								<%# Format(DataBinder.Eval(Container,"DataItem.IMPORTE_PAGADO_DOL"),"###0.00") %>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Estado Trans." ItemStyle-HorizontalAlign="Center" SortExpression="ESTADO">
							<HeaderStyle Wrap="False" Width="130px" HorizontalAlign="Center"></HeaderStyle>
							<ItemTemplate>
								<%# DataBinder.Eval(Container,"DataItem.ESTADO") %>
							</ItemTemplate>
							<EditItemTemplate>
								<asp:DropDownList ID="cboEstado" Runat="server" CssClass="clsSelectEnable"></asp:DropDownList>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Nro. Teléfono" ItemStyle-HorizontalAlign="Center" SortExpression="NRO_TELEFONO">
							<HeaderStyle Wrap="False" Width="80px" HorizontalAlign="Center"></HeaderStyle>
							<ItemTemplate>
								<%# DataBinder.Eval(Container,"DataItem.NRO_TELEFONO") %>
							</ItemTemplate>
							<EditItemTemplate>
								<%# DataBinder.Eval(Container,"DataItem.NRO_TELEFONO") %>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Cod. Cajero" ItemStyle-HorizontalAlign="Center" SortExpression="COD_CAJERO">
							<HeaderStyle Wrap="False" Width="80px" HorizontalAlign="Center"></HeaderStyle>
							<ItemTemplate>
								<%# DataBinder.Eval(Container,"DataItem.COD_CAJERO") %>
							</ItemTemplate>
							<EditItemTemplate>
								<%# DataBinder.Eval(Container,"DataItem.COD_CAJERO") %>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Cajero" ItemStyle-HorizontalAlign="Center" SortExpression="NOM_CAJERO">
							<HeaderStyle Wrap="False" Width="270px" HorizontalAlign="Center"></HeaderStyle>
							<ItemTemplate>
								<%# DataBinder.Eval(Container,"DataItem.NOM_CAJERO") %>
							</ItemTemplate>
							<EditItemTemplate>
								<%# DataBinder.Eval(Container,"DataItem.NOM_CAJERO") %>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Vía Pago" ItemStyle-HorizontalAlign="Center" SortExpression="DESC_VIA_PAGO">
							<HeaderStyle Wrap="False" Width="150px" HorizontalAlign="Center"></HeaderStyle>
							<ItemTemplate>
								<%# DataBinder.Eval(Container,"DataItem.DESC_VIA_PAGO") %>
							</ItemTemplate>
							<EditItemTemplate>
								<%# DataBinder.Eval(Container,"DataItem.DESC_VIA_PAGO") %>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Nro. Cheque / Tarjeta" ItemStyle-HorizontalAlign="Center" SortExpression="NRO_CHEQUE">
							<HeaderStyle Wrap="False" Width="130px" HorizontalAlign="Center"></HeaderStyle>
							<ItemTemplate>
								<%# DataBinder.Eval(Container,"DataItem.NRO_CHEQUE") %>
							</ItemTemplate>
							<EditItemTemplate>
								<%# DataBinder.Eval(Container,"DataItem.NRO_CHEQUE") %>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Ruc Deudor" ItemStyle-HorizontalAlign="Center" SortExpression="RUC_DEUDOR">
							<HeaderStyle Wrap="False" Width="90px" HorizontalAlign="Center"></HeaderStyle>
							<ItemTemplate>
								<%# DataBinder.Eval(Container,"DataItem.RUC_DEUDOR") %>
							</ItemTemplate>
							<EditItemTemplate>
								<%# DataBinder.Eval(Container,"DataItem.RUC_DEUDOR") %>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Servicio" ItemStyle-HorizontalAlign="Center" SortExpression="DESC_SERVICIO">
							<HeaderStyle Wrap="False" Width="130px" HorizontalAlign="Center"></HeaderStyle>
							<ItemTemplate>
								<%# DataBinder.Eval(Container,"DataItem.DESC_SERVICIO") %>
							</ItemTemplate>
							<EditItemTemplate>
								<%# DataBinder.Eval(Container,"DataItem.DESC_SERVICIO") %>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Doc. Contable" ItemStyle-HorizontalAlign="Center" SortExpression="DOC_CONTABLE">
							<HeaderStyle Wrap="False" Width="70px" HorizontalAlign="Center"></HeaderStyle>
							<ItemTemplate>
								<%# DataBinder.Eval(Container,"DataItem.DOC_CONTABLE") %>
							</ItemTemplate>
							<EditItemTemplate>
								<%# DataBinder.Eval(Container,"DataItem.DOC_CONTABLE") %>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Usuario FI" ItemStyle-HorizontalAlign="Center" SortExpression="USUARIO_FI">
							<HeaderStyle Wrap="False" Width="80px" HorizontalAlign="Center"></HeaderStyle>
							<ItemTemplate>
								<%# DataBinder.Eval(Container,"DataItem.USUARIO_FI") %>
							</ItemTemplate>
							<EditItemTemplate>
								<%# DataBinder.Eval(Container,"DataItem.USUARIO_FI") %>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Fecha Envio" ItemStyle-HorizontalAlign="Center" SortExpression="FECHA_FI">
							<HeaderStyle Wrap="False" Width="160px" HorizontalAlign="Center"></HeaderStyle>
							<ItemTemplate>
								<%# DataBinder.Eval(Container,"DataItem.FECHA_FI") %>
							</ItemTemplate>
							<EditItemTemplate>
								<%# DataBinder.Eval(Container,"DataItem.FECHA_FI") %>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Estado" ItemStyle-HorizontalAlign="Center" SortExpression="ESTADO_CONT">
							<HeaderStyle Wrap="False" Width="160px" HorizontalAlign="Center"></HeaderStyle>
							<ItemTemplate>
								<%# DataBinder.Eval(Container,"DataItem.ESTADO_CONT") %>
							</ItemTemplate>
							<EditItemTemplate>
								<%# DataBinder.Eval(Container,"DataItem.ESTADO_CONT") %>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:EditCommandColumn HeaderText="Opción" ButtonType="LinkButton" UpdateText="Aceptar" CancelText="Cancelar" EditText="Editar" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="90px" ItemStyle-Wrap="False">
						</asp:EditCommandColumn>
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
			<div style="float:left; padding-left: 350px;">
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
				document.frmTmp.action = 'visorRecTrsPago_excel.aspx';
				document.frmTmp.submit();
			}
		</script>
		<form method=post name=frmTmp action="" target=_blank>
		</form>
	</body>
	
</HTML>
