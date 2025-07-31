<%@ Page Language="vb" AutoEventWireup="false" Codebehind="DetLlamadaAnteriores.ASPX.vb" Inherits="SisCajas.DetLlamadaAnteriores"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>DetLlamadaAnteriores</title>
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
			<TABLE class="Arial12b" id="Table1" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 10px"
				cellSpacing="0" cellPadding="1" width="400" border="0" borderColor="#999999">
				<TR>
					<TD>Fecha:
						<asp:TextBox id="TextBox1" runat="server" Width="72px" Height="17px"></asp:TextBox>
						<A onmouseover="window.status='Date Picker';return true;" onmouseout="window.status='';return true;"
							href="javascript:show_calendar('frmPrincipal.txtFecha');">
							<img src="images/botones/btn_Calendario.gif" style="cursor:hand" border="0" style="WIDTH:15px; HEIGHT:15px">
						</A>
						</TD>
					<TD>
					<!--<img id="cmdLBS" src="BotTemp/Buscar.ico" alt="Buscar" style="WIDTH:20px;HEIGHT:20px">
					-->
					&nbsp;
					</TD>
					<TD>
						<input name="cmdImprimir" type="button" class="BotonOptm" style="WIDTH:70px; HEIGHT:19px"
							value="Imprimir" id="cmdImprimir"></TD>
					<TD>
						<input name="cmdVisualizar" type="button" class="BotonOptm" style="WIDTH:70px; HEIGHT:19px"
							value="Visualizar" id="cmdVisualizar"></TD>
					<TD style="WIDTH: 70px">
						<input name="cmdEnviEmail" type="button" class="BotonOptm" style="WIDTH:85px; HEIGHT:19px"
							value="Envíar email" id="cmdEnviEmail"></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
