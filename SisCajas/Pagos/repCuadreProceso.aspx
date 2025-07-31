<%@ Page Language="vb" AutoEventWireup="false" Codebehind="repCuadreProceso.aspx.vb" Inherits="SisCajas.repCuadreProceso" %>
<HTML>
	<HEAD>
		<title>Aplicativo PVU</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../estilos/est_General.css" type="text/css" rel="styleSheet">
		<script language="JavaScript" src="../librerias/date-picker.js"></script>
		<script language="JavaScript" src="../librerias/Lib_FuncValidacion.js"></script>
		<script language="JavaScript" src="../librerias/Lib_FuncGenerales.js"></script>
		<!--PROY-27440 INI-->
		<script language="JavaScript" src="../librerias/msrsclient.js"></script>
		<script language="JavaScript" src="../Scripts/jquery-1.1.js"></script>
		<script language="JavaScript" src="../Scripts/form.js"></script>
		<script language="JavaScript" src="../Scripts/xml2json.js"></script>
		<script language="JavaScript" src="../Scripts/operacionPOS.js"></script>
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<!--PROY-27440 FIN-->
		<script type="text/javascript">
		// INICIO JYMMYT
		function proceSar() {
            var rpta = window.confirm('Desea continuar con el cuadre?');
            if (rpta) {
                frmPrincipal.procesarHandler.click();
            }
        }
			// FIN JYMMYT
		
			//PROY-27440 INI
	var varArrayEstTrans;
	var serverURL =  '../Pos/ProcesoPOS.aspx';
	var webServiceURL = '<%= System.Configuration.ConfigurationSettings.AppSettings("consWsLocal_Url") %>';
	var timeOutWsLocal = '<%= System.Configuration.ConfigurationSettings.AppSettings("consWsLocal_Url_TimeOut") %>';
	var varTipoOperacion;
	var tiposError = 'EXC_';
	var varTipTarjeta;
	var varNameDoc;
	var varMoneda;
	var varTransMonto;
	var varTipoPos;
	var varValueTar;
	var varMonedaVisa;
	var varNomEquipoPOS = '';
	var varEstadoMov;
	var varMsgRsptMov;
	var varIntAutPosVISA;
	var varIntAutPosMC;
	
	//Mastercard
	var varTransMC;
	var varMonedaMC;
	var varApliMC;
	var varNroRefMC;
	var varMontoMC;
	var varPwdComercioMC = '';
	var varTipoOpeFiMC = '';
	
	//VisaNet
	var varTransVisa;
	var varMonedaVisa;
	var varNroRefVisa;
	var varMontoVisa;
	
	var varNroRegistro;
	var varNroTienda;
	var varCodEstablec;
	var varCodigoCaja;
	var varNomPcPos;
	var varCodTerminal;
	var varIpPos;
	var varIdRefAnu = '';
	
	var varTipoTrans;
	var vContVisa= 1;
	var vContMC = 1;
	var varExitoVisa=''; //0 EXITO - 1 ERROR
	var varExitoMC=''; //0 EXITO - 1 ERROR
		
	//CR//
	var varNroPedido = '';
	var varNroTelefono = '';
	var varContadorPagos = 0;
	var strIntegracionVISA = '';
	var strIntegracionMC = '';
	var varContCuadre = 1;
	var varTipoTarjeta = '';
	var varMonedaOpe = '';
	var varTipoPago = '';
	var varEstadoTrans = '';
	var varIdCabecera = '';
	var varNroTarjeta = '';
	var varNroRef = '';
	var varNroRefAnul = '';
	var varArrayTipoTran='';
	var varArrayTipoPOS='';
	var varArrayDatoPosVisa = '';
	var varArrayDatoPosMC = '';
	var varDesRpta = '';
	var flagExecMC = 0;
	var sCodRespTarj = '';
	//CR//
	
	//PROY-27440 FIN
	function CerrarLotePOS()
	{
	  strIntegracionVISA = document.getElementById("HidIntVisa").value;
	  strIntegracionMC = document.getElementById("HidIntMC").value;
	  varContCuadre = document.getElementById("HidIntentosCuadre").value;
	  varTipoTarjeta = document.getElementById("HidTipoTarjeta").value.split("|"); //0->VISA //1->MC
	 

	 if (strIntegracionVISA == '1' && strIntegracionMC == '0')
	 {
		if(document.getElementById("vContVisa").value <= varContCuadre)
	    {	       
			GenerarTransaccionVisa();	
		}
		else
		{
			alert('Por favor realizar el cierre el POS desde Operaciones No Financieras');
			document.getElementById("vContVisa").value = 1;
			document.getElementById('btnGrabarOculto').click();
		return;	
		}
	 }	 
	 else if (strIntegracionVISA == '0' && strIntegracionMC == '1')
	 {
	    if(document.getElementById("vContMC").value <= varContCuadre)
	    {
			GenerarTransaccionMC();
	    }
	    else
		{
			alert('Por favor realizar el cierre el POS desde Operaciones No Financieras');
			document.getElementById("vContMC").value = 1;
			document.getElementById('btnGrabarOculto').click();
			return;	
		}
		
	 }	 
	 else if (strIntegracionVISA == '1' && strIntegracionMC == '1')
	 {
	    if(document.getElementById("vContVisa").value <= varContCuadre)
	    {
			GenerarTransaccionVisa();
			flagExecMC = 1;	
		}
	    else
		{
			alert('Por favor realizar el cierre los POS desde Operaciones No Financieras');
			document.getElementById("vContVisa").value = 1;
			document.getElementById('btnGrabarOculto').click();
			return;	
		}
	 }
	
	}
	function GenerarTransaccionVisa()
	{		
		f_EnvioPOS(varTipoTarjeta[0]); // VISA	
	}
	
	function GenerarTransaccionMC()
	{				
		f_EnvioPOS(varTipoTarjeta[1]); // MASTERCARD	
	}
	
	function f_EnvioPOS(varTipoTar)
	{	
		varArrayTipoTran=document.getElementById("HidTipoTran").value.split("|");
		varArrayTipoPOS=document.getElementById("HidTipoPOS").value.split("|");
		varArrayEstTrans=document.getElementById("HidEstTrans").value.split("|");
		varArrayDatoPosVisa = document.getElementById("HidDatoPosVisa").value.split("|");
		varArrayDatoPosMC = document.getElementById("HidDatoPosMC").value.split("|");
		
		varCodOpe = ''; 
		varDescriOpe = '';
		varNameTipoPOS='';
		varNameDoc=''
		varNroRegistro = ''; 
		varNroTienda = '';
		varCodigoCaja = '';
		varCodEstablec = '';
		varNomPcPos = '';
		varCodTerminal = '';
		varIpPos = '';
		
		switch (varTipoTar)
		{
			case varTipoTarjeta[0]://VISA
				varTipoPos= varArrayTipoPOS[0];
				varValueTar = 'VIS';
				varTipoOperacion = document.getElementById("HidTipoOpera").value;
				
				var varArrayMonedaVisa = document.getElementById("HidMonedaVisa").value.split("|");					
				varMonedaOpe = varArrayMonedaVisa[0];//SOLES VISA
				
				varNroRegistro = varArrayDatoPosVisa[0].substr(varArrayDatoPosVisa[0].indexOf("=")+1);
				varNroTienda = varArrayDatoPosVisa[1].substr(varArrayDatoPosVisa[1].indexOf("=")+1);
				varCodigoCaja = varArrayDatoPosVisa[2].substr(varArrayDatoPosVisa[2].indexOf("=")+1);
				varCodEstablec = varArrayDatoPosVisa[3].substr(varArrayDatoPosVisa[3].indexOf("=")+1);
				varNomPcPos = varArrayDatoPosVisa[4].substr(varArrayDatoPosVisa[4].indexOf("=")+1);
				varCodTerminal = varArrayDatoPosVisa[6].substr(varArrayDatoPosVisa[6].indexOf("=")+1);
				varIpPos = varArrayDatoPosVisa[7].substr(varArrayDatoPosVisa[7].indexOf("=")+1);
				
				break;
			case varTipoTarjeta[1]://MC			
				varTipoPos= varArrayTipoPOS[1];
				varValueTar = 'MCD';
				
				varTipoOperacion = document.getElementById("HidTipoOperaMC").value;	
							
				var varArrayMonedaMC = document.getElementById("HidMonedaMC").value.split("|");					
				varMonedaOpe = varArrayMonedaMC[0];//SOLES MC
				
				varNroRegistro = varArrayDatoPosMC[0].substr(varArrayDatoPosMC[0].indexOf("=")+1);
				varNroTienda = varArrayDatoPosMC[1].substr(varArrayDatoPosMC[1].indexOf("=")+1);
				varCodigoCaja = varArrayDatoPosMC[2].substr(varArrayDatoPosMC[2].indexOf("=")+1);
				varCodEstablec = varArrayDatoPosMC[3].substr(varArrayDatoPosMC[3].indexOf("=")+1);
				varNomPcPos = varArrayDatoPosMC[4].substr(varArrayDatoPosMC[4].indexOf("=")+1);
				varCodTerminal = varArrayDatoPosMC[6].substr(varArrayDatoPosMC[6].indexOf("=")+1);
				varIpPos = varArrayDatoPosMC[7].substr(varArrayDatoPosMC[7].indexOf("=")+1);
				
				break;
		}

		varTipTarjeta = varTipoTar;//VISA Y MC
		varMonto = '0.00';	
		varMoneda=document.getElementById("HidTipoMoneda").value;		
	    varEstadoTrans= varArrayEstTrans[0];//PENDIENTE
	    varIdCabecera = document.getElementById("HidIdCabez").value;
	    varApliMC = document.getElementById("HidApliPOS").value;
		varTipoTrans = varArrayTipoTran[0] ;//CIERRE		
		
	    varTramaInsert = '';
		varNroTarjeta = '';
		varNroRef = '';
		varTipoPago = '';

		varTramaInsert = 'codOperacion=' + varCodOpe + 
		'|desOperacion=' + varDescriOpe + 
		'|tipoOperacion=' + varTipoOperacion + 
		'|montoOperacion=' + varMonto + 
		'|monedaOperacion=' + varMoneda + 
		'|tipoTarjeta=' + varTipTarjeta + 
		'|tipoPago=' + varTipoPago + 
		'|estadoTransaccion=' + varEstadoTrans + 
		'|tipoPos=' + varTipoPos + 
		'|tipoTransaccion=' + varTipoTrans + 
		'|ipCaja=' + varIpPos+ 
		'|NroRegistro=' + varNroRegistro + 
		'|NroTienda=' + varNroTienda + 
		'|CodigoCaja=' + varCodigoCaja +
		'|CodEstablec=' + varCodEstablec + 
		'|NomPcPos=' + varNomPcPos + 
		'|CodTerminal=' + varCodTerminal +
		'|IdCabecera=' + varIdCabecera +
		'|nroTarjeta=' + varNroTarjeta + 
		'|nroRef=' + varNroRef;		

		//1 - PENDIENTE	
		RSExecute(serverURL,"GuardarTransaction",varTramaInsert,
		varNroTelefono,varNroPedido,CallBack_GuardarTransaction,GuardarTransactionError,"X");
		
	}
	
	function CallBack_GuardarTransaction(response) 
	{
		
		var varTramaUpdate='';
		var varRpta = response.return_value;
		
		var res = (varRpta.replace("<BODY><SELECT>", "")).replace("</SELECT>", "").replace("\r\n","");
		
		varRpta = res;
		
		if(varRpta.substr(0,1) == '0'){					
		
			var varArrayRpta = varRpta.split("|");
			var varIdTran = varArrayRpta[2];			
			var varFlagPago = '1';
			varIdCabecera = varArrayRpta[3];
			document.getElementById("HidIdCabez").value = varIdCabecera;
			//2 - EN PROCESO			
	
			var varNumVoucher='';
			var varNumAutTransaccion='0';
			var varCodRespTransaccion='';
			var varDescTransaccion='';
			var varCodAprobTransaccion='0';
			var varNroTarjeta='';
			var varFechaExpiracion = '';
			var varNomCliente = '';
			var varImpVoucher = '';
			var varSeriePOS = '';
			var nomEquipoPOS = '';
			var varNroPedido = ''; 
			var varIdUnicoTrans = '';
			
			varEstadoTrans = '';
			varEstadoTrans = varArrayEstTrans[1];//EN PROCESO			
			varTramaUpdate = '';
				
			
			varTramaUpdate =
			'monedaOperacion=' + varMoneda + 
			'|montoOperacion=' + varMonto + 
			'|nroRegistro=' + varNroRegistro + 
			'|numVoucher=' + varNumVoucher + 
			'|numAutTransaccion=' + varNumAutTransaccion + 
			'|codRespTransaccion=' + varCodRespTransaccion + 
			'|descTransaccion=' + varDescTransaccion + 
			'|codAprobTransaccion=' +  varCodAprobTransaccion + 
			'|tipoPos=' + varTipoPos + 
			'|varNroTarjeta=' + varNroTarjeta + 
			'|fechaExpiracion=' + varFechaExpiracion + 
			'|nomCliente=' + varNomCliente + 
			'|impVoucher=' + varImpVoucher + 
			'|seriePOS=' + varSeriePOS + 
			'|nomEquipoPOS=' + nomEquipoPOS +
			'|estadoTransaccion=' + varEstadoTrans +
			'|NroTienda=' + varNroTienda + 
			'|CodigoCaja=' + varCodigoCaja + 
			'|CodEstablec=' + varCodEstablec + 
			'|IpPos=' + varIpPos + 
			'|IdCabez=' + varIdCabecera +
			'|FlagPago=' + varFlagPago + 
			'|Pedido=' + varNroPedido + 
			'|IdUnico=' + varIdUnicoTrans +
			'|TipoTrans=' + varTipoTrans + 
			'|IdRefAnulador=' + varIdRefAnu +
			'|TipoPago='+
			'|ResTarjetaPos=';//Proy-31949-Inicio; ;                        
                   
						
			RSExecute(serverURL,"ActualizarTransaction",varTramaUpdate,varIdTran,CallBack_ActualizarTransaction,GuardarTransactionError,"X");
			
			objEntityPOS=
			{
				monedaOperacion: '', 
				montoOperacion: '', 
				CodigoTienda: varNroTienda, 
				NroPedido: '',
				ipAplicacion: '', 
				nombreAplicacion: '', 
				usuarioAplicacion: '',
				TrnsnId: varIdTran,
				tipoPos: '',
				CodigoCaja: varCodigoCaja
			};
				
			if (varValueTar == 'VIS'){//VISA
				CallService(varTipTarjeta,varNameDoc,objEntityPOS);
			}
			else if(varValueTar=='AMX' || varValueTar=='MCD' || varValueTar=='DIN'){//MASTERCARD
				CallService(varTipTarjeta,varNameDoc,objEntityPOS);
			}
		}
		else {
			alert('Error al registrar la transaccion en estado PENDIENTE');
			document.getElementById("vContVisa").value = document.getElementById("vContVisa").value*1 + 1;
			document.getElementById("vContMC").value = document.getElementById("vContMC").value*1 + 1;
			return;
		}
	}
	
	
	function CallService(tipoPOS,NameDoc,objEntityPOS)
	{
		varBolWsLocal = false;
		var entityOpe;
		var soapMSG;
		var soapDataUpdate;
		var varNroTarjeta;
			
		var varFechaExpiracion;
		var varSeriePOS;
		
				
		var VarTrnsnId='';
		var VarToday='';	
		var varNroReferencia = '';
		
		varTipOpePOS = '';
		varEstTran = '';
		
		//Variables de auditoria Ini		
		var varArrayAudi = document.getElementById("HidDatoAuditPos").value.split("|");
		var varNroPedido = '';
		var VarToday = new Date();
		var varIdTransaccion = '' + '_' + formatDate(VarToday);
		var varIpApplicacion = varArrayAudi[0];
		var varNombreAplicacion = varArrayAudi[1];
		var varUsuarioAplicacion = varArrayAudi[2];
		//Variables de auditoria Fin
		
    
        switch (tipoPOS) {
			case varTipoTarjeta[0]: //VISANET
				
				        entityOpe = {
				        TipoOperacion: varTipoOperacion, 
				        SalidaMensaje: '', 
				        RutaArchivoINI: '', 
						TipoMoneda: '',
						Monto: '', 
						CodigoTienda: objEntityPOS.CodigoTienda,
					    CodigoCaja: objEntityPOS.CodigoCaja, 
						Empresa: '',
						Funcion: '',
					    TipoPS: '', 
					    CapturaTarjeta: '',
						Cuotas: '', 
						Diferido: '', 
						Nombre: '', 
						Valor: '',
						IdTransaccion: varIdTransaccion,
						IpApplicacion: varIpApplicacion,
						NombreAplicacion: varNombreAplicacion,
						UsuarioAplicacion: varUsuarioAplicacion
						};
        
        soapMSG = f_data_VisaNet(entityOpe);
        
        $.ajax({
          url: webServiceURL + '?op=peticionOperacionVisaNet',
          type: "POST",
          dataType: "text",
          data: soapMSG,
          processData: false,
          contentType: "text/xml; charset=\"utf-8\"",
          async: true,
          cache: false,
          success: function (objResponse, status) {
						/*Inicio success*/
						SuccessVisaNet(objResponse,NameDoc);	
						
						if (varEstTran != varArrayEstTrans[2])
						{
							if(document.getElementById("vContVisa").value < varContCuadre)
							{
								document.getElementById("vContVisa").value = document.getElementById("vContVisa").value*1 + 1;
								var r = confirm("Error al Intentar Ejecutar Cierre VISA, Reintentar?");

								if (r == true) 
								{
									CallService(tipoPOS,NameDoc,objEntityPOS)
								}				
								else 
								{   
									if (flagExecMC == 1)
									{
									    alert('Por favor realizar el cierre de VISA desde Operaciones No Financieras');
									    document.getElementById("vContMC").value = 1;
										GenerarTransaccionMC();							
									}
									else
									{
									    alert('Por favor realizar el cierre de VISA desde Operaciones No Financieras');
									    document.getElementById("vContVisa").value = 1;
										document.getElementById('btnGrabarOculto').click();
										return;									
									}
									
								}
							}
							else
							{   
							
								if (flagExecMC == 1)
								{
								    alert('Se Supero el Maximo de Intentos Permitidos , Por favor realizar el cierre de VISA desde Operaciones No Financieras');
									document.getElementById("vContMC").value = 1;
									GenerarTransaccionMC();							
								}
								else
								{
									alert('Se Supero el Maximo de Intentos Permitidos , Por favor realizar el cierre de VISA desde Operaciones No Financieras');
									document.getElementById("vContVisa").value = 1;
									document.getElementById('btnGrabarOculto').click();
									return;									
								}
									
														
							}

						}
						else
						{						
							if (flagExecMC == 1)
							{
								
								document.getElementById("vContMC").value = 1;
								GenerarTransaccionMC();							
							}
							else
							{	
							    document.getElementById("vContVisa").value = 1;	
								document.getElementById('btnGrabarOculto').click();
								return;
							}
						}						
						
						/*Fin success*/
          },
			error: function (request, status) {
			/*Inicio Error*/
			varBolWsLocal = true;
			alert('Sin respuesta del POS, tiempo de espera superado.');
			document.getElementById("vContVisa").value = document.getElementById("vContVisa").value*1 + 1;
			
			return;			
			/*Fin Error*/
          },
          timeout: Number(timeOutWsLocal)
        });        
        return true;
				break;
			case varTipoTarjeta[1]: //MASTER CARD
				
				varTransMontoMC = Number(objEntityPOS.montoOperacion).toFixed(2);

				entityOpe = { 
				IdTransaccion: varIdTransaccion,
				IpApplicacion: varIpApplicacion,
				NombreAplicacion: varNombreAplicacion,
				UsuarioAplicacion: varUsuarioAplicacion,
				Aplicacion: varApliMC, 
				Transaccion: varTipoOperacion, 
				Monto: '', 
				TipoMoneda: '',
				DataAdicional: '', 
				CodigoServicio: '', 
				ClaveComercio: '',
				Dni: '',
			    Ruc: '',
			    Producto: '', 
				OpeMonto: '',
			    Nombre: '',
			    Valor: ''
				};
				soapMSG = f_data_MC(entityOpe);

				$.ajax({
				url: webServiceURL + '?op=peticionOperacionMC',
				type: "POST",
				dataType: "text",
				data: soapMSG,
				timeout: timeOutWsLocal,
				processData: false,
				contentType: "text/xml; charset=\"utf-8\"",
				async: false,
				cache: false,
				success: function (objResponse, status) {

				SuccessMasterCard(objResponse,NameDoc);					
						
						if (varEstTran != varArrayEstTrans[2])
						{
							if(document.getElementById("vContMC").value < varContCuadre)
							{
								document.getElementById("vContMC").value = document.getElementById("vContMC").value*1 + 1;
								
								var r = confirm("Error al Intentar Ejecutar Cierre MC, Reintentar?");

								if (r == true) 
								{
									CallService(tipoPOS,NameDoc,objEntityPOS)
								}				
								else 
								{   
									    alert('Por favor realizar el cierre de MC desde Operaciones No Financieras');
									    document.getElementById("vContMC").value = 1;
									    document.getElementById("vContVisa").value = 1;
										document.getElementById('btnGrabarOculto').click();
										return;								
																	
								}
							}
							else
							{   
									alert('Se Supero el Maximo de Intentos Permitidos , Por favor realizar el cierre de MC desde Operaciones No Financieras');
									document.getElementById("vContMC").value = 1;
									document.getElementById("vContVisa").value = 1;
									document.getElementById('btnGrabarOculto').click();
									return;						
										
							}

						}
						else
						{	
						    document.getElementById("vContMC").value = 1;
						    document.getElementById("vContVisa").value = 1;	
							document.getElementById('btnGrabarOculto').click();
							return;							
						}						
						
						/*Fin success*/		

				},
				error: function (request, status) {
				varBolWsLocal = true;
				alert('Sin respuesta del POS, tiempo de espera superado.');
				document.getElementById("vContMC").value = document.getElementById("vContMC").value*1 + 1;
				return;

				},
				timeout: Number(timeOutWsLocal)
				});
				return true;
				break;
		}
	}
	
	function CallBack_ActualizarTransaction(response) 
	{
		var varRpta = response.return_value;
		
		var res = (varRpta.replace("<BODY><SELECT>", "")).replace("</SELECT>", "").replace("\r\n","");
		
		varRpta = res;
		
		var ArrayRpta  = varRpta.split("|");
		
		varMsgRsptMov = ArrayRpta[1];
			
	}
	
	function CallBack_GuardarTransactionCierrePOS(response) 
	{
		var varRpta = response.return_value;
		
		var res = (varRpta.replace("<BODY><SELECT>", "")).replace("</SELECT>", "").replace("\r\n","");
		
		varRpta = res;
		
	
	}		
	
	function GuardarTransactionCierrePOSError(co) 
	{
		if (co.message) {
			alert("Context:" + co.context + "\nError: " + co.message);
		}
	}
	
	function GuardarTransactionError(co) 
	{
		if (co.message) {
			alert("Context:" + co.context + "\nError: " + co.message);
		}
	}

	function SuccessVisaNet(objResponse,NameDoc)
	{
		try
		{
			var x2js = new X2JS();
			var jsonObj = x2js.xml_str2json(objResponse);
			var varImpVoucher = '';
		    varDesRpta = '';
			varNroTarjeta = '';
			varNroReferencia = '';	
			var varNumAutTransaccion = '';
			varFechaExpiracion = '';
			var varCodOperVisa = '';
			var varTramaUpdate = '';
			var varCodRptaAudit = '';
			var varMsjRptaAudit = '';
			var varCodRptaWs = '';
			var varMsgAlert = '';
			var varPrintData = '';
			var varNroPedido = ''; 
			var varIdUnicoTrans = '';
			var varFlagPago = '1';
			var varIdCabez = document.getElementById("HidIdCabez").value;
			
			sCodRespTarj = '';
			varSeriePOS = '';
			var varCodResPos = ''

			varCodRptaAudit = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.AuditResponse.CodigoRespuesta;
			varMsjRptaAudit = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.AuditResponse.mensajeRespuesta;			

			if ( varCodRptaAudit != '0' ) 
			{				
				if (typeof jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.ImprimirData == 'undefined')
				{
					varImpVoucher = '';
					varDesRpta = varMsjRptaAudit;
				}							
				else
				{
					varImpVoucher = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.ImprimirData;
					varDesRpta = varImpVoucher;
				}	

				varEstTran = varArrayEstTrans[3];//RECHAZADO

				
			}
			else
			{
				varCodResPos = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.CodigoRespuesta;
				
				if(varCodResPos != '0')
				{
					varDesRpta = String(jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.DescripcionRespuesta);
					varImpVoucher = String(jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.ImprimirData);;
					varEstTran = varArrayEstTrans[3];//RECHAZADO
					sCodRespTarj = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.CodigoRespuesta;
					
								
				}
				else
				{
					sCodRespTarj = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.CodigoRespuesta;
					varDesRpta = String(jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.DescripcionRespuesta);
					varNroTarjeta = '';
					varNroReferencia = '';
					varNumAutTransaccion = '';
					varFechaExpiracion = '';
					sCodRespTarj = '';
					varCodOperVisa = '';
					varSeriePOS = '';
					varImpVoucher = '';
					varImpVoucher = String(jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.ImprimirData);
					varNroReferencia = '';
					varIdUnicoTrans = '';				

					varEstTran = varArrayEstTrans[2];//ACEPTADO
					varExitoVisa='0'; //EXITO VISA
								
				}
				
				
			}
						
			if ( varImpVoucher != null )
			{
				if (String(varImpVoucher).length > 50)
					varImpVoucher = String(varImpVoucher).substring(1, 18);
				else
					varImpVoucher = String(varImpVoucher);
			}
			else
			{
				varImpVoucher = '';
			}

			varEstadoMov = varEstTran;
			
		
			varTramaUpdate = '';
			
			varTramaUpdate = 
			'monedaOperacion=' + varMoneda + 
			'|montoOperacion=' + varMonto + 
			'|nroRegistro=' + varNroRegistro + 
			'|numVoucher='  + 
			'|numAutTransaccion=' + varNumAutTransaccion + 
			'|codRespTransaccion=' + sCodRespTarj + 
			'|descTransaccion=' + varDesRpta + 
			'|codAprobTransaccion='  + 
			'|tipoPos=' + varTipoPos + 
			'|varNroTarjeta='  + 
			'|fechaExpiracion='  + 
			'|nomCliente='  + 
			'|impVoucher=' + varImpVoucher + 
			'|seriePOS=' + varSeriePOS + 
			'|nomEquipoPOS=' + varNomEquipoPOS + 
			'|estadoTransaccion=' + varEstTran +
			'|NroTienda=' + varNroTienda + 
			'|CodigoCaja=' + varCodigoCaja + 
			'|CodEstablec=' + varCodEstablec + 
			'|IpPos=' + varIpPos + 
			'|IdCabez=' + varIdCabez +
			'|FlagPago=' + varFlagPago + 
			'|Pedido=' + 
			'|IdUnico=' +
			'|TipoTrans=' + varTipoTrans + 
			'|IdRefAnulador='  +
			'|TipoPago='+
			'|ResTarjetaPos=';//Proy-31949-Inicio;
			
			if (varEstTran != varArrayEstTrans[2])
			{			
				alert(varDesRpta);
								
			}
			
			var varRpta = RSExecute(serverURL,"ActualizarTransaction",varTramaUpdate,
			objEntityPOS.TrnsnId);

			ActualizaMovimiento(varEstTran);

		}
		catch(err) {
		    document.getElementById("vContVisa").value = document.getElementById("vContVisa").value*1 + 1;
			alert(err.description);
		}	
	}
	
	function ActualizaMovimiento(strRspt)
	{
		
		var varTramaInsertMov='';
		var varRpta = strRspt;

		var res = (varRpta.replace("<BODY><SELECT>", "")).replace("</SELECT>", "").replace("\r\n","");

		varRpta = res;

		var ArrayRpta  = varRpta.split("|");

		var varTipoMoc = "2"; //CIERRE

		varTramaInsertMov = 'varTipoMoc=' + varTipoMoc + '|varEstadoMov=' + varEstadoMov + 
		'|msgRspt=' + varDesRpta + 
		'|varNroRegistro=' + varNroRegistro;

		RSExecute(serverURL,"GuardarTransactionApeCiePOS",varTramaInsertMov
		,CallBack_GuardarTransactionCierrePOS,GuardarTransactionCierrePOSError,"X");
		
	}
	
	function trim1 (string) {
		return string.replace(/^\s\s*/, '').replace(/\s\s*$/, '');
	}
	
	function SuccessMasterCard(objResponse,NameDoc)
	{
		try{
			var x2js = new X2JS();
			var jsonObj = x2js.xml_str2json(objResponse);
			
			var varImpVoucher = '';
			var varDesRpta = '';
			var varCliente = '';
			var varNumAutTransaccion = '';
			var varCodOper = '';
			var varTramaUpdate = '';
			var varCodRptaAudit = '';
			var varMsjRptaAudit = '';
			var varCodRptaWs = '';
			var varMsgAlert = '';
			var varPrintData = '';
			var varNroReferencia = '';
			var varFechaExpiracion = '';
			var varNroPedido = ''; 
			var varIdUnicoTrans = '';
			var varFlagPago = '1';
			var varIdCabez = document.getElementById("HidIdCabez").value;
			var varSeriePOS = '';
			
			varCodRptaAudit = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.AuditResponse.CodigoRespuesta;
			varMsjRptaAudit = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.AuditResponse.mensajeRespuesta;	
			
			if ( varCodRptaAudit != '0' ) 
			{
				varDesRpta = varMsjRptaAudit;
				varEstTran = varArrayEstTrans[3];//RECHAZADO
				
			}
			else
			{
			
				varCodResPos = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.CodigoRespuesta;
				
				if(varCodResPos != '00')
				{	
				    sCodRespTarj = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.CodigoRespuesta;			
					varDesRpta = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.DescripcionRespuesta;					
					varImpVoucher = '';
					varEstTran = varArrayEstTrans[3];//RECHAZADO
				}
				else
				{
					varIdUnicoTrans = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.CodigoAdquiriente;
				
					/*Ouput Visa Ini(Venta&Anulacion)*/
					/*CodigoAprobacion*/
					sCodRespTarj = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.CodigoRespuesta;
					varDesRpta = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.DescripcionRespuesta;				  
					varNumAutTransaccion = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.NumeroAutorizacion;					
					varNroRefMC = String(jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.NumeroReferencia);
					varNroRefMC = (varNroRefMC == null) ? '' : String(varNroRefMC).replace("REF","");
					varNroRefMC = trim1(varNroRefMC);
					varNroTarjeta = '';
					varFechaExpiracion = '';
					varCodOper = '';
					varSeriePOS = '';
					varImpVoucher = '';
					varImpVoucher = String(jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.ImprimirData);
					varNroReferencia = varNroRefMC;
					
					varEstTran = varArrayEstTrans[2];//ACEPTADO
				
				}				
				
				if ( varImpVoucher != null )
				{
					if (String(varImpVoucher).length > 50)
						varImpVoucher = String(varImpVoucher).substring(1, 20);
					else
						varImpVoucher = String(varImpVoucher);
				}
				else
				{
					varImpVoucher = '';
				}
			
			}
			
			
			varMontoOperacion = ''; varMontoOperacion = objEntityPOS.montoOperacion;
			varNomEquipoPOS = ''; varNomEquipoPOS = varNomPcPos;
		
			varTramaUpdate = '';
			varTramaUpdate = 
			'monedaOperacion=' + varMoneda + 
			'|montoOperacion=' + varMonto + 
			'|nroRegistro=' + varNroRegistro + 
			'|numVoucher=' + varNroReferencia + 
			'|numAutTransaccion=' + varNumAutTransaccion + 
			'|codRespTransaccion=' + sCodRespTarj + 
			'|descTransaccion=' + varDesRpta + 
			'|codAprobTransaccion=' + varCodOper + 
			'|tipoPos=' + varTipoPos + 
			'|varNroTarjeta=' + '' + 
			'|fechaExpiracion=' + varFechaExpiracion + 
			'|nomCliente=' + varCliente+ 
			'|impVoucher=' + varImpVoucher + 
			'|seriePOS=' + varSeriePOS + 
			'|nomEquipoPOS=' + varNomEquipoPOS + 
			'|estadoTransaccion=' + varEstTran +
			'|NroTienda=' + varNroTienda + 
			'|CodigoCaja=' + varCodigoCaja + 
			'|CodEstablec=' + varCodEstablec + 
			'|IpPos=' + varIpPos + 
			'|IdCabez=' + varIdCabez +
			'|FlagPago=' + varFlagPago + 
			'|Pedido=' + varNroPedido + 
			'|IdUnico=' + varIdUnicoTrans + 
			'|TipoTrans=' + varTipoTrans + 
			'|IdRefAnulador=' + varIdRefAnu +
			'|TipoPago='+
            '|ResTarjetaPos=';//Proy-31949-Inicio;	
			
			
			if (varEstTran != varArrayEstTrans[2])
			{			
				alert(varDesRpta);			
			}
			
			var varRpta = RSExecute(serverURL,"ActualizarTransaction",varTramaUpdate,
			objEntityPOS.TrnsnId);
			
			varEstadoMov = varEstTran;
			
			ActualizaMovimiento(varEstadoMov);
		}
		catch(err) {
		    document.getElementById("vContMC").value = document.getElementById("vContMC").value*1 + 1;
			alert(err.description);
		}	
	}
	
	
	
		</script>
	</HEAD>
	<body leftMargin="0" topMargin="0" marginheight="0" marginwidth="0">
		<form id="frmPrincipal" name="frmPrincipal" method="post" runat="server">
			<input id="hldVerif" type="hidden" value="1" name="hldVerif" runat="server">
			<div id="overDiv" style="Z-INDEX: 1; POSITION: absolute; WIDTH: 100px"></div>
			<table cellSpacing="0" cellPadding="0" width="800" border="0">
				<tr>
					<td vAlign="top" width="10">&nbsp;</td>
					<td vAlign="top" width="820">
						<table height="14" cellSpacing="0" cellPadding="0" width="820" border="0" name="Contenedor">
							<tr>
								<td align="center"></td>
							</tr>
						</table>
						<table cellSpacing="0" cellPadding="0" width="790" align="center" border="0" name="Contenedor">
							<tr>
								<td align="center">
									<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="790" align="center"
										border="1">
										<tr>
											<td align="center">
												<table class="Arial10B" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
													<tr>
														<td width="10" height="4" border="0"></td>
														<td class="TituloRConsulta" align="center" width="98%" height="32">Proceso de 
															Cuadre de Caja
															<% if Ucase(trim(request.item("tipocuadre"))) = "I" then%>
															individual
															<% end if %>
														</td>
														<td vAlign="top" width="14" height="32"></td>
													</tr>
												</table>
												<table cellSpacing="0" cellPadding="0" width="790" align="center" border="0">
													<tr>
														<td style="PADDING-BOTTOM: 5px; PADDING-LEFT: 5px; PADDING-RIGHT: 5px; PADDING-TOP: 5px"
															width="98%">
															<table cellSpacing="0" cellPadding="0" width="770" border="0">
																<tr>
																	<td height="4"></td>
																</tr>
																<tr>
																	<td>
																		<table cellSpacing="2" cellPadding="0" width="100%" border="0">
																			<!-- <tr>
																				<td width="250">&nbsp;</td>
																				<td class="Arial12b" width="200">&nbsp;&nbsp;&nbsp;Saldo Inicial :</td>
																				<td class="Arial12b" width="450">-->
																				<asp:textbox id="txtSaldo" runat="server" ReadOnly="True" MaxLength="15" CssClass="clsInputDisable" Visible="False"></asp:textbox>
																			<!-- </td></tr> -->
																			<tr>
																				<td width="250">&nbsp;</td>
																				<!--INICIATIVA-318--><td style="DISPLAY: none" class="Arial12b" width="200">&nbsp;&nbsp;&nbsp;Ingreso Efectivo :</td>
																				<!--INICIATIVA-318--><td style="DISPLAY: none" class="Arial12b" width="450"><asp:textbox id="txtIngreso" runat="server" MaxLength="15" CssClass="clsInputEnable"></asp:textbox></td>
																			</tr>
																			<tr>
																				<td width="250">&nbsp;</td>
																				<td class="Arial12b" width="200">&nbsp;&nbsp;&nbsp;Caja Buzón :</td>
																				<td class="Arial12b" width="450"><asp:textbox id="txtCaja" runat="server" ReadOnly="True" MaxLength="15" CssClass="clsInputDisable"></asp:textbox></td>
																			</tr>
																			<tr>
																				<td width="250">&nbsp;</td>
																				<td class="Arial12b" width="200">&nbsp;&nbsp;&nbsp;Caja Buzón Cheque:</td>
																				<td class="Arial12b" width="450"><asp:textbox id="txtCajaCheque" runat="server" ReadOnly="True" MaxLength="15" CssClass="clsInputDisable"></asp:textbox></td>
																			</tr>
																			<tr id="trRemesa" runat="server">
																				<td style="HEIGHT: 23px" width="250">&nbsp;</td>
																				<td class="Arial12b" style="HEIGHT: 23px" width="200">&nbsp;&nbsp;&nbsp;Remesa :</td>
																				<td class="Arial12b" style="HEIGHT: 23px" width="450"><asp:textbox id="txtRemesa" runat="server" ReadOnly="True" MaxLength="15" CssClass="clsInputDisable"></asp:textbox></td>
																			</tr>
																			<!-- PROY-27440 -Inicio -->
																			<tr>
																				<td width="250">&nbsp;</td>
																				<td class="Arial12b" width="200">&nbsp;&nbsp;&nbsp;<asp:Label id="lblMontoP" runat="server" Text="Efectivo Faltante :"></asp:Label></td>
																				<td class="Arial12b" width="450"><asp:textbox id="txtMontoP" runat="server" ReadOnly="True" MaxLength="15" CssClass="clsInputDisable"></asp:textbox></td>
																			</tr>
																			<tr id="trDifVisa" runat="server">
																				<td width="250">&nbsp;</td>
																				<td class="Arial12b" width="200">&nbsp;&nbsp;&nbsp;Diferencia Visa :</td>
																				<td class="Arial12b" width="450"><asp:textbox id="txtDifVisa" runat="server" ReadOnly="True" MaxLength="15" CssClass="clsInputDisable"></asp:textbox></td>
																			</tr>
																			<tr id="trDifMCD" runat="server">
																				<td width="250">&nbsp;</td>
																				<td class="Arial12b" width="200">&nbsp;&nbsp;&nbsp;Diferencia Mastercard :</td>
																				<td class="Arial12b" width="450"><asp:textbox id="txtDifMCD" runat="server" ReadOnly="True" MaxLength="15" CssClass="clsInputDisable"></asp:textbox></td>
																			</tr>
																			<tr id="trDifAmex" runat="server">
																				<td width="250">&nbsp;</td>
																				<td class="Arial12b" width="200">&nbsp;&nbsp;&nbsp;Diferencia American Express :</td>
																				<td class="Arial12b" width="450"><asp:textbox id="txtDifAmex" runat="server" ReadOnly="True" MaxLength="15" CssClass="clsInputDisable"></asp:textbox></td>
																			</tr>
																			<tr id="trDifDiners" runat="server">
																				<td width="250">&nbsp;</td>
																				<td class="Arial12b" width="200">&nbsp;&nbsp;&nbsp;Diferencia Diners :</td>
																				<td class="Arial12b" width="450"><asp:textbox id="txtDifDiners" runat="server" ReadOnly="True" MaxLength="15" CssClass="clsInputDisable"></asp:textbox></td>
																			</tr>
																			<!-- PROY-27440 -Fin -->
																			<tr id="trCierre" runat="server">
																				<td width="250">&nbsp;</td>
																				<td class="Arial12b" width="200">&nbsp;&nbsp;&nbsp;Cierre Caja :</td>
																				<td class="Arial12b" width="450"><asp:checkbox id="chkCierre" runat="server"></asp:checkbox></td>
																			</tr>
																		</table>
																	</td>
																</tr>
															</table>
														</td>
													</tr>
												</table>
											</td>
										</tr>
									</table>
									<br>
									<table borderColor="#336699" cellSpacing="0" cellPadding="4" width="550" align="center"
										border="1">
										<tr id="trOpciones">
											<td>
												<table class="Arial10B" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
													<tr>
														<td vAlign="middle" align="center" width="130"><asp:textbox id="txtFecha" runat="server" CssClass="clsInputEnable" Width="67px"></asp:textbox>&nbsp;<A onmouseover="window.status='Date Picker';return true;" onmouseout="window.status='';return true;"
																href="javascript:show_calendar('frmPrincipal.txtFecha');"><IMG src="../../images/botones/btn_Calendario.gif" border="0"></A>
														</td>
														<td align="center"><asp:button id="btnBuscar" runat="server" CssClass="BotonOptm" Width="100px" Text="Buscar"></asp:button>&nbsp;
															<asp:button id="btnGrabar" runat="server" CssClass="BotonOptm" Width="100px" Text="Procesar"></asp:button>&nbsp;
															<asp:button id="btnRecalcula" runat="server" CssClass="BotonOptm" Width="100px" Text="Recalcular"
																style="Z-INDEX: 0" Visible="False"></asp:button>&nbsp;
															<asp:button id="bntForzado" runat="server" CssClass="BotonOptm" Width="100px" Text="Forzar Cuadre"
																Visible="False"></asp:button>
															<asp:button id="btnGrabarOculto" runat="server" CssClass="BotonOptm" Width="0px" OnClick="btnGrabarOculto_Click"
																Visible="True"></asp:button>
														    <asp:button style="Z-INDEX: 0" id="btnRegregsar" runat="server" Visible="False" CssClass="BotonOptm"
																Text="Regresar" Width="100px"></asp:button></td>
													</tr>
												</table>
												<asp:Button id="procesarHandler" runat="server" Text="Button" Style="DISPLAY:none"></asp:Button>
											</td>
										</tr>
										<tr id="trProcesando" align="center" style="display:none;color: #F00; font-weight: bold; font-size: 16px;">
											<td>
												<label>PROCESANDO…</label><img src="..\images\ajax-loader.gif"></img>
											</td>
										</tr>
									</table>
									<br>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<!--PROY-27440 INI--><input id="HidPtoVenta" type="hidden" name="HidPtoVenta" runat="server">
			<input id="HidIntAutPos" type="hidden" name="HidIntAutPos" runat="server"> <input id="HidCodOpera" type="hidden" name="HidCodOpera" runat="server">
			<input id="HidDesOpera" type="hidden" name="HidDesOpera" runat="server"> <input id="HidTipoOpera" type="hidden" name="HidTipoOpera" runat="server">
			<input id="HidTipoTarjeta" type="hidden" name="HidTipoTarjeta" runat="server"> <input id="HidEstTrans" type="hidden" name="HidEstTrans" runat="server">
			<input id="HidTipoPOS" type="hidden" name="HidTipoPOS" runat="server"> <input id="HidTipoTran" type="hidden" name="HidTipoTran" runat="server">
			<input id="HidFila1" type="hidden" name="HidFila1" runat="server"> <input id="HidMonedaMC" type="hidden" name="HidMonedaMC" runat="server">
			<input id="HidApliPOS" type="hidden" name="HidApliPOS" runat="server"> <input id="HidTipoMoneda" type="hidden" name="HidTipoMoneda" runat="server">
			<input id="HidMonedaVisa" type="hidden" name="HidMonedaVisa" runat="server"> <input id="HidIntAutPosMC" type="hidden" name="HidIntAutPosMC" runat="server">
			<input id="HidMsgRsptMov" type="hidden" name="HidMsgRsptMov" runat="server"> <input id="HidDatoPosVisa" type="hidden" name="HidDatoPosVisa" runat="server">
			<input id="HidDatoPosMC" type="hidden" name="HidDatoPosMC" runat="server"> <input id="HidDatoAuditPos" type="hidden" name="HidDatoAuditPos" runat="server">
			<input id="HidIdCabez" type="hidden" name="HidIdCabez" runat="server"> <input id="HidTipoOperaMC" type="hidden" name="HidTipoOperaMC" runat="server">
			<input id="HidTransMC" type="hidden" name="HidTransMC" runat="server"> <input id="HidValidaExitoPOS" type="hidden" name="HidValidaExitoPOS" runat="server">
			<input id="HidIntAutPosRep" type="hidden" name="HidIntAutPosRep" runat="server"><input id="HidPosIdVISA" type="hidden" name="HidPosIdVISA" runat="server">
			<input id="HidPosIdMC" type="hidden" name="HidPosIdMC" runat="server">
			<input id="HidIntVisa" type="hidden" name="HidIntVisa" runat="server"> 
			<input id="HidIntMC" type="hidden" name="HidIntMC" runat="server">
			<input id="HidIntentosCuadre" type="hidden" name="HidIntentosCuadre" runat="server">  
			<input id="vContVisa" type="hidden" name="vContVisa" runat="server" value="1"> 
			<input id="vContMC" type="hidden" name="vContMC" runat="server" value="1"> 
		        <!-- INICIATIVA-318 ARQUEO INI -->
			<input id="hidEstado" type="hidden" name="hidEstado" runat="server" value="0"> 
			<!-- INICIATIVA-318 ARQUEO FIN -->
			<!--PROY-27440 FIN--></form>
		<script language="JavaScript" type="text/javascript">
function f_Buscar(){
	event.returnValue = false; 
	if (f_ValidarB()) {
		event.returnValue = true; 
	}
}

function f_Aprobar(){
    event.returnValue = false; 
	var chk = f_Validar();
	if(chk == true){
		event.returnValue = true;
		f_OcultarBotonera();
	}
}
		
function f_Validar(){
	//if (!ValidaDecimalB('document.frmPrincipal.txtSaldo','El campo Saldo Inicial',true)) return false;
	//if (!ValidaDecimalB('document.frmPrincipal.txtIngreso','El campo Ingreso Efectivo',false)) return false;//INICIATIVA-318
	if (!ValidaDecimalB('document.frmPrincipal.txtCaja','El campo Caja Buzón',true)) return false;
	if (document.getElementById("txtRemesa") != null)
	   if (!ValidaDecimalB('document.frmPrincipal.txtRemesa','El campo Remesa',true)) return false;
	if (!ValidaDecimalB('document.frmPrincipal.txtMontoP','El campo Monto Pendiente Ingreso',true)) return false;
	//if (!ValidaDecimalB('document.frmPrincipal.txtMontoS','El campo Monto Sobrante',true)) return false;
	if (!ValidaFechaA('document.frmPrincipal.txtFecha',false)) return false;
	if (!FechaMayorSistema('document.frmPrincipal.txtFecha','el campo Fecha ')) return false;
	return true;	
}

function f_ValidarB(){
	if (!ValidaFechaA('document.frmPrincipal.txtFecha',false)) return false;
	if (!FechaMayorSistema('document.frmPrincipal.txtFecha','el campo Fecha ')) return false;
	return true;
}

function f_OcultarBotonera(){
	setVisible('trOpciones', false);
	setVisible('trProcesando', true);
}

var esNavegador, esIExplorer;
  
esNavegador = (navigator.appName == "Netscape") ? true : false;
esIExplorer = ((navigator.appName.indexOf("Microsoft") != -1) || (navigator.appName.indexOf("MSIE") != -1)) ? true : false;

function e_mayuscula(){
	if (event.keyCode>96&&event.keyCode<123)
		event.keyCode=event.keyCode-32;
}

function e_minuscula(){
	if (event.keyCode>64&&event.keyCode<91)
		event.keyCode=event.keyCode+32;
}

if (esIExplorer) {
	//window.document.frmPrincipal.txtPNombre.onkeypress=e_mayuscula;
}
		</script>
	</body>
</HTML>
