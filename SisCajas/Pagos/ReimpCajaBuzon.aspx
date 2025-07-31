<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ReimpCajaBuzon.aspx.vb" Inherits="SisCajas.ReimpCajaBuzon"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ReimpCajaBuzon</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="JavaScript" src="../librerias/ubigeo.js"></script>
		<script language="JavaScript" src="../librerias/date-picker.js"></script>
		<script language="JavaScript" src="../librerias/Lib_FuncValidacion.js"></script>
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<LINK href="../estilos/est_General.css" type="text/css" rel="styleSheet">
		<script language="javascript">
		
		
		function f_Imprimir(){
	
			var objIframe = document.getElementById("IfrmImpresion");
			
			objIframe.style.visibility = "visible";
			objIframe.style.width = 0;
			objIframe.style.height = 0;
			
			objIframe.src = "FormatoBuzonReimp.aspx?strBolsa=" + document.frmPrincipal.txtBolsa.value;
		}
		
		function Imprimir()
	    {					
			var objIframe = document.getElementById("IfrmImpresion");
			window.open(objIframe.contentWindow.location);
			
	    }
	    
	    
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="frmPrincipal" name="frmPrincipal" method="post" runat="server">
			<input id="hldVerif" type="hidden" name="hldVerif" runat="server">
			<div id="overDiv" style="Z-INDEX: 101; WIDTH: 100px; POSITION: absolute"></div>
			<table cellSpacing="0" cellPadding="0" width="800" border="0">
				<tr>
					<td vAlign="top" width="10">&nbsp;</td>
					<td vAlign="top" width="810">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0" name="Contenedor">
							<tr>
								<td align="center" height="10"></td>
							</tr>
						</table>
						<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="100%" align="center"
							border="1" name="Contenedor">
							<tr>
								<td align="center">
									<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
										<tr>
											<td vAlign="top" width="10" height="32"></td>
											<td class="TituloRConsulta" style="PADDING-TOP: 4px" vAlign="top" align="center" width="98%"
												height="32">Reimpresión de envío a Caja Buzón</td>
											<td vAlign="top" width="10" height="32"></td>
										</tr>
									</table>
									<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
										<tr>
											<td vAlign="top" width="14"></td>
											<td style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px; PADDING-BOTTOM: 5px; PADDING-TOP: 5px"
												width="98%">
												<table cellSpacing="0" cellPadding="0" width="770" border="0">
													<tr>
														<td height="4"></td>
													</tr>
												</table>
												<table cellSpacing="0" cellPadding="0" width="770" border="0">
													<TR>
														<TD style="WIDTH: 66px; HEIGHT: 18px" width="66"></TD>
														<TD class="Arial12b" style="WIDTH: 121px; HEIGHT: 18px">&nbsp;&nbsp; Número de 
															sobre:</TD>
														<TD class="Arial12b" style="WIDTH: 85px; HEIGHT: 18px"><asp:textbox id="txtBolsa" runat="server" CssClass="clsInputEnable" MaxLength="10"></asp:textbox></TD>
														<TD class="Arial12b" style="WIDTH: 60px; HEIGHT: 18px" width="60"></TD>
														<TD style="HEIGHT: 18px" width="300"></TD>
													</TR>
												</table>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
							<tr>
								<td height="10"></td>
							</tr>
							<tr>
								<td height="10"></td>
							</tr>
						</table>
						<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="400" align="center"
							border="1">
							<TR>
								<TD align="center">
									<TABLE cellSpacing="2" cellPadding="0" border="0">
										<TR>
											<TD align="center" width="28"></TD>
											<TD align="center" width="85"><input type="button" class="BotonOptm" id="btnImprimir" value="Imprimir" onclick="f_Imprimir()"
													style="WIDTH: 98px"></TD>
											<TD align="center" width="28"></TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
						</table>
					</td>
				</tr>
			</table>
			<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
				<tr>
					<td height="10"></td>
				</tr>
			</table>
			</TD></TR></TABLE> <iframe id="IfrmImpresion"  name="IfrmImpresion" style="VISIBILITY: hidden; WIDTH: 0px; HEIGHT: 0px" src="#"></iframe>
		</form>
		<script language="javascript" type="text/javascript">

document.frmPrincipal.txtBolsa.onkeypress = e_numero;
function e_numero(){
	if ( !( (event.keyCode>=48) && (event.keyCode<=57)) && !(event.keyCode==13) && !(event.keyCode==46) )
		event.keyCode=0;
}

function f_Valida()
{
  event.returnValue = false;
  
  if(!ValidaAlfanumerico('document.frmPrincipal.txtBolsa','el campo numero de bolsa ',false)) return false
  
  event.returnValue = true;
  return true
    
}

		</script>
	</body>
</HTML>
