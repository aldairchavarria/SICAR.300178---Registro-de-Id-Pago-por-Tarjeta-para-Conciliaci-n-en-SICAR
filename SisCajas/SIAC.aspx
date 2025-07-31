<%@ Page Language="vb" AutoEventWireup="false" Codebehind="SIAC.aspx.vb" Inherits="SisCajas.SIAC"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>SIAC</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../estilos/est_General.css" type="text/css" rel="styleSheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table id="Table1" borderColor="#336699" cellSpacing="0" width="980" align="center" border="1">
				<tr class="TituloRConsulta">
					<td align="center"><asp:label id="Label4" runat="server">SISTEMA DE ATENCION A CLIENTES PREPAGO</asp:label></td>
				</tr>
			</table>
			<table id="Table1" style="Z-INDEX: 102; LEFT:5px; WIDTH:995px; POSITION: absolute; TOP:53px; HEIGHT: 460px"
				borderColor="#336699" cellSpacing="1" cellPadding="1" width="720" border="1">
				<TR>
					<TD vAlign="top">
						<TABLE id="Table1" style="HEIGHT: 10px" borderColor="#336699" cellSpacing="1" cellPadding="1"
							width="425" align="center" border="0">
							<tr class="Arial12b">
								<td align="center"><asp:label id="lblTitActualizaBIN" runat="server"><b>DATOS GENERALES DEL 
											CLIENTE</b></asp:label></td>
							</tr>
						</TABLE>
						<iframe id="IfrmDatGenCliente" style="WIDTH: 440px; HEIGHT: 102px" name="IfrmDatGenCliente"
							src="DatGeneral.aspx" frameBorder="no" width="50%" scrolling="no"></iframe>
						<TABLE id="Table1" style="HEIGHT: 10px" borderColor="#336699" cellSpacing="1" cellPadding="1"
							width="425" align="center" border="0">
							<tr class="Arial12b">
								<td align="center"><asp:label id="Label5" runat="server"><b>MOTIVO DE ULTIMAS LLAMADAS</b></asp:label></td>
							</tr>
						</TABLE>
						<iframe id="IfrmMotLlamadas" style="WIDTH: 440px; HEIGHT: 102px" name="IfrmMotLlamadas"
							src="DatMotLlamada.aspx" frameBorder="no" width="50%" scrolling="no"></iframe>
					</TD>
					<TD vAlign="top">
						<TABLE id="Table2" style="HEIGHT: 10px" borderColor="#336699" cellSpacing="1" cellPadding="1"
							width="425" align="center" border="0">
							<tr class="Arial12b">
								<td align="center"><asp:label id="Label6" runat="server"><b>DATOS GENERALES DE LA LINEA</b></asp:label></td>
							</tr>
						</TABLE>
						<iframe id="IfrmDatGenLinea" style="WIDTH: 440px; HEIGHT: 209px" name="IfrmDatGenLinea"
							src="DatGeneralLinea.aspx" frameBorder="no" width="50%" scrolling="no"></iframe>
					</TD>
				</TR>
				<TR>
					<TD vAlign="top">
						<TABLE id="Table2" style="HEIGHT:10px" borderColor="#336699" cellSpacing="1" cellPadding="1"
							width="425" align="center" border="0">
							<tr class="Arial12b">
								<td align="center"><asp:label id="Label1" runat="server"><b>DATOS DE LLAMADAS MES ACTUAL</b></asp:label></td>
							</tr>
						</TABLE>
						<iframe id="IfrmDetLLamada" style="WIDTH: 440px; HEIGHT: 135px" name="IfrmDetLLamada" src="DetLLamada.aspx"
							frameBorder="no" width="50%" scrolling="no"></iframe>
						<TABLE id="Table5" style="HEIGHT: 10px" borderColor="#336699" cellSpacing="1" cellPadding="1"
							width="425" align="center" border="0">
							<tr class="Arial12b">
								<td align="center"><asp:label id="Label3" runat="server"><b>DETALLE DE LLAMADAS MESES 
											ANTERIORES</b></asp:label></td>
							</tr>
						</TABLE>
						<iframe id="IfrmLlamadasAnte" style="WIDTH: 440px; HEIGHT: 50px" name="IfrmLlamadasAnte"
							src="DetLlamadaAnteriores.aspx" frameBorder="no" width="50%" scrolling="no"></iframe>
					</TD>
					</TD>
					<TD vAlign="top">
						<TABLE id="Table6" style="HEIGHT: 10px" borderColor="#336699" cellSpacing="1" cellPadding="1"
							width="425" align="center" border="0">
							<tr class="Arial12b">
								<td align="center"><asp:label id="Label2" runat="server"><b>DETALLE DE RECARGAS</b></asp:label></td>
							</tr>
						</TABLE>
						<iframe id="IfrmDetRecarga" style="WIDTH: 440px; HEIGHT: 150px" name="IfrmDetRecarga" src="Recargas.aspx"
							frameBorder="no" width="50%" scrolling="no"></iframe><iframe id="IfrmProcRecarga" style="WIDTH: 440px; HEIGHT: 50px" name="IfrmProcRecarga" src="DetRecargas.aspx"
							frameBorder="no" width="50%" scrolling="no"></iframe>
					</TD>
				</TR>
			</table>
			<TABLE id="Table8" style="Z-INDEX: 102; LEFT:5px; WIDTH:995px; POSITION: absolute; TOP:530px; HEIGHT:40px"
				borderColor="#336699" cellSpacing="0" cellPadding="1" width="720" border="1">
				<TR class="Arial10" style="CURSOR:hand">
					<TD>
						<TABLE id="Table9" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD valign="top">
									<!--
									&nbsp;<input class="BotonOptm" id="cmdVariaciones" style="WIDTH:75px; HEIGHT:30px" type="button"
										value="VARIACIONES"> 
									-->
								&nbsp;<img id="cmdAltaNF" src="BotTemp/NumeroFrecuente.ico" alt="Alta/Baja/Cambio número frecuente" style="WIDTH:20px; HEIGHT:20px">
								&nbsp;<img id="cmdDesDoll" src="BotTemp/DesDoll.gif" alt="Desactivación dol"  style="WIDTH:20px; HEIGHT:20px">
								&nbsp;<img id="cmdBloqLinea" src="BotTemp/BloqDesLinea.ico" alt="Bloqueo/Desbloqueo de linea" style="WIDTH:25px;HEIGHT:25px">
								&nbsp;<img id="cmdCambSIM" src="BotTemp/CambioSIM.ico" alt="Cambio de SIM" style="WIDTH:25px; HEIGHT:25px">
								&nbsp;<img id="cmdAjusCredito" src="BotTemp/AjusCredito.ico" alt="Ajuste de credito" style="WIDTH:25px;HEIGHT:25px">
								&nbsp;<img id="cmdAjusDebito" src="BotTemp/AfDebito.ico" alt="Afiliación debito" style="WIDTH:25px;HEIGHT:25px">
								&nbsp;<img id="cmdAltbuzon" src="BotTemp/ActDesactBuzon.ico" alt="Alta/Baja buzón de voz" style="WIDTH:25px;HEIGHT:25px">
								&nbsp;<img id="cmdActSMS" src="BotTemp/ActDesaSMS.ico" alt="Activar/Desactivar SMS" style="WIDTH:25px;HEIGHT:25px">
								&nbsp;<img id="cmdActMMS" src="BotTemp/ActDesaMMS.ico" alt="Activar/Desactivar MMS" style="WIDTH:25px;HEIGHT:25px">
								&nbsp;<img id="cmdTransSMS" src="BotTemp/TransfSMS.ico" alt="Transferencia SMS" style="WIDTH:20px;HEIGHT:20px">
								&nbsp;<img id="cmdCamTitularidad" src="BotTemp/CambioTitularidad.ico" alt="Cambio de titularidad" style="WIDTH:20px;HEIGHT:20px">
								&nbsp;<img id="cmdAST" src="BotTemp/Asistencia.ico" alt="Asistencia tecnica" style="WIDTH:22px;HEIGHT:22px">
								&nbsp;<img id="cmdLBS" src="BotTemp/ConsLBS.ico" alt="Consultas LBS" style="WIDTH:20px;HEIGHT:20px">
								&nbsp;<img id="cmdHLR" src="BotTemp/ConsultaHLR.ico" alt="Consultas HLR" style="WIDTH:20px;HEIGHT:20px">
								&nbsp;<img id="cmdHistVariaciones" src="BotTemp/HistVariaciones.ico" alt="Historico de variaciones" style="WIDTH:20px;HEIGHT:20px">
								<!--
									<input class="BotonOptm" id="cmdEnlaces" style="WIDTH:55px;HEIGHT:30px" type="button"
										value="ENLACES">&nbsp;&nbsp;
								-->
								</td>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
