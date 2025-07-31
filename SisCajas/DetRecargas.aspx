<%@ Page Language="vb" AutoEventWireup="false" Codebehind="DetRecargas.aspx.vb" Inherits="SisCajas.DetRecargas"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>DetRecargas</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="JavaScript" src="../librerias/date-picker.js"></script>
		<script language="JavaScript" src="../librerias/Lib_FuncValidacion.js"></script>
		<script language="vbscript" src="librerias/Lib_Imprimir.vbs"></script>
		<LINK href="../estilos/est_General.css" type="text/css" rel="styleSheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="frmPrincipal" method="post" runat="server">
			<TABLE class="Arial12b" id="Table1" style="Z-INDEX: 101; LEFT: 1px; POSITION: absolute; TOP: 10px"
				cellSpacing="0" cellPadding="0" width="435" border="0" borderColor="#999999">
				<TR align="center">
					<TD style="WIDTH:125px">Fecha:
						<asp:TextBox class="Arial12b" id="TextBox1" runat="server" Width="64px" Height="17px"></asp:TextBox>
						<A onmouseover="window.status='Date Picker';return true;" onmouseout="window.status='';return true;"
							href="javascript:show_calendar('frmPrincipal.txtFecha');"><img src="images/botones/btn_Calendario.gif" style="WIDTH:15px; CURSOR:hand; HEIGHT:15px"
								border="0"> </A>
					</TD>
					<TD style="WIDTH:95px">
						<input name="cmdEstTar" type="button" class="BotonOptm" style="WIDTH:80px; HEIGHT:19px"
							value="Estado tarjeta" id="cmdEstTar"></TD>
					<TD style="WIDTH:70px">
						<input name="cmdBloqTar" type="button" class="BotonOptm" style="WIDTH:75px; HEIGHT:19px"
							value="Bloq. tarjeta" id="cmdBloqTar"></TD>
					<TD style="WIDTH: 70px">
						<input name="cmdRecarga" type="button" class="BotonOptm" style="WIDTH:48px; HEIGHT:19px"
							value="Recarga" id="cmdRecarga"></TD>
					<TD style="WIDTH: 70px">
						<input name="cmdEnviamail" type="button" class="BotonOptm" style="WIDTH:75px; HEIGHT:19px"
							value="Enviar email" id="cmdEnviamail"></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
