<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ConsultaCajaGen_cargardatos.aspx.vb" Inherits="SisCajas.ConsultaCajaGen_cargardatos" %>
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
			
		function f_VerCuadre(of, ff, var2){
			if (var2 != '0'){
				Direcc = "ConsultaCajaGen_detalle.aspx?ov="+of+"&fc="+ff;
				window.open(Direcc,"VerCuadreG","directories=no,menubar=no,scrollbars=no,top=50,resizable=no,left=150,width=880,height=600");
			}else{
				alert("El cuadre o pre-cuadre de caja general aún no se ha realizado.")
			}
		}
		
		function f_Refrescar(){
			frmCCGCargarDatos.refreshHandler.click();
		}
		
		function f_Liberar(var1, var2){
			if (var2 != '0'){
				var rpta = window.confirm('¿Desea liberar la caja general seleccionada?');
				if (rpta) {
					document.frmCCGCargarDatos.hidIDCajaGeneral.value = var1;
					frmCCGCargarDatos.liberarHandler.click();
				}
            }else{
				alert("El cuadre o pre-cuadre de caja general aún no se ha realizado.")
            }
		}
		
		//INICIATIVA-318 INI
		function f_Exportar()
		{
			<%Session("oficinas") = hidOficinas.value%>; //INI-936 - JH
			<%Session("ExportarConsultaCajaGen") = tblExportar%>; //INI-936 - JH
			document.all.ifraExcel.src="../reportes/SICAR_Reporte_ConsultaCaja_Excel.aspx?fi="+getParameterByName('fi')+"&ff="+getParameterByName('ff')+"&cc="+getParameterByName('cc')+"&cj="+getParameterByName('cj')+"&st="+getParameterByName('st')+"&origen=G"; //INI-936 - JH - Eliminado parametro ov de la url
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
		<form id="frmCCGCargarDatos" method="post" runat="server">
			<input id="hidIDCajaGeneral" type="hidden" name="hidIDCajaGeneral" value="0">
                        <input id="hidOficinas" type="hidden" name="hidOficinas" value="0" runat="server"> <!-- INI-936 - JH -->
			<table border="0" cellSpacing="0" cellPadding="0" width="975" >
				<TBODY>
					<tr>
						<td vAlign="top" width="10">&nbsp;</td>
						<td vAlign="top" width="790"> 
							<TABLE style="BORDER-BOTTOM: #336699 1px solid; BORDER-LEFT: #336699 1px solid; BORDER-TOP: #336699 1px solid; BORDER-RIGHT: #336699 1px solid"
								id="Table1" border="0" cellSpacing="0" cellPadding="5" width="750" align="center">
								<TR>
									<TD class="TituloRConsulta" align="center">Reporte Caja - General</TD>
								</TR>
								<TR>
									<TD>
										<TABLE id="Table5" class="Arial12b" cellPadding="3">
											<tbody>
												<tr>
													<td style="PADDING-BOTTOM: 0px; PADDING-LEFT: 0px; PADDING-RIGHT: 0px; PADDING-TOP: 0px"
														colSpan="7">
														<div style="Z-INDEX: 0; BORDER-BOTTOM: 1px; TEXT-ALIGN: center; BORDER-LEFT: 1px; OVERFLOW-X: scroll; OVERFLOW-Y: scroll; WIDTH: 750px; HEIGHT: 330px; BORDER-TOP: 1px; BORDER-RIGHT: 1px" class="frame2">
															<asp:datagrid style="Z-INDEX: 0" id="DGLista" runat="server" CssClass="Arial12b" AutoGenerateColumns="False">
																<AlternatingItemStyle HorizontalAlign="Center" Height="21px" BackColor="#DDDEE2"></AlternatingItemStyle>
																<ItemStyle HorizontalAlign="Center" Height="21px" BackColor="#E9EBEE"></ItemStyle>
																<HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderColor="#999999" CssClass="Arial12B"></HeaderStyle>
																<Columns>
																	<asp:BoundColumn DataField="ID_TRS_CUADRE_GEN" HeaderText="ID" Visible="False"></asp:BoundColumn>
																	<asp:BoundColumn DataField="OFICINA" HeaderText="Oficina">
																		<HeaderStyle Wrap="False" Width="130px" HorizontalAlign="Left"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Left"></ItemStyle>
																	</asp:BoundColumn>
																	<asp:BoundColumn DataField="CAJERO" HeaderText="Cajero">
																		<HeaderStyle Wrap="False" Width="150px" HorizontalAlign="Left"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Left"></ItemStyle>
																	</asp:BoundColumn>
																	<asp:BoundColumn DataField="NOMBRE_CAJA" HeaderText="Nombre Caja">
																		<HeaderStyle Wrap="False" Width="150px" HorizontalAlign="Left"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Left"></ItemStyle>
																	</asp:BoundColumn>
																	<asp:BoundColumn DataField="FECHA" HeaderText="Fecha de Apertura">
																		<HeaderStyle Wrap="False" Width="80px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Center"></ItemStyle>
																	</asp:BoundColumn>
																	<asp:BoundColumn DataField="FECHA_CIERRE" HeaderText="Fecha de Cierre" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}">
																		<HeaderStyle Wrap="False" Width="160px" HorizontalAlign="Left"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Left"></ItemStyle>
																	</asp:BoundColumn>
																	<asp:BoundColumn DataField="DIAS_PENDIENTES" HeaderText="Días Pendientes" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}">
																		<HeaderStyle Wrap="False" Width="160px" HorizontalAlign="Left"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Left"></ItemStyle>
																	</asp:BoundColumn>
																	<asp:BoundColumn DataField="ESTADO" HeaderText="Estado">
																		<HeaderStyle Wrap="False" Width="50px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Center"></ItemStyle>
																	</asp:BoundColumn>
																	<asp:TemplateColumn HeaderText="Opción">
																		<HeaderStyle Wrap="False" Width="150px" HorizontalAlign="Center"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Center"></ItemStyle>
																		<ItemTemplate>
																				<div>
																					<div style="float:left;width: 70px;padding-left:30px;" class="<%# IIf(DataBinder.Eval(Container,"DataItem.LIBERAR") <> "0","", "grises")%>">
																						<a href="javascript:void(0)" onclick="f_Liberar('<%# DataBinder.Eval(Container,"DataItem.ID_TRS_CUADRE_GEN")%>','<%# DataBinder.Eval(Container,"DataItem.LIBERAR")%>')" >
																							<IMG style="CURSOR: hand;border:0;" alt="Liberar Cuadre" src="../images/botones/modificar.png">
																						</a> 
																					</div>
																					<div style="float:left;width: 50px;padding-left:0px;" class="<%# IIf(DataBinder.Eval(Container,"DataItem.LIBERAR") <> "0","", "grises")%>">
																						<a href="javascript:void(0)" onclick="f_VerCuadre('<%# DataBinder.Eval(Container,"DataItem.CODOFICINA")%>','<%# DataBinder.Eval(Container,"DataItem.FECHA")%>','<%# DataBinder.Eval(Container,"DataItem.LIBERAR")%>')" >
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
												<td align="center">&nbsp;<asp:button id="btnCancelar" runat="server" CssClass="BotonOptm" Width="100px" Text="Regresar"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;<input class="BotonOptm" style="WIDTH: 100px" onclick="javascript:f_Exportar();" type="button"
																value="Exportar Excel" name="btnExportar"><!--INICIATIVA-318--></td>
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
