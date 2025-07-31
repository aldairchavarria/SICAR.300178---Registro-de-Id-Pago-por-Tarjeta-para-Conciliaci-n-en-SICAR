<%@ Page Language="vb" AutoEventWireup="false" Codebehind="grdDocumentos.aspx.vb" Inherits="SisCajas.grdDocumentos"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Documentos</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href="../Estilos/est_General.css">
		<script src="../librerias/fncRecaudacion.js"></script>
		<script src="../librerias/DATE-PICKER.js"></script>
		<script type="text/javascript" src="../librerias/Lib_FuncGenerales.js"></script>
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<script language="javascript">
        //PROY-27440 INI
        
        //E75810
        function obtieneDatosAnular(nro_trans, est_trans,fec_trans,nombre_deu,mon_pago, estado_sicar)
        {       
			setValue('hidFecTrans',"");    
			setValue('hidNombreCliente',""); 
				setValue('hidMonPago',"0.00");   
            setValue('txtNroTransac', nro_trans);
            setValue('hidEstadoTransActual', est_trans);
            setValue('hidFecTrans',fec_trans);
             setValue('hidNombreCliente',nombre_deu);
              setValue('hidMonPago',mon_pago);
             //PROY-27440 FIN
             setValue('hidEstadoSicar', estado_sicar); //INICIATIVA-529
        }

        function HasSelectedDoc()
        {
            if (frmPrincipal.txtNroTransac.value==""){
                alert("Se debe seleccionar un documento para anular.");
                event.returnValue = false;
            }
            else{
                if (!confirm("Desea anular el documento seleccionado...?"))
                    event.returnValue = false;  
            }
            
        }
        
        //INICIATIVA - 529 INI
        function HasSelectedDoc2()
        {
            if (frmPrincipal.txtNroTransac.value==""){
                alert("Se debe seleccionar un documento para editar.");
                event.returnValue = false;
            }
            else{
                if (!confirm("Desea editar el documento seleccionado...?"))
                    event.returnValue = false;  
            }
            
        }
        //INICIATIVA - 529 FIN
 //PROY-27440 INI
function HasSelectedDocFormaPago()
        {
            if (frmPrincipal.txtNroTransac.value==""){
                alert("Se debe seleccionar un documento.");
                event.returnValue = false;
            } 
                       
        }
//PROY-27440 FIN
//PROY-33111 INI
function f_Exportar()
{		

    var FechaConsulta = document.getElementById("txtFecha").value;
    var TipoPago = document.getElementById("dwTipoPaginas").value;
    
	document.all.ifraExcel.src="../reportes/rep_Recaudacion.aspx?TipoPago="+TipoPago+"&Fecha="+FechaConsulta;
					
}
//PROY-33111 INI
        
		</script>
	</HEAD>
	<body>
		<form id="frmPrincipal" method="post" runat="server">
			<table border="0" cellSpacing="0" cellPadding="0">
				<TBODY>
					<tr>
						<td>
							<TABLE style="BORDER-RIGHT: #336699 2px solid; BORDER-TOP: #336699 2px solid; BORDER-LEFT: #336699 2px solid; BORDER-BOTTOM: #336699 2px solid"
								id="Table1" border="0" cellSpacing="0" cellPadding="5" width="750" align="center">
								<TR>
									<TD class="TituloRConsulta" align="center">Pool de Recaudaciones Procesadas</TD>
								</TR>
								<TR>
									<TD>
										<TABLE id="Table5" class="Arial12b" cellPadding="3">
											<tbody>
												<tr>
													<td style="PADDING-RIGHT: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; PADDING-TOP: 0px"
														colSpan="7">
														<div style="OVERFLOW-Y: scroll; WIDTH: 750px; HEIGHT: 313px" class="frame2"><asp:datagrid id="dgrRecauda" runat="server" CssClass="Arial11b" AutoGenerateColumns="False">
																<AlternatingItemStyle CssClass="RowEven"></AlternatingItemStyle>
																<ItemStyle CssClass="RowOdd"></ItemStyle>
																<HeaderStyle CssClass="Arial12b"></HeaderStyle>
																<Columns>
																	<asp:TemplateColumn HeaderText="Opci&#243;n">
																		<HeaderStyle Wrap="False" Width="45px"></HeaderStyle>
																		<ItemTemplate>
																			<INPUT id="rdoDocumentoSAP" type="radio" name="rdoDocumentoSAP" onclick='<%# String.Format("javascript:obtieneDatosAnular(&quot;{0}&quot;,&quot;{1}&quot;,&quot;{2}&quot;,&quot;{3}&quot;,&quot;{4}&quot;,&quot;{5}&quot;);",DataBinder.Eval(Container, "DataItem.NRO_TRANSACCION"),DataBinder.Eval(Container, "DataItem.Estado_Transac"),DataBinder.Eval(Container,"DataItem.FECHA_TRANSAC"),DataBinder.Eval(Container,"DataItem.NOM_DEUDOR"),DataBinder.Eval(Container,"DataItem.IMPORTE_PAGO"),DataBinder.Eval(Container,"DataItem.ESTADO_SICAR"))%>'>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="N&#250;mero Documento">
																		<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="135px"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Center"></ItemStyle>
																		<ItemTemplate>
																			<asp:HyperLink id="Hyperlink1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NRO_TRANSACCION") %>' NavigateUrl='<%# String.Format("conDocumentos.aspx?act=2&num={0}&fec={1}&est={2}&estsicar={3}",DataBinder.Eval(Container,"DataItem.NRO_TRANSACCION"),DataBinder.Eval(Container,"DataItem.FECHA_TRANSAC"),DataBinder.Eval(Container,"DataItem.Estado_Transac"),DataBinder.Eval(Container,"DataItem.ESTADO_SICAR")) %>'>
																			</asp:HyperLink>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:BoundColumn DataField="RUC_DEUDOR" HeaderText="Doc. Identidad">
																		<HeaderStyle Wrap="False" Width="135px"></HeaderStyle>
																	</asp:BoundColumn>
																	<asp:BoundColumn DataField="NOM_DEUDOR" HeaderText="Nombre del Cliente">
																		<HeaderStyle Wrap="False" Width="200px"></HeaderStyle>
																	</asp:BoundColumn>
																	<asp:BoundColumn DataField="MON_PAGO" HeaderText="Moneda">
																		<HeaderStyle Wrap="False" Width="80px"></HeaderStyle>
																	</asp:BoundColumn>
																	<asp:BoundColumn DataField="IMPORTE_PAGO" HeaderText="Importe Pago">
																		<HeaderStyle Wrap="False" Width="120px"></HeaderStyle>
																	</asp:BoundColumn>
																	<asp:BoundColumn DataField="TIPOPAGO" HeaderText="Tipo">
																		<HeaderStyle Wrap="False" Width="120px"></HeaderStyle>
																	</asp:BoundColumn>
																	<asp:TemplateColumn HeaderText="Estado SAP" Visible="False">
																		<HeaderStyle Width="120px" Wrap="False" />
																		<ItemTemplate>
																			<asp:Label CssClass='<%# IIf(DataBinder.Eval(Container,"DataItem.Estado_Transac").ToLower() ="procesado", "estadoItemOK","estadoItemError") %>' Text='<%# DataBinder.Eval(Container,"DataItem.Estado_Transac") %>' runat="server" ID="lblEstado">
																			</asp:Label>
																		</ItemTemplate>
																		<ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
																			Font-Underline="False" HorizontalAlign="Center" />
																	</asp:TemplateColumn>
																	<asp:BoundColumn DataField="ESTADO_SICAR" HeaderText="Estado">
																		<HeaderStyle Wrap="False" Width="100"></HeaderStyle>
																	</asp:BoundColumn>
																</Columns>
															</asp:datagrid></div>
													</td>
												</tr>
											</tbody>
										</TABLE>
									</TD>
								</TR>
							</TABLE>
							<br>
							<div id="divComandos">
								<TABLE style="BORDER-RIGHT: #336699 2px solid; BORDER-TOP: #336699 2px solid; BORDER-LEFT: #336699 2px solid; BORDER-BOTTOM: #336699 2px solid"
									id="Table2" border="0" cellSpacing="0" cellPadding="2" align="center">
									<TR>
										<TD align="center">
											<TABLE id="Table4" align="center">
												<TR>
													<TD style="WIDTH: 96px"><asp:dropdownlist id="dwTipoPaginas" runat="server">
															<asp:ListItem Value="00">-- Todas --</asp:ListItem>
															<asp:ListItem Value="01">Movil</asp:ListItem>
															<asp:ListItem Value="02">Fija y Paginas</asp:ListItem>
														</asp:dropdownlist></TD>
													<td><input id="txtFecha" class="clsInputEnable" maxLength="10" size="10" name="txtFecha" runat="server"></td>
													<td><A href="javascript:show_calendar('frmPrincipal.txtFecha');"><IMG border="0" src="../../images/botones/btn_Calendario.gif"></A></td>
													<td><INPUT style="WIDTH: 93px; HEIGHT: 19px" id="cmdBuscar" class="BotonOptm" value="Buscar"
															type="submit" name="cmdBuscar" runat="server"></td>
													<td><INPUT style="WIDTH: 93px; HEIGHT: 19px" id="cmdEditar" class="BotonOptm" value="Editar"
															type="submit" name="cmdEditar" runat="server" onclick="HasSelectedDoc2();"></td> <!--INICIATIVA - 529-->
													<TD style="WIDTH: 96px"><INPUT style="WIDTH: 93px; HEIGHT: 19px" id="cmdAnular" class="BotonOptm" onclick="HasSelectedDoc();"
															value="Anular" type="submit" name="cmdAnular" runat="server"></TD>
													<td style="WIDTH: 96px"><input style="WIDTH: 93px; HEIGHT: 19px" id="cmdFormaPago" class="BotonOptm" onclick="HasSelectedDocFormaPago();"
															value="Forma Pago" type="submit" name="cmdFormaPago" runat="server"></td>
													<td style="TEXT-ALIGN: center"><INPUT style="WIDTH: 100px" id="btnExportar" class="BotonOptm" onclick="f_Exportar()" value="Exportar"
														type="button">
												</td>
												</TR>
												<tr>
													<td><iframe style="DISPLAY: none" id="ifraExcel"></iframe>
												</td>
											</tr>
											</TABLE>
										</TD>
									</TR>
								</TABLE>
							</div>
							<div><INPUT style="WIDTH: 8px" id="txtNroTransac" type="hidden" name="txtNroTransac" runat="server"><INPUT style="WIDTH: 8px" id="txtFechaTran" type="hidden" name="txtFechaTran" runat="server">
								<input style="WIDTH: 8px" id="hdnPuntoDeVenta" type="hidden" name="hdnPuntoDeVenta" runat="server">
								<input style="WIDTH: 8px" id="hdnUsuario" type="hidden" name="hdnUsuario" runat="server">
								<input style="WIDTH: 8px" id="hdnBinAdquiriente" type="hidden" name="hdnBinAdquiriente"
									runat="server"> <input style="WIDTH: 8px" id="hdnCodComercio" type="hidden" name="hdnCodComercio" runat="server">
								<input style="WIDTH: 8px" id="intCanal" type="hidden" name="intCanal" runat="server">
								<input style="WIDTH: 8px" id="hdnRutaLog" type="hidden" name="hdnRutaLog" runat="server">
								<input style="WIDTH: 8px" id="hdnDetalleLog" type="hidden" name="hdnDetalleLog" runat="server">
								<input style="WIDTH: 8px" id="hidEstadoTransActual" type="hidden" name="hidEstadoTransActual"
									runat="server"> <input style="WIDTH: 8px" id="hidFecTrans" type="hidden" name="hidFecTrans" runat="server">
								<input style="WIDTH: 8px" id="hidNombreCliente" type="hidden" name="hidNombreCliente" runat="server">
								<input style="WIDTH: 8px" id="hidMonPago" type="hidden" name="hidMonPago" runat="server">
								<input style="WIDTH: 8px" id="hidEstadoSicar" type="hidden" name="hidEstadoSicar" runat="server">
							</div>
		</form>
		</TD></TR></TBODY></TABLE>
	</body>
</HTML>
