<%@ Page Language="vb" AutoEventWireup="false" Codebehind="sicar_MantRecargaVirtual.aspx.vb" Inherits="SisCajas.sicar_MantRecargaVirtual" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>sicar_MantRecargaVirtual</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../estilos/est_General.css" type="text/css" rel="styleSheet">
		<script language="javascript">
		
			var pagina = "sicar_nuevaRecargaVirtual.aspx";
			var parametros = "dialogTop:200;status:no;edge:sunken;dialogHide:true;help:no;dialogWidth:455px;dialogHeight:250px";

			function f_Nuevo(){

				window.showModelessDialog(pagina + '?accion=Nuevo', window, parametros);
			
			}
			function f_AgregarEditarRecarga(){
				alert("Se deberá validar el monto con el área de SWITCH.")
				document.frmPricipal.submit();
				
			}
			function f_Editar(id, valor, descripcion, estado, fechaReg, usuarioReg, fechaUpd, usuarioUpd){
				var strDatosParametros;
				
				if(estado=='Activo'){
					estado = '0';
				}else{
					estado = '1';
				}
				strDatosParametros = '?accion=Editar';
				strDatosParametros = strDatosParametros +  '&id=' + id ;
				strDatosParametros = strDatosParametros +  '&valor=' + valor ;
				strDatosParametros = strDatosParametros +  '&descripcion=' + descripcion ;
				strDatosParametros = strDatosParametros +  '&estado=' + estado ;
				strDatosParametros = strDatosParametros +  '&fechaReg=' + fechaReg ;
				strDatosParametros = strDatosParametros +  '&usuarioReg=' + usuarioReg ;
				strDatosParametros = strDatosParametros +  '&fechaUpd=' + fechaUpd ;
				strDatosParametros = strDatosParametros +  '&usuarioUpd=' + usuarioUpd ;				
			
				window.showModelessDialog(pagina + strDatosParametros, window, parametros);			
				
			}			
			function ValidaNumero(obj){
				var KeyAscii = window.event.keyCode;

				if (KeyAscii==13) return;	
				if (!(KeyAscii >= 46 && KeyAscii<=57) | (KeyAscii==46 && obj.value.indexOf(".")>=0) ){		
					window.event.keyCode = 0;
				}	
				else
				{	
					if (obj.value.indexOf(".")>=0 ){		
						if (KeyAscii!=46  && obj.value.substring(obj.value.indexOf(".")+1,obj.value.length).length>1)
							window.event.keyCode = 0;	
					}
				}
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="frmPricipal" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="975" border="0">
				<tr>
					<td vAlign="top" width="10">&nbsp;</td>
					<td vAlign="top" width="790">
						<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="100%" align="center"
							border="1">
							<tr>
								<td align="center">
									<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
										<tr>
											<td vAlign="top" width="10" height="32"></td>
											<td class="TituloRConsulta" style="PADDING-TOP: 4px" vAlign="top" align="center" width="98%"
												height="32">Mantenimiento de Recargas Virtuales</td>
											<td vAlign="top" width="10" height="32"></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<table cellSpacing="0" cellPadding="0" width="800" border="0">
							<tr>
								<td vAlign="top" width="10">&nbsp;</td>
							</tr>
						</table>
						<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="100%" align="center"
							border="1">
							<tr>
								<td align="center">
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
														<TD class="Arial12b" style="WIDTH: 20px; HEIGHT: 28px" width="20"></TD>
														<TD class="Arial12b" style="WIDTH: 83px; HEIGHT: 28px" width="83">&nbsp;Monto S/ :</TD>
														<TD class="Arial12b" style="HEIGHT: 28px" width="250"><asp:textbox id="txtMonto" runat="server" CssClass="clsInputEnable" MaxLength="15"></asp:textbox></TD>
														<TD class="Arial12b" style="WIDTH: 98px; HEIGHT: 28px" width="98"></TD>
														<TD class="Arial12b" style="WIDTH: 84px; HEIGHT: 28px" width="84">
															<P></P>
														</TD>
													</TR>
													<TR>
														<TD class="Arial12b" style="WIDTH: 20px; HEIGHT: 28px" width="20"></TD>
														<TD class="Arial12b" style="WIDTH: 83px; HEIGHT: 28px" width="83">&nbsp;Descripción 
															:</TD>
														<TD class="Arial12b" style="HEIGHT: 28px" width="250"><asp:textbox id="txtDescripcion" runat="server" CssClass="clsInputEnable" MaxLength="15" Width="248px"></asp:textbox></TD>
														<TD class="Arial12b" style="WIDTH: 98px; HEIGHT: 28px" width="98">Estado :</TD>
														<TD class="Arial12b" style="WIDTH: 84px; HEIGHT: 28px" width="84">
															<P><asp:dropdownlist id="cboEstado" runat="server" CssClass="clsSelectEnable"></asp:dropdownlist></P>
														</TD>
													</TR>
												</table>
											</td>
										</tr>
									</table>
									<table>
										<tr>
											<td>
												<table cellSpacing="0" cellPadding="4">
													<tr>
														<td>
															<asp:button id="btnBuscar" runat="server" Width="98px" CssClass="BotonOptm" Text="Buscar"></asp:button>
														</td>
														<td>
															<asp:button id="btnLimpiar" runat="server" Width="98px" CssClass="BotonOptm" Text="Limpiar"></asp:button>
														</td>
													</tr>
												</table>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<table cellSpacing="0" cellPadding="0" width="800" border="0">
							<tr>
								<td vAlign="top" width="10">&nbsp;</td>
							</tr>
						</table>
						<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="100%" align="center"
							border="1">
							<tr>
								<td>
									<table class="Arial12B" cellSpacing="0" cellPadding="0" width="775" align="left" border="0">
										<tr>
											<td style="PADDING-LEFT: 5px">
												<div class="frame2" style="BORDER-RIGHT: 1px; BORDER-TOP: 1px; OVERFLOW-Y: scroll; Z-INDEX: 1; OVERFLOW-X: scroll; BORDER-LEFT: 1px; WIDTH: 775px; BORDER-BOTTOM: 1px; POSITION: relative; HEIGHT: 300px; TEXT-ALIGN: center">
													<asp:datagrid id="gridDetalle" runat="server" Width="750px" BorderColor="White" BorderWidth="1px"
														CellPadding="1" CellSpacing="1" AutoGenerateColumns="False">
														<AlternatingItemStyle HorizontalAlign="Center" BackColor="#E9EBEE" Width="200px"></AlternatingItemStyle>
														<ItemStyle HorizontalAlign="Center" Height="25px" Width="200px" CssClass="Arial11B" BackColor="#DDDEE2"></ItemStyle>
														<HeaderStyle HorizontalAlign="Center" Height="22px" CssClass="Arial12B" BorderWidth="1px" BorderColor="#999999"></HeaderStyle>
														<Columns>
															<asp:BoundColumn DataField="PK_REVIT_REVIN_ID" HeaderText="Número" HeaderStyle-Width="40px">
																<HeaderStyle BorderWidth="1px" BorderColor="#999999"></HeaderStyle>
															</asp:BoundColumn>
															<asp:BoundColumn DataField="REVIN_DESCRIP_RECARGA" HeaderText="Descripción"  HeaderStyle-Width="120px">
																<HeaderStyle BorderWidth="1px" BorderColor="#999999"></HeaderStyle>
															</asp:BoundColumn>
															<asp:BoundColumn DataField="REVIN_VALOR_RECARGA" HeaderText="Monto (S/)"  HeaderStyle-Width="60px">
																<HeaderStyle BorderWidth="1px" BorderColor="#999999"></HeaderStyle>
															</asp:BoundColumn>
															<asp:BoundColumn DataField="REVIC_ESTADO_RECARGA" HeaderText="Estado"  HeaderStyle-Width="60px">
																<HeaderStyle BorderWidth="1px" BorderColor="#999999"></HeaderStyle>
															</asp:BoundColumn>
															<asp:BoundColumn DataField="REVIC_USUARIO_REG" HeaderText="Us. Creación"  HeaderStyle-Width="80px">
																<HeaderStyle BorderWidth="1px" BorderColor="#999999"></HeaderStyle>
															</asp:BoundColumn>
															<asp:BoundColumn DataField="REVID_FECHA_REG" HeaderText="Fecha Creación" DataFormatString="{0:dd/MM/yyyy}"  HeaderStyle-Width="80px">
																<HeaderStyle BorderWidth="1px" BorderColor="#999999"></HeaderStyle>
															</asp:BoundColumn>
															<asp:BoundColumn DataField="REVIC_USUARIO_UPD" HeaderText="Ult. Us. Modificación" HeaderStyle-Width="80px">
																<HeaderStyle BorderWidth="1px" BorderColor="#999999"></HeaderStyle>
															</asp:BoundColumn>
															<asp:BoundColumn DataField="REVID_FECHA_UPD" HeaderText="Ult. Fec. Modificación" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="80px">
																<HeaderStyle BorderWidth="1px" BorderColor="#999999"></HeaderStyle>
															</asp:BoundColumn>
															<asp:TemplateColumn HeaderText="Editar" ItemStyle-HorizontalAlign="Center">
																<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="50px"></HeaderStyle>
																<ItemTemplate>
																	<a href='javascript:f_Editar("<%# DataBinder.Eval(Container.DataItem, "PK_REVIT_REVIN_ID")%>","<%# DataBinder.Eval(Container.DataItem, "REVIN_VALOR_RECARGA")%>","<%# DataBinder.Eval(Container.DataItem, "REVIN_DESCRIP_RECARGA")%>","<%# DataBinder.Eval(Container.DataItem, "REVIC_ESTADO_RECARGA")%>","<%# DataBinder.Eval(Container.DataItem, "REVID_FECHA_REG")%>","<%# DataBinder.Eval(Container.DataItem, "REVIC_USUARIO_REG")%>","<%# DataBinder.Eval(Container.DataItem, "REVID_FECHA_UPD")%>","<%# DataBinder.Eval(Container.DataItem, "REVIC_USUARIO_UPD")%>")'>
																		<img border="0" src="../../images/botones/edit.gif"  title="Editar"> </a>
																</ItemTemplate>
															</asp:TemplateColumn>
														</Columns>
													</asp:datagrid></div>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<table>
							<tr>
								<td height="5"></td>
							</tr>
						</table>
						<table borderColor="#336699" cellSpacing="0" cellPadding="4" align="center" border="1">
							<tr>
								<td>
									<table cellSpacing="0" cellPadding="4">
										<tr>
											<td>
												<input class="BotonOptm" id="btnNuevo" style="WIDTH: 100px; HEIGHT: 20px" onclick="f_Nuevo();"
													type="button" value="Nuevo">
											</td>
											<td>
												<asp:button id="btnCancelar" runat="server" Width="98px" CssClass="BotonOptm" Text="Cancelar"></asp:button>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
					<td vAlign="top" width="10">&nbsp;</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
