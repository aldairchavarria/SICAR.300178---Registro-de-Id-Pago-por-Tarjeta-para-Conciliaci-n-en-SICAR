<%@ Page Language="vb" AutoEventWireup="false" Codebehind="rep_pagosPorDeliveryExcel.aspx.vb" Inherits="SisCajas.rep_pagosPorDeliveryExcel" contentType="application/vnd.ms-excel" enableViewState="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
    <title>rep_pagosPorDeliveryExcel</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
  </head>
  <body MS_POSITIONING="GridLayout">

    <form id="frmPrincipal" method="post" runat="server">
		<table width="800">
			<tr>
			   <td>
			    <asp:datagrid id="dgReporte" runat="server" AutoGenerateColumns="False" BorderColor="Black" CssClass="Arial11B">
					<ItemStyle Font-Size="X-Small" Font-Names="Arial" BackColor="#E9EBEE"></ItemStyle>
					<HeaderStyle Font-Size="X-Small" Font-Names="Arial" Font-Bold="True" HorizontalAlign="Center" BorderWidth="1px" BorderColor="#999999" CssClass="Arial12B"
					VerticalAlign="Top"></HeaderStyle>
					<Columns>
						<asp:BoundColumn DataField="D_FECHA_REGISTRO" HeaderText="Fecha Hora Registro"></asp:BoundColumn>
						<asp:BoundColumn DataField="N_PEDIN_NRO_PEDIDO" HeaderText="Numero Pedido"></asp:BoundColumn>
						<asp:BoundColumn DataField="V_TIPODOCUMENTO" HeaderText="Tipo Documento"></asp:BoundColumn>
						<asp:BoundColumn DataField="V_NRODOCUMENTO" HeaderText="N Documento"></asp:BoundColumn>
						<asp:BoundColumn DataField="D_FECHACOBRO" HeaderText="Fecha Cobro"></asp:BoundColumn>
						<asp:BoundColumn DataField="MODALIDAD_PAGO" HeaderText="Modalidad Pago"></asp:BoundColumn>
						<asp:BoundColumn DataField="V_MEDIOPAGO" HeaderText="Medio Pago"></asp:BoundColumn>
						<asp:BoundColumn DataField="'ID Venta'" HeaderText="Id. Venta"></asp:BoundColumn>
						<asp:BoundColumn DataField="'Nro Aprobacion'" HeaderText="N Autorizacion"></asp:BoundColumn>
						<asp:BoundColumn DataField="D_MONTO_PAGADO" HeaderText="Monto Pagado"></asp:BoundColumn>
					</Columns>
				 </asp:datagrid>
			   </td>
			</tr>
		</table>
    </form>
  </body>
</html>
