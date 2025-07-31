<%@ Page Language="vb" AutoEventWireup="false" Codebehind="grdDocumentosDAC.aspx.vb" Inherits="SisCajas.grdDocumentosDAC"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>grdDocumentosDAC</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../estilos/est_General.css" type="text/css" rel="stylesheet">
		<script src="../librerias/fncRecaudacion.js"></script>
		<script src="../librerias/date-picker.js"></script>
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<script language="javascript">
		
        window.onload = window_onload;	
		
		function window_onload()
		{
		   if (document.frmPrincipal.txtTrama.value != "")
		   {
		     f_Imprimir();    
		    
		   }
		}
		
		function f_Imprimir()
		{
		   var objIframe = document.getElementById("ifrmRecDac")
		   
		   objIframe.style.visibility = "visible";
		   objIframe.style.width = 0;
		   objIframe.style.height = 0;
		   
		   objIframe.src = "docRecaudacionDAC.aspx?strTrama=" + document.frmPrincipal.txtTrama.value + "&MontoTotalPagado=" + document.frmPrincipal.txtMonto.value + "&Dealer=" + document.frmPrincipal.txtDealer.value;
		
		}
		
		function Imprimir(){
		var objIframe = document.getElementById("ifrmRecDac");
		window.open(objIframe.contentWindow.location);
		
		
	}



		function HasSelectedDoc()
		{	
			var objs = document.getElementsByName("optSel");
			var txtNroTransac="";
			for (i=0;i<objs.length;i++){
				if (objs.item(i).checked){
					txtNroTransac = document.getElementById('rdoDocumentoSAP').value;
					break;
				}
			}
			if (txtNroTransac==""){
				alert("Se debe seleccionar un documento para imprimir.");
				event.returnValue = false;
			}
		}
		
		function f_Selecciona(rdoDocumentoSAP,nroRecaudacion) 
		{
			document.getElementById('rdoDocumentoSAP').value=rdoDocumentoSAP;
			document.getElementById('hddID').value=nroRecaudacion;
		}
		 
		</script>
	</HEAD>
	<body>
		<form id="frmPrincipal" method="post" runat="server">
			<table border="0" cellpadding="0" cellspacing="0">
				<tr>
					<td>
						<TABLE id="Table1" style="BORDER-RIGHT: #336699 2px solid; BORDER-TOP: #336699 2px solid; BORDER-LEFT: #336699 2px solid; BORDER-BOTTOM: #336699 2px solid"
							cellSpacing="0" cellPadding="5" align="center" border="0" width="750">
							<THEAD>
								<TR>
									<TD class="TituloRConsulta" align="center">Pool de Recaudaciones DAC Procesadas</TD>
								</TR>
							</THEAD>
							<TR>
								<TD>
									<TABLE class="Arial12b" id="Table5" cellPadding="3">
										<THEAD>
										</THEAD>
										<tbody>
											<tr>
												<td colspan="7" style="PADDING-RIGHT:0px; PADDING-LEFT:0px; PADDING-BOTTOM:0px; PADDING-TOP:0px">
													<div class="frame2" style="OVERFLOW-Y:scroll;WIDTH:750px;HEIGHT:313px">
														                <INPUT type=hidden value='' name="rdoDocumentoSAP" id="rdoDocumentoSAP"> 
														                <INPUT type=hidden value='' name="hddID" id="hddID">
														<asp:DataGrid id="dgrRecauda" runat="server" AutoGenerateColumns="False" CssClass="Arial11b">
															<AlternatingItemStyle CssClass="RowEven"></AlternatingItemStyle>
															<ItemStyle CssClass="RowOdd"></ItemStyle>
															<HeaderStyle CssClass="Arial12b"></HeaderStyle>
															<Columns>
																<asp:TemplateColumn HeaderText="Opci&#243;n">
																	<HeaderStyle Wrap="False" Width="45px"></HeaderStyle>
																	<ItemTemplate>
																	    <input type="radio" id="optSel" name="optSel"
																	      onclick="f_Selecciona('<%# DataBinder.Eval(Container.DataItem, "nroat")%>','<%# DataBinder.Eval(Container.DataItem, "id_t_nro_recaudacion_dac")%>');">
																	</ItemTemplate>
																</asp:TemplateColumn>
																<asp:BoundColumn DataField="nroat" HeaderText="N&#250;mero Documento">
																	<HeaderStyle Wrap="False" Width="135px"></HeaderStyle>
																</asp:BoundColumn>
																<asp:BoundColumn DataField="COD_CLIENTE" HeaderText="Cod.Cliente">
																	<HeaderStyle Wrap="False" Width="100px"></HeaderStyle>
																</asp:BoundColumn>
																<asp:BoundColumn DataField="CLIENTE" HeaderText="Nombre del Cliente">
																	<HeaderStyle Wrap="False" Width="200px"></HeaderStyle>
																</asp:BoundColumn>
																<asp:BoundColumn DataField="monto" HeaderText="Importe Pago">
																	<HeaderStyle Wrap="False" Width="80px"></HeaderStyle>
																</asp:BoundColumn>
																<asp:BoundColumn DataField="oficina" HeaderText="Oficina">
																	<HeaderStyle Wrap="False" Width="100px"></HeaderStyle>
																</asp:BoundColumn>
																<asp:BoundColumn DataField="estado" HeaderText="Estado">
																	<HeaderStyle Wrap="False" Width="60px"></HeaderStyle>
																</asp:BoundColumn>
																<asp:BoundColumn DataField="usuario_registro" HeaderText="Usuario">
																	<HeaderStyle Wrap="False" Width="100px"></HeaderStyle>
																</asp:BoundColumn>
																<asp:BoundColumn DataField="fecha_registro" HeaderText="Fecha">
																	<HeaderStyle Wrap="False" Width="80px"></HeaderStyle>
																</asp:BoundColumn>
															</Columns>
														</asp:DataGrid>
													</div>
												</td>
											</tr>
										</tbody>
										<DIV></DIV>
									</TABLE>
								</TD>
							</TR>
						</TABLE>
						<br>
						<div id="divComandos">
							<TABLE id="Table2" style="BORDER-RIGHT: #336699 2px solid; BORDER-TOP: #336699 2px solid; BORDER-LEFT: #336699 2px solid; BORDER-BOTTOM: #336699 2px solid"
								cellSpacing="0" cellPadding="2" align="center" border="0">
								<TR>
									<TD align="center">
										<TABLE id="Table4" align="center">
											<TR>
												<TD><INPUT class="BotonOptm" type="submit" value="Imprimir" name="cmdImprimir" onclick="HasSelectedDoc();"
														id="cmdImprimir" style="WIDTH: 93px; HEIGHT: 19px" runat="server"></TD>
												<td><input class="clsInputEnable" type="text" name="txtFecha" id="txtFecha" maxLength="10"
														size="10" runat="server"></td>
												<td><a href="javascript:show_calendar('frmPrincipal.txtFecha');"><img border="0" src="../../images/botones/btn_Calendario.gif"></a></td>
												<td><INPUT class="BotonOptm" type="submit" value="Buscar" name="cmdBuscar" id="cmdBuscar" style="WIDTH: 93px; HEIGHT: 19px"
														runat="server"></td>
											</TR>
										</TABLE>
									</TD>
								</TR>
							</TABLE>
						</div>
					</td>
				</tr>
			</table>
			<br>
			<p style="DISPLAY: none">
				<asp:textbox id="txtTrama" runat="server" Width="104px" CssClass="clsInputEnable" MaxLength="15"></asp:textbox>
				<asp:textbox id="txtMonto" runat="server" Width="104px" CssClass="clsInputEnable" MaxLength="15"></asp:textbox>
				<asp:textbox id="txtDealer" runat="server" Width="104px" CssClass="clsInputEnable" MaxLength="15"></asp:textbox>
				<asp:textbox id="txtPDV" runat="server" Width="104px" CssClass="clsInputEnable" MaxLength="100"></asp:textbox>
				<asp:textbox id="txtCajero" runat="server" Width="104px" CssClass="clsInputEnable" MaxLength="100"></asp:textbox>
				<asp:textbox id="txtFechaRegistro" runat="server" Width="104px" CssClass="clsInputEnable" MaxLength="100"></asp:textbox>
			</p>
			<iframe id="ifrmRecDac" style="VISIBILITY:hidden;WIDTH:0px;HEIGHT:0px" src="#">
			</iframe>
		</form>
	</body>
</HTML>
