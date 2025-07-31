<%@ Page Language="vb" AutoEventWireup="false" Codebehind="home.aspx.vb" Inherits="SisCajas.home" aspcompat=true %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>America Móvil</title>
		<meta content="False" name="vs_showGrid">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="estilos/est_General.css" type="text/css" rel="styleSheet">
		<!--PROY-27440 INI-->
		<script language="JavaScript" src="librerias/msrsclient.js"></script>
		<script language="JavaScript" src="Scripts/jquery-1.1.js"></script>
		<script language="JavaScript" src="Scripts/form.js"></script>
		<script language="JavaScript" src="Scripts/xml2json.js"></script>
		<script language="JavaScript" src="Scripts/operacionPOS.js"></script>
		<!--PROY-27440 FIN-->
		<script language="jscript">
		
		//PROY-27440 INI
		var varArrayEstTrans;
		var serverURL =  '../Pos/ProcesoPOS.aspx';
		var webServiceURL = '<%= System.Configuration.ConfigurationSettings.AppSettings("consWsLocal_Url") %>';
		var timeOutWsLocal = '<%= System.Configuration.ConfigurationSettings.AppSettings("consWsLocal_Url_TimeOut") %>';
        var VarToday = new Date();
        var varIntAutPos;
		
		window.onload = window_onload;		
		//document.onclick = document_onclick;
		
		function window_onload(){
			if (frmPrincipal.txtCodOficina.value!=""){				
				CambiaImprOficina(frmPrincipal.txtCodOficina.value);							
			}
			try
			{
				f_datos_POS();					
			}			
			catch(err) {}
		}
	
		/* HOME SINERGIA 60
		function document_onclick(){
			var obj = event.srcElement;
			
			if (obj.tagName=="INPUT") {	
				if (obj.id=="IngRegularizador" || obj.id=="Ingreso"){
				
					//var oIfmImpresora = self.frames["IfrmCambiaImpr"];	
					var oIfmImpresora = document.getElementById("IfrmCambiaImpr");				
					if(oIfmImpresora){						
						var cbImpresora = oIfmImpresora.document.getElementById('cbImpresora');						
						if(cbImpresora){
							if(cbImpresora.length>0 && cbImpresora.value == ''){
								alert('<%=System.Configuration.ConfigurationSettings.AppSettings("FE_MensajeSinImpresora") %>');
								event.returnValue = false;
							}							
						}
					}					
				
					frmPrincipal.txtImpresora.value="";
					if (!window.IfrmCambiaImpr.ValidaUsuarioImp()){
						event.returnValue = false;
					}
					else
					{
						frmPrincipal.txtImpresora.value = window.IfrmCambiaImpr.frmPrincipal.cbImpresora.value;
						frmPrincipal.txtMensajeImp.value = window.IfrmCambiaImpr.frmPrincipal.txtMensaje.value;
					}
				}
			}
		}
		*/
		
		function Botones()
		{
			var txtFlag = document.getElementById("txtFlag");			
			if (txtFlag.value != "NV")
			{
				alert('<%=System.Configuration.ConfigurationSettings.AppSettings("FE_MensajeSinImpresora") %>');
				event.returnValue = false;
			}
		
		}
		
		function CambiaImprOficina(codficina)
		{
			var objIframeCamb = document.getElementById("IfrmCambiaImpr");			
			document.getElementById("IfrmCambiaImpr").src = '';	
			document.getElementById('IfrmCambiaImpr').contentWindow.opener = window;			
			
			//INICIATIVA-318 INI
			/*if (frmPrincipal.txtTipoUsuario.value=="C" || frmPrincipal.txtTipoUsuario.value=="R")
			{	
				objIframeCamb.contentWindow.location.replace('ifrCambiaImpresora.aspx?codOficina=' + codficina);
			}*/
			//INICIATIVA-318 INI
		}
			function f_datos_POS()
		{
			try
			{
				var varIdTransaccion = formatDate(VarToday);
				var varArrayAudi = document.getElementById("HidDatoAuditPos").value.split("|");				
				var varNombreAplicacion = varArrayAudi[1];
				var varUsuarioAplicacion = varArrayAudi[2];
			
				var entityIP = 
				{ 
				IdTransaccion: varIdTransaccion, 
				IpApplicacion: '', 
				NombreAplicacion: varNombreAplicacion, 
				UsuarioAplicacion: varUsuarioAplicacion, 
				TipoOperacion: document.getElementById("HidTipOpera").value
				};
		
				var varIpLocal = '';
		
				var soapIP = f_data_IpCaja(entityIP);
		
						$.ajax({
						url: webServiceURL + '?op=peticionInformacionCaja',
						type: "POST",
						dataType: "text",
						data: soapIP,
						processData: false,
						contentType: "text/xml; charset=\"utf-8\"",
						async: true,
						cache: false,
						success: function (objResponse, status) {						
							var x2js = new X2JS();
							var jsonObj = x2js.xml_str2json(objResponse);
							varIpLocal = jsonObj.Envelope.Body.RespuestaPeticionInformacionCaja.Atributos.Atributo[1].Valor;
							varIpLocal = (varIpLocal == null) ? '' : String(varIpLocal);
							document.getElementById("HidDatoIP").value = varIpLocal;
						},
						error: function (request, status) {
							varIpLocal = '';
							document.getElementById("HidDatoIP").value = varIpLocal;
						},
						timeout: Number(timeOutWsLocal)
					});			
				}
			catch(err) {
				
			}
		}
		</script>
	</HEAD>
	<body bgColor="#ffffff" MS_POSITIONING="GridLayout">
		<form id="frmPrincipal" method="post" runat="server">
			<table borderColor="#5991bb" cellSpacing="0" cellPadding="3" width="640" align="center"
				bgColor="#ffffff" border="1">
				<BR>
				<BR>
				<BR>
				<BR>
				<BR>
				<tr>
					<td>
						<table cellSpacing="0" cellPadding="0" width="630" bgColor="#ffffff" border="0">
							<tr>
								<td style="PADDING-BOTTOM: 5px; PADDING-LEFT: 20px; FONT-FAMILY: Verdana, Arial, Helvetica; COLOR: #ffffff; FONT-SIZE: 24px; CURSOR: default; FONT-WEIGHT: bold; PADDING-TOP: 12px"
									vAlign="bottom" bgColor="#5991bb" height="75"><span><i>SOLUCION DE CAJAS Y RECAUDACION</i></span></td>
							</tr>
							<tr bgColor="#ffffff">
								<td><img height="3" width="1" border="0"></td>
							</tr>
							<tr bgColor="#ff9900">
								<td><img height="3" width="1" border="0"></td>
							</tr>
							<tr bgColor="#ffffff">
								<td><img height="3" width="1"></td>
							</tr>
							<tr>
								<td bgColor="#cccccc">&nbsp;</td>
							</tr>
							<tr vAlign="top">
								<td bgColor="#ffffff" height="170">
									<table cellSpacing="0" cellPadding="0" width="100%" border="0">
										<!--<tr><td colspan="6">&nbsp;</td></tr>-->
										<tr>
											<td class="login01" vAlign="top" align="center" width="150"><IMG src="images/principal/hm_tim.gif"><br>
												América Móvil Peru</td>
											<!--<td style="PADDING-TOP:4px" width="100">&nbsp;</td>-->
											<td width="300">
												<table cellSpacing="1" cellPadding="1" border="0">
													<br>
													<br>
													&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
													&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
													<asp:imagebutton id="Ingreso" runat="server" ImageUrl="../images/principal/hm_ingreso.gif"></asp:imagebutton>&nbsp;
													<tr>
														<td class="login01" style="HEIGHT: 20px" vAlign="middle" align="left" width="100">&nbsp;&nbsp;&nbsp;&nbsp;
															<asp:label id="lblOficina" runat="server">Oficina&nbsp;:</asp:label></td>
														<td style="HEIGHT: 20px" align="right" width="200"></SELECT><asp:dropdownlist id="cboOficina" runat="server" CssClass="clsSelectEnable" Width="175px"></asp:dropdownlist></td>
													</tr>
													<tr>
														<td colSpan="2">
															<div id="DivImpresora" name="DivImpresora"><iframe id="IfrmCambiaImpr" style="HEIGHT: 20px" name="IfrmCambiaImpr" frameBorder="no"
																	width="100%" scrolling="no"></iframe>
															</div>
														</td>
													</tr>
													<tr>
														<td vAlign="top" align="center" colSpan="2" height="5"><asp:imagebutton id="IngRegularizador" runat="server" ImageUrl="images/principal/hm_ingreso.gif"></asp:imagebutton></td>
													</tr>
												</table>
											</td>
											<td vAlign="top" align="right" width="50"><br>
												<IMG height="53" src="images/fondos/LogoAM.gif"></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<div style="TOP: 200px; LEFT: 150px"></div>
			<input id="txtCodOficina" type="hidden" name="txtCodOficina" runat="server"> <input id="txtDesOficina" type="hidden" name="txtDesOficina" runat="server">
			<input id="txtCodOficina2" type="hidden" name="txtCodOficina2" runat="server"> <input id="txtTipoUsuario" type="hidden" name="txtTipoUsuario" runat="server">
			<input id="txtImpresora" type="hidden" name="txtImpresora" runat="server"> <input id="txtMensajeImp" type="hidden" name="txtMensajeImp" runat="server">
			<input id="txtFlag" type="hidden" name="txtFlag" runat="server">
			<!--PROY-27440 INI-->
			<input id="HidDatoIP" type="hidden" name="HidDatoIP" runat="server">
			<input id="HidTipOpera" type="hidden" name="HidTipOpera" runat="server">
			<input id="HidDatoAuditPos" type="hidden" name="HidDatoAuditPos" runat="server">
			<input id="HidIntAutPos" type="hidden" name="HidIntAutPos" runat="server"> 
			<!--PROY-27440 FIN-->
		</form>
	</body>
</HTML>
