<%@ Page Language="vb" AutoEventWireup="false" Codebehind="bsqDocRecargaDTH.aspx.vb" Inherits="SisCajas.bsqDocRecargaDTH" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>bsqDocRecargaDTH</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<meta http-equiv="Cache-Control" content="no-cache">
		<meta http-equiv="Pragma" content="no-cache">
		<meta http-equiv="Expires" content="0">
		<LINK href="../estilos/est_General.css" type="text/css" rel="styleSheet">
		<script language="JavaScript" src="../librerias/Lib_FuncValidacion.js"></script>		
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<script language="javascript">
		
		//                     DNI   CE    RUC   PASP
		var arrayTipoDocPVU = ['01', '04', '06', '07'];
		var arraySizeDocPVU = [ 8  ,   9 ,  11 ,  15 ];
			
		function buscarDocRegargaDTH() {

			var tipoDoc = document.getElementById('ddlTipoDocumento').value == '00' ? '' : document.getElementById('ddlTipoDocumento').value;
			var numeroDoc = document.getElementById('txtNroDocumento').value;
			var codRecarga = document.getElementById('txtIdentificador').value;
			
			// Filtro por Tipo Documento
			if ( tipoDoc == '' ) {
				alert('Error. Debe seleccionar un Tipo de Documento.');
				document.getElementById('ddlTipoDocumento').focus();
				return false;
			}
			// Filtro por Numero Documento
			if ( numeroDoc == '' ) {
				alert('Error. Debe ingresar un Número de Documento.');
				document.getElementById('txtNroDocumento').focus();
				return false;
			}
			// Filtro por Codigo de Recarga
			if ( codRecarga == '' ) {
				alert('Error. Debe ingresar el Código de Recarga.');
				document.getElementById('txtIdentificador').focus();
				return false;
			}
			// Validar longitud
			var indexOf = getIndexOf(arrayTipoDocPVU, document.getElementById('ddlTipoDocumento').value);
			if (arrayTipoDocPVU[indexOf] == "01" || arrayTipoDocPVU[indexOf] == "04" || arrayTipoDocPVU[indexOf] == "06") {
				if ( document.getElementById('txtNroDocumento').value.length != arraySizeDocPVU[indexOf]) {
					alert('Verifique longitud del Número de Documento.');
					document.getElementById('txtNroDocumento').focus();
					return false;
				}
			}

			document.getElementById('hidTipoDoc').value = tipoDoc;
			document.getElementById('hidNumeroDoc').value = numeroDoc;
			return true;
		}
		
		function cambiarNroDoc(valor) {
			
			document.getElementById('txtNroDocumento').value = '';
			
			if ( valor == '00' ) {
				document.getElementById('ddlTipoDocumento').focus();
			} else {
				var indexOf = getIndexOf(arrayTipoDocPVU, valor);
				document.getElementById('txtNroDocumento').maxLength = arraySizeDocPVU[indexOf];

				// Validar Ingreso Numerico solo para los Casos DNI y RUC
				if (valor == '01' || valor == '06') {
					document.getElementById('txtNroDocumento').onkeydown = function() { validarNumero(event) };
				} else { //CE
					document.getElementById('txtNroDocumento').onkeydown = function() { validarAlfaNumerico(event) };
				}
			}
		}

		function OnchangeDefecto() {
			var valor = document.getElementById('ddlTipoDocumento').value;
			var indexOf = getIndexOf(arrayTipoDocPVU, valor);
			document.getElementById('txtNroDocumento').maxLength = arraySizeDocPVU[indexOf];
		}
		
		function validarNumero(event) {
			if (event.keyCode == 8 || event.keyCode == 46) {
				return;
			}
			if (event.keyCode >= 37 && event.keyCode <= 40) { // Allow directional arrows
				return;
			}
			eventoSoloNumeros(event);
		}		

		function validarAlfaNumerico(event) {
			if (event.keyCode == 8 || event.keyCode == 46) {
				return;
			}
			if (event.keyCode >= 37 && event.keyCode <= 40) { // Allow directional arrows
				return;
			}
			eventoAlfaNumerico(event);
		}

		function eventoSoloNumeros(){
			// NOTE: Backspace = 8, Enter = 13, '0' = 48, '9' = 57		
			var key = event.keyCode;	
			if ((key <= 13 || (key >= 48 && key <= 57) || (key >= 96 && key <= 105) || (key == 33) || (key == 34) || (key == 35) || (key == 36)|| (key == 37) || (key == 38) || (key == 39) || (key == 40)|| (key == 45) || (key == 46) || (key == 86) || (key == 67)|| (key == 88))==true)
				event.returnValue = true;
			else
				event.returnValue = false;	
		}				

		function eventoAlfaNumerico(event) {
			if( (event.keyCode>=65 && event.keyCode<=90) ) {
				event.returnValue = true;
			}
			else {
				eventoSoloNumeros(event);
			}
		}

		function getValue(id){
			var c = document.getElementById(id);
			if (c != null ) return Trim(c.value);
			return '';
		}
		
		function getIndexOf(aElementos, elemento) {
			var i;
			for (i = 0; i < aElementos.length; i++) {
				if (aElementos[i] == elemento) {
					return i;
				}
			}
			return -1;
		}
		
		function Inicio() {
			OnchangeDefecto();
			
			var mensajeError;
			mensajeError = document.getElementById('hidMensaje').value;
			if ( mensajeError != "" ) {
				alert(mensajeError);
			}
			document.getElementById('hidMensaje').value = "";
		}
		</script>
	</HEAD>
	<body onload="Inicio();">
		<form id="frmBsqDocRecargaDTH" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="750" border="0">
				<tr>
					<td>
						<table class="tabla_borde" cellSpacing="0" cellPadding="0" width="750" align="center">
							<tr>
								<td class="TituloRConsulta" vAlign="middle" align="center" colSpan="10" height="40">Recaudación 
									- Recarga Virtual DTH</td>
							</tr>
							<tr>
								<td colSpan="10" height="15">&nbsp;</td>
							</tr>
							<tr>
								<td class="Arial12b" width="15">&nbsp;</td>
								<td class="Arial12b" width="130">Tipo de Documento:</td>
								<td class="Arial12b" width="150"><asp:dropdownlist id="ddlTipoDocumento" runat="server" Width="150px" CssClass="Arial11B">
										<asp:ListItem Value="00" Selected="True">--SELECCIONAR--</asp:ListItem>
										<asp:ListItem Value="01">DNI</asp:ListItem>
										<asp:ListItem Value="04">Carnet Extranjeria</asp:ListItem>
										<asp:ListItem Value="06">RUC</asp:ListItem>
										<asp:ListItem Value="07">Pasaporte</asp:ListItem>
									</asp:dropdownlist></td>
								<td class="Arial12b" width="15">&nbsp;</td>
								<td class="Arial12b" width="120">Nro. Documento:</td>
								<td class="Arial12b" width="100"><asp:textbox id="txtNroDocumento" runat="server" Width="100px" CssClass="clsInputEnable" MaxLength="15" onpaste="return false;"></asp:textbox></td>
								<td class="Arial12b" width="15">&nbsp;</td>
								<td class="Arial12b" width="120">Código Recarga:</td>
								<td class="Arial12b" width="100"><asp:textbox id="txtIdentificador" runat="server" Width="100px" CssClass="clsInputEnable" MaxLength="15" onpaste="return false;"></asp:textbox></td>
								<td class="Arial12b" width="15">&nbsp;</td>
							</tr>
							<tr>
								<td colSpan="10" height="20">&nbsp;</td>
							</tr>
							<tr>
								<td class="Arial12b" align="center" colSpan="10"><asp:button id="btnBuscar" style="CURSOR: hand" runat="server" Width="100" CssClass="BotonOptm"
										Text="Buscar" Height="19"></asp:button></td>
							</tr>
							<tr>
								<td colSpan="10" height="10">&nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<input id="hidMensaje" type="hidden" name="hidMensaje" value="" runat="server">
			<input id="hidTipoDoc" type="hidden" name="hidTipoDoc" value="" runat="server">
			<input id="hidNumeroDoc" type="hidden" name="hidNumeroDoc" value="" runat="server">
		</form>
	</body>
</HTML>
