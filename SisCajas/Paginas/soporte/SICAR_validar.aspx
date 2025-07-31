<%@ Page Language="vb" AutoEventWireup="false" Codebehind="SICAR_validar.aspx.vb" Inherits="SisCajas.SICAR_validar"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>SICAR_validar</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="styleSheet" type="text/css" href="../../estilos/est_General.css">
		<script language="JavaScript" src="../../librerias/date-picker.js"></script>
		<script language="JavaScript" src="../../librerias/Lib_FuncValidacion.js"></script>
		<script language="JavaScript" src="../../librerias/msrsclient.js"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<DIV id="div_contenido" align="center">
			<FORM id="Form1" method="post" runat="server">
				<input id="hidAccion" value="0" type="hidden" name="hidAccion"> <INPUT style="WIDTH: 10px; HEIGHT: 18px" id="hidPerfilesAValidar" size="1" type="hidden"
					name="hidPerfilesAValidar" runat="server"> <INPUT style="WIDTH: 10px; HEIGHT: 18px" id="hidOpcion" size="1" type="hidden" name="hidOpcion"
					runat="server"> <INPUT style="WIDTH: 10px; HEIGHT: 18px" id="hidnValueAccion" size="1" type="hidden" name="hidnValueAccion"
					runat="server"> <INPUT style="WIDTH: 10px; HEIGHT: 18px" id="hidVeces" value="0" size="1" type="hidden"
					name="hidVeces" runat="server"> <INPUT style="WIDTH: 10px; HEIGHT: 18px" id="HidVerificar" size="1" type="hidden" name="HidVerificar"
					runat="server">
				<TABLE style="WIDTH: 230px; HEIGHT: 130px" id="tblContenedor" class="Arial11BV" border="1"
					cellSpacing="0" cellPadding="0" align="center">
					<TR borderColor="#336699">
						<TD>
							<TABLE class="Arial11BV" id="Table1" borderColor="#336699" cellSpacing="0" cellPadding="0"
								width="100%" border="0">
								<TBODY>
									<TR>
										<TD class="Arial12b" bgColor="#5991bb" vAlign="top" colSpan="3" align="center"><ASP:LABEL id="lblTitulo" runat="server" forecolor="White" font-bold="True" font-size="12pt">AUTORIZACIÓN</ASP:LABEL></TD>
									</TR>
									<TR>
										<TD class="Arial12b" bgColor="#ffffff" vAlign="top" colSpan="3" align="center"><ASP:LABEL id="lblMensaje" runat="server" forecolor="Black" font-bold="True" font-size="10pt"></ASP:LABEL></TD>
									</TR>
									<TR>
										<TD style="HEIGHT: 10px" class="Arial12b" colSpan="3"></TD>
									</TR>
									<TR>
										<TD style="WIDTH: 100px; HEIGHT: 20px" class="Arial12b" align="right">Usuario</TD>
										<TD style="PADDING-LEFT: 3px; WIDTH: 10px" class="Arial12b" align="left">:</TD>
										<TD class="Arial12b"><asp:textbox id="txtUsuario" runat="server" CssClass="clsInputEnable"></asp:textbox></TD>
									</TR>
									<TR>
										<TD style="WIDTH: 100px; HEIGHT: 20px" class="Arial12b" align="right">Contraseña</TD>
										<TD style="PADDING-LEFT: 3px; WIDTH: 10px" class="Arial12b" align="left">:</TD>
										<TD class="Arial12b">
											<DIV class="form-group has-feedback"><asp:textbox id="txtPass" runat="server" CssClass="clsInputEnable" TextMode="Password"></asp:textbox></DIV>
										</TD>
									</TR>
									<TR>
										<TD style="HEIGHT: 10px" class="Arial12b" colSpan="3"></TD>
									</TR>
									<TR>
										<TD colSpan="3">
											<TABLE id="Table2" border="0" cellSpacing="0" cellPadding="0" width="100%">
												<TR>
													<TD align="right"><INPUT style="WIDTH: 100px; HEIGHT: 24px; CURSOR: hand" id="btnValidar" class="BotonOptm"
															onmouseover="this.className='BotonResaltado';" onmouseout="this.className='Boton';" onclick="f_Validar()"
															value="Aceptar" type="button" name="btnValidar" runat="server"></TD>
													<TD style="WIDTH: 10px"></TD>
													<TD align="left"><INPUT style="WIDTH: 100px; HEIGHT: 23px; CURSOR: hand" id="btnCancelar" class="BotonOptm"
															onmouseover="this.className='BotonResaltado';" onmouseout="this.className='Boton';" onclick="f_Cancelar()"
															value="Cancelar" type="button" name="btnCancelar" runat="server"></TD>
												</TR>
											</TABLE>
										</TD>
									</TR>
								</TBODY>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
			</FORM>
		</DIV>
		<script language="javascript">
			function FC_Enter(event)
			{
				var nav4 = window.Event ? true : false;
				var key = nav4 ? event.which : event.keyCode; 
				if(key == 13) f_Validar();
			}
			
			function f_Validar()
			{
				var temp = parseFloat(document.getElementById('hidVeces').value);
				
				document.getElementById('hidnValueAccion').value='V';
				document.getElementById('HidVerificar').value='1';                                                    
              	document.getElementById('btnValidar').disabled = true;
				document.getElementById('btnCancelar').disabled = true;
				
				temp = temp + 1;
				document.getElementById('hidVeces').value = temp;
				
				if (temp >= 4)
				{
					var mensaje = 'A terminado en número de intentos, consulte con el administrador del sistema.'
					alert(mensaje);
					document.getElementById("txtUsuario").value = "";
					document.getElementById("txtPass").value = "";
					document.getElementById("txtUsuario").disabled = true;
					document.getElementById("txtPass").disabled = true;
					document.getElementById('btnValidar').disabled = true;
					document.getElementById('btnCancelar').disabled = true;
					
					window.close();
				}else
				{
					document.getElementById('hidAccion').value = <%=K_ACEPTAR%>;
					document.Form1.submit();
				}
			}
			
			function f_Cancelar()
			{
				window.close();
			}
					
		</script>
	</body>
</HTML>
