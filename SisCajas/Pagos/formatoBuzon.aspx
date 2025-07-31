<%@ Page Language="vb" AutoEventWireup="false" Codebehind="formatoBuzon.aspx.vb" Inherits="SisCajas.formatoBuzon" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>formatoBuzon</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
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
		<style type="text/css"> @page { margin : 0cm; }
		</style>
		<script language="Javascript">
		
		window.onload = window_onload;							
		function window_onload() {			
				window.parent.Imprimir();					
		}
		function Imp_Buzon(){
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
							<td align="center"><input class="Boton" id="btnImprimir" onmouseover="this.className='BotonResaltado'" onclick="javascript:Imp_Buzon();"
									onmouseout="this.className='Boton'" size="15" value="Imprimir" name="btnAgregar" runat="server" width="50">
							</td>
							<td align="center"><input class="Boton" id="btnCancelar" onmouseover="this.className='BotonResaltado'" onclick="window.close();"
									onmouseout="this.className='Boton'" size="15" value="Cerrar" name="btnCancelar" runat="server" width="50">
							</td>
						</tr>
					</table>
				</div>
			<table style="WIDTH: 610px; HEIGHT: 288px">
				<tr>
					<td style="WIDTH: 1000px; HEIGHT: 133px" width="1000"><br>
						<br>
						<br>
						<br>
						<br>
						<font style="FONT-WEIGHT: normal; FONT-SIZE: 15px">&nbsp;&nbsp;&nbsp;
							<%= request.item("strCajero") %>
						</font>
						<br>
						<br>
						<font style="FONT-WEIGHT: normal; FONT-SIZE: 15px">&nbsp;&nbsp;&nbsp;
							<%= SESSION("OFICINA") %>
						</font>
					</td>
					<td style="HEIGHT: 133px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
						&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<table width="50" align="right">
							<tr>
								<td><%=request.item("strBolsa")%>
									<br>
								</td>
							</tr>
							<tr>
								<td><font style="FONT-SIZE: 15px">
										<%=request.item("strFecha")%> <!--INICIATIVA-565-->
									</font>
								</td>
							</tr>
							<tr>
								<td><font style="FONT-SIZE: 15px">
										<%=format(now.hour,"00") & ":" & format(now.minute,"00") & ":" & format(now.second,"00") %>
									</font>
								</td>
							</tr>
						</table>
						<br>
						<br>
						<br>
						<br>
						<br>
						<br>
					</td>
				</tr>
				<tr>
					<td width="987" style="WIDTH: 987px; HEIGHT: 133px">
						<font style="FONT-WEIGHT: normal; FONT-SIZE: 15px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
							&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
							<%=request.item("strMonto")%>
						</font>
					</td>
					<td width="200">
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
