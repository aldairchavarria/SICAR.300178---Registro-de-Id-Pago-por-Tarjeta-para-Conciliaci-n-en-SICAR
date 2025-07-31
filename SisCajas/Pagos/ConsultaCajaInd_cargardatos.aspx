<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ConsultaCajaInd_cargardatos.aspx.vb" Inherits="SisCajas.ConsultaCajaInd_cargardatos" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>América Móvil</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href="../Estilos/est_General.css">
		<script type="text/javascript" src="../librerias/Lib_FuncGenerales.js"></script>
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<script language="javascript">
			
		function f_VerCuadre(of, ff, cj , var2){
			if (var2 != '0'){
				Direcc = "ConsultaCajaInd_detalle.aspx?ov="+of+"&fc="+ff+"&cc="+cj;
				window.open(Direcc,"VerCuadre1","directories=no,menubar=no,scrollbars=no,top=50,resizable=no,left=150,width=880,height=600");
			}else{
				alert("El cuadre o pre-cuadre de caja individual aún no se ha realizado.")
			}
		}
		
		function f_Refrescar(){
			frmCCICargarDatos.refreshHandler.click();
		}
		
		function f_Retirar(var1, var2, var3){
			if (var2 == 'N' && var3 == '0'){
				var rpta = window.confirm('¿Desea retirar la caja individual seleccionada?');
				if (rpta) {
					document.frmCCICargarDatos.hidIDCajaDiarioInd.value = var1;
					frmCCICargarDatos.retirarHandler.click();
				}
			}else{
				alert("No es posible retirar asignación. El cuadre o pre-cuadre de caja individual ya se ha realizado.")
			}
		}
		
		function f_Liberar(var1, var2){
			if (var2 != '0'){
				var rpta = window.confirm('¿Desea liberar la caja individual seleccionada?');
				if (rpta) {
					document.frmCCICargarDatos.hidIDCajaDiarioInd.value = var1;
					frmCCICargarDatos.liberarHandler.click();
				}
            }else{
				alert("El cuadre o pre-cuadre de caja individual aún no se ha realizado.")
            }
		}
		
		function f_Asignar(){
			Direcc = "ConsultaCajaInd_asignar.aspx";
			window.open(Direcc,"AsignarCajeroA","directories=no,menubar=no,scrollbars=no,top=150,resizable=no,left=300,width=500,height=280");
		}
		
		//INICIATIVA-318 INI
		function f_Exportar()
		{
			<%Session("oficinas") = hidOficinas.value%>; //INI-936 - JH
			<%Session("ExportarExcel_ConsultaCajaInd") = tblExportar%>; //INI-936 - JH
			document.all.ifraExcel.src="../reportes/SICAR_Reporte_ConsultaCaja_Excel.aspx?fi="+getParameterByName('fi')+"&ff="+getParameterByName('ff')+"&cc="+getParameterByName('cc')+"&cj="+getParameterByName('cj')+"&st="+getParameterByName('st')+"&origen=I";
		}
		
		function getParameterByName(name, url) {
			if (!url) url = window.location.href;
			name = name.replace(/[\[\]]/g, '\\$&');
			var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
				results = regex.exec(url);
			if (!results) return null;
			if (!results[2]) return '';
			return decodeURIComponent(results[2].replace(/\+/g, ' '));
		}
		//INICIATIVA-318 FIN
		</script>
		
		<style type="text/css">
			a:link {text-decoration:none;}
			a:visited {text-decoration:none;}
			a:active {text-decoration:none;}
			
			.grises img{
				filter: url('#grayscale');
				-webkit-filter: grayscale(100%);
				-moz-filter:grayscale(100%);
				-ms-filter:grayscale(100%);
				-o-filter:grayscale(100%);
				filter:grayscale(100%);
				filter: Gray();
				
				-webkit-transition: all 0.5s ease;
				-moz-transition: all 0.5s ease;
				-ms-transition: all 0.5s ease;
				-o-transition: all 0.5s ease;
				transition: all 0.5s ease;
			}
			
			.grises img:hover{
				-webkit-filter: grayscale(0%);
				-moz-filter:grayscale(0%);
				-ms-filter:grayscale(0%);
				-o-filter:grayscale(0%);
				filter: none;
				
				-webkit-transition: all 0.5s ease;
				-moz-transition: all 0.5s ease;
				-ms-transition: all 0.5s ease;
				-o-transition: all 0.5s ease;
				transition: all 0.5s ease;
			}
		</style>
	</head>
	<body MS_POSITIONING="GridLayout">
		<form id="frmCCICargarDatos" method="post" runat="server">
			<input id="hidIDCajaDiarioInd" type="hidden" name="hidIDCajaDiarioInd" value="0">
			<input id="hidOficinas" type="hidden" name="hidOficinas" value="0" runat="server"> <!-- INI-936 - JH -->
			<table border="0" cellSpacing="0" cellPadding="0">
				<TBODY>
					<tr>
						<td vAlign="top" width="10">&nbsp;</td>
						<td vAlign="top" width="790"> 
							<TABLE style="BORDER-BOTTOM: #336699 1px solid; BORDER-LEFT: #336699 1px solid; BORDER-TOP: #336699 1px solid; BORDER-RIGHT: #336699 1px solid"
								id="Table1" border="0" cellSpacing="0" cellPadding="5" width="750" align="center">
								<TR>
									<TD class="TituloRConsulta" align="center">Reporte Caja - Individual</TD>
								</TR>
								<TR>
									<TD>
										<TABLE id="Table5" class="Arial12b" cellPadding="3">
											<tbody>
												<tr>
													<td style="PADDING-BOTTOM: 0px; PADDING-LEFT: 0px; PADDING-RIGHT: 0px; PADDING-TOP: 0px"
														colSpan="7">
														<div style="Z-INDEX: 0; BORDER-BOTTOM: 1px; TEXT-ALIGN: center; BORDER-LEFT: 1px; OVERFLOW-X: scroll; OVERFLOW-Y: scroll; WIDTH: 725px; HEIGHT: 330px; BORDER-TOP: 1px; BORDER-RIGHT: 1px" class="frame2">
															<asp:datagrid style="Z-INDEX: 0" id="DGLista" runat="server" CssClass="Arial12b" AutoGenerateColumns="False">
																<AlternatingItemStyle HorizontalAlign="Center" Height="21px" BackColor="#DDDEE2"></AlternatingItemStyle>
																<ItemStyle HorizontalAlign="Center" Height="21px" BackColor="#E9EBEE"></ItemStyle>
																<HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderColor="#999999" CssClass="Arial12B"></HeaderStyle>
																<Columns>
																	<asp:BoundColumn DataField="ID_T_TI_CAJA_DIARIO" HeaderText="ID" Visible="False"></asp:BoundColumn>
																	<asp:BoundColumn DataField="OFICINA" HeaderText="Oficina">
																		<HeaderStyle Wrap="False" Width="130px" HorizontalAlign="Left"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Left"></ItemStyle>
																	</asp:BoundColumn>
																	<asp:BoundColumn DataField="CAJA" HeaderText="Caja">
																		<HeaderStyle Wrap="False" Width="50px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Center"></ItemStyle>
																	</asp:BoundColumn>
																	<asp:BoundColumn DataField="NOMBRE_CAJA" HeaderText="Nombre de Caja">
																		<HeaderStyle Wrap="False" Width="50px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Center"></ItemStyle>
																	</asp:BoundColumn>
																	<asp:BoundColumn DataField="CAJERO" HeaderText="Cajero">
																		<HeaderStyle Wrap="False" Width="150px" HorizontalAlign="Left"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Left"></ItemStyle>
																	</asp:BoundColumn>
																	<asp:BoundColumn DataField="FECHA" HeaderText="Fecha de Apertura">
																		<HeaderStyle Wrap="False" Width="60px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Center"></ItemStyle>
																	</asp:BoundColumn>
																	<asp:BoundColumn DataField="FECHA_CIERRE" HeaderText="Fecha de Cierre">
																		<HeaderStyle Wrap="False" Width="120px" HorizontalAlign="Left"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Left"></ItemStyle>
																	</asp:BoundColumn>
																	<asp:BoundColumn DataField="DIAS_PENDIENTES" HeaderText="Dias Pendientes">
																		<HeaderStyle Wrap="False" Width="120px" HorizontalAlign="Left"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Left"></ItemStyle>
																	</asp:BoundColumn>
																	<asp:BoundColumn DataField="ESTADO" HeaderText="Estado">
																		<HeaderStyle Wrap="False" Width="50px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Center"></ItemStyle>
																	</asp:BoundColumn>
																	<asp:TemplateColumn HeaderText="Opción">
																		<HeaderStyle Wrap="False" Width="140px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Center"></ItemStyle>
																		<ItemTemplate>
																			<div>
																				<div style="float:left;width: 60px;padding-left:30px;" class="<%# IIf(DataBinder.Eval(Container,"DataItem.CUADRE_REALIZADO") = "N" And DataBinder.Eval(Container,"DataItem.LIBERAR") = "0","", "grises")%>"
																					<a href="javascript:void(0)" onclick="f_Retirar('<%# DataBinder.Eval(Container,"DataItem.ID_T_TI_CAJA_DIARIO")%>','<%# DataBinder.Eval(Container,"DataItem.CUADRE_REALIZADO")%>','<%# DataBinder.Eval(Container,"DataItem.LIBERAR")%>')" >
																					<IMG style="CURSOR: hand;border:0;" alt="Retirar Asignación" src="../images/botones/rechazar.gif">
																					</a>
																				</div>
																				<div style="float:left;width: 30px;padding-left:0px;" class="<%# IIf(DataBinder.Eval(Container,"DataItem.LIBERAR") <> "0","", "grises")%>">
																					<a href="javascript:void(0)" onclick="f_Liberar('<%# DataBinder.Eval(Container,"DataItem.ID_T_TI_CAJA_DIARIO")%>','<%# DataBinder.Eval(Container,"DataItem.LIBERAR")%>')" >
																						<IMG style="CURSOR: hand;border:0;" alt="Liberar Cuadre" src="../images/botones/modificar.png">
																					</a>
																				</div>
																				<div style="float:left;width: 30px;padding-left:5px;" class="<%# IIf(DataBinder.Eval(Container,"DataItem.LIBERAR") <> "0","", "grises")%>">
																					<a href="javascript:void(0)" onclick="f_VerCuadre('<%# DataBinder.Eval(Container,"DataItem.CODOFICINA")%>','<%# DataBinder.Eval(Container,"DataItem.FECHA")%>','<%# DataBinder.Eval(Container,"DataItem.CODCAJERO")%>','<%# DataBinder.Eval(Container,"DataItem.LIBERAR")%>')" >
																						<IMG style="CURSOR: hand;border:0;" alt="Ver Cuadre" src=../images/botones/ico_lupa.gif>
																					</a>
																				</div>
																				<div style="clear:both;"></div>
																			</div>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																</Columns>
															</asp:datagrid>
														</div>
													</td>
												</tr>
											</tbody>
										</TABLE>
									</TD>
								</TR>
								<tr>
									<td>
										<asp:Button id="retirarHandler" runat="server" Text="Button" Style="DISPLAY:none"></asp:Button>
										<asp:Button id="liberarHandler" runat="server" Text="Button" Style="DISPLAY:none"></asp:Button>
										<asp:Button id="refreshHandler" runat="server" Text="Button" Style="DISPLAY:none"></asp:Button>
									</td>
								</tr>
							</TABLE>
							<br>
							<table borderColor="#336699" cellSpacing="0" cellPadding="4" width="360" align="center"
								border="1">
								<tr>
									<td>
										<table class="Arial10B" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
											<tr>
												<td align="center">
													&nbsp;<input style="WIDTH: 100px" id="cmdAsignar" class="BotonOptm" value="Asignar" type="button" name="cmdAsignar" runat="server" onclick="f_Asignar()">&nbsp;&nbsp;
													&nbsp;&nbsp;<asp:button id="btnCancelar" runat="server" CssClass="BotonOptm" Width="100px" Text="Regresar">
													</asp:button>&nbsp;&nbsp;&nbsp;&nbsp;<input class="BotonOptm" style="WIDTH: 100px" onclick="javascript:f_Exportar();" type="button"
																value="Exportar Excel" name="btnExportar"><!--INICIATIVA-318-->
												</td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
							<br>
						</td>
					</tr>
				</TBODY>
			</table>
			<iframe id="ifraExcel" style="DISPLAY: none"></iframe><!--INICIATIVA-318-->
		</form>
	</body>
</html>
