<%@ Page Language="vb" AutoEventWireup="false" Codebehind="bsqDocumentosFijoPaginas.aspx.vb" Inherits="SisCajas.bsqDocumentosFijoPaginas" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Busqueda de Documentos Cliente Fijo y Páginas</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../estilos/est_General.css" type="text/css" rel="stylesheet">
		<script src="../librerias/Lib_FuncGenerales.js" type="text/javascript"></script>
		<script src="../librerias/Lib_FuncValidacion.js" type="text/javascript"></script>
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<script language="javascript" type="text/javascript">
		
				function onchangeServicio(id)
				{
					if (getValue(id)==getValue('hidCodFactSolesTelefono'))
					{
						setValueHTML('lblIdentificador','Número de Teléfono');
					}
					else
					{
						setValueHTML('lblIdentificador','Código de Cliente');
					}
				}
		
				function validarCampos()
				{
					if (!ValidarCampoHidden('hdnPuntoDeVenta','El Punto de Venta'))
						return false;
					if (!ValidarCampoHidden('hdnUsuario','El Usuario'))
						return false;
					if (!ValidarCampo('ddlServicios','El Tipo de Identificador'))
						return false;
					if (!ValidarCampo('txtIdentificador',getValueHTML('lblIdentificador')))
						return false;
					else {
						//---valida longitud y formato de dato ???
						 
					}
						
					if (!ValidarCampoHidden('hdnBinAdquiriente','El Bin del Adquiriente'))
						return false;
					if (!ValidarCampoHidden('hdnCodComercio','El Código del Comercio'))
						return false;
					//---	
					btn = document.getElementById('btnBuscar'); 
					btn.style.disabled=true;
					
					return true;
				}
				
		</script>
	</HEAD>
	<body vLink="#ceefff" aLink="#ceefff" link="#ceefff">
		<form id="frmRecauda" method="post" runat="server">
			<div id="divContenido" style="MARGIN: 8px">
				<!-- 1ea parte-->
				<div id="divCuerpo" style="BORDER-RIGHT: #336699 2px solid; BORDER-TOP: #336699 2px solid; BORDER-LEFT: #336699 2px solid; WIDTH: 750px; BORDER-BOTTOM: #336699 2px solid">
					<span class="TituloRConsulta" style="MARGIN:8px; WIDTH:100%; HEIGHT:30px; TEXT-ALIGN:center">
						Recaudación&nbsp;Clientes Fijo y Páginas&nbsp;- Búsqueda de Documentos</span>
					<div style="MARGIN:8px; HEIGHT:22px">
						<!-- fila 1 - inicio--><span><LABEL class="Arial12b" id="lblServicios" for="ddlServicios">Servicio</LABEL>:</span>
						<span>
							<asp:dropdownlist id="ddlServicios" runat="server" Width="352px" onchange="javascript:onchangeServicio(this.id);"
								Height="16px" CssClass="clsSelectEnable"></asp:dropdownlist></span><span><label class="Arial12b" id="lblIdentificador" style="WIDTH: 125px; TEXT-ALIGN: right" for="txtIdentificador">Código 
								de Cliente</label>:</span><span>
							<asp:textbox id="txtIdentificador" runat="server" Width="137px" MaxLength="15" CssClass="clsInputEnable"></asp:textbox></span>
						<!-- fila 1 - fin--></div>
					<div style="MARGIN:8px; TEXT-ALIGN:center"><asp:button id="btnBuscar" runat="server" Width="109px" CssClass="BotonOptm" Text="Buscar"></asp:button></div>
				</div>
				<!-- 2da parte-->
				<div id="divPie"><input id="hdnMensaje" style="WIDTH: 8px" type="hidden" name="hdnMensaje" runat="server">
					<!-- Valor que lanza los mensajes -->
					<!-- Atributos de la Página --><input id="hdnPuntoDeVenta" style="WIDTH: 8px" type="hidden" name="hdnPuntoDeVenta" runat="server">
					<input id="hdnUsuario" style="WIDTH: 8px" type="hidden" name="hdnUsuario" runat="server">
					<input id="hdnBinAdquiriente" style="WIDTH: 8px" type="hidden" name="hdnBinAdquiriente"
						runat="server"> <input id="hdnCodComercio" style="WIDTH: 8px" type="hidden" name="hdnCodComercio" runat="server">
					<input id="intCanal" style="WIDTH: 8px" type="hidden" name="intCanal" runat="server">
					<input id="hdnRutaLog" style="WIDTH: 8px" type="hidden" name="hdnRutaLog" runat="server">
					<input id="hdnDetalleLog" style="WIDTH: 8px" type="hidden" size="1" name="hdnDetalleLog"
						runat="server"> <INPUT id="hidCodFactSolesTelefono" style="WIDTH: 8px" type="hidden" name="hidCodFactSolesTelefono"
						runat="server">
				</div>
			</div>
		</form>
	</body>
</HTML>
