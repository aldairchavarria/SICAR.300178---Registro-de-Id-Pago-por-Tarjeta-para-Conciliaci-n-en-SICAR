<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ListarTurno.aspx.vb" Inherits="SisCajas.ListarTurno" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ListarTurno</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../estilos/est_General.css" type="text/css" rel="styleSheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="frmPrincipal" method="post" runat="server">
			<input id="hldIdConfig" type="hidden" name="hldIdConfig">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 136px; POSITION: absolute; TOP: 32px" borderColor="#336699"
				cellSpacing="0" cellPadding="4" width="475" align="center" border="1">
				<TR><TD align="center"><asp:label class="TituloRConsulta" id="lblTitActualizaBIN" runat="server">Mantenimiento de Turnos</asp:label></TD></TR>
			</TABLE>
			<TABLE id="Table2" style="Z-INDEX: 103; LEFT: 160px; POSITION: absolute; TOP: 312px" borderColor="#336699"
				cellSpacing="0" cellPadding="4" width="340" align="center" border="1">
				<TR><TD>
						<TABLE class="Arial10B" id="Table3" cellSpacing="0" cellPadding="0" width="100%" align="center"
							border="0">
							<TR><TD align="center"><INPUT class="BotonOptm" style="WIDTH: 100px" onclick="f_Grabar();" type="button" value="Nuevo"
									name="btnGrabar" runat="server">&nbsp;&nbsp;</TD>
								<TD align="center"><INPUT class="BotonOptm" style="WIDTH: 100px" onclick="f_Cancelar()" type="button" value="Cancelar"
									name="btnCancelar" runat="server">&nbsp;&nbsp;</TD>
							</TR>
						</TABLE>
					</TD></TR>
			</TABLE>
			<asp:datagrid class="tabla_interna_borde2" id="DGListaTurnos" style="Z-INDEX: 102; LEFT: 112px; POSITION: absolute; TOP: 96px"
				runat="server" CellPadding="1" AutoGenerateColumns="False" Width="520px" Height="156px">
				<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="Black" VerticalAlign="Middle"></HeaderStyle>
				<Columns>
					<asp:BoundColumn DataField="TURNO_DESCRIP" HeaderText="Descripci&#243;n"></asp:BoundColumn>
					<asp:BoundColumn DataField="TURNO_INICIO" HeaderText="Hora Inicio"></asp:BoundColumn>
					<asp:BoundColumn DataField="TURNO_FIN" HeaderText="Hora Final"></asp:BoundColumn>
					<asp:BoundColumn DataField="TURNO_TOLERANCIA" HeaderText="Tolerancia"></asp:BoundColumn>
					<asp:BoundColumn DataField="BIN_ESTADO" HeaderText="Estado"></asp:BoundColumn>
					<asp:TemplateColumn HeaderText="Act.">
						<HeaderStyle Width="50px"></HeaderStyle>
						<ItemTemplate>
							<img src="../../images/botones/btn_Iconolupa.gif" style="cursor:hand" onclick="f_Actualizar('<%# DataBinder.Eval(Container,"DataItem.ID_CONFTURNO")%>')" >
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
			</asp:datagrid></form>
<script language="javascript">
  function f_Grabar(){
	Direcc = "NuevoTurno.aspx?CodTurno=NUEVO"
	window.open(Direcc,"Adiciona","directories=no,menubar=no,scrollbars=no,top=200,resizable=yes,left=185,width=430,height=375");
  }
  function f_Cancelar(){
	history.back()   
  } 
  function f_Actualizar(var1){
	Direcc = "ActTurno.aspx?CodTurno="+var1
	window.open(Direcc,"Adiciona","directories=no,menubar=no,scrollbars=no,top=200,resizable=yes,left=185,width=430,height=375");
  }
  function f_Refrescar(){
	frmPrincipal.submit()
  }
</script>
</body>
</HTML>
