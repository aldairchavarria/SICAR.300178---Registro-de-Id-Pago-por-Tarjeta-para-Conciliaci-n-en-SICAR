<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ListadoClienteB.aspx.vb" Inherits="SisCajas.ListadoClienteB"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>América Móvil</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<LINK href="../estilos/est_General.css" type="text/css" rel="styleSheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="frmPrincipal" method="post" runat="server">
			<TABLE id="Table1" borderColor="#336699" cellSpacing="0" cellPadding="0" width="700" align="left"
				border="1">
				<TR>
					<TD>
						<TABLE class="Arial10B" id="Table2" cellSpacing="0" cellPadding="0" width="475" align="center"
							border="0">
							<TR>
								<TD width="10" height="4" border="0"></TD>
								<TD class="TituloRConsulta" align="center" width="90%" height="32">Reporte Clientes 
									Business</TD>
								<TD vAlign="top" width="14" height="32"></TD>
							</TR>
						</TABLE>
						<BR>
						<TABLE class="Arial10B" id="Table2" cellSpacing="0" cellPadding="0" width="475" border="0">
							<TR>
								<TD class="Arial11B" width="90%" height="32">&nbsp;&nbsp;Número de RUC: <b>
										<asp:label id="lblRUC" runat="server"></asp:label></b></TD>
								<TD vAlign="top" width="14" height="32"></TD>
							</TR>
						</TABLE>
						&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:datagrid class="Arial11B" id="DGReporte" runat="server" Width="700px" AutoGenerateColumns="False"
							CellPadding="0">
							<HeaderStyle Wrap="False"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="CUENTA" HeaderText="Cuenta">
									<HeaderStyle Width="110px"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="RAZON_SOCIAL" HeaderText="Raz&#243;n social">
									<HeaderStyle Width="225px"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="TELEFONO" HeaderText="Telf. referencia">
									<HeaderStyle Width="75px"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="OHREFNUM" HeaderText="Recibo">
									<HeaderStyle Width="90px"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="FEC_EMISION" HeaderText="Fec emisi&#243;n" DataFormatString="{0:d}">
									<HeaderStyle Width="80px"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="FEC_VENCIMIENTO" HeaderText="Fec vencimiento" DataFormatString="{0:d}">
									<HeaderStyle Width="80px"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="DEUDA_PENDIENTE" HeaderText="Deuda" DataFormatString="{0:N2}">
									<HeaderStyle Width="60px"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right" VerticalAlign="Middle"></ItemStyle>
								</asp:BoundColumn>
							</Columns>
						</asp:datagrid>
						<TABLE class="Arial10B" id="Table2" cellSpacing="0" cellPadding="0" width="700" border="0">
							<TR>
								<TD class="Arial11B" align="right" width="90%" height="32">&nbsp;&nbsp;Total: <b>
										<asp:label id="lblTotal" runat="server"></asp:label>&nbsp;&nbsp;</b></TD>
							</TR>
						</TABLE>
						<div id=divBotones name=divBotones style="visibility:visible">
						<TABLE class="Arial10B" id="Table2" align=center cellSpacing="0" cellPadding="0" width="400" border="0">
							<tr><td colspan=2>&nbsp;</td></tr>
							<TR>
								<TD class="Arial11B" align="center" width="50%"><input id="cmdCancelar" name="cmdCancelar" value="Cancelar" class="BotonOptm" style="WIDTH: 91px; HEIGHT: 18px"
										size="9" onclick="f_cancelar()"></TD>
								<TD class="Arial11B" align="center" width="50%"><input id="cmdImprimir" name="cmdImprimir" value="Imprimir" class="BotonOptm" style="WIDTH: 80px; HEIGHT: 18px"
										size="8" onclick="f_imprimir()"></TD>
								
							</TR>
							<tr><td colspan=2>&nbsp;</td></tr>
						</TABLE>
						</div>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</html>
<script language=javascript>
	function f_cancelar(){
		history.back();
	}
	function f_imprimir(){
		divBotones.style.visibility ="hidden";
		window.print();
		divBotones.style.visibility ="visible";
	}
</script>
