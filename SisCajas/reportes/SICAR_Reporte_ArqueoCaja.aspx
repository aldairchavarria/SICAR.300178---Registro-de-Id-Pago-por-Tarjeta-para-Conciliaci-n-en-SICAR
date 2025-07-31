<%@ Page Language="vb" aspcompat="true" AutoEventWireup="false" Codebehind="SICAR_Reporte_ArqueoCaja.aspx.vb" Inherits="SisCajas.SICAR_Reporte_ArqueoCaja" %>
<HTML>
	<HEAD>
		<title>Aplicativo TIM</title>
		<meta http-equiv="Pragma" content="no-cache">
		<META http-equiv="Expires" content="Mon, 06 Jan 1990 00:00:01 GMT">
		<LINK href="../estilos/est_General.css" type="text/css" rel="styleSheet">
			<script language="JavaScript" src="../librerias/date-picker.js"></script>
			<script language="JavaScript" src="../librerias/Lib_FuncValidacion.js"></script>
			<script language="JavaScript">
<!--

function MM_findObj(n, d) { //v4.01
  var p,i,x;  if(!d) d=document; if((p=n.indexOf("?"))>0&&parent.frames.length) {
    d=parent.frames[n.substring(p+1)].document; n=n.substring(0,p);}
  if(!(x=d[n])&&d.all) x=d.all[n]; for (i=0;!x&&i<d.forms.length;i++) x=d.forms[i][n];
  for(i=0;!x&&d.layers&&i<d.layers.length;i++) x=MM_findObj(n,d.layers[i].document);
  if(!x && d.getElementById) x=d.getElementById(n); return x;
}
function MM_showHideLayers() { //v6.0
  var i,p,v,obj,args=MM_showHideLayers.arguments;
  for (i=0; i<(args.length-2); i+=3) if ((obj=MM_findObj(args[i]))!=null) { v=args[i+2];
    if (obj.style) { obj=obj.style; v=(v=='show')?'visible':(v=='hide')?'hidden':v; }
    obj.visibility=v; }
}
function f_Excel(){
	document.frmPrincipal.action = "toExcel.aspx";
	document.frmPrincipal.submit();
};

//-->
			</script>
			<meta http-equiv="pragma" content="no-cache">
	</HEAD>
	<body leftMargin="0" topMargin="0" marginheight="0" marginwidth="0">
		<form id="frmPrincipal" name="frmPrincipal" method="post" runat="server" targetd="_blank">
			<input type=hidden value="<%=strFecha%>" name=strFecha> <input type="hidden" value="1" name="tipo">
			<table cellSpacing="0" cellPadding="0" width="975" border="0">
				<tr>
					<td vAlign="top" width="820">
						<table height="14" cellSpacing="0" cellPadding="0" width="820" border="0" name="Contenedor">
							<tr>
								<td align="center"></td>
							</tr>
						</table>
						<table cellSpacing="0" cellPadding="0" width="790" align="center" border="0" name="Contenedor">
							<tr>
								<td align="center">
									<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="790" align="center"
										border="1">
										<tr>
											<td align="center">
												<table class="Arial10B" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
													<tr>
														<td width="10" height="4" border="0"></td>
														<td class="TituloRConsulta" align="center" width="98%" height="32">
															Reporte&nbsp;Arqueo deCaja</td>
														<td vAlign="top" width="14" height="32"></td>
													</tr>
												</table>
												<table cellSpacing="0" cellPadding="0" width="790" align="center" border="0">
													<tr>
														<td width="98%">
															<table class="Arial10B" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
																<tr>
																	<td align="center"></td>
																</tr>
															</table>
															<table cellSpacing="0" cellPadding="0" width="770" border="0">
																<!--
																<tr><td height="4"></td>
																	<td height="4"></td></tr>
															-->
																<tr>
																	<td height="18">
																		<table cellSpacing="1" cellPadding="0" border="0">
																			<tr class="Arial12br">
																				<td width="250">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Datos 
																						Generales</b></td>
																			</tr>
																		</table>
																	</td>
																	<TD height="18"></TD>
																</tr>
																<tr>
																	<td>
																		<table cellSpacing="2" cellPadding="0" width="100%" border="0">
																			<tr>
																				<td width="26">&nbsp;</td>
																				<td class="Arial12b" width="116">&nbsp;&nbsp;&nbsp;Fecha Desde:</td>
																				<td class="Arial12b" width="170"><input class="clsInputEnable" id="txtFechaDesde" tabIndex="34" maxLength="10" size="10"
																						name="txtFechaDesde" runat="server"> <A onmouseover="window.status='Date Picker';return true;" onmouseout="window.status='';return true;"
																						href="javascript:show_calendar('frmPrincipal.txtFechaDesde');"><IMG src="../images/botones/btn_Calendario.gif" border="0"></A>
																				</td>
																				<td width="30">&nbsp;</td>
																				<td class="Arial12b" width="38">&nbsp;&nbsp;&nbsp;</td>
																				<td class="Arial12b" width="220"></td>
																			</tr>
																		</table>
																	</td>
																	<TD></TD>
																</tr>
																<tr>
																	<td>
																		<table cellSpacing="2" cellPadding="0" width="100%" border="0">
																			<tr>
																				<td width="26">&nbsp;</td>
																				<td class="Arial12b" width="116">&nbsp;&nbsp;&nbsp;Fecha Hasta :</td>
																				<td class="Arial12b" width="170"><input class="clsInputEnable" id="txtFechaHasta" tabIndex="34" maxLength="10" size="10"
																						name="txtFechaHasta" runat="server"> <A onmouseover="window.status='Date Picker';return true;" onmouseout="window.status='';return true;"
																						href="javascript:show_calendar('frmPrincipal.txtFechaHasta');"><IMG src="../images/botones/btn_Calendario.gif" border="0"></A>
																				</td>
																				<td width="30">&nbsp;</td>
																				<td class="Arial12b" width="38">&nbsp;&nbsp;&nbsp;</td>
																				<td class="Arial12b" width="220"></td>
																			</tr>
																		</table>
																	</td>
																	<TD></TD>
																</tr>
															</table>
														</td>
													</tr>
												</table>
												<TABLE id="Table1" borderColor="#336699" cellSpacing="0" cellPadding="4" width="360" align="center"
													border="0">
													<TR>
														<TD>
															<TABLE class="Arial10B" id="Table2" cellSpacing="0" cellPadding="0" width="100%" align="center"
																border="0">
																<TR>
																	<TD align="center">&nbsp;&nbsp;
																		<asp:button id="btnBuscar" runat="server" Width="100px" CssClass="BotonOptm" Text="Buscar"></asp:button></TD>
																</TR>
															</TABLE>
														</TD>
													</TR>
												</TABLE>
											</td>
										</tr>
									</table>
									<br>
									<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="790" align="center"
										border="1">
										<tr>
											<td align="center">
												<table class="Arial10B" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
													<tr>
														<td width="10" height="4" border="0"></td>
														<td class="TituloRConsulta" align="center" width="98%" height="32">Datos</td>
														<td vAlign="top" width="14" height="32"></td>
													</tr>
												</table>
												<table cellSpacing="0" cellPadding="0" width="790" align="center" border="0">
													<tr>
														<td width="98%">
															<table cellSpacing="0" cellPadding="0" width="770" border="0">
																<tr>
																	<td height="4"></td>
																</tr>
																<tr>
																	<td height="18">
																		<div class="frame2" style="Z-INDEX: 1; BORDER-BOTTOM: 1px; POSITION: relative; TEXT-ALIGN: center; BORDER-LEFT: 1px; OVERFLOW-X: scroll; OVERFLOW-Y: scroll; WIDTH: 770px; HEIGHT: 260px; BORDER-TOP: 1px; BORDER-RIGHT: 1px">
																			<table class="tabla_interna_borde1" cellSpacing="1" cellPadding="1" width="100%">
																				<%=auxprint%>
																			</table>
																			<asp:datagrid id="DgArqueoCaja" runat="server" Width="738px" CssClass="Arial11B" CellSpacing="1"
																				BorderColor="White" Height="25px" AutoGenerateColumns="False">
																				<AlternatingItemStyle BackColor="#DDDEE2"></AlternatingItemStyle>
																				<ItemStyle BackColor="#E9EBEE"></ItemStyle>
																				<HeaderStyle BorderWidth="1px" BorderColor="#999999" CssClass="Arial12B"></HeaderStyle>
																				<Columns>
																					<asp:BoundColumn Visible="False" DataField="region" HeaderText="Region">
																						<HeaderStyle BorderWidth="1px" BorderColor="#999999"></HeaderStyle>
																					</asp:BoundColumn>
																					<asp:BoundColumn DataField="codigo" HeaderText="PDV">
																						<HeaderStyle BorderWidth="1px" BorderColor="#999999"></HeaderStyle>
																					</asp:BoundColumn>
																					<asp:BoundColumn DataField="descripcion" HeaderText="CAC">
																						<HeaderStyle BorderWidth="1px" BorderColor="#999999"></HeaderStyle>
																					</asp:BoundColumn>
																					<asp:BoundColumn DataField="diasArqueo" HeaderText="Dias de Arqueo">
																						<HeaderStyle BorderWidth="1px" BorderColor="#999999"></HeaderStyle>
																					</asp:BoundColumn>
																					<asp:BoundColumn DataField="diasLaborales" HeaderText="Dias de Laborables">
																						<HeaderStyle BorderWidth="1px" BorderColor="#999999"></HeaderStyle>
																					</asp:BoundColumn>
																					<asp:BoundColumn DataField="efectividad" HeaderText="% Efectividad">
																						<HeaderStyle BorderWidth="1px" BorderColor="#999999"></HeaderStyle>
																					</asp:BoundColumn>
																					<asp:BoundColumn DataField="pendiente" HeaderText="Pendiente">
																						<HeaderStyle BorderWidth="1px" BorderColor="#999999"></HeaderStyle>
																					</asp:BoundColumn>
																				</Columns>
																			</asp:datagrid></div>
																	</td>
																</tr>
															</table>
														</td>
													</tr>
												</table>
											</td>
										</tr>
									</table>
									<br>
									<table borderColor=#336699 cellSpacing=0 cellPadding=4 
            width="<%=sAncho%>" align=center border=1>
										<tr>
											<td>
												<table class="Arial10B" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
													<tr>
														<td align="center">
															<%
								If Session("STRMessage") = "" then
								%>
															<input class="BotonOptm" style="WIDTH: 100px" onclick="javascript:f_Exportar();" type="button"
																value="Exportar Excel" name="btnExportar">&nbsp;&nbsp;
															<%
								End If
								%>
														</td>
													</tr>
												</table>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<iframe id="ifraExcel" style="DISPLAY: none"></iframe>
			<script language="JavaScript" type="text/javascript">
var esNavegador, esIExplorer;

esNavegador = (navigator.appName == "Netscape") ? true : false;
esIExplorer = ((navigator.appName.indexOf("Microsoft") != -1) || (navigator.appName.indexOf("MSIE") != -1)) ? true : false;

if (esIExplorer) {
}

			</script>
		</form>
		<script language="JavaScript">
	function f_Exportar()
	{
		var strFechaDesde = frmPrincipal.txtFechaDesde.value.substr(6,4)+frmPrincipal.txtFechaDesde.value.substr(3,2)+frmPrincipal.txtFechaDesde.value.substr(0,2);
		var strFechaHasta = frmPrincipal.txtFechaHasta.value.substr(6,4)+frmPrincipal.txtFechaHasta.value.substr(3,2)+frmPrincipal.txtFechaHasta.value.substr(0,2);
		
		document.all.ifraExcel.src="SICAR_Reporte_ArqueoCajaExcel.aspx?pfechaDesde="+strFechaDesde+"&pfechaHasta="+strFechaHasta;

	}

		</script>
	</body>
</HTML>
