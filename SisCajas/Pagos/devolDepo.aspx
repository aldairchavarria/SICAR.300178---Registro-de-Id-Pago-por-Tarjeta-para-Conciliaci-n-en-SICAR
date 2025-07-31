<%@ Page Language="vb" AutoEventWireup="false" Codebehind="devolDepo.aspx.vb" Inherits="SisCajas.devolDepo" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>devolDepo</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../estilos/est_General.css" type="text/css" rel="styleSheet">
		<script language="JavaScript" src="../librerias/ubigeo.js"></script>
		<script language="JavaScript" src="../librerias/date-picker.js"></script>
		<script language="JavaScript" src="../librerias/Lib_FuncValidacion.js"></script>
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<script language="javascript">
		<!--
		function MM_findObj(n, d) { //v4.01
		var p,i,x;  if(!d) d=document; if((p=n.indexOf("?"))>0&&parent.frames.length) {
			d=parent.frames[n.substring(p+1)].document; n=n.substring(0,p);}
		if(!(x=d[n])&&d.all) x=d.all[n]; for (i=0;!x&&i<d.forms.length;i++) x=d.forms[i][n];
		for(i=0;!x&&d.layers&&i<d.layers.length;i++) x=MM_findObj(n,d.layers[i].document);
		if(!x && d.getElementById) x=d.getElementById(n); return x;
		}

		function MM_showHideLayers() { //v6.0
		var i,p,v,obj,args=MM_showHideLayers.arguments;
		for (i=0; i<(args.length-2); i+=3) if ((obj=MM_findObj(args[i]))!=null) { v=args[i+2];
			if (obj.style) { obj=obj.style; v=(v=='show')?'visible':(v=='hide')?'hidden':v; }
			obj.visibility=v; }
		}

		document.onclick = document_onclick;
		
		function document_onclick() {
			var obj = event.srcElement;
			switch (obj.tagName) {			
				case "INPUT":
					switch (obj.id) {
						case "btnGrabar":						
							if (!f_Grabar()) event.returnValue = false;
							break;							
					}
					break;
			}
		}
		
		function f_Grabar()
		{
			if (f_Validar())
				return true;
			else
				return false;
		}

		
		
		function f_Validar() {// valida campos
			if (!ValidaNumero('document.frmPrincipal.txtCorrRef','el campo Correlativo Ref. ',false)) return false;
			if (!ValidaFechaA('document.frmPrincipal.txtFecReg',false)) return false;
			return true;  
		};


		function f_Cancelar() { 
			//document.frmPrincipal.action = '../inicio/Inicial.asp';
			//document.frmPrincipal.submit();
		}

		function e_mayuscula() {
			if ((event.keyCode>96&&event.keyCode<123)||(event.keyCode==241)||(event.keyCode==250)||(event.keyCode==243)||(event.keyCode==237)||(event.keyCode==233)||(event.keyCode==225))
				event.keyCode=event.keyCode-32;
			
		}	
							
		//-->
		</script>
	</HEAD>
	<body>
		<form id="frmPrincipal" method="post" runat="server">
			<table border="0" cellspacing="0" cellpadding="0">
				<tr>
					<td>
						<table width="750" border="1" cellspacing="0" cellpadding="0" name="Contenedor" align="left"
							bordercolor="#336699">
							<tr>
								<td align="center">
									<table width="100%" border="0" cellspacing="0" cellpadding="0" align="center">
										<tr>
											<td width="10" valign="top" height="32"></td>
											<td width="98%" height="32" align="center" class="TituloRConsulta" valign="top" style="PADDING-TOP:4px">Devolución 
												de Deposito en Garantía</td>
											<td width="10" valign="top" height="32"></td>
										</tr>
									</table>
									<table width="100%" border="0" cellspacing="0" cellpadding="0" align="center">
										<tr>
											<td valign="top" width="14"></td>
											<td width="98%" style="PADDING-RIGHT:5px; PADDING-LEFT:5px; PADDING-BOTTOM:5px; PADDING-TOP:5px">
												<table width="700" border="0" cellspacing="0" cellpadding="0">
													<tr>
														<td height="4"></td>
													</tr>
													<tr>
														<td height="18">
															<table border="0" cellspacing="1" cellpadding="0">
																<tr class="Arial12b" bgcolor="white">
																	<td width="200"><font color="#ff0000">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Datos 
																				de la Devolución</b></font></td>
																</tr>
															</table>
														</td>
													</tr>
													<tr>
														<td height="4"></td>
													</tr>
												</table>
												<table width="700" border="0" cellspacing="0" cellpadding="0">
													<tr>
														<td>
															<table border="1" width="100%" cellspacing="2" cellpadding="0" bordercolor="white">
																<tr>
																	<td width="25">&nbsp;</td>
																	<td width="145" class="Arial12b">&nbsp;&nbsp;&nbsp;Correlativo Ref. (*):</td>
																	<td width="150" class="Arial12b"><input class="clsInputEnable" type="text" name="txtCorrRef" id="txtCorrRef" runat="server"
																			size="30" maxlength="5" tabindex="2" onkeypress="e_mayuscula();"></td>
																	<td width="25" height="22">&nbsp;</td>
																	<td width="150" class="Arial12b">&nbsp;&nbsp;&nbsp;Fecha de Reg. (*):</td>
																	<td width="170" valign="middle"><input name="txtFecReg" type="text" class="clsInputEnable" id="txtFecReg" tabindex="4"
																			runat="server" size="25" maxlength="10"> <a href="javascript:show_calendar('frmPrincipal.txtFecReg');" onMouseOut="window.status='';return true;"
																			onMouseOver="window.status='Date Picker';return true;"><img border="0" src="../../images/botones/btn_Calendario.gif"></a>
																	</td>
																</tr>
															</table>
														</td>
													</tr>
												</table>
											</td>
											<td valign="top" width="14"></td>
										</tr>
									</table>
									<table width="100%" border="0" cellspacing="0" cellpadding="0" align="center">
										<tr>
											<td height="17"></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<br>
					</td>
				</tr>
				<tr>
					<td>
						<br>
						<table width="400" border="1" cellspacing="0" cellpadding="0" align="center" bordercolor="#336699">
							<tr>
								<td align="center">
									<table border="0" cellspacing="2" cellpadding="0">
										<tr>
											<td align="center" width="28"></td>
											<td align="center" width="85">
												<asp:Button id="btnGrabar" runat="server" Text="Grabar" Width="98px" CssClass="BotonOptm"></asp:Button></td>
											<TD align="center" width="28"></TD>
											<TD align="center" width="85"><INPUT class="BotonOptm" id="btnCancelar" style="WIDTH: 98px" onclick="f_Cancelar()" type="button"
													value="Cancelar" name="btnCancelar"></TD>
											<TD align="center" width="28"></TD>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
