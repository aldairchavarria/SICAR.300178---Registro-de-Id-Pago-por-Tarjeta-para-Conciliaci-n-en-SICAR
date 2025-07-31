<%@ Page Language="vb" AutoEventWireup="false" Codebehind="grdDocumentosFijoPaginas.aspx.vb" Inherits="SisCajas.grdDocumentosFijoPaginas" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Recaudacions Procesadas Fijo y Páginas</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Estilos/est_General.css" type="text/css" rel="stylesheet">
		<script src="../librerias/DATE-PICKER.js"></script>
		<script src="../librerias/Lib_FuncGenerales.js" type="text/javascript"></script>
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<script type="text/javascript" language="javascript">
		
		function obtieneDatosAnular(nro_trans, est_trans)
		{		
			setValue('txtNroTransac', nro_trans);
			setValue('hidEstadoTransActual', est_trans);
		}

		function mostrarVer(nro_trans, est_trans)
		{
			valFec = getValue('hidFechaTran');
			valTC = getValue('hidTipoCambioFechaPago');
			//---
			window.location.href = 'conDocumentos.aspx?act=2&num='+ nro_trans + '&fec=' + valFec + '&tc=' + valTC + '&es=' + est_trans;			
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
		
	function f_Buscar()
	{
		if(ValidaFechaA('document.frmPrincipal.txtFecha', false))
		{			
			document.frmPrincipal.hidTipoTrs.value = "B";
			document.frmPrincipal.submit();
		}else{
			event.returnValue = false;
			
		}
	}
		
		</script>
	</HEAD>
	<body>
		<form id="frmPrincipal" method="post" runat="server">
			<div id="divContenido" style="MARGIN: 8px">
				<div id="divCuerpo" style="WIDTH: 750px">
					<!--seccion datos-->
					<div style="BORDER-RIGHT: #336699 1px solid; PADDING-RIGHT: 8px; BORDER-TOP: #336699 1px solid; PADDING-LEFT: 8px; PADDING-BOTTOM: 8px; BORDER-LEFT: #336699 1px solid; WIDTH: 100%; PADDING-TOP: 8px; BORDER-BOTTOM: #336699 1px solid">
						<!--titulo-->
						<div id="divTituloGeneral" style="WIDTH: 100%; HEIGHT: 28px"><span class="TituloRConsulta" style="WIDTH: 100%; TEXT-ALIGN: center">Pool 
								de Recaudaciones&nbsp;Procesadas&nbsp;Fijo y Páginas</span>
						</div>
						<!--pool-->
						<div id="divPool" style="BORDER-RIGHT: #95b7f3 1px solid; BORDER-TOP: #95b7f3 1px solid; OVERFLOW-Y: scroll; BORDER-LEFT: #95b7f3 1px solid; WIDTH: 100%; BORDER-BOTTOM: #95b7f3 1px solid; HEIGHT: 313px">
							<asp:datagrid id="dgrRecauda" runat="server" AutoGenerateColumns="False" CssClass="Arial11b">
								<AlternatingItemStyle CssClass="RowEven"></AlternatingItemStyle>
								<ItemStyle CssClass="RowOdd"></ItemStyle>
								<HeaderStyle CssClass="Arial12b" HorizontalAlign="Center" Height="24px"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="Opci&#243;n">
										<HeaderStyle Wrap="False" Width="45px"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<INPUT id="rdoDocumentoSAP" type="radio" name="rdoDocumentoSAP" onclick='<%# String.Format("javascript:obtieneDatosAnular(&quot;{0}&quot;,&quot;{1}&quot;);",DataBinder.Eval(Container, "DataItem.NRO_TRANSACCION"),DataBinder.Eval(Container, "DataItem.Estado_Transac") ) %>'>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="N&#250;mero Documento">
										<HeaderStyle Wrap="False" HorizontalAlign="Center" Width="135px"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<a href='<%# String.Format("javascript:mostrarVer(&quot;{0}&quot;, &quot;{1}&quot;);", DataBinder.Eval(Container, "DataItem.NRO_TRANSACCION"), DataBinder.Eval(Container, "DataItem.Estado_Transac") ) %>' >
												<%# DataBinder.Eval(Container,"DataItem.NRO_TRANSACCION") %>
											</a>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="RUC_DEUDOR" HeaderText="Doc. Identidad">
										<HeaderStyle Wrap="False" Width="135px"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="NOM_DEUDOR" HeaderText="Nombre del Cliente">
										<HeaderStyle Wrap="False" Width="200px"></HeaderStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Moneda">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label ID="lblTipoMoneda" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MON_PAGO") %>'>
											</asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="IMPORTE_PAGO" HeaderText="Importe Pago">
										<HeaderStyle Wrap="False" Width="120px"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
								</Columns>
							</asp:datagrid>
						</div>
					</div>
					<!-- seccion botones -->
					<div id="divComandos" style="WIDTH: 100%; TEXT-ALIGN: center">
						<div style="BORDER-RIGHT: #336699 1px solid; BORDER-TOP: #336699 1px solid; MARGIN-TOP: 8px; PADDING-BOTTOM: 8px; BORDER-LEFT: #336699 1px solid; WIDTH: 400px; PADDING-TOP: 8px; BORDER-BOTTOM: #336699 1px solid; TEXT-ALIGN: center">
							<span id="spnBotones" style="TEXT-ALIGN: center"><INPUT class="BotonOptm" id="cmdAnular" style="WIDTH: 93px; HEIGHT: 19px" onclick="HasSelectedDoc();"
									type="submit" value="Anular" name="cmdAnular" runat="server"> <input class="clsInputEnable" id="txtFecha" type="text" maxLength="10" size="10" name="txtFecha"
									runat="server"><A href="javascript:show_calendar('frmPrincipal.txtFecha');"><IMG src="../../images/botones/btn_Calendario.gif" border="0" align="middle"></A>
								<INPUT class="BotonOptm" id="cmdBuscar" style="WIDTH: 93px; HEIGHT: 19px" type="submit"
									value="Buscar" name="cmdBuscar" runat="server" onclick="f_Buscar();"></span>
							</SPAN>
						</div>
					</div>
				</div>
				<!-- seccion pie -->
				<div id="divPie">
					<INPUT id="txtNroTransac" style="WIDTH: 8px" type="hidden" name="txtNroTransac" runat="server"><INPUT id="hidFechaTran" style="WIDTH: 8px" type="hidden" name="hidFechaTran" runat="server">
					<input id="hdnPuntoDeVenta" style="WIDTH: 8px" type="hidden" name="hdnPuntoDeVenta" runat="server">
					<input id="hdnUsuario" style="WIDTH: 8px" type="hidden" name="hdnUsuario" runat="server">
					<input id="hdnBinAdquiriente" style="WIDTH: 8px" type="hidden" name="hdnBinAdquiriente"
						runat="server"> <input id="hdnCodComercio" style="WIDTH: 8px" type="hidden" name="hdnCodComercio" runat="server">
					<input id="intCanal" style="WIDTH: 8px" type="hidden" name="intCanal" runat="server">
					<input id="hdnRutaLog" style="WIDTH: 8px" type="hidden" name="hdnRutaLog" runat="server">
					<input id="hdnDetalleLog" style="WIDTH: 8px" type="hidden" name="hdnDetalleLog" runat="server">
					<input id="hidEstadoTransActual" style="WIDTH: 8px" type="hidden" name="hidEstadoTransActual"
						runat="server"> <input id="hidTipoCambioFechaPago" style="WIDTH: 8px" type="hidden" name="hidTipoCambioFechaPago"
						runat="server"> <input id="hidTipoTrs" style="WIDTH: 8px" type="hidden" name="hidTipoTrs" runat="server" value= "B">
				</div>
			</div>
		</form>
	</body>
</HTML>
