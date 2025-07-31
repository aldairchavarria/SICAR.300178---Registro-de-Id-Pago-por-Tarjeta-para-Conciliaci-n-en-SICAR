<%@ Page Language="vb" AutoEventWireup="false" Codebehind="sicar_MantCajeroVirtual.aspx.vb" Inherits="SisCajas.MantCajeroVirtual" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>MantCajeroVirtual</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="styleSheet" type="text/css" href="../../estilos/est_General.css">
		<script language="javascript">
		
			var pagina = "sicar_popup_MantCajeroVirtual.aspx";
			var parametros = "dialogTop:200;status:no;edge:sunken;dialogHide:true;help:no;dialogWidth:462px;dialogHeight:461px";

			function f_Nuevo(){

				window.showModalDialog(pagina + '?accion=Nuevo', window, parametros);
			
			}
			function f_AgregarEditarRecarga(){
				alert("Se deberá validar el monto con el área de SWITCH.")
				document.frmPricipal.submit();
				
			}
			function f_Editar(id){
				var strDatosParametros;
				
				strDatosParametros = '?accion=Editar&id='+ id;
				//strDatosParametros = strDatosParametros +  '&id=' + id ;				
			
				//window.showModelessDialog(pagina + strDatosParametros, window, parametros);
				var ValRetorno = window.showModalDialog(pagina + strDatosParametros, window, parametros);
				if (ValRetorno == "Registro grabado correctamente"){
					alert(ValRetorno);
					f_cargarGrilla();
				}
				
			}
				function f_MantCodigoTarjeta(codTarjeta, flagOperacion)
			{
				var url;
				var parametros;
				parametros = '?codtarjeta=' + codTarjeta + "&flagOperacion=" + flagOperacion;
				url = 'sicar_popup_MantCajeroVirtual.aspx'+ parametros;
		    
			    window.open(url,'','toolbar=no,left=400,top=50,width=462,height=461,directories=no,status=no,scrollbars=yes,resize=no,menubar=no');
												
			}	
			
			function f_cargarGrilla()
			{
				document.getElementById("btnBuscar").click();
			}
			
			function f_respuesta(respuesta)
			{			
				alert(respuesta);
				f_cargarGrilla();
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
			<table border="0" cellSpacing="0" cellPadding="0" width="975">
				<tr>
					<td vAlign="top" width="10">&nbsp;</td>
					<td vAlign="top" width="790">
						<table border="1" cellSpacing="0" borderColor="#336699" cellPadding="0" width="100%" align="center">
							<tr>
								<td align="center">
									<table border="0" cellSpacing="0" cellPadding="0" width="100%" align="center">
										<tr>
											<td height="32" vAlign="top" width="10"></td>
											<td style="PADDING-TOP: 4px" class="TituloRConsulta" height="32" vAlign="top" width="98%"
												align="center">Mantenimiento de Cajeros Virtuales</td>
											<td height="32" vAlign="top" width="10"></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<table border="0" cellSpacing="0" cellPadding="0" width="800">
							<tr>
								<td vAlign="top" width="10">&nbsp;</td>
							</tr>
						</table>
						<table border="1" cellSpacing="0" borderColor="#336699" cellPadding="0" width="100%" align="center">
							<tr>
								<td align="center">
									<table border="0" cellSpacing="0" cellPadding="0" width="100%" align="center">
										<tr>
											<td vAlign="top" width="14"></td>
											<td style="PADDING-BOTTOM: 5px; PADDING-LEFT: 5px; PADDING-RIGHT: 5px; PADDING-TOP: 5px"
												width="98%">
												<table border="0" cellSpacing="0" cellPadding="0" width="770">
													<tr>
														<td height="4"></td>
													</tr>
												</table>
												<table border="0" cellSpacing="0" cellPadding="0" width="770">
													<TR>
														<TD style="WIDTH: 100px; HEIGHT: 28px" class="Arial12b" width="100"></TD>
														<TD style="WIDTH: 266px; HEIGHT: 28px" class="Arial12b" width="266">&nbsp;Oficina 
															de Venta :</TD>
														<TD style="HEIGHT: 28px" class="Arial12b" width="250"><asp:dropdownlist id="ddlOficinaVenta" OnSelectedIndexChanged="ddlOficinaVenta_SelectedIndexChanged"
																CssClass="clsSelectEnable" Width="248px" Runat="server" Height="20px" AutoPostBack="True"></asp:dropdownlist></TD>
														<TD style="WIDTH: 98px; HEIGHT: 28px" class="Arial12b" width="98"></TD>
														<TD style="WIDTH: 84px; HEIGHT: 28px" class="Arial12b" width="84">
															<P></P>
														</TD>
													</TR>
													<TR>
														<TD style="WIDTH: 20px; HEIGHT: 28px" class="Arial12b" width="20"></TD>
														<TD style="WIDTH: 266px; HEIGHT: 28px" class="Arial12b" width="266">&nbsp;Cajero :</TD>
														<TD style="HEIGHT: 28px" class="Arial12b" width="250"><asp:dropdownlist id="ddlCajero" CssClass="clsSelectEnable" Width="248px" Runat="server" Height="20px"></asp:dropdownlist></TD>
														<TD style="WIDTH: 98px; HEIGHT: 28px" class="Arial12b" width="98"></TD>
														<TD style="WIDTH: 84px; HEIGHT: 28px" class="Arial12b" width="84">
															<P></P>
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
														<td><asp:button id="btnBuscar" runat="server" CssClass="BotonOptm" Width="98px" Text="Buscar"></asp:button></td>
														<td><asp:button id="btnLimpiar" runat="server" CssClass="BotonOptm" Width="98px" Text="Limpiar"></asp:button></td>
													</tr>
												</table>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<table border="0" cellSpacing="0" cellPadding="0" width="800">
							<tr>
								<td vAlign="top" width="10">&nbsp;</td>
							</tr>
						</table>
						<table border="1" cellSpacing="0" borderColor="#336699" cellPadding="0" width="100%" align="center">
							<tr>
								<td>
									<table class="Arial12B" border="0" cellSpacing="0" cellPadding="0" width="775" align="left">
										<tr>
											<td style="PADDING-LEFT: 5px">
												<div style="Z-INDEX: 1; BORDER-BOTTOM: 1px; POSITION: relative; TEXT-ALIGN: center; BORDER-LEFT: 1px; OVERFLOW-X: scroll; OVERFLOW-Y: scroll; WIDTH: 775px; HEIGHT: 300px; BORDER-TOP: 1px; BORDER-RIGHT: 1px"
													class="frame2"><asp:datagrid id="gridDetalle" runat="server" Width="750px" AutoGenerateColumns="False" CellSpacing="1"
														CellPadding="1" BorderWidth="1px" BorderColor="White">
														<AlternatingItemStyle HorizontalAlign="Center" BackColor="#E9EBEE"></AlternatingItemStyle>
														<ItemStyle HorizontalAlign="Center" Height="25px" CssClass="Arial11B" BackColor="#DDDEE2"></ItemStyle>
														<HeaderStyle HorizontalAlign="Center" Height="22px" CssClass="Arial12B" BorderWidth="1px" BorderColor="#999999"></HeaderStyle>
														<Columns>
															<asp:BoundColumn DataField="ID" HeaderText="ID" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="20px">
																<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="20px" HorizontalAlign="Center"></HeaderStyle>
															</asp:BoundColumn>
															<asp:BoundColumn DataField="OFICINA" HeaderText="Oficina" HeaderStyle-Width="120px">
																<HeaderStyle BorderWidth="1px" BorderColor="#999999"></HeaderStyle>
															</asp:BoundColumn>
															<asp:BoundColumn DataField="CAJERO" HeaderText="Cajero" HeaderStyle-Width="60px">
																<HeaderStyle BorderWidth="1px" BorderColor="#999999"></HeaderStyle>
															</asp:BoundColumn>
															<asp:BoundColumn DataField="NOMBRE_CAJA" HeaderText="Caja" HeaderStyle-Width="60px">
																<HeaderStyle BorderWidth="1px" BorderColor="#999999"></HeaderStyle>
															</asp:BoundColumn>
															<asp:BoundColumn DataField="NROCAJA" HeaderText="Nº" HeaderStyle-Width="80px">
																<HeaderStyle BorderWidth="1px" BorderColor="#999999"></HeaderStyle>
															</asp:BoundColumn>
															<asp:BoundColumn DataField="FECHAREGISTRO" HeaderText="Fecha Registro" DataFormatString="{0:dd/MM/yyyy}"
																HeaderStyle-Width="80px">
																<HeaderStyle BorderWidth="1px" BorderColor="#999999"></HeaderStyle>
															</asp:BoundColumn>
															<asp:BoundColumn DataField="USUARIOREG" HeaderText="Usuario Registro" HeaderStyle-Width="80px">
																<HeaderStyle BorderWidth="1px" BorderColor="#999999"></HeaderStyle>
															</asp:BoundColumn>
															<asp:BoundColumn DataField="ESTADO" HeaderText="ESTADO" HeaderStyle-Width="80px" Visible="False">
																<HeaderStyle BorderWidth="1px" BorderColor="#999999"></HeaderStyle>
															</asp:BoundColumn>
															<asp:TemplateColumn HeaderText="Estado" ItemStyle-HorizontalAlign="Center">
																<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="50px"></HeaderStyle>
																<ItemTemplate>
																	<img border="0" src="../../images/botones/imgColorVerde.gif" title="Estado">
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="Accion" ItemStyle-HorizontalAlign="Center">
																<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="50px"></HeaderStyle>
																<ItemTemplate>
																	<a href='javascript:f_Editar("<%# DataBinder.Eval(Container.DataItem, "ID")%>")'>
																		Editar </a>
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
						<table border="1" cellSpacing="0" borderColor="#336699" cellPadding="4" align="center">
							<tr>
								<td>
									<table cellSpacing="0" cellPadding="4">
										<tr>
											<td><input style="WIDTH: 100px; HEIGHT: 20px" id="btnNuevo" class="BotonOptm" onclick="f_Nuevo();"
													value="Nuevo" type="button">
											</td>
											<td><asp:button id="btnCancelar" runat="server" CssClass="BotonOptm" Width="98px" Text="Cancelar"></asp:button></td>
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
