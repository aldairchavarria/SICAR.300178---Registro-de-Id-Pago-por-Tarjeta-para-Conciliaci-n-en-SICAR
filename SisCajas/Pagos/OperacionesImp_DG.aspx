<%@ Page Language="vb" AutoEventWireup="false" Codebehind="OperacionesImp_DG.aspx.vb" Inherits="SisCajas.OperacionesImp_DG" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>OperacionesImp_DG</title>
		<meta name="vs_snapToGrid" content="True">
		<meta name="vs_showGrid" content="True">
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<link href="../estilos/est_General.css" rel="styleSheet" type="text/css">
		<STYLE> BODY { FONT-SIZE: 10pt; FONT-FAMILY: Verdana }
	.ClsEncab { FONT-WEIGHT: bold; FONT-SIZE: 10px; FONT-FAMILY: Verdana }
	TABLE { BORDER-RIGHT: 0px solid; BORDER-TOP: 0px solid; FONT-SIZE: 9px; BORDER-LEFT: 0px solid; WIDTH: 9.0cm; BORDER-BOTTOM: 0px solid; FONT-FAMILY: Verdana }
	
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
		<script language="jscript">
		
		window.onload = window_onload;
				
			
		function window_onload() {	
			var cod = document.all("codDG");
			if (cod.innerText!=""){
				window.parent.Imprimir();			
			}
		}
				
		function doprint() {
			printbtn.style.visibility = "HIDDEN";
			window.print();
			printbtn.style.visibility = "VISIBLE";
		}
			
		function Imp_DG()
		{
			divBotones.style.visibility = "HIDDEN";
			window.print();
			divBotones.style.visibility = "VISIBLE";
		}
			
		</script>
	</HEAD>
	<body>
		<form id="frmPrincipal" method="post" runat="server">
		<div id="divBotones">
					<table class="tabla_borde" cellSpacing="0" cellPadding="4" width="400" align="center">
						<tr>
							<td align="center"><input class="Boton" id="btnImprimir" onmouseover="this.className='BotonResaltado'" onclick="javascript:Imp_DG();"
									onmouseout="this.className='Boton'" size="15" value="Imprimir" name="btnAgregar" runat="server" width="50">
							</td>
							<td align="center"><input class="Boton" id="btnCancelar" onmouseover="this.className='BotonResaltado'" onclick="window.close();"
									onmouseout="this.className='Boton'" size="15" value="Cerrar" name="btnCancelar" runat="server" width="50">
							</td>
						</tr>
					</table>
				</div>
			<div id="printbtn" STYLE="LEFT:50px; POSITION:absolute; TOP:0px">
				<a href="javascript:doprint();"><img src="../../images/botones/btn_Imprimir.gif" border="0" style="CURSOR:hand"></a>&nbsp;&nbsp;
				<a href="javascript:window.close();"><img src="../../images/botones/btn_Cerrar.gif" border="0" style="CURSOR:hand"></a>
			</div>
			<table border="0" cellspacing="0" style="WIDTH: 6.073cm">
				<TR>
					<TD style='TEXT-ALIGN:center'><b><%=ConfigurationSettings.AppSettings("gStrRazonSocial")%></b></TD>
				</TR>
				<TR>
					<TD style='TEXT-ALIGN:center'><b>RUC <%=ConfigurationSettings.AppSettings("gStrRUC")%></b></TD>
				</TR>
				<TR>
					<TD style='TEXT-ALIGN:center'><b><%=ConfigurationSettings.AppSettings("gStrDireccion")%></b></TD>
				</TR>
			</table>
			<BR>
			<table border="0" cellspacing="0" style="WIDTH: 6.073cm">
				<TR>
					<TD WIDTH="45%" class='clsTextoDeta'><b>N° Doc Autorizado</b></TD>
					<TD class='clsTextoDeta'><b>:</b></TD>
					<TD class='clsTextoDeta' id="codDG"><b><%=strDocAutorizado%></b></TD>
				</TR>
				<TR>
					<TD WIDTH="45%" class='clsTextoDeta'>Cliente</TD>
					<TD class='clsTextoDeta'>:</TD>
					<TD class='clsTextoDeta'><%=strCliente%></TD>
				</TR>
				<TR>
					<TD WIDTH="45%" class='clsTextoDeta'>Contacto</TD>
					<TD class='clsTextoDeta'>:</TD>
					<TD class='clsTextoDeta'><%=strContacto%></TD>
				</TR>
				<TR>
					<TD WIDTH="45%" class='clsTextoDeta'>Direccion</TD>
					<TD class='clsTextoDeta'>:</TD>
					<TD class='clsTextoDeta'><%=strDireccion%></TD>
				</TR>
				<TR>
					<TD WIDTH="45%" class='clsTextoDeta'>Distrito</TD>
					<TD class='clsTextoDeta'>:</TD>
					<TD class='clsTextoDeta'><%=strDistrito%></TD>
				</TR>
				<TR>
					<TD WIDTH="45%" class='clsTextoDeta'>Provincia</TD>
					<TD class='clsTextoDeta'>:</TD>
					<TD class='clsTextoDeta'><%=strProvincia%></TD>
				</TR>
				<TR>
					<TD WIDTH="45%" class='clsTextoDeta'>Departamento</TD>
					<TD class='clsTextoDeta'>:</TD>
					<TD class='clsTextoDeta'><%=strDepartamento%></TD>
				</TR>
				<TR>
					<TD WIDTH="45%" class='clsTextoDeta'>DOCUMENTO</TD>
					<TD class='clsTextoDeta'>:</TD>
					<TD class='clsTextoDeta'><%=strDocumento%></TD>
				</TR>
				<TR>
					<TD WIDTH="45%" class='clsTextoDeta'>Fecha de Emision</TD>
					<TD class='clsTextoDeta'>:</TD>
					<TD class='clsTextoDeta'><%=strFechaEmision%></TD>
				</TR>
				<TR>
					<TD WIDTH="45%" class='clsTextoDeta'>Fecha de Vencimiento</TD>
					<TD class='clsTextoDeta'>:</TD>
					<TD class='clsTextoDeta'><%=strFechaVencimiento%></TD>
				</TR>
				<TR>
					<TD WIDTH="45%" class='clsTextoDeta'>Codigo de Pago</TD>
					<TD class='clsTextoDeta'>:</TD>
					<TD class='clsTextoDeta'><%=strCodigoPago%></TD>
				</TR>
				<TR>
					<TD WIDTH="45%" class='clsTextoDeta'>Punto de Venta</TD>
					<TD class='clsTextoDeta'>:</TD>
					<TD class='clsTextoDeta'><%=strPuntoVenta%></TD>
				</TR>
			</table>
			<BR>
			<table border="0" cellspacing="0" style="WIDTH: 6.073cm">
				<TR>
					<TD width="65%" class='clsTextoDeta'><b>Descripcion</b></TD>
					<TD class='clsTextoDeta' style='TEXT-ALIGN:right'><b>Importe en S/</b></TD>
				</TR>
				<TR>
					<TD width="65%" class='clsTextoDeta'>Renta Adelantada
						<br>
						<%=strTelefono%>
					</TD>
					<TD class='clsTextoDeta' style='TEXT-ALIGN:right'><%=strImporte%></TD>
				</TR>
			</table>
			<br>
			<table border="0" cellspacing="0" style="WIDTH: 6.073cm">
				<TR>
					<TD width="50%" class='clsTextoDeta'>&nbsp;</TD>
					<TD class='clsTextoDeta'>Subtotal</TD>
					<TD class='clsTextoDeta' style='TEXT-ALIGN:right'><%=strSubTotal%></TD>
				</TR>
				<TR>
					<TD width="50%" class='clsTextoDeta'>&nbsp;</TD>
					<TD class='clsTextoDeta'><%=strEtiquetaIGV%></TD>
					<TD class='clsTextoDeta' style='TEXT-ALIGN:right'><%=strIgv%></TD>
				</TR>
				<TR>
					<TD width="50%" class='clsTextoDeta'>&nbsp;</TD>
					<TD class='clsTextoDeta'><b>Total Pago:</b></TD>
					<TD class='clsTextoDeta' style='TEXT-ALIGN:right'><b><%=strTotalPago%></b></TD>
				</TR>
			</table>
			<br>
			<table border="0" cellspacing="0" style="WIDTH: 6.073cm">
				<TR>
					<TD width="10%" class='clsTextoDeta'>&nbsp;</TD>
					<TD width="80%" style="VERTICAL-ALIGN: baseline; TEXT-ALIGN: center" class='clsTextoDeta'><%
					       'PROY-26366-IDEA-34247 FASE 1 - INICIO
							Dim reimpre as String 
							reimpre= request.item("Reimpresion")
							if reimpre="1" Then
							  Response.Write("***REIMPRESION***")
							End If
						    'PROY-26366-IDEA-34247 FASE 1 - FIN
						%>
					</TD>
					<TD width="10%" class='clsTextoDeta'>&nbsp;</TD>
				</TR>
			</table>
			<br>
		</form>
	</body>
</HTML>
