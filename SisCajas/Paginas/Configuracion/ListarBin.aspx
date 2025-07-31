<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ListarBin.aspx.vb" Inherits="SisCajas.ListarBin"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>America Movil</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../estilos/est_General.css" type="text/css" rel="styleSheet">
	</head>
	<body MS_POSITIONING="GridLayout">
		<form id="frmListaBin" method="post" runat="server">
			<table  borderColor="#336699"
				cellSpacing="0" cellPadding="4" width="600"  border="1">
				<tr><td align="center"><asp:label class="TituloRConsulta" id="lblTitActualizaBIN" runat="server">Mantenimiento de BIN's</asp:label></td></tr>
			</table>
			
			
			<br>
			<table width=600 border = 0 >
			
			<tr>
			
			<td align=center>
			<asp:datagrid id="DGListaBin" 
				class="tabla_interna_borde2" runat="server" Width="448px" AutoGenerateColumns="False" Height="141px">
				<HeaderStyle Font-Bold="True" ForeColor="Black"></HeaderStyle>
				<Columns>
					<asp:BoundColumn DataField="BIN_COD" HeaderText="Numero">
						<HeaderStyle Width="100px"></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="BIN_DESC" HeaderText="Descripcion">
						<HeaderStyle Width="200px"></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="BIN_ESTADO" HeaderText="Estado">
						<HeaderStyle Width="75px"></HeaderStyle>
					</asp:BoundColumn>
					<asp:TemplateColumn HeaderText="Act.">
						<HeaderStyle Width="50px"></HeaderStyle>
						<ItemTemplate>
							<!--<asp:Image id="img" runat="server" style="cursor:hand" src="../../images/botones/btn_Iconolupa.gif"></asp:Image>-->
							<img src="../../images/botones/btn_Iconolupa.gif" style="cursor:hand" onclick="f_Actualizar('<%# DataBinder.Eval(Container,"DataItem.ID_CONFBIN")%>')" >
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
			</asp:datagrid>
			</td></tr>
			<tr>
			<td>
			<TABLE id="Table2"  borderColor="#336699"
				cellSpacing="0" cellPadding="4" width="340" align="center" border="1">
				<TR><TD>
						<TABLE class="Arial10B" id="Table3" cellSpacing="0" cellPadding="0" width="100%" align="center"
							border="0">
							<TR><TD align="center"><INPUT class="BotonOptm" style="WIDTH: 100px" onclick="f_Grabar();" type="button" value="Nuevo"
									name="btnGrabar" runat="server" ID="Button1">&nbsp;&nbsp;</TD>
								<TD align="center"><INPUT class="BotonOptm" style="WIDTH: 100px" onclick="f_Cancelar()" type="button" value="Cancelar"
									name="btnCancelar" runat="server" ID="Button2">&nbsp;&nbsp;</TD>
							</TR>
						</TABLE>
					</TD></TR>
			</TABLE>
			</td>
			</tr>
			</form>
	</body>

<script language="javascript">
  function f_Grabar(){
	Direcc = "NuevoBin.aspx?CodBin=NUEVO"
	window.open(Direcc,"Adiciona","directories=no,menubar=no,scrollbars=no,top=200,resizable=yes,left=185,width=400,height=350");
  }
  function f_Cancelar(){
	history.back()   
  } 
  function f_Actualizar(var1){
	Direcc = "ActBin.aspx?CodBin="+var1
	window.open(Direcc,"Adiciona","directories=no,menubar=no,scrollbars=no,top=200,resizable=yes,left=185,width=400,height=350");
  }
  function f_Refrescar(){
	frmListaBin.submit()
  }
</script>
</html>
