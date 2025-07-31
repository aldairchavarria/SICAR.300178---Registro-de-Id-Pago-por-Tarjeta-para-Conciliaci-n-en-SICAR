<%@ Page Language="vb" aspcompat="true" AutoEventWireup="false" Codebehind="rep_OperaDetInd.aspx.vb" Inherits="SisCajas.rep_OperaDetInd" %>

<HTML>
  <HEAD>
		<title>Aplicativo TIM</title>
<meta http-equiv=Pragma content=no-cache>
<META http-equiv=Expires content="Mon, 06 Jan 1990 00:00:01 GMT"><LINK href="../estilos/est_General.css" type=text/css rel=styleSheet >
<script language=JavaScript src="../librerias/Lib_FuncValidacion.js"></script>

<script language=JavaScript>
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

function f_Excel(){
	document.frmPrincipal.action = "ExcelCaja.asp";
	document.frmPrincipal.submit();
};
//-->
			</script>

<meta http-equiv=pragma content=no-cache>
  </HEAD>
<body leftMargin=0 topMargin=0 marginheight="0" 
marginwidth="0">
<form id=frmPrincipal name=frmPrincipal method=post target=_blank><input id=strFecha type=hidden value="<%=strFecha%>" 
name=strFecha> <input type=hidden value=2 name=tipo> 
<table cellSpacing=0 cellPadding=0 width=975 border=0>
  <tr>
	<!--<td width="170" valign="top"></td>-->
	<!--<td width="10" valign="top">&nbsp;</td>-->
    <td vAlign=top width=820>
      <table height=14 cellSpacing=0 cellPadding=0 width=820 border=0 
       name="Contenedor">
        <tr>
          <td align=center></TD></TR></TABLE>
      <table cellSpacing=0 cellPadding=0 width=790 align=center border=0 
       name="Contenedor">
        <tr>
          <td align=center>
            <table borderColor=#336699 cellSpacing=0 cellPadding=0 width=790 
            align=center border=1>
              <tr>
                <td align=center>
                  <table class=Arial10B cellSpacing=0 cellPadding=0 width="100%" 
                  align=center border=0>
                    <tr>
                      <td width=10 height=4 
border="0"></TD>
                      <td class=TituloRConsulta align=center width="98%" 
                      height=32>Cuadre de Caja - Detalle 
                        de Operaciones Procesadas Individual</TD>
                      <td vAlign=top width=14 height=32 
                      ></TD></TR></TABLE>
                  <table cellSpacing=0 cellPadding=0 width=790 align=center 
                  border=0>
                    <tr>
                      <td width="98%">
                        <table cellSpacing=0 cellPadding=0 width=770 border=0>
                          <tr><td height=4></TD></TR>
                          <tr>
                            <td height=18>
                              <table cellSpacing=1 cellPadding=0 border=0>
                                <tr class=Arial12br>
                                <td width=250>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <b>Datos Generales</B></TD></TR></TABLE></TD></TR>
                          <tr><td height=4></TD></TR>
                          <tr>
                            <td>
                              <table cellSpacing=2 cellPadding=0 width="100%" border=0>
                                <tr>
									<td width=30>&nbsp;</TD>
									<td class=Arial12b width=166>&nbsp;&nbsp;&nbsp;Fecha :</TD>
									<td class=Arial12b width=170>
									<input class=clsInputDisable id=txtNombres4 type=text maxLength=15 size=30 value="<%=sFechaActual%>" name=txtNombres></TD>
									<td width=30>&nbsp;</TD>
									<td class=Arial12b width=120>&nbsp;&nbsp;&nbsp;Hora :</TD>
									<td class=Arial12b width=220>
									<input class=clsInputDisable id=txtNombres4 readOnly 
									type=text maxLength=15 size=30 
									value="<%=sHoraActual%>" name=txtNombres ></TD></TR></TABLE></TD></TR></TABLE></TD></TR></TABLE></TD></TR></TABLE>
									<br><br>
			</TD></TR></TABLE>
      <TABLE borderColor=#336699 cellSpacing=0 cellPadding=0 width=790 align=center border=1>
		<TR>
			<TD align=center>
            <TABLE class=Arial10B cellSpacing=0 cellPadding=0 width="100%" align=center border=0>
				<TR>
					<TD width=10 height=4 border="0"></TD>
					<TD class=TituloRConsulta align=center width="98%" height=32 >Datos</TD>
					<TD vAlign=top width=14 height=32></TD></TR>
            </TABLE></TD></TR>
      </TABLE>
      <TABLE cellSpacing=0 cellPadding=0 width=790 align=center border=0>
        <TR><TD width="98%">
            <TABLE cellSpacing=0 cellPadding=0 width=770 border=0>
              <tr><td height=4></td></tr>
              <tr><td height=18>
                  <DIV class=frame2 style="BORDER-RIGHT:1px;BORDER-TOP:1px;OVERFLOW-Y:scroll;OVERFLOW-X:scroll;BORDER-LEFT:1px;WIDTH:785px;BORDER-BOTTOM:1px;HEIGHT:300px;TEXT-ALIGN:center">
                  <TABLE class=tabla_interna_borde1 cellSpacing=1 cellPadding=1 width=1800>
                    <tr class=Arial12B height=21></tr>
                    <tr height=1></tr></table><asp:datagrid id=DgOpera runat="server" BorderColor="White" CssClass="Arial11B" AutoGenerateColumns="False" CellSpacing="1" Height="30px" Width="100%">
												<AlternatingItemStyle BackColor="#DDDEE2"></AlternatingItemStyle>
												<ItemStyle BackColor="#E9EBEE"></ItemStyle>
												<HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderColor="#999999" CssClass="Arial12B"
													VerticalAlign="Top"></HeaderStyle>
												<Columns>
													<asp:BoundColumn DataField="ID_CONFTRAN" HeaderText="Transacci&#243;n">
														<HeaderStyle Wrap="False" BorderWidth="1px" Width="50px"></HeaderStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="OPER_TIP_DOCUMENTO" HeaderText="Tipo Doc.">
														<HeaderStyle Wrap="False" BorderWidth="1px" Width="80px"></HeaderStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="OPER_COD_ARTICULO" HeaderText="Art&#237;culo">
														<HeaderStyle Wrap="False" BorderWidth="1px" Width="150px"></HeaderStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="OPER_NOM_CLIENTE" HeaderText="Nombre Cliente">
														<HeaderStyle Wrap="False" BorderWidth="1px" Width="180px"></HeaderStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="OPER_NRO_FACT_SAP" HeaderText="Nro Fact. SAP">
														<HeaderStyle Wrap="False" BorderWidth="1px" Width="100px"></HeaderStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="OPER_NRO_FAC_SUNAT" HeaderText="Doc. Sunat">
														<HeaderStyle Wrap="False" BorderWidth="1px" Width="120px"></HeaderStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="OPER_NRO_TELEFONO" HeaderText="Tel&#233;fono">
														<HeaderStyle Wrap="False" BorderWidth="1px" Width="70px"></HeaderStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="OPER_CANTIDAD" HeaderText="Cantidad">
														<HeaderStyle Wrap="False" BorderWidth="1px" Width="30px"></HeaderStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="OPER_MEDIO_PAGO" HeaderText="Medio de Pago">
														<HeaderStyle Wrap="False" BorderWidth="1px" Width="120px"></HeaderStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="OPER_MONTO" HeaderText="Monto">
														<HeaderStyle Wrap="False" BorderWidth="1px" Width="60px"></HeaderStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="OPER_DOCUMENTO_CLTE" HeaderText="Nro Doc. Cliente">
														<HeaderStyle Wrap="False" BorderWidth="1px" Width="100px"></HeaderStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="oper_nro_referencia" HeaderText="Nro de Referencia">
														<HeaderStyle Wrap="False" BorderWidth="1px" Width="130px"></HeaderStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="COD_CAJERO" HeaderText="Cajero">
														<HeaderStyle Wrap="False" BorderWidth="1px" Width="80px"></HeaderStyle>
													</asp:BoundColumn>
												</Columns>
											</asp:datagrid>
										</DIV></TD></TR>
									</table>
							</td></tr>
					</table>
		<table borderColor=#336699 cellSpacing=0 cellPadding=4 width=360 align=center border=1>
			<TR>
			<TD>
				<TABLE class=Arial10B cellSpacing=0 cellPadding=0 width="100%" 
				align=center border=0>
				<TR>
					<TD align=center><INPUT class=BotonOptm style="WIDTH: 100px" onclick=javascript:f_Exportar(); type=button value="Exportar Excel" name=btnBuscar>&nbsp;&nbsp; 
					</TD></TR></TABLE></TD></TR></TABLE></TD></TR></TABLE>
<script language=JavaScript type=text/javascript>
var esNavegador, esIExplorer;

esNavegador = (navigator.appName == "Netscape") ? true : false;
esIExplorer = ((navigator.appName.indexOf("Microsoft") != -1) || (navigator.appName.indexOf("MSIE") != -1)) ? true : false;

if (esIExplorer) {
}

			</script>
</FORM>
<script language=JavaScript>
	function f_Exportar()
	{
		//alert("rep_OperaDetExcel.aspx?pfecha="+frmPrincipal.strFecha.value);
		document.all.ifraExcel.src="rep_OperaDetIndExcel.aspx?pfecha="+frmPrincipal.strFecha.value;
		//document.frmTmp.action = '<%=strRuta%>/reportes/toExcel.aspx?tipo=2&Individual=0&strFecha=<%=strFecha%>';
		//document.frmTmp.submit();
	}

		</script>
<iframe id=ifraExcel style="DISPLAY: none"></IFRAME>
<form name=frmTmp action="" method=post target=_blank></FORM>
	</body>
</HTML>
