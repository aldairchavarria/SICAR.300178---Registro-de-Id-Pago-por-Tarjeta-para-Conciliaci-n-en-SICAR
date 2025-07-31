<%@ Page Language="vb" AutoEventWireup="false" Codebehind="grdCajeroxSubOf.aspx.vb" Inherits="SisCajas.grdCajeroxSubOf" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>grdCajeroxSubOf</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../estilos/est_General.css" type="text/css" rel="styleSheet">
		<style>
		.tbl_Cajas { BORDER-BOTTOM: #999999 1px solid; BORDER-LEFT: #999999 1px solid; FONT-FAMILY: Arial; FONT-SIZE: 10px; BORDER-TOP: #999999 1px solid; BORDER-RIGHT: #999999 1px solid; TEXT-DECORATION: none }
		.tbl_Cajas TH { TEXT-ALIGN: center; PADDING-BOTTOM: 5px; BACKGROUND-COLOR: #21618c; HEIGHT: 22px; COLOR: #ffffff; PADDING-TOP: 5px }
		.tbl_Cajas TD { BORDER-BOTTOM: #999999 1px solid; TEXT-ALIGN: center; BORDER-LEFT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-RIGHT: #999999 1px solid }
		.listBozCssClass { BORDER-BOTTOM: #bfbee9 1px solid; BORDER-LEFT: #bfbee9 1px solid; BACKGROUND-COLOR: #bfbee9; BORDER-TOP: #bfbee9 1px solid; BORDER-RIGHT: #bfbee9 1px solid }
		</style>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<div style="width:975px">
				<div style="BORDER-BOTTOM:#336699 3px solid; TEXT-ALIGN:center; BORDER-LEFT:#336699 3px solid; PADDING-BOTTOM:7px; MARGIN-TOP:0px; PADDING-LEFT:7px; WIDTH:90%; PADDING-RIGHT:7px; MARGIN-LEFT:5%; BORDER-TOP:#336699 3px solid; BORDER-RIGHT:#336699 3px solid; PADDING-TOP:7px">
					<label class="TituloRConsulta">Mantenimiento de Cajero por Sub Oficina</label>
				</div>
				<div style="WIDTH:90%; MARGIN-LEFT:5%; padding-top:10px; padding-bottom:10px;">
					<div style="width:100%; border:3px #336699 solid;">
						<div style="width:80%; margin-left:10%;">
							<table style="position:relative; width:100%" class="Arial12B" cellSpacing="5" cellPadding="0">
								<tr>
									<td style="WIDTH:15%">Oficina de Venta:</td>
									<td style="WIDTH:20%">
										<input id="txtCodOficina" class="clsInputEnable" style="WIDTH:100%" runat="server" readonly >
									</td>
									<td style="WIDTH:40%">
										<input id="txtDesOficina" class="clsInputEnable" style="WIDTH:100%" runat="server" readonly >
									</td>
								</tr>
								<tr>
									<td style="WIDTH:15%">Sub Oficina:</td>
									<td style="WIDTH:20%">
										<input id="txtCodSubOficina" class="clsInputEnable" style="WIDTH:100%" runat="server" readonly >
									</td>
									<td style="WIDTH:40%">
										<input id="txtDesSubOficina" class="clsInputEnable" style="WIDTH:100%" runat="server" readonly >
									</td>
								</tr>
							</table>
						</div>
					</div>
				</div>
				<div style="WIDTH:90%; MARGIN-LEFT:5%; PADDING-TOP:10px; border:3px #336699 solid;">
					<div class="frame2" style="Z-INDEX: 1; BORDER-BOTTOM: 1px; POSITION: relative; TEXT-ALIGN: center; BORDER-LEFT: 1px; OVERFLOW-X: scroll; OVERFLOW-Y: scroll; WIDTH: 100%; HEIGHT: 300px; BORDER-TOP: 1px; BORDER-RIGHT: 1px">
						<asp:datagrid id="gridDetalle" runat="server" BorderWidth="1px" Width="750px" AutoGenerateColumns="False"
							CellSpacing="1" CellPadding="1" BorderColor="White">
							<AlternatingItemStyle HorizontalAlign="Center" BackColor="#E9EBEE" Width="200px"></AlternatingItemStyle>
							<ItemStyle HorizontalAlign="Center" Height="25px" Width="200px" CssClass="Arial11B" BackColor="#DDDEE2"></ItemStyle>
							<HeaderStyle HorizontalAlign="Center" Height="22px" CssClass="Arial12B" BorderWidth="1px" BorderColor="#999999"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="CASOV_ID" HeaderText="ID" HeaderStyle-Width="10%">
									<HeaderStyle BorderWidth="1px" BorderColor="#999999"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Cajero" HeaderStyle-Width="10%">
									<HeaderStyle BorderWidth="1px" BorderColor="#999999"></HeaderStyle>
									<ItemTemplate>
										<%# DataBinder.Eval(Container,"DataItem.CASOC_CAJERO") & " - " & DataBinder.Eval(Container,"DataItem.CAJERO_DESC")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="CASOV_COMENTARIO" HeaderText="Comentario" HeaderStyle-Width="10%">
									<HeaderStyle BorderWidth="1px" BorderColor="#999999"></HeaderStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Estado" ItemStyle-HorizontalAlign="Center">
									<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="10%"></HeaderStyle>
									<ItemTemplate>
										<%# IIf(DataBinder.Eval(Container,"DataItem.CASOV_ESTADO") = "1", "ACTIVO", "INACTIVO")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Editar" ItemStyle-HorizontalAlign="Center">
									<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="10%"></HeaderStyle>
									<ItemTemplate>
										<a  href='javascript:f_Editar("<%# DataBinder.Eval(Container.DataItem, "CASOV_ID")%>")'>
											Editar </a>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:datagrid>
					</div>
				</div>
				<div style="WIDTH:90%; MARGIN-LEFT:5%; PADDING-TOP:10px; text-align:center">
					<div style="width:40%; padding-top:7px; padding-bottom:7px; border:3px #336699 solid;">
						<input type="button" id="btnNuevo" class="BotonOptm" value="Nuevo" style="width:48%;" onclick="f_RegistroNuevo()" />
						<input type="button" id="btnSalir" class="BotonOptm" value="Regresar" style="width:48%;" onclick="FN_Close()" />
					</div>
				</div>
			</div>
			
			<input type="hidden" id="hdnCodOficina" runat="server" >
			<input type="hidden" id="hdnDesOficina" runat="server" >
			<input type="hidden" id="hdnCodSubOficina" runat="server" >
			<input type="hidden" id="hdnDesSubOficina" runat="server" >
			<input type="hidden" id="hdnOficina" runat="server" NAME="hdnOficina">
			<input type="hidden" id="hdnUsuario" runat="server" NAME="hdnUsuario">
			<asp:Button Runat="server" ID="btnloadDataHandler" style="display:none;"></asp:Button>
		</form>
		
		<script>
		function f_Editar(strID){
			var strVariables = "?strOption=Update&strID="+strID+"&strCodOficina=" + document.getElementById("hdnCodOficina").value + "&strDesOficina=" + document.getElementById("hdnDesSubOficina").value + "&strCodSubOficina=" + document.getElementById("hdnCodSubOficina").value;
			
			var url = 'mntCajeroxSubOf.aspx';
			var retVal = window.showModalDialog(url+strVariables,null, 'dialogWidth:420px;dialogHeight:450px;Menubar=no;Status=no;Titlebar=no;Toolbar=no;Location=no');
			if (retVal != undefined){
				document.getElementById('btnloadDataHandler').click();
			}
		}
		
		function f_RegistroNuevo(){
			var strVariables = "?strOption=Insert&strID=&strCodOficina=" + document.getElementById("hdnCodOficina").value + "&strDesOficina=" + document.getElementById("hdnDesSubOficina").value + "&strCodSubOficina=" + document.getElementById("hdnCodSubOficina").value;
			
			var url = 'mntCajeroxSubOf.aspx';
			var retVal = window.showModalDialog(url+strVariables,null, 'dialogWidth:420px;dialogHeight:450px;Menubar=no;Status=no;Titlebar=no;Toolbar=no;Location=no');
			if (retVal != undefined){
				document.getElementById('btnloadDataHandler').click();
			}
		}
		
		function FN_Close(){			
			var strURL = "grdSubOficinas.aspx?back=R";
			window.location = strURL;
		}
		</script>
		
	</body>
</HTML>
