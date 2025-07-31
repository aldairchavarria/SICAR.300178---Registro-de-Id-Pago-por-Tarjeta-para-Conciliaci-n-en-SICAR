<%@ Page Language="vb" AutoEventWireup="false" Codebehind="formatoRemesa.aspx.vb" Inherits="SisCajas.formatoRemesa" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>formatoRemesa</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<STYLE>
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
		</STYLE>
		<script language="Javascript">
		
		window.onload = window_onload;							
		function window_onload() {			
				window.parent.Imprimir();					
		}
		
		function Imp_Remesa(){
			divBotones.style.visibility = "HIDDEN";
			window.print();
			divBotones.style.visibility = "VISIBLE";
		}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="frmPrincipal" method="post" runat="server">
			<div id="divBotones">
					<table class="tabla_borde" cellSpacing="0" cellPadding="4" width="400" align="center">
						<tr>
							<td align="center"><input class="Boton" id="btnImprimir" onmouseover="this.className='BotonResaltado'" onclick="javascript:Imp_Remesa();"
									onmouseout="this.className='Boton'" size="15" value="Imprimir" name="btnAgregar" runat="server" width="50">
							</td>
							<td align="center"><input class="Boton" id="btnCancelar" onmouseover="this.className='BotonResaltado'" onclick="window.close();"
									onmouseout="this.className='Boton'" size="15" value="Cerrar" name="btnCancelar" runat="server" width="50">
							</td>
						</tr>
					</table>
				</div>
			<table style="WIDTH: 648px; HEIGHT: 117px">
				<tr>
					<td colspan="4" align="center"><FONT style="FONT-WEIGHT: bold;FONT-FAMILY: Arial">COMPROBANTE 
							DE ENVIO DE DINERO / REMESAS</FONT>
					</td>
				</tr>
				<tr>
					<td colspan="4" style="HEIGHT: 20px"></td>
				</tr>
				<TR>
					<TD style="HEIGHT: 20px" colSpan="4"></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 20px" colSpan="4"></TD>
				</TR>
				<tr>
					<td style="WIDTH: 259px; HEIGHT: 21px"><FONT style="FONT-WEIGHT: bold; FONT-SIZE: 10pt; FONT-FAMILY: Arial">Código 
							PDV</FONT></td>
					<td style="WIDTH: 97px; HEIGHT: 21px"><%= Session("ALMACEN")%></td>
					<td style="WIDTH: 130px; HEIGHT: 21px"><FONT style="FONT-WEIGHT: bold; FONT-SIZE: 10pt; FONT-FAMILY: Arial">Fecha 
							y Hora</FONT></td>
					<td style="HEIGHT: 21px"><%= format(now.day,"00") & "/" & format(now.month,"00") & "/" & now.year & "  " & format(Now.Hour,"00") & ":" & format(Now.Minute,"00") & ":" & format(Now.Second,"00")  %></td>
				</tr>
				<tr>
					<td style="WIDTH: 259px"><FONT style="FONT-WEIGHT: bold; FONT-SIZE: 10pt; FONT-FAMILY: Arial">Nombre 
							PDV</FONT></td>
					<td colspan="3"><%=SESSION("OFICINA")%></td>
				</tr>
				<tr>
					<td style="WIDTH: 259px"><FONT style="FONT-WEIGHT: bold; FONT-SIZE: 10pt; FONT-FAMILY: Arial">Número 
							de bolsa empresa recaudadora</FONT></td>
					<td colspan="3"><%=request.item("Bolsa")%></td>
				</tr>
				<!--INICIATIVA - 529 INI-->
				<tr>
					<td style="WIDTH: 259px"><FONT style="FONT-WEIGHT: bold; FONT-SIZE: 10pt; FONT-FAMILY: Arial">Número 
							de comprobante de servicio</FONT></td>
					<td colspan="3"><%=request.item("CompServ")%></td>
				</tr>
				<!--INICIATIVA - 529 FIN-->
				<tr>
					<td style="WIDTH: 259px"><FONT style="FONT-WEIGHT: bold; FONT-SIZE: 10pt; FONT-FAMILY: Arial">Nombre 
							del responsable</FONT></td>
					<td colspan="3"><%=SESSION("NOMBRE_COMPLETO")%></td>
				</tr>
				<tr>
					<td style="WIDTH: 130px"><FONT style="FONT-WEIGHT: bold; FONT-SIZE: 10pt; FONT-FAMILY: Arial">
							Cod.&nbsp;responsable</FONT></td>
					<td colspan="3"><%=Session("USUARIO")%></td>
				</tr>
			</table>
			<table>
				<tr height="30">
					<td>&nbsp;</td>
				</tr>
			</table>
			<table style="WIDTH: 648px; HEIGHT: 117px" border="1">
				<tr>
					<td>&nbsp;</td>
					<td align="center"><FONT style="FONT-WEIGHT: bold; FONT-SIZE: 10pt; FONT-FAMILY: Arial">SOLES 
							(S/)</FONT></td>
					<td align="center"><FONT style="FONT-WEIGHT: bold; FONT-SIZE: 10pt; FONT-FAMILY: Arial">DOLARES 
							(USD)</FONT></td>
				</tr>
				<tr>
					<td style="HEIGHT: 26px"><FONT style="FONT-SIZE: 10pt; FONT-FAMILY: Arial">Importe a 
							depositar</FONT></td>
					<td style="HEIGHT: 26px" align="right"><%=Request.Item("MonEfSol")%>&nbsp;</td>
					<td style="HEIGHT: 26px" align="right"><%=Request.Item("MonEfDol")%>&nbsp;</td>
				</tr>
				<tr>
					<td><FONT style="FONT-SIZE: 10pt; FONT-FAMILY: Arial">Cheque(s) a depositar</FONT></td>
					<td align="right"><%=Request.Item("MonChSol")%>&nbsp;</td>
					<td align="right"><%=Request.Item("MonChDol")%>&nbsp;</td>
				</tr>
				<tr>
					<td><FONT style="FONT-WEIGHT: bold; FONT-SIZE: 10pt; FONT-FAMILY: Arial">TOTAL A 
							DEPOSITAR</FONT></td>
					<td align="right"><%=format(cDbl(Request.Item("MonEfSol")) +  cDbl(Request.Item("MonChSol")),"####0.00")%>&nbsp;</td>
					<td align="right"><%=format(cDbl(Request.Item("MonEfDol")) +  cDbl(Request.Item("MonChDol")),"####0.00")%>&nbsp;</td>
				</tr>
			</table>
			<br>
			<br>
			<table width="600">
				<tr>
					<td>
						<asp:DataGrid id="dgSobres" runat="server" AutoGenerateColumns="False">
							<ItemStyle Font-Size="10pt" Font-Names="Arial"></ItemStyle>
							<HeaderStyle Font-Size="10pt" Font-Names="Arial" Font-Bold="True" HorizontalAlign="Center"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="BUZON_BOLSA" HeaderText="N&#176; de sobre"></asp:BoundColumn>
								<asp:BoundColumn DataField="BUZON_TIPOVIA" HeaderText="Tipo Via">
									<HeaderStyle Width="200px"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="BUZON_MONTO" HeaderText="Importe" DataFormatString="{0:0.00}">
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="BUZON_FECHA" HeaderText="Fecha">
									<HeaderStyle Width="100px"></HeaderStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="BUZON_USUARIO" HeaderText="Usuario"></asp:BoundColumn>
							</Columns>
						</asp:DataGrid>
					</td>
				</tr>
			</table>
			<br>
			<br>
			<br>
			<br>
			<br>
			<table>
				<tr>
					<td>____________________________________</td>
				</tr>
				<tr>
					<td align="center"><FONT style="FONT-WEIGHT: bold;FONT-SIZE: 10pt;FONT-FAMILY: Arial">Firma 
							del responsable</FONT></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
