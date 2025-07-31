<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ImpSobresPendientes.aspx.vb" Inherits="SisCajas.ImpSobresPendientes"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ImpSobresPendientes</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<style>
			.tabla_borde
			{
				BORDER-RIGHT: #336699 1px solid;
				BORDER-TOP: #336699 1px solid;
				FONT-SIZE: 12px;
				BORDER-LEFT: #336699 1px solid;
				COLOR: #ff0000;
				BORDER-BOTTOM: #336699 1px solid;
				FONT-FAMILY: Arial;
				TEXT-DECORATION: none
			}
			
			.Boton
			{
				border-right: #95b7f3 1px solid;
				border-top: #95b7f3 1px solid;
				font-weight: bold;
				font-size: 10px;
				border-left: #95b7f3 1px solid;
				cursor: hand;
				color: #003399;
				border-bottom: #95b7f3 1px solid;
				font-family: Verdana;
				background-color: white;
				text-align: center;
				TEXT-DECORATION: none;
				BACKGROUND-REPEAT: repeat-x;
				background-color: #e9f2fe;
				/*BACKGROUND-IMAGE: url(../images/toolgrad.gif); */
				border-color :#95b7f3	
			}
			
			.BotonResaltado
			{
				
				border-right: #95b7f3 1px solid;
				border-top: #95b7f3 1px solid;
				font-weight: bold;
				font-size: 10px;
				border-left: #95b7f3 1px solid;
				cursor: hand;
				color: #003399;
				border-bottom: #95b7f3 1px solid;
				font-family: Verdana;
				background-color: white;
				text-align: center;
				TEXT-DECORATION: none;
				BACKGROUND-REPEAT: repeat-x; 	
				border-color :#95b7f3
				
			}
		</style>
		<script language="Javascript">
		
		window.onload = window_onload;							
		function window_onload() {			
				window.parent.Imprimir();					
		}
		
		function Imp_pendientes()
		{
			divBotones.style.visibility = "HIDDEN";
			window.print();
			divBotones.style.visibility = "VISIBLE";
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
		<div id="divBotones">
					<table class="tabla_borde" cellSpacing="0" cellPadding="4" width="400" align="center">
						<tr>
							<td align="center"><input class="Boton" id="btnImprimir" onmouseover="this.className='BotonResaltado'" onclick="javascript:Imp_pendientes();"
									onmouseout="this.className='Boton'" size="15" value="Imprimir" name="btnAgregar" runat="server" width="50">
							</td>
							<td align="center"><input class="Boton" id="btnCancelar" onmouseover="this.className='BotonResaltado'" onclick="window.close();"
									onmouseout="this.className='Boton'" size="15" value="Cerrar" name="btnCancelar" runat="server" width="50">
							</td>
						</tr>
					</table>
				</div>
			<table width="650">
				<tr>
					<td align="center" style="FONT-WEIGHT: bold; FONT-SIZE: 10pt; FONT-FAMILY: Arial">
						SOBRES EN CAJA BUZON PENDIENTES DE REMESAR
					</td>
				</tr>
				<TR>
					<TD style="FONT-WEIGHT: bold; FONT-SIZE: 10pt; FONT-FAMILY: Arial" align="center">
						<%=Session("OFICINA")%>
					</TD>
				</TR>
			</table>
			<br>
			<br>
			<table>
				<tr>
					<td>
						<asp:datagrid id="dgSobres" runat="server" CellSpacing="1" CellPadding="1" BorderWidth="1px" AutoGenerateColumns="False"
							Width="650px">
							<ItemStyle Font-Size="10pt" Font-Names="Arial" Height="30px" CssClass="Arial11B"></ItemStyle>
							<HeaderStyle Font-Size="10pt" Font-Names="Arial" Font-Bold="True" HorizontalAlign="Center" Height="22px"
								CssClass="Arial12B"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="BUZON_BOLSA" HeaderText="N&#176; de sobre"></asp:BoundColumn>
								<asp:BoundColumn DataField="BUZON_FECHA" HeaderText="Fecha"></asp:BoundColumn>
								<asp:BoundColumn DataField="BUZON_TIPOVIA" HeaderText="Tipo Via"></asp:BoundColumn>
								<asp:BoundColumn DataField="BUZON_MONTO" HeaderText="Importe" DataFormatString="{0:0.00}">
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="BUZON_USUARIO" HeaderText="Usuario">
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
							</Columns>
						</asp:datagrid>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
