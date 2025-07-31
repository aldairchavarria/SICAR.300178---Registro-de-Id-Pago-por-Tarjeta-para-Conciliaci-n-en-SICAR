<%@ Page Language="vb" AutoEventWireup="false" Codebehind="detPagoCuotas.aspx.vb" Inherits="SisCajas.detPagoCuotas"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>detPagoCuotas</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../estilos/est_General.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="../librerias/date-picker.js"></script>
		<script language="JavaScript" src="../librerias/Lib_FuncValidacion.js"></script>
		<script src="../librerias/Lib_Redondeo.js" type="text/javascript"></script>
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<script language="javascript">
		//Regularizacion 20.12.2012

		function f_Grabar(){
		    var blnErr = false;
			if (document.getElementById("txtMonto1") != null)
			{
			if (f_Validar()) {
				CalculoVuelto();
				if (frmPrincipal.txtVuelto.value*1<0){
					alert('El importe recibido no puede ser menor al importe pagado..');
					event.returnValue = false;	
					blnErr = true;		
				}
				//document.frmPrincipal.hidNumFilas.value = f_NumFilas();			
				if (! f_ValidarTarjeta())
				{
					event.returnValue = false;			
					blnErr = true;
				}	
				if 	(!blnErr)
			    {
			      document.getElementById("Botones").style.display ="none"
			      document.getElementById("divTitulo").style.display ="block"
			    }
				
			}
			else
				event.returnValue = false;
			}	
		}	
		
		function f_ValidarTarjeta() {					
			for(i=0; i<document.frmPrincipal.elements.length; i++){			
				if(document.frmPrincipal.elements[i].name.substring(0,15)=="cboTipDocumento"){						
					if (document.frmPrincipal.elements[i].value!=""){
						if (frmPrincipal.txtTarjCred.value.indexOf(document.frmPrincipal.elements[i].value)>=0) {
							if(document.frmPrincipal.elements[i+2].value!=""){ 
								if (frmPrincipal.txtBIN.value.indexOf(document.frmPrincipal.elements[i+1].value.substr(0,4))< 0 ){
									return confirm('El prefijo de la tarjeta no se encuentra registrado. Desea Continuar ?');
								}								
							}	
							else{							
								alert('Debe ingresar el número de la tarjeta...');
								return false
							}
						}
					}
				}
			}				
			return true;
		}
		function f_Validar() {					
			for(i=0; i<document.frmPrincipal.elements.length; i++){
				if(document.frmPrincipal.elements[i].name.substring(0,8)=="txtMonto"){
					if(document.frmPrincipal.elements[i].value!=""){
						if (!ValidaDecimal("document.frmPrincipal." + document.frmPrincipal.elements[i].name,'El campo Monto a Pagar debe tener el formato 0.00',false)) return false;
					}
				}
				valor=""
				if(document.frmPrincipal.elements[i].name.substring(0,15)=="cboTipDocumento"){
					if(document.frmPrincipal.elements[i+3].value!=""){
						if (document.frmPrincipal.elements[i].value==""){
						alert('Debe de seleccionar una forma de pago');
						return false;
						}
					}
					if(document.frmPrincipal.elements[i].value!=""){
						if(document.frmPrincipal.elements[i+3].value==""){
							alert('Debe ingresar un monto de pago');
							return false;
						}
					}
				}
			}				
			return true;
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
		
		function f_Recalcular(obj){
			CalculoVuelto();
		}
		
		function CalculoVuelto(){
			suma = 0.0;
					
			for(i=1; i<4; i++){
				eval("ss=document.frmPrincipal.txtMonto"+i+".value");
				if(ss!='')
				{	
				  eval("vv=document.frmPrincipal.cboTipDocumento"+i+".value")
			      if (vv == 'ZEFE')
			        suma = suma + (ss*1);	
			   	}
			}			
			suma= Math.round(suma*100)/100;			
			if (frmPrincipal.txtRecibidoPen.value=="")	
				frmPrincipal.txtRecibidoPen.value = "0.00"
			if (frmPrincipal.txtRecibidoUsd.value=="") 
				frmPrincipal.txtRecibidoUsd.value = "0.00"
			var tc = document.all.lblTC.innerText*1;
		
			//var vuelto = Math.round((frmPrincipal.txtRecibidoPen.value * 1 + frmPrincipal.txtRecibidoUsd.value * tc - suma)*100)/100;//aotane 05.08.2013							
			var vuelto = Math.round((frmPrincipal.txtRecibidoPen.value * 1 + frmPrincipal.txtRecibidoUsd.value * tc - suma)*1000)/1000;//aotane 05.08.2013	
						
			//frmPrincipal.txtVuelto.value = vuelto;			
			var vueltoRedondeo = Math.round(RedondeaInmediatoSuperior(vuelto)*100)/100;//aotane 05.08.2013		
			frmPrincipal.txtVuelto.value = vueltoRedondeo;//aotane 05.08.2013	
		}
		
		//INICIATIVA-318 INI
		function f_validaCajaCerrada() {
			
			var serverURL =  '../Pos/ProcesoPOS.aspx';
			RSExecute(serverURL,"ValidarCajaCerrada",CallBack_ValidarCajaCerrada,"X");  
		}

		function CallBack_ValidarCajaCerrada(response){

			var varRpta = response.return_value;
			var res = (varRpta.replace("<BODY><SELECT>", "")).replace("</SELECT>", "").replace("\r\n","");
			
			var varArrayRpta = res.split("|");
			var varCodRpta = varArrayRpta[0];
			var varMsjRta = varArrayRpta[1];
			
			if(varCodRpta=="1"){
				alert(varMsjRta);
			}
		}
		//INICIATIVA-318 FIN
		
		</script>
	</HEAD>
	<body onload="Javascript:RedondearInicio();">
		<form id="frmPrincipal" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="750" border="0">
				<tr>
					<td>
						<table style="BORDER-RIGHT: #336699 2px solid; BORDER-TOP: #336699 2px solid; BORDER-LEFT: #336699 2px solid; BORDER-BOTTOM: #336699 2px solid"
							cellSpacing="0" cellPadding="5" align="left" border="0">
							<thead>
								<tr>
									<td class="TituloRConsulta" align="center">Pago de Cuotas&nbsp;- Resultados 
										Búsqueda</td>
								</tr>
							</thead>
							<tbody>
								<tr>
									<td>
										<table class="Arial12b" style="WIDTH: 720px; HEIGHT: 52px" cellPadding="3">
											<thead>
												<tr>
													<td class="Arial12br" colSpan="3">Datos Cliente</td>
													<td class="Arial12br" align="right">Tipo de Cambio:
														<asp:label id="lblTC" runat="server" Font-Bold="True"></asp:label></td>
												</tr>
											</thead>
											<tbody>
												<tr>
													<td>Teléfono:</td>
													<td><input class="clsInputDisable" id="txtIdentificadorCliente" readOnly type="text" maxLength="30"
															size="16" name="txtIdentificadorCliente" runat="server"></td>
													<td>Nombre Cliente:</td>
													<td><input class="clsInputDisable" id="txtNombreCliente" readOnly type="text" size="60" name="txtNombreCliente"
															runat="server"></td>
												</tr>
												<tr>
													<TD id="Td2" vAlign="top" runat="server">Cuotas:</TD>
													<TD colSpan="2"></TD>
													<td></td>
												</tr>
												<TR>
													<TD colSpan="3">
														<div style="OVERFLOW-Y: scroll; OVERFLOW-X: scroll; WIDTH: 350px; HEIGHT: 120px"><asp:datagrid id="dgCuotas" runat="server" AutoGenerateColumns="False" CssClass="Arial11b">
																<AlternatingItemStyle CssClass="RowEven"></AlternatingItemStyle>
																<ItemStyle CssClass="RowOdd"></ItemStyle>
																<HeaderStyle CssClass="Arial12b"></HeaderStyle>
																<Columns>
																	<asp:BoundColumn DataField="BKTXT" HeaderText="Cuota">
																		<HeaderStyle Wrap="False" Width="50px"></HeaderStyle>
																	</asp:BoundColumn>
																	<asp:BoundColumn DataField="FECHA" HeaderText="Fecha">
																		<HeaderStyle Wrap="False" Width="60px"></HeaderStyle>
																	</asp:BoundColumn>
																	<asp:BoundColumn DataField="MONTO" HeaderText="Monto">
																		<HeaderStyle Wrap="False" Width="45px"></HeaderStyle>
																	</asp:BoundColumn>
																	<asp:BoundColumn DataField="XBLNR" HeaderText="Documento">
																		<HeaderStyle Wrap="False" Width="100px"></HeaderStyle>
																	</asp:BoundColumn>
																	<asp:BoundColumn DataField="FECHA_PAGO" HeaderText="Fecha de Pago">
																		<HeaderStyle Wrap="False" Width="60px"></HeaderStyle>
																	</asp:BoundColumn>
																</Columns>
															</asp:datagrid></div>
													</TD>
													<td></td>
												</TR>
											</tbody>
										</table>
										<table class="Arial12b" cellPadding="3">
											<colgroup>
												<col align="center" width="200">
												<col align="center" width="300">
												<col align="center" width="200">
											</colgroup>
											<thead>
												<tr>
													<td class="Arial12br" colSpan="3">Pagos</td>
												</tr>
												<tr align="center">
													<td class="ColumnHeader">Forma de Pago</td>
													<td class="ColumnHeader">Nro. Tarjeta/Nro Cheque</td>
													<td class="ColumnHeader">Monto a Pagar</td>
												</tr>
											</thead>
											<tbody>
												<tr class="RowEven">
													<td style="HEIGHT: 20px"><asp:dropdownlist id="cboTipDocumento1" onChange="javascript:RedondearEfectivo('txtMonto1','cboTipDocumento1');" tabIndex="6" runat="server" CssClass="clsSelectEnable"></asp:dropdownlist></td>
													<td style="HEIGHT: 20px" width="500"><asp:dropdownlist id="cboBanco1" runat="server" CssClass="clsSelectEnable"></asp:dropdownlist>&nbsp;&nbsp;
														<asp:textbox id="txtDoc1" tabIndex="7" runat="server" CssClass="clsInputEnable" Width="147px"></asp:textbox></td>
													<td style="HEIGHT: 20px"><asp:textbox id="txtMonto1" onkeyup="javascript:RedondearEfectivo('txtMonto1','cboTipDocumento1');" tabIndex="8" runat="server" CssClass="clsInputEnable"
															Width="147px"></asp:textbox></td>
												</tr>
												<tr class="RowOdd">
													<td><asp:dropdownlist id="cboTipDocumento2" onChange="javascript:RedondearEfectivo('txtMonto2','cboTipDocumento2');" tabIndex="9" runat="server" CssClass="clsSelectEnable"></asp:dropdownlist></td>
													<td><asp:dropdownlist id="cboBanco2" runat="server" CssClass="clsSelectEnable"></asp:dropdownlist>&nbsp;&nbsp;
														<asp:textbox id="txtDoc2" tabIndex="10" runat="server" CssClass="clsInputEnable" Width="147px"></asp:textbox></td>
													<td><asp:textbox id="txtMonto2" onkeyup="javascript:RedondearEfectivo('txtMonto2','cboTipDocumento2');" tabIndex="8" runat="server" CssClass="clsInputEnable"
															Width="147px"></asp:textbox></td>
												</tr>
												<tr class="RowEven">
													<td><asp:dropdownlist id="cboTipDocumento3" onChange="javascript:RedondearEfectivo('txtMonto3','cboTipDocumento3');" tabIndex="12" runat="server" CssClass="clsSelectEnable"></asp:dropdownlist></td>
													<td><asp:dropdownlist id="cboBanco3" runat="server" CssClass="clsSelectEnable"></asp:dropdownlist>&nbsp;&nbsp;
														<asp:textbox id="txtDoc3" tabIndex="13" runat="server" CssClass="clsInputEnable" Width="147px"></asp:textbox></td>
													<td><asp:textbox id="txtMonto3" onkeyup="javascript:RedondearEfectivo('txtMonto3','cboTipDocumento3');" tabIndex="8" runat="server" CssClass="clsInputEnable"
															Width="147px"></asp:textbox></td>
												</tr>
											</tbody>
										</table>
									</td>
								</tr>
							</tbody>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
							<tr>
								<td height="10"></td>
							</tr>
						</table>
						<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="100%" align="center"
							border="1">
							<tr>
								<td>
									<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
										<tr>
											<td vAlign="top" width="10"></td>
											<td vAlign="middle" width="100%">
												<table class="Arial12B" cellSpacing="2" cellPadding="0" width="90%" align="center" border="0">
													<tr>
														<td width="190" style="HEIGHT: 19px">Importe Recibido Soles:</td>
														<td style="HEIGHT: 19px"><asp:textbox id="txtRecibidoPen" tabIndex="10" runat="server"
																CssClass="clsInputEnable"></asp:textbox></td>
														<td style="HEIGHT: 19px">Vuelto:</td>
														<td style="HEIGHT: 19px"><asp:textbox id="txtVuelto" runat="server" CssClass="clsInputDisable" ReadOnly="True"></asp:textbox></td>
													</tr>
													<tr>
														<td width="190">Importe Recibido Dolares:</td>
														<td><asp:textbox id="txtRecibidoUsd" tabIndex="10" runat="server"
																CssClass="clsInputEnable"></asp:textbox></td>
														<td></td>
														<td></td>
													</tr>
												</table>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
							<tr>
								<td height="10"></td>
							</tr>
						</table>
						<div id="Botones">
							<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="400" align="center"
								border="1">
								<tr>
									<td align="center">
										<table cellSpacing="2" cellPadding="0" border="0">
											<tr>
												<td align="center" width="28"></td>
												<td align="center" width="60"><asp:button id="btnGrabar" runat="server" CssClass="BotonOptm" Width="100px" Text="Grabar"></asp:button></td>
												<td align="center" width="28"></td>
												<td align="center" width="60"><asp:button id="btnCancelar" runat="server" CssClass="BotonOptm" Width="100px" Text="Cancelar"></asp:button></td>
												<td align="center" width="28"></td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
						</div>
						<div id="divTitulo" style="DISPLAY: none">
							<table cellSpacing="2" cellPadding="0" width="400" align="center" border="0">
								<tr>
									<td class="TituloRConsulta" align="center" width="28">El&nbsp;pago&nbsp;se&nbsp;está&nbsp;procesando</td>
								</tr>
							</table>
						</div>
					</td>
				</tr>
			</table>
			<p style="DISPLAY: none"><asp:textbox id="txtTarjCred" runat="server"></asp:textbox><asp:textbox id="txtBIN" runat="server"></asp:textbox></p>
		</form>
	</body>
</HTML>
