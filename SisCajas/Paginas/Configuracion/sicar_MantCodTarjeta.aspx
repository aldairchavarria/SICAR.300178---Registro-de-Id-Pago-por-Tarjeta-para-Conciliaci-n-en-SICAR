<%@ Page Language="vb" AutoEventWireup="false" Codebehind="sicar_MantCodTarjeta.aspx.vb" Inherits="SisCajas.sicar_MantCodTarjeta"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>sicar_MantCodTarjeta</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="styleSheet" type="text/css" href="../../estilos/est_General.css">
		<script language="JavaScript" src="../librerias/date-picker.js"></script>
		<script language="JavaScript" src="../librerias/Lib_FuncValidacion.js"></script>
		<script language="JavaScript" src="../librerias/msrsclient.js"></script>
		<script language="javascript">
		
		    var ventana ;
		    function f_chkCambio()
		    {
				var valor = document.getElementById('chkTodos').checked;
				if (valor == true)
				{ 
				
				  document.getElementById('cboTarjeta').disabled = true;
				  document.getElementById('cboTarjeta').selectedIndex = 0;
				}
				else
				{
				document.getElementById('cboTarjeta').disabled = false;
				}
						
			}
						
			function f_BuscarTarjeta()
			{			
				var url;
				url = 'sicar_popup_BucarTarjeta.aspx';
				//window.showModalDialog(url,'', 'dialogWidth:350px;dialogHeight:400px;Menubar=no;Status=no;Titlebar=no;Toolbar=no;Location=no');
				window.open(url,'', 'dialogWidth:350px;dialogHeight:400px;Menubar=no;Status=no;Titlebar=no;Toolbar=no;Location=no');
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
			
			function f_MantCodigoTarjeta(codTarjeta, flagOperacion) //1 - Nuevo - 2 Actualizar
			{
				var url;
				var parametros;
				parametros = '?codtarjeta=' + codTarjeta + "&flagOperacion=" + flagOperacion;
				url = 'sicar_popup_MantCodigoTarjeta.aspx'+ parametros;
		    
			    window.open(url,'','toolbar=no,left=400,top=50,width=460,height=460,directories=no,status=no,scrollbars=yes,resize=no,menubar=no');
												
			}	
						
					
		</script>
	</HEAD>
	<body leftMargin="0" topMargin="0">
		<form id="Form1" method="post" runat="server">
			<table class="tabla_borde" cellSpacing="0" cellPadding="0" width="790" align="left" style="margin-left:15px;">
				<tr>
					<td class="TituloRConsulta" height="30" colSpan="4" align="center">Mantenimiento de 
						Códigos de Tarjetas
					</td>
				</tr>
		

				<tr>
					<td align="center">
						<table class="Arial12B" border="0" cellSpacing="2" cellPadding="0" width="500" align="center">
							<tr>
								<td>&nbsp;&nbsp;&nbsp;Tarjeta:</td>
								<td style="WIDTH: 326px">&nbsp;
									<asp:dropdownlist id="cboTarjeta" runat="server" Width="318px" CssClass="clsSelectEnable"></asp:dropdownlist></td>
								<td class="Arial12b"><asp:checkbox id="chkTodos" Runat="server" Checked="False" Text="Todos" onClick="f_chkCambio();"></asp:checkbox></td>
							</tr>
						</table>
					</td>
				</tr>
			
			
				<tr>
				    <td colspan="2" align="center">
				      <table class="tabla_borde" cellSpacing="0" cellPadding="0" style="padding:10px; margin:10px;" width="350" align="center">
						<tr>
						     <td align="right"><asp:button id="btnBuscar" runat="server" CssClass="BotonOptm" Width="100" Text="Buscar"></asp:button></td>
					        <td><asp:button id="btnLimpiar" runat="server" CssClass="BotonOptm" Width="100" Text="Limpiar"></asp:button></td></tr>
				      </table>
				    </td>
				
				</tr>
			
			
				<tr>
					<td colspan="2">
						<table id="tbl_CajasPos" class="tabla_borde" cellSpacing="0" cellPadding="0" style="padding:10px;margin:10px; " width="790" align="center">
							<tr>
								<td style="PADDING-LEFT: 5px; WIDTH: 100%">
									<div style="Z-INDEX: 1; BORDER-BOTTOM: 1px; POSITION: relative; TEXT-ALIGN: center; BORDER-LEFT: 1px; OVERFLOW-X: scroll; OVERFLOW-Y: scroll; WIDTH: 775px; HEIGHT: 200px; BORDER-TOP: 1px; BORDER-RIGHT: 1px"
										class="frame2"><asp:datagrid id="gridDetalle" runat="server" Width="750px" BorderWidth="1px" AutoGenerateColumns="False"
											CellSpacing="1" CellPadding="1" BorderColor="White">
											<AlternatingItemStyle HorizontalAlign="Center" BackColor="#E9EBEE" Width="200px"></AlternatingItemStyle>
											<ItemStyle HorizontalAlign="Center" Height="25px" Width="200px" CssClass="Arial11B" BackColor="#DDDEE2"></ItemStyle>
											<HeaderStyle HorizontalAlign="Center" Height="22px" CssClass="Arial12B" BorderWidth="1px" BorderColor="#999999"></HeaderStyle>
											<Columns>
												<asp:BoundColumn DataField="COTAN_ID" HeaderText="ID" HeaderStyle-Width="10%">
													<HeaderStyle BorderWidth="1px" BorderColor="#999999"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="CCINS_DESCRIPTION" HeaderText="Tarjeta" HeaderStyle-Width="10%">
													<HeaderStyle BorderWidth="1px" BorderColor="#999999"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="COTAV_ARD_ID" HeaderText="Codigo" HeaderStyle-Width="10%">
													<HeaderStyle BorderWidth="1px" BorderColor="#999999"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="COTAV_COMENTARIO" HeaderText="Comentario" HeaderStyle-Width="10%">
													<HeaderStyle BorderWidth="1px" BorderColor="#999999"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="ESTADODESC" HeaderText="Estado" HeaderStyle-Width="10%">
													<HeaderStyle BorderWidth="1px" BorderColor="#999999"></HeaderStyle>
												</asp:BoundColumn>
												<asp:TemplateColumn HeaderText="Editar" ItemStyle-HorizontalAlign="Center">
													<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="10%"></HeaderStyle>
													<ItemTemplate>
													<IMG style="Z-INDEX: 0;CURSOR: hand" onclick="f_MantCodigoTarjeta('<%# DataBinder.Eval(Container.DataItem, "COTAN_ID")%>','2')" alt="Buscar" src="../../images/botones/btn_Iconolupa.gif">
														
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
										</asp:datagrid></div>
								</td>
							</tr>
						</table>						
					</td>
				</tr>				
				<tr>
					<td colspan="2" align="center">
						<table class="tabla_borde" cellSpacing="0" cellPadding="0" style="padding:10px; margin:10px;" width="200" align="center">
						<tr>
						
							<td align="center"><input style="WIDTH: 100px" id="cmdNuevo" class="BotonOptm" onclick="javascript:f_MantCodigoTarjeta('0','1');"
									value="Nuevo" type="button" name="cmdNuevo" runat="server">
							</td>
						
					    </tr>
					
					    </table>
			         </td>
				</tr>
			</table>
			
			

		
		</form>
	</body>
</HTML>
