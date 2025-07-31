<%@ Page Language="vb" AutoEventWireup="false" Codebehind="Terminar.aspx.vb" Inherits="SisCajas.Terminar"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Terminar</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<script language="javascript">
				
		window.onload = window_onload;							
		
		function window_onload() {
                //PROY-23700-IDEA-29415 - INI
            alert('Proceso terminado con éxito');
			if (frmPrincipal.txtDocSap.value!=""){
			    if(frmPrincipal.txtpImp.value=="NCJ"){
				    f_Imprimir_NCJ();
					frmPrincipal.action="Terminar.aspx";
					frmPrincipal.txtDocSap.value="";
				}
				else{
				f_Imprimir();
				frmPrincipal.action="Terminar.aspx"
					frmPrincipal.txtDocSap.value=""	;
			}
			    
						
		}
		}
                //PROY-23700-IDEA-29415 - FIN
		
		function Imprimir()
	{					
		var objIframe = document.getElementById("IfrmImpresion");
		window.open(objIframe.contentWindow.location);
		
	}
		
		function f_Imprimir(){
			var strCodSAP = frmPrincipal.txtDocSap.value;			
			var strCodSunat = frmPrincipal.txtDocSunat.value;
			var strDepGar = frmPrincipal.txtNroDG.value;
			var strTipoDoc = frmPrincipal.txtTipDoc.value;
						
			var strEfectivo = frmPrincipal.txtEfectivo.value;
			var strRecibido = frmPrincipal.txtRecibido.value;
			// RECIBIR DOLARES
			var strRecibidoUS = frmPrincipal.txtRecibidoUS.value;
			var strEntregar = frmPrincipal.txtEntregar.value;
			
			//Para Recibir ORIGEN
			var strOrigen = frmPrincipal.txtOrigen.value;
			
			// Parametros Recarga Virtual DTH
			var strCodPago = frmPrincipal.strCodPago.value;
			var strFechaExpira = frmPrincipal.strFechaExpira.value;
			var strNroRecarga = frmPrincipal.strNroRecarga.value;
			
			///JTN  INICIO
			var isOffline = frmPrincipal.txtOffline.value;
			///JTN FIN
			
			var objIframe = document.getElementById("IfrmImpresion");
			
			objIframe.style.visibility = "visible";
			objIframe.style.width = 0;
			objIframe.style.height = 0;

			if (strTipoDoc=="ZFPA" || strTipoDoc=="G/R"){
				
			}
			else{
				if (strTipoDoc=="DG") {
					objIframe.src = "OperacionesImp_DG.aspx?numDepGar="+ strCodSAP;					
				}
				else {
					//alert("from terminar: OperacionesImp.aspx?codRefer="+ strCodSAP+ "&FactSunat="+strCodSunat+"&TipoDoc="+strTipoDoc + "&strEfectivo=" + strEfectivo + "&strRecibido=" + strRecibido + "&strEntregar=" + strEntregar + "&strCodPago=" + strCodPago + "&strFechaExpira=" + strFechaExpira + "&strNroRecarga=" + strNroRecarga + "&isOffline=" + isOffline);
					//objIframe.src = "OperacionesImp.aspx?codRefer="+ strCodSAP+ "&FactSunat="+strCodSunat+"&TipoDoc="+strTipoDoc + "&strEfectivo=" + strEfectivo + "&strRecibido=" + strRecibido + "&strEntregar=" + strEntregar + "&strCodPago=" + strCodPago + "&strFechaExpira=" + strFechaExpira + "&strNroRecarga=" + strNroRecarga + "&isOffline=" + isOffline;
					objIframe.src = "OperacionesImp.aspx?codRefer="+ strCodSAP+ "&FactSunat="+strCodSunat+"&TipoDoc="+strTipoDoc + "&strEfectivo=" + strEfectivo + "&strRecibido=" + strRecibido + "&strRecibidoUS=" + strRecibidoUS + "&strEntregar=" + strEntregar + "&strCodPago=" + strCodPago + "&strFechaExpira=" + strFechaExpira + "&strNroRecarga=" + strNroRecarga + "&isOffline=" + isOffline + "&strOrigenVenta=" + strOrigen;
				}				
			}
		}
		//PROY-23700-IDEA-29415 - INI
		function f_Imprimir_NCJ(){
			var strCodSAP = frmPrincipal.txtDocSap.value;			
			var strCodSunat = frmPrincipal.txtDocSunat.value;
			var strDepGar = frmPrincipal.txtNroDG.value;
			var strTipoDoc = frmPrincipal.txtTipDoc.value;
						
			var strEfectivo = frmPrincipal.txtEfectivo.value;
			var strRecibido = frmPrincipal.txtRecibido.value;
			// RECIBIR DOLARES
			var strRecibidoUS = frmPrincipal.txtRecibidoUS.value;
			var strEntregar = frmPrincipal.txtEntregar.value;
			
			//Para Recibir ORIGEN
			var strOrigen = frmPrincipal.txtOrigen.value;
		
			// Parametros Recarga Virtual DTH
			var strCodPago = frmPrincipal.strCodPago.value;
			var strFechaExpira = frmPrincipal.strFechaExpira.value;
			var strNroRecarga = frmPrincipal.strNroRecarga.value;
			
			var isOffline = frmPrincipal.txtOffline.value;

			var objIframe = document.getElementById("IfrmImpresion");
		
			objIframe.style.visibility = "visible";
			objIframe.style.width = 0;
			objIframe.style.height = 0;
		
			objIframe.src = "OperacionesImp.aspx?codRefer="+ strCodSAP+ "&FactSunat="+strCodSunat+"&TipoDoc="+strTipoDoc + "&strEfectivo=" + strEfectivo + "&strRecibido=" + strRecibido + "&strRecibidoUS=" + strRecibidoUS + "&strEntregar=" + strEntregar + "&strCodPago=" + strCodPago + "&strFechaExpira=" + strFechaExpira + "&strNroRecarga=" + strNroRecarga + "&isOffline=" + isOffline + "&strOrigenVenta=" + strOrigen;			
		}
		//PROY-23700-IDEA-29415 - FIN
		
		</script>
	</HEAD>
	<body>
		<form id="frmPrincipal" method="post" runat="server" style="VISIBILITY:hidden;WIDTH:0px;HEIGHT:0px">
			<table>
				<tr>
					<td>
						<p style="DISPLAY:none">
							<asp:Button id="cmdAnular" runat="server" Text="Anular"></asp:Button><INPUT id="txtRbPagos" type="hidden" name="txtRbPagos" runat="server">
							<asp:textbox id="txtpImp" runat="server"></asp:textbox>
							<asp:TextBox id="txtDocSap" runat="server"></asp:TextBox>
							<asp:TextBox id="txtDocSunat" runat="server"></asp:TextBox>
							<asp:TextBox id="txtNroDG" runat="server"></asp:TextBox>
							<asp:TextBox id="txtTipDoc" runat="server"></asp:TextBox>
							<asp:TextBox id="txtEfectivo" runat="server"></asp:TextBox>
							<asp:TextBox id="txtRecibido" runat="server"></asp:TextBox>
							<!-- DOLARES  -->
							<asp:TextBox id="txtRecibidoUS" runat="server"></asp:TextBox>
							<asp:TextBox id="txtEntregar" runat="server"></asp:TextBox>
							<asp:TextBox id="strCodPago" runat="server"></asp:TextBox>
							<asp:TextBox id="strFechaExpira" runat="server"></asp:TextBox>
							<asp:TextBox id="strNroRecarga" runat="server"></asp:TextBox>
							<!-- Para que envie el Origen -->
							<asp:TextBox id="txtOrigen" runat="server"></asp:TextBox>							
							<asp:TextBox id="txtOffline" runat="server"></asp:TextBox>
						</p>
					</td>
				</tr>
			</table>
			<iframe id="IfrmImpresion" name="IfrmImpresion" src="#" style="VISIBILITY:hidden;WIDTH:0px;HEIGHT:0px">
			</iframe>
		</form>
	</body>
</HTML>
