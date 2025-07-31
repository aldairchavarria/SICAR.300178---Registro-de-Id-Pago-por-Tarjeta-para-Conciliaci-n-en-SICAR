<%@ Page Language="vb" AutoEventWireup="false" Codebehind="devolEfectivo.aspx.vb" Inherits="SisCajas.devolEfectivo" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>devolEfectivo</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="styleSheet" type="text/css" href="../estilos/est_General.css">
		<script language="JavaScript" src="../librerias/ubigeo.js"></script>
		<script language="JavaScript" src="../librerias/date-picker.js"></script>
		<script language="JavaScript" src="../librerias/Lib_FuncValidacion.js"></script>
		<!--PROY-27440 INI-->
		<script language="JavaScript" src="../librerias/msrsclient.js"></script>
		<script language="JavaScript" src="../librerias/jquery-1.8.2.min.js"></script>
		<script language="JavaScript" src="../Scripts/form.js"></script>
		<script language="JavaScript" src="../Scripts/xml2json.js"></script>
		<script language="JavaScript" src="../Scripts/operacionPOS.js"></script>
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<style>.tbl_pagos { BORDER-BOTTOM: #999999 1px solid; BORDER-LEFT: #999999 1px solid; FONT-FAMILY: Arial; FONT-SIZE: 10px; BORDER-TOP: #999999 1px solid; BORDER-RIGHT: #999999 1px solid; TEXT-DECORATION: none }
	.tbl_pagos TH { TEXT-ALIGN: center; PADDING-BOTTOM: 5px; BACKGROUND-COLOR: #21618c; HEIGHT: 22px; COLOR: #ffffff; PADDING-TOP: 5px }
	.tbl_pagos TD { BORDER-BOTTOM: #999999 1px solid; TEXT-ALIGN: center; BORDER-LEFT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-RIGHT: #999999 1px solid }
		</style>
		<!--PROY-27440 FIN-->
		<script language="javascript">
		<!--
		        //PROY-31949 
		        jQuery.support.cors = true;
	                var blnClosingMod = false; 
			window.onbeforeunload = FN_ExitModal;
			
			function FN_ExitModal()
			{
				if(blnClosingMod == true) 
				{ 
					FN_ExitModal = false;
				    return "Tiene Transacciones realizadas con el POS, debe grabar o anular la Transacción antes de salir."; 
				} 
					
				else
				{
					return;
				}
			}	
			
			$(document).on('click','#btnGrabar',function(){ blnClosingMod = false; });
			$(document).on('click','#btnCancelar',function(){ blnClosingMod = false; });
			//PROY-31949 
			//PROY-27440 INI
			var serverURL =  '../Pos/ProcesoPOS.aspx';
			var webServiceURL = '<%= System.Configuration.ConfigurationSettings.AppSettings("consWsLocal_Url") %>';
			var timeOutWsLocal = '<%= System.Configuration.ConfigurationSettings.AppSettings("consWsLocal_Url_TimeOut") %>';
			var MaxNumIntentosAnular;//PROY-31949 
			var sCodRespTarj;
			var varCont;
			var varTramaInsert;
			var varTramaAuditAux;
			var varIdTransAux;
			var varEstadoTrans;				
			//Mastercard
			var varMontoMC;
			var varTransMC;
			var varMonedaMC;
			var varApliMC;
			var varNroRefMC;			
			var varPwdComercioMC;
			//VisaNet
			var varMonedaVisa;
			var varMontoVisa;
			var varNroRef;
						//Parametros Envio TB Transacciones

			var varTipTarjeta;
			var varTransMonto;
			var varTipoPos;
			var varValueTar;
			var varValueTarAux;
			var varTipoTrans;
			var varNroTarjeta;
			var varNroTelefono;
			var varTipoOpeFi;	
			var varCodOpe;
			var varDescriOpe;
			var varMoneda;
			var	varNroPedido;
			var	varTipoPago; //DEVOLUCIONES

			//Declarar Informacion POS//		
			var varNroRegistro;
			var varNumVoucher;
			var varNumAutTransaccion;
			var varCodRespTransaccion;
			var varDescTransaccion;
			var varCodAprobTransaccion;		
			var varFechaExpiracion;
			var varNomCliente;
			var varImpVoucher;
			var varSeriePOS;
			var nomEquipoPOS;
			var varNroRegistro;
			var varNroTienda;
			var varCodEstablec;
			var varCodigoCaja;
			var varNomPcPos;
			var varNomEquipoPOS;
			var varCodTerminal;
			var varIpPOS;
			var varIdCabecera;
			var varIdRefAnu;
			//
			//Parametros SICAR POS
			var varArrayEstTrans;
			var varArrayCodOpe;
			var varArrayDesOpe;
			var varArrayCodTarjeta;
			var varArrayTipoPOS;
			var varArrayTipoTran;
			var varArrayTipoOper;
			var varArrayDatoPosVisa;	
			var varArrayDatoPosMC;
			var varArrayTranMC;
						
			//PROY-27440 INI
			function f_validar_switch(flag)
			{ 
				if(flag == '1')
				{
					document.getElementById('tbl_FormasPagoC').style.setAttribute('display', 'block');
				}
				else
				{
					document.getElementById('tbl_FormasPagoC').style.setAttribute('display', 'none');
				}
						
			}
			
			function CallBack_GuardarAutorizacion(response)
			{         
			    document.getElementById("lblEnvioPos").innerHTML  = "Enviando al POS........";
				var varRpta = response.return_value;
				var res = (varRpta.replace("<BODY><SELECT>", "")).replace("</SELECT>", "").replace("\r\n","");

				varRpta = res;	

				if (varRpta=='0')
				{					
					alert('Ud. no está autorizado a realizar esta operación. Comuniquese con el administrador');
					RSExecute(serverURL,"GuardarAutorizacion",varTramaAuditAux,CallBack_GuardarAutorizacion,GuardarAutorizacionError,"X");					
					return;	
				}
				else
				{
					f_EnvioPOS('EliminarPOS');
				}
			}
			
			function GuardarAutorizacionError(co)
			{
				if (co.message)
			    {
					alert("Context:" + co.context + "\nError: " + co.message);
				}
			}	
			
			function f_Evaluar_datos(TIPO_TRANSACCION,NRO_TARJETA,MONTO,NRO_INTENTOS,CAJERO,MONEDA,NUMERO_TELEFONO,TIPO_TARJETA_POS,ID_REF,IP_CAJA)
			{
				NRO_INTENTOS = parseInt(NRO_INTENTOS) + 1;  //PROY-31949 
				
				document.getElementById("lblEnvioPos").innerHTML  = "Enviando al POS........";
				MaxNumIntentosAnular = Number(document.getElementById("HidNumIntentosAnular").value);//PROY-31949 
				varArrayEstTrans=document.getElementById("HidEstTrans").value.split("|");
				varArrayCodOpe=document.getElementById("HidCodOpera").value.split("|");
				varArrayDesOpe=document.getElementById("HidDesOpera").value.split("|");
				varArrayCodTarjeta=document.getElementById("HidTipoTarjeta").value.split("|");
				varArrayTipoPOS=document.getElementById("HidTipoPOS").value.split("|");
				varArrayTipoTran=document.getElementById("HidTipoTran").value.split("|");
				varArrayTipoOper=document.getElementById("HidTipoOpera").value.split("|");
				varArrayDatoPosVisa = document.getElementById("HidDatoPosVisa").value.split("|");
				varArrayDatoPosMC = document.getElementById("HidDatoPosMC").value.split("|");
				varArrayTranMC = document.getElementById("HidTransMC").value.split("|");
				
				
				
				varNroTarjeta=NRO_TARJETA;
				varTransMonto= MONTO; 
				varMoneda=MONEDA;	
				varNroTelefono =  NUMERO_TELEFONO;	
				varValueTar = TIPO_TARJETA_POS;
				varValueTarAux = TIPO_TARJETA_POS;
				varNroRef = ID_REF;
				varCont = NRO_INTENTOS;	
				
				//PROY-31949   
				if(document.getElementById("HidFlagCajaCerrada").value == '1')
				{	  
					alert(document.getElementById("HidMsjCajaCerrada").value);	
					document.getElementById("lblEnvioPos").innerHTML  = "";							
					return;
				}
                                //PROY-31949 
				
				if(IP_CAJA!=varArrayDatoPosVisa[7].split("=")[1]){
					alert(document.getElementById("HidMsjCajero").value)
					document.getElementById("lblEnvioPos").innerHTML  = "";		
					return;
				}
				if(CAJERO !=  document.getElementById("HidCajero").value)
				{				
					alert(document.getElementById("HidMsjCajero").value)
					document.getElementById("lblEnvioPos").innerHTML  = "";		
					return;
				}
				
		        if (varValueTarAux == 'VISA')
		        {
					if(varArrayDatoPosVisa == '' || varArrayDatoPosVisa == null)
					{
						alert(document.getElementById("HidMsjValIP").value)
						document.getElementById("lblEnvioPos").innerHTML  = "";		
						return;
					}
		        }
		        else
		        {
					if(varArrayDatoPosMC == '' || varArrayDatoPosMC == null)
					{
						alert(document.getElementById("HidMsjValIP").value)
						document.getElementById("lblEnvioPos").innerHTML  = "";		
						return;
					}
		        
		        }
					
		            
		        
		      
				
				varNroPedido = document.getElementById("txtNroPedido").value;
				varTipoPago = document.getElementById("HidTipoPago").value; //DEVOLUCIONES
							
				varNomCliente = document.getElementById("txtNombCli").value;
				varNroPedido = document.getElementById("txtNroPedido").value; 
				var varTramaAudit = '';    
				
				varTramaAudit = 'NomCliente=' + varNomCliente + 
				'|NroTelefono=' + varNroTelefono + 
				'|NroPedido=' + varNroPedido + 
				'|IdTransaccion=' + "" +
				'|nMonto=' + MONTO ;		

				if (NRO_INTENTOS <= MaxNumIntentosAnular || TIPO_TRANSACCION=="ImprimirPOS") //PROY-31949 
				{					
					f_EnvioPOS(TIPO_TRANSACCION);
					
				}
				else
				{  
					varTramaAuditAux = varTramaAudit;
					RSExecute(serverURL,"GuardarAutorizacion",varTramaAudit,CallBack_GuardarAutorizacion,GuardarAutorizacionError,"X");												
				}

			}			
			
			function f_EnvioPOS(TIPO_TRANSACCION)
			{						
					
				varNroRegistro= 0;
				varNumVoucher='';
				varNumAutTransaccion='0';
				varCodRespTransaccion='';
				varDescTransaccion='';
				varCodAprobTransaccion='0';		
				varFechaExpiracion = '';
				varNomCliente = '';
				varImpVoucher = '';
				varSeriePOS = '';
				nomEquipoPOS = '';
				varIpPOS = '';
				
				
				varTransMC = '';
				varMonedaMC = '';
				varApliMC = '';	
							  
				varMonedaVisa = '';					
					
				varTipTarjeta ='';			
				varTipoPos ='';				
				varTipoTrans ='';				
				varTipoOpeFi ='';	
				varCodOpe ='';
				varDescriOpe ='';				
				varTramaInsert = '';
				varEstadoTrans = '';
				varValueTar = varValueTarAux;
				
				
				switch (varValueTar)
				{
					case "VISA":
					varTipoPos= varArrayTipoPOS[0];
					varTipTarjeta = varArrayCodTarjeta[0];//VISA
					varValueTar = "VIS";
					
				    varNroRegistro = varArrayDatoPosVisa[0].substr(varArrayDatoPosVisa[0].indexOf("=")+1);
					varNroTienda = varArrayDatoPosVisa[1].substr(varArrayDatoPosVisa[1].indexOf("=")+1);
					varCodigoCaja = varArrayDatoPosVisa[2].substr(varArrayDatoPosVisa[2].indexOf("=")+1);
					varCodEstablec = varArrayDatoPosVisa[3].substr(varArrayDatoPosVisa[3].indexOf("=")+1);
					varNomPcPos = varArrayDatoPosVisa[4].substr(varArrayDatoPosVisa[4].indexOf("=")+1);
					varCodTerminal = varArrayDatoPosVisa[6].substr(varArrayDatoPosVisa[6].indexOf("=")+1);
					varIpPos = varArrayDatoPosVisa[7].substr(varArrayDatoPosVisa[7].indexOf("=")+1);	
					
					break;
					
					case "MASTERCARD":		
					varTipoPos= varArrayTipoPOS[1];
					varTipTarjeta = varArrayCodTarjeta[1];//MASTERCARD
					varValueTar = "MCD";
					varNroRegistro = varArrayDatoPosMC[0].substr(varArrayDatoPosMC[0].indexOf("=")+1);
					varNroTienda = varArrayDatoPosMC[1].substr(varArrayDatoPosMC[1].indexOf("=")+1);
					varCodigoCaja = varArrayDatoPosMC[2].substr(varArrayDatoPosMC[2].indexOf("=")+1);
					varCodEstablec = varArrayDatoPosMC[3].substr(varArrayDatoPosMC[3].indexOf("=")+1);
					varNomPcPos = varArrayDatoPosMC[4].substr(varArrayDatoPosMC[4].indexOf("=")+1);
					varCodTerminal = varArrayDatoPosMC[6].substr(varArrayDatoPosMC[6].indexOf("=")+1);
					varIpPos = varArrayDatoPosMC[7].substr(varArrayDatoPosMC[7].indexOf("=")+1);			
					break;
					
					case "AMEX":
					varTipoPos= varArrayTipoPOS[2];
					varTipTarjeta = varArrayCodTarjeta[1];
					varValueTar = "AMX";
					varNroRegistro = varArrayDatoPosMC[0].substr(varArrayDatoPosMC[0].indexOf("=")+1);
					varNroTienda = varArrayDatoPosMC[1].substr(varArrayDatoPosMC[1].indexOf("=")+1);
					varCodigoCaja = varArrayDatoPosMC[2].substr(varArrayDatoPosMC[2].indexOf("=")+1);
					varCodEstablec = varArrayDatoPosMC[3].substr(varArrayDatoPosMC[3].indexOf("=")+1);
					varNomPcPos = varArrayDatoPosMC[4].substr(varArrayDatoPosMC[4].indexOf("=")+1);
					varCodTerminal = varArrayDatoPosMC[6].substr(varArrayDatoPosMC[6].indexOf("=")+1);
					varIpPos = varArrayDatoPosMC[7].substr(varArrayDatoPosMC[7].indexOf("=")+1);				
					break;
					
					case "DINERS":
					varTipoPos= varArrayTipoPOS[3];
					varTipTarjeta = varArrayCodTarjeta[1];
					varValueTar = "DIN";
					varNroRegistro = varArrayDatoPosMC[0].substr(varArrayDatoPosMC[0].indexOf("=")+1);
					varNroTienda = varArrayDatoPosMC[1].substr(varArrayDatoPosMC[1].indexOf("=")+1);
					varCodigoCaja = varArrayDatoPosMC[2].substr(varArrayDatoPosMC[2].indexOf("=")+1);
					varCodEstablec = varArrayDatoPosMC[3].substr(varArrayDatoPosMC[3].indexOf("=")+1);
					varNomPcPos = varArrayDatoPosMC[4].substr(varArrayDatoPosMC[4].indexOf("=")+1);
					varCodTerminal = varArrayDatoPosMC[6].substr(varArrayDatoPosMC[6].indexOf("=")+1);
					varIpPos = varArrayDatoPosMC[7].substr(varArrayDatoPosMC[7].indexOf("=")+1);				
					break;
				}
				
				switch (TIPO_TRANSACCION)
				{					
					case "EliminarPOS":

					varCodOpe = varArrayCodOpe[2];//Anulacion
					varDescriOpe = varArrayDesOpe[2];
					varTipoTrans= varArrayTipoTran[1];//Anulacion ;
					varTipoOpeFi = varArrayTipoOper[0];//Financiera
					
					if(varValueTar == 'VIS')
					{  
						var varArrayMonedaVisa = document.getElementById("HidMonedaVisa").value.split("|");					
						varMonedaVisa = varArrayMonedaVisa[0];//SOLES VISA	
						
						if(varNroRef == '')
						{
							alert('El Pago seleccionado no cuenta con numero de referencia');
                                                        return;
					    } 
					    else
					    {
					                if(varCont <= MaxNumIntentosAnular) //PROY-31949 
			                 {
					        alert('Nro de referencia: ' + varNroRef);
					    }
					    }
								
					}
					else
					{
						varArrayTranMC = document.getElementById("HidTransMC").value.split("|");
						varPwdComercioMC = varArrayTranMC[6]
						
						varTransMC = varArrayTranMC[1];//06
				     	varMonedaMC = '';
						
						if(varNroRef == '')
						{
							alert('No tiene numero de referencia para eliminar la transaccion');
							document.getElementById("lblEnvioPos").innerHTML  = "";	
							return;
						}
						else
						{
							varNroRefMC = varNroRef;
						}
						
					}	
								
					break;
					
					case "ImprimirPOS":

					varCodOpe = varArrayCodOpe[2];//Anulacion
					varDescriOpe = varArrayDesOpe[2];
					varTipoTrans= varArrayTipoTran[2]//Impresion;
					varTipoOpeFi = varArrayTipoOper[1];//No Financiera
									
					if(varValueTar == 'VIS')
					{
						var varArrayMonedaVisa = document.getElementById("HidMonedaVisa").value.split("|");
						varMonedaVisa = varArrayMonedaVisa[0];//SOLES VISA
					}
					else
					{						
						varTransMC = varArrayTranMC[4];//11
						varMonedaMC = '';
					
						if(varNroRef == '')
						{
							alert('No tiene numero de referencia para re-imprimir la transaccion');
							document.getElementById("lblEnvioPos").innerHTML  = "";	
							return;
						}
						else
						{						
							varNroRefMC = varNroRef;
						}
					}
					break;
				}
					
				varEstadoTrans= varArrayEstTrans[0];//ESTADO PENDIENTE
				varIdCabecera = document.getElementById("HidIdCabez").value;	
				varApliMC = document.getElementById("HidApliPOS").value;

				varTramaInsert = 'codOperacion=' + varCodOpe +
				'|desOperacion=' + varDescriOpe + 
				'|tipoOperacion=' + varTipoOpeFi +
				'|montoOperacion=' + varTransMonto + 
				'|monedaOperacion=' + varMoneda +
				'|tipoTarjeta=' + varTipTarjeta + 																	
				'|tipoPago=' + varTipoPago + 
				'|estadoTransaccion=' + varEstadoTrans + 
				'|tipoPos=' + varTipoPos +
				'|tipoTransaccion=' + varTipoTrans + 
				'|ipCaja=' + varIpPos +
				'|NroRegistro=' + varNroRegistro + 
                '|NroTienda=' + varNroTienda + 
                '|CodigoCaja=' + varCodigoCaja +
				'|CodEstablec=' + varCodEstablec +
                '|NomPcPos=' + varNomPcPos + 
                '|CodTerminal=' + varCodTerminal+
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

				if( varRpta.substr(0,1) == '0')
				{	
						var varArrayRpta = varRpta.split("|");
						var varIdTran = varArrayRpta[2];
						var varIdCabez = varArrayRpta[3];
						var varFlagPago = '1';
						var varIdUnicoTrans = '';
			
					document.getElementById("HidIdCabez").value = varIdCabez;
					//2 - EN PROCESO				   

					varEstadoTrans = '';
					varEstadoTrans = varArrayEstTrans[1];//EN PROCESO
                                        varIdRefAnu = '';
					varTramaUpdate = '';
					varTramaUpdate = 'monedaOperacion=' + varMoneda + 
					'|montoOperacion=' + varTransMonto + 
					'|nroRegistro=' + varNroRegistro + 
					'|numVoucher=' + varNroRef + 
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
					'|estadoTransaccion=' + varEstadoTrans+
					'|NroTienda=' + varNroTienda + 
				    '|CodigoCaja=' + varCodigoCaja + 
					'|CodEstablec=' + varCodEstablec + 
					'|IpPos=' + varIpPos+
					'|IdCabez=' + varIdCabez +
					'|FlagPago=' + varFlagPago + 
					'|Pedido=' + varNroPedido + 
					'|IdUnico=' + varIdUnicoTrans +
					'|TipoTrans=' + varTipoTrans + 
					'|IdRefAnulador=' + varIdRefAnu +
					'|TipoPago=' + document.getElementById("HidTipoPago").value +
					'|ResTarjetaPos=';//PROY-31949 

					RSExecute(serverURL,"ActualizarTransaction",varTramaUpdate,
					varIdTran,CallBack_ActualizarTransaction1,GuardarTransactionError,"X");
							
                                        
					objEntityPOS =
					{
						monedaOperacion: '', 
						montoOperacion: varTransMonto, 
						CodigoTienda: varNroTienda,  
						NroPedido: '',
						ipAplicacion: '', 
						nombreAplicacion: '', 
						usuarioAplicacion: '',
						TrnsnId: varIdTran,
						tipoPos: '',
						CodigoCaja: varCodigoCaja
					};
					//PROY-31949 
					if(varTipoTrans == varArrayTipoTran[1]) //Anulacion
					{
						if(varCont <= MaxNumIntentosAnular)
						{						
					if (varValueTar == 'VIS')//VISA
					{  
						CallService(varTipTarjeta,varNroTarjeta,objEntityPOS);
					}
					else if(varValueTar=='AMX' || varValueTar=='MCD' || varValueTar=='DIN')//MASTERCARD
					{
						CallService(varTipTarjeta,varNroTarjeta,objEntityPOS);
					}												
						}
						else
						{						
							if (varValueTar == 'VIS')
							{
								ErrorVisaNet('',varNroTarjeta,objEntityPOS);
							}
							else if(varValueTar=='AMX' || varValueTar=='MCD' || varValueTar=='DIN')
							{
								ErrorMasterCard('',varNroTarjeta,objEntityPOS);
							}	
						}
					}
					else
					{
					if (varValueTar == 'VIS')//VISA
					{  
						CallService(varTipTarjeta,varNroTarjeta,objEntityPOS);
					}
					else if(varValueTar=='AMX' || varValueTar=='MCD' || varValueTar=='DIN')//MASTERCARD
					{
						CallService(varTipTarjeta,varNroTarjeta,objEntityPOS);
					}												
					}
                                        //PROY-31949 												
								
				}
				else
				{
					alert(document.getElementById("HidMsjErrorGuardarTrans").value)
					document.getElementById("lblEnvioPos").innerHTML  = "";		
					return;
				}
			}
	        
	        function CallBack_ActualizarTransaction1(response) 
			{
				var varRpta = response.return_value;
				var res = (varRpta.replace("<BODY><SELECT>", "")).replace("</SELECT>", "").replace("\r\n","");
				varRpta = res;
				
				if( varRpta.substr(0,1) == '0')
				{
					
				}
				else
				{
					alert('callback actualizar '+ varRpta);
					document.getElementById("lblEnvioPos").innerHTML  = "";	
					f_CargarFormasPago();
					return;
				}
			}
			
			function CallBack_ActualizarTransaction2(response) 
			{
				var varRpta = response.return_value;
				var res = (varRpta.replace("<BODY><SELECT>", "")).replace("</SELECT>", "").replace("\r\n","");
				varRpta = res;
				
				if( varRpta.substr(0,1) == '0')
				{
					
				}
				else
				{
					alert('callback actualizar '+ varRpta);
					document.getElementById("lblEnvioPos").innerHTML  = "";	
					f_CargarFormasPago();
					return;
				}
			}	

			function GuardarTransactionError(co) 
			{    
				if (co.message) 
				{
					alert("Context:" + co.context + "\nError: " + co.message);
				}
				varRptTtipoPos='1|Error';
				document.getElementById("lblEnvioPos").innerHTML  = "";	
				f_CargarFormasPago();	
                return;
			}
	        
			function CallService(tipoPOS,NameDoc,objEntityPOS)
			{  
				
                varBolWsLocal = false;  
                varIdRefAnu = '';            
				var entityOpe;
				var soapMSG;
				
				var varMontoOperacion;		
				var varNroReferencia = '';
				
				
					//Variables de auditoria Ini		
				var varArrayAudi = document.getElementById("HidDatoAuditPos").value.split("|");			
				var VarToday = new Date();
				var varIdTransaccion = varNroPedido + '_' + formatDate(VarToday);
				var varIpApplicacion = varArrayAudi[0];
				var varNombreAplicacion = varArrayAudi[1];
				var varUsuarioAplicacion = varArrayAudi[2];
					//Variables de auditoria Fin
				
				varImpVoucher = '';
				varNumAutTransaccion = 0;
				varCodRespTransaccion='';
				varDescTransaccion='';
				varCodAprobTransaccion='0';	
				varFechaExpiracion = '';
				varNomCliente = '';
				varImpVoucher = '';
				varSeriePOS='';
				varNomEquipoPOS='';
				varIpPos = '';
				varTipOpePOS = '';
				varEstTran = '';
				
				var varDesRpta = '';
				var varCliente = '';		
			
				var varCodOper = '';
				
    				switch (tipoPOS) 
					{
					case "01": //VISANET        
						
						if (varTipoTrans == '3')
						{
							varMonedaVisa = '';
							varMontoVisa = '' ;
						}
						else
						{
							varMontoVisa = parseFloat(objEntityPOS.montoOperacion).toFixed(2);
						}					

						entityOpe = 
						{ 
							TipoOperacion: varTipoOpeFi, 
							SalidaMensaje: '',
						    RutaArchivoINI: '', 
							TipoMoneda: varMonedaVisa,
							Monto: varMontoVisa, 
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

						$.ajax
						({
							url: webServiceURL + '?op=peticionOperacionVisaNet',
							type: "POST",
							dataType: "text",
							data: soapMSG,
							timeout: timeOutWsLocal,
							processData: false,
							contentType: "text/xml; charset=\"utf-8\"",
							async: true,
							cache: false,
							success: function (objResponse, status)
							{  														
								SuccessVisaNet(objResponse,varNroTarjeta);
							},
							error: function (request, status)
							{				
						    	varBolWsLocal = true;	
						    	alert('Sin respuesta del POS, tiempo de espera superado.');  		
								ErrorVisaNet(request,NameDoc,objEntityPOS);						
								
							},
							timeout: Number(timeOutWsLocal)
						}); 
													
						return true;						
						break;						
						
					case "02": //MASTER CARD
			
						if (varTipoTrans == '3')
						{
							varMonedaMC = '';
							varMontoMC = '' ;
						}
						else
						{
							//varMontoMC = parseFloat(objEntityPOS.montoOperacion);
							varMontoMC = Number(objEntityPOS.montoOperacion).toFixed(2);
						}				
				
						entityOpe = 
						{ 
							IdTransaccion: varIdTransaccion,
							IpApplicacion: varIpApplicacion,
							NombreAplicacion: varNombreAplicacion,
							UsuarioAplicacion: varUsuarioAplicacion,
							Aplicacion: varApliMC, 
							Transaccion: varTransMC, 
							Monto: varMontoMC, 
							TipoMoneda: varMonedaMC,
							DataAdicional: varNroRefMC, 
							CodigoServicio: '',
						    ClaveComercio: varPwdComercioMC,
							Dni: '', 
							Ruc: '', 
							Producto: '', 
							OpeMonto: '', 
							Nombre: '', 
							Valor: ''
						};
        
						soapMSG = f_data_MC(entityOpe);
        
						$.ajax
						({
							url: webServiceURL + '?op=peticionOperacionMC',
							type: "POST",
							dataType: "text",
							data: soapMSG,
							timeout: timeOutWsLocal,
							processData: false,
							contentType: "text/xml; charset=\"utf-8\"",
							async: true,
							cache: false,
							success: function (objResponse, status)
							{							
								SuccessMasterCard(objResponse,NameDoc);
							},
							error: function (request, status) 
							{											
								varBolWsLocal = true;
								alert('Sin respuesta del POS, tiempo de espera superado.');
								ErrorMasterCard(request,NameDoc,objEntityPOS);
								/*Fin Error*/
							},
							timeout: Number(timeOutWsLocal)
						});
						return true;
						break;
					}
								
			}
			
			function ErrorMasterCard()
			{
				try 
				{
					
					var varClienteMCD= '';				
					var varCodOperMCD = '';
					var varImpVoucher = '';  
					
					var varIdUnicoTrans = '';
					var varIdCabez = document.getElementById("HidIdCabez").value;
					var varFlagPago = '1';
					varDesRpta = "Error de timeout MCD";
					varMontoOperacion = objEntityPOS.montoOperacion;
					varNomEquipoPOS = varNomPcPos;					
			    
			    
					varNroReferencia = '';
					varNroReferencia = varNroRefMC;
					varFechaExpiracion = '';
					varCodOperVisa = '';
					varSeriePOS = '';
					varImpVoucher = '';
					sCodRespTarj = '';
					varEstTran = varArrayEstTrans[3];//RECHAZADO
					
					//PROY-31949 
					if(varTipoTrans == varArrayTipoTran[1])
					{
						if(varCont > MaxNumIntentosAnular)
					{
							varEstTran = varArrayEstTrans[4];//INCONPLETO
							varDesRpta = document.getElementById("HidMsjErrorNumIntentos").value;
							//BloqueoBotones();	
					}  
						else
						{
							varEstTran = varArrayEstTrans[3];//RECHAZADO
							varDesRpta = document.getElementById("HidMsjErrorTimeOut").value;					
						}					
					}  
					//PROY-31949 
					
					varTramaUpdate = '';
					varTramaUpdate = 'monedaOperacion=' + varMoneda + 
					'|montoOperacion=' + varMontoOperacion + 
					'|nroRegistro=' + varNroRegistro + 
					'|numVoucher=' + varNroRef + 
					'|numAutTransaccion=' + varNumAutTransaccion + 
					'|codRespTransaccion=' + sCodRespTarj + 
					'|descTransaccion=' + varDesRpta + 
					'|codAprobTransaccion=' + varCodOperMCD + 
					'|tipoPos=' + varTipoPos + 
					'|varNroTarjeta=' + varNroTarjeta + 
					'|fechaExpiracion=' + varFechaExpiracion + 
					'|nomCliente=' + varClienteMCD + 
					'|impVoucher=' + varImpVoucher + 
					'|seriePOS=' + varSeriePOS + '|nomEquipoPOS=' + varNomEquipoPOS + 
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
					'|TipoPago=' + document.getElementById("HidTipoPago").value +
					'|ResTarjetaPos=';//PROY-31949 
	                
					
					RSExecute(serverURL,"ActualizarTransaction",varTramaUpdate,
					objEntityPOS.TrnsnId,CallBack_ActualizarTransaction2,GuardarTransactionError,"X");
					                 
					var varTramaAudit = '';    
					
					varTramaAudit = 'NomCliente=' + document.getElementById("txtNombCli").value + 
					'|NroTelefono=' + "" + 
					'|NroPedido=' + varNroPedido + 
					'|IdTransaccion=' + objEntityPOS.TrnsnId +
					'|nMonto=' + varTransMonto ;

					varIdTransAux = objEntityPOS.TrnsnId;

					//PROY-31949 
					if(varTipoTrans == varArrayTipoTran[1] && varEstTran == varArrayEstTrans[3])
					{			
						if(varCont >= MaxNumIntentosAnular)
					{
						varTramaAuditAux = varTramaAudit;
						varCont = parseInt(varCont) + 1;
						RSExecute(serverURL,"GuardarAutorizacion",varTramaAudit,CallBack_GuardarAutorizacion,GuardarAutorizacionError,"X");
						 
					}
											
					}
					else
					{
						alert('Operacion Rechazada');
					}		
					//PROY-31949 
					document.getElementById("lblEnvioPos").innerHTML  = "";
					f_CargarFormasPago();	
		
				}
				catch(err)
				{
					alert(err.description);
					document.getElementById("lblEnvioPos").innerHTML  = "";
					f_CargarFormasPago();
				}	
			
			}
			
			function SuccessMasterCard(objResponse,NameDoc)
			{
			
				try
				{
					var x2js = new X2JS();
					var jsonObj = x2js.xml_str2json(objResponse);
					
					var varImpVoucher = '';
					var varDesRpta = '';
					var varClienteMCD = '';
				
					var varCodOperMCD = '';
					var varTramaUpdate = '';
					var varCodRptaAudit = '';
					var varMsjRptaAudit = '';
					var varCodRptaWs = '';
					var varMsgAlert = '';
					var varPrintData = '';					
				
					var varIdUnicoTrans = '';
					var varFlagPago = '1';
					var varIdCabez = document.getElementById("HidIdCabez").value;
					
					varCodRptaAudit = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.AuditResponse.CodigoRespuesta;
					varMsjRptaAudit = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.AuditResponse.mensajeRespuesta;	
					varCodRptaWs = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.CodigoRespuesta;
					
					varNroRef = varNroRef;
					varNroTarjeta = varNroTarjeta;
					varMontoOperacion = objEntityPOS.montoOperacion;
					
					if(varCodRptaAudit != "0")
			        {			        
						varEstTran = varArrayEstTrans[3];//RECHAZADO
						varDesRpta = varMsjRptaAudit;	
						sCodRespTarj = varCodRptaAudit;	
						      
			        } 
			        else
			        {
			        
			         	if (varTipoTrans == '3')
						{	
							if (varCodRptaWs == "00")
			                {
								sCodRespTarj = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.CodigoRespuesta;
								varDesRpta = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.DescripcionRespuesta;
								
								
								varImpVoucher = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.ImprimirData;
								
								varEstTran = varArrayEstTrans[2];
					
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
			                else
			                {
								
								sCodRespTarj = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.CodigoRespuesta;
								varDesRpta = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.DescripcionRespuesta;
								varImpVoucher = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.ImprimirData;
								varEstTran = varArrayEstTrans[3];
																
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
			            }
			            if (varTipoTrans == '2')
						{
							if (varCodRptaWs == "00")
							{
								if (typeof jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.CodigoAdquiriente == 'undefined')					
									varIdUnicoTrans = '';
								else
									varIdUnicoTrans = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.CodigoAdquiriente;
					
							
								sCodRespTarj = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.CodigoRespuesta;
								varDesRpta = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.DescripcionRespuesta;
								varClienteMCD = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.NombreCliente;   
								varNumAutTransaccion = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.NumeroAutorizacion;					
						
						
								varNroRefMC = trim1(varNroRefMC)							
								varIdRefAnu = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.NumeroReferencia;
							    varIdRefAnu = (varIdRefAnu == null) ? '' : String(varIdRefAnu).replace("REF","");
							
						        varEstTran = varArrayEstTrans[2];//ACEPTADO
						
								varNroTarjeta = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.NumeroTarjeta;
								varFechaExpiracion = '';
								varCodOperMCD = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.CodigoAprobacion;
								varSeriePOS = '';
								varImpVoucher = '';
								varImpVoucher = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.ImprimirData;
								varNroReferencia = varNroRefMC;
						
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
															
								varMontoOperacion = ''; 
								varMontoOperacion = objEntityPOS.montoOperacion;
								varNomEquipoPOS = ''; 
								varNomEquipoPOS = varNomPcPos;
							}
							else
							{
							    sCodRespTarj = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.CodigoRespuesta;
								varDesRpta = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.DescripcionRespuesta;								
								varEstTran = varArrayEstTrans[3];																
												
							}
						}	//Fin de Anular	        
			        
			        } //Fin de Transaccion
			                  //PROY-31949 
			                  if(varTipoTrans == varArrayTipoTran[1] && varEstTran == varArrayEstTrans[3])
					{
						if(varCont > MaxNumIntentosAnular)
						{
							varEstTran = varArrayEstTrans[4];//INCONPLETO
							varDesRpta = document.getElementById("HidMsjErrorNumIntentos").value;
							//BloqueoBotones();	
					}  
						else
					{
							varEstTran = varArrayEstTrans[3];//RECHAZADO												
						}					
					}  
					 //PROY-31949 
					varTramaUpdate = '';
					varTramaUpdate = 'monedaOperacion=' + varMoneda + 
					'|montoOperacion=' + varMontoOperacion + 
					'|nroRegistro=' + varNroRegistro + 
					'|numVoucher=' + varNroRef + 
					'|numAutTransaccion=' + varNumAutTransaccion + 
					'|codRespTransaccion=' + sCodRespTarj + 
					'|descTransaccion=' + varDesRpta + 
					'|codAprobTransaccion=' + varCodOperMCD + 
					'|tipoPos=' + varTipoPos + 
					'|varNroTarjeta=' + varNroTarjeta + 
					'|fechaExpiracion=' + varFechaExpiracion + 
					'|nomCliente=' + varClienteMCD + 
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
					'|TipoPago=' + document.getElementById("HidTipoPago").value+
					'|ResTarjetaPos=';//PROY-31949 
	                
					
					RSExecute(serverURL,"ActualizarTransaction",varTramaUpdate,
					objEntityPOS.TrnsnId,CallBack_ActualizarTransaction2,GuardarTransactionError,"X");
					                 
					var varTramaAudit = '';    
					
					varTramaAudit = 'NomCliente=' + document.getElementById("txtNombCli").value + 
					'|NroTelefono=' + "" + 
					'|NroPedido=' + varNroPedido + 
					'|IdTransaccion=' + objEntityPOS.TrnsnId +
					'|nMonto=' + varTransMonto ;

					varIdTransAux = objEntityPOS.TrnsnId;
                                        //PROY-31949 
				        if(varTipoTrans == varArrayTipoTran[1] && varEstTran == varArrayEstTrans[3])
					{			
						if(varCont >= MaxNumIntentosAnular)
					{
						varTramaAuditAux = varTramaAudit;
						varCont = parseInt(varCont) + 1;
						RSExecute(serverURL,"GuardarAutorizacion",varTramaAudit,CallBack_GuardarAutorizacion,GuardarAutorizacionError,"X");
							  
						}						
					}
											
					if(varEstTran == varArrayEstTrans[2])
					{
					   alert('Operacion Exitosa');
					}
					else if (varEstTran == varArrayEstTrans[3])
					{
					   alert('Operacion Rechazada');
					}
					//PROY-31949 
					document.getElementById("lblEnvioPos").innerHTML  = "";
					f_CargarFormasPago();		
					
				}
				
				catch(err)
				{
					alert(err.description);
					document.getElementById("lblEnvioPos").innerHTML  = "";
					f_CargarFormasPago();
				}	
			
			
			}
	        function trim1 (string)
	        {
				return string.replace(/^\s\s*/, '').replace(/\s\s*$/, '');
			}
			function ErrorVisaNet(request,NameDoc,objEntityPOS)
			{
			
				try 
				{
				    varNroRef = varNroRef;
					varNroTarjeta = varNroTarjeta;
					varMontoOperacion = objEntityPOS.montoOperacion;
			        var varCodOperVisa = '';
			        var varClienteVisa = '';    
			        var varIdCabez = document.getElementById("HidIdCabez").value;  
			        var varIdUnicoTrans = '';
					var varFlagPago = '1';
					varEstTran = varArrayEstTrans[3];//RECHAZADO
					varDesRpta = "Error de timeout VISA";	
					sCodRespTarj = '';
								        
			            //PROY-31949 
				    if(varTipoTrans == varArrayTipoTran[1] && varEstTran == varArrayEstTrans[3])
			        {
						if(varCont > MaxNumIntentosAnular)
			        {
							varEstTran = varArrayEstTrans[4];//INCONPLETO
							varDesRpta = document.getElementById("HidMsjErrorNumIntentos").value;
							//BloqueoBotones();	
			        }
						else
						{							
							varEstTran = varArrayEstTrans[3];//RECHAZADO
							varDesRpta = document.getElementById("HidMsjErrorTimeOut").value;												
						}  
						}  
				     //PROY-31949 		
						varTramaUpdate = '';
						varTramaUpdate = 'monedaOperacion=' + varMoneda + 
						'|montoOperacion=' + varMontoOperacion + 
						'|nroRegistro=' + varNroRegistro + 
						'|numVoucher=' + varNroRef + 
						'|numAutTransaccion=' + varNumAutTransaccion + 
						'|codRespTransaccion=' + sCodRespTarj + 
						'|descTransaccion=' + varDesRpta + 
						'|codAprobTransaccion=' + varCodOperVisa + 
						'|tipoPos=' + varTipoPos + 
						'|varNroTarjeta=' + varNroTarjeta + 
						'|fechaExpiracion=' + varFechaExpiracion + 
						'|nomCliente=' + varClienteVisa + 
						'|impVoucher=' + varImpVoucher + 
						'|seriePOS=' + varSeriePOS + '|nomEquipoPOS=' + varNomEquipoPOS + 
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
						'|TipoPago=' + document.getElementById("HidTipoPago").value +
						'|ResTarjetaPos='; //PROY-31949 
		                
						
						RSExecute(serverURL,"ActualizarTransaction",varTramaUpdate,
						objEntityPOS.TrnsnId,CallBack_ActualizarTransaction2,GuardarTransactionError,"X");
						                 
						var varTramaAudit = '';    
						
						varTramaAudit = 'NomCliente=' + document.getElementById("txtNombCli").value + 
						'|NroTelefono=' + "" + 
						'|NroPedido=' + varNroPedido + 
						'|IdTransaccion=' + objEntityPOS.TrnsnId +
						'|nMonto=' + varTransMonto ;

						varIdTransAux = objEntityPOS.TrnsnId;
                                                //PROY-31949 
						if(varTipoTrans == varArrayTipoTran[1] && varEstTran == varArrayEstTrans[3])
						{			
							if(varCont >= MaxNumIntentosAnular)
						{
							varTramaAuditAux = varTramaAudit;
							varCont = parseInt(varCont) + 1;
							RSExecute(serverURL,"GuardarAutorizacion",varTramaAudit,CallBack_GuardarAutorizacion,GuardarAutorizacionError,"X");
					
						}
							else
						{
						   alert('Operacion Rechazada');
												
						}						
					
						}						
						//PROY-31949 											
						document.getElementById("lblEnvioPos").innerHTML  = "";
					    f_CargarFormasPago();					
			
			
				}
				catch(err)
				{
					alert(err.description);
					document.getElementById("lblEnvioPos").innerHTML  = "";
					f_CargarFormasPago();
				}	
				
			}
			
			function SuccessVisaNet(objResponse,NameDoc)
			{
				try{
					
					var x2js = new X2JS();
					var jsonObj = x2js.xml_str2json(objResponse);
					var varImpVoucher = '';
					var varDesRpta = '';
					var varClienteVisa = '';
					var varNumAutTransaccion = '';
					var varCodOperVisa = '';
					var varTramaUpdate = '';					
					var varNroRefVisa = '';
					var varCodRptaAudit = '';
					var varMsjRptaAudit = '';
					var varCodRptaWs = '';
					var varDesRptaWs = '';
					var varMsgAlert = '';
					var varPrintData = '';				
				
					var varIdUnicoTrans = '';
					var varFlagPago = '1';
					var varIdCabez = document.getElementById("HidIdCabez").value;	
					
				    varCodRptaAudit = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.AuditResponse.CodigoRespuesta;
			        varMsjRptaAudit = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.AuditResponse.mensajeRespuesta;	
			        varCodRptaWs = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.CodigoRespuesta;
			        
			        
			        varNroRef = varNroRef;
					varNroTarjeta = varNroTarjeta;
					varMontoOperacion = objEntityPOS.montoOperacion;
			        
			        if(varCodRptaAudit != "0")
			        {			        
						varEstTran = varArrayEstTrans[3];//RECHAZADO
						varDesRpta = varMsjRptaAudit;	
								        
			        } 
			        else
			        {				
			           	if (varTipoTrans == '3')
						{	
							if (varCodRptaWs == "0")
			                {
						
								varEstTran = varArrayEstTrans[2];		           
								varDesRpta = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.DescripcionRespuesta;
								varImpVoucher = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.ImprimirData;
								sCodRespTarj = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.CodigoRespuesta;
							
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
							
							else
							{
								varEstTran = varArrayEstTrans[3];
								varDesRpta = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.DescripcionRespuesta;
								varImpVoucher = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.ImprimirData;
								sCodRespTarj = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.CodigoRespuesta;
								
																
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
						}
						 
						if (varTipoTrans == '2')
						{
							if (varCodRptaWs == "00")
							{
								sCodRespTarj = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.CodigoRespuesta;
								varDesRpta = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.DescripcionRespuesta;
								varClienteVisa = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.NombreCliente;   
								varNumAutTransaccion = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.NumeroAutorizacion;
								varNroRef = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.NumeroReferencia;
								varNroTarjeta = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.NumeroTarjeta;     
								varFechaExpiracion = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.FechaExpiracion;
								varCodOperVisa = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.CodigoOperacion;
								varSeriePOS = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.Terminal;
								varImpVoucher = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.ImprimirData;							
								varNroReferencia = varNroRef;
								varEstTran = varArrayEstTrans[2];
								varIdRefAnu = varNroReferencia;							
							
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
								
								if (typeof jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.IdUnico == 'undefined')					
									varIdUnicoTrans = '';
								else
									varIdUnicoTrans = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.IdUnico;
								
							}
							else
							{
								varEstTran = varArrayEstTrans[3];
								varDesRpta = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.DescripcionRespuesta;
								varImpVoucher = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.ImprimirData;
								sCodRespTarj = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.CodigoRespuesta;
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
						}
					}					          
					//PROY-31949 	
					 if(varTipoTrans == varArrayTipoTran[1] && varEstTran == varArrayEstTrans[3])
				         { 
						if(varCont > MaxNumIntentosAnular)
					{
							varEstTran = varArrayEstTrans[4];//INCONPLETO
							varDesRpta = document.getElementById("HidMsjErrorNumIntentos").value;
							//BloqueoBotones();	
					}  
						else
					{
							varEstTran = varArrayEstTrans[3];//RECHAZADO																			
						}					
					}  
					//PROY-31949 
					varTramaUpdate = '';
					varTramaUpdate = 'monedaOperacion=' + varMoneda + 
					'|montoOperacion=' + varMontoOperacion + 
					'|nroRegistro=' + varNroRegistro + 
					'|numVoucher=' + varNroRef + 
					'|numAutTransaccion=' + varNumAutTransaccion + 
					'|codRespTransaccion=' + sCodRespTarj + 
					'|descTransaccion=' + varDesRpta + 
					'|codAprobTransaccion=' + varCodOperVisa + 
					'|tipoPos=' + varTipoPos + 
					'|varNroTarjeta=' + varNroTarjeta + 
					'|fechaExpiracion=' + varFechaExpiracion + 
					'|nomCliente=' + varClienteVisa + 
					'|impVoucher=' + varImpVoucher + 
					'|seriePOS=' + varSeriePOS + '|nomEquipoPOS=' + varNomEquipoPOS + 
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
					'|TipoPago=' + document.getElementById("HidTipoPago").value +
					'|ResTarjetaPos='; //PROY-31949 
					
					RSExecute(serverURL,"ActualizarTransaction",varTramaUpdate,
					objEntityPOS.TrnsnId,CallBack_ActualizarTransaction2,GuardarTransactionError,"X");
					                 
					var varTramaAudit = '';    
					
					varTramaAudit = 'NomCliente=' + document.getElementById("txtNombCli").value + 
					'|NroTelefono=' + "" + 
					'|NroPedido=' + varNroPedido + 
					'|IdTransaccion=' + objEntityPOS.TrnsnId +
					'|nMonto=' + varTransMonto ;

					varIdTransAux = objEntityPOS.TrnsnId;
                                          //PROY-31949 
					    if(varTipoTrans == varArrayTipoTran[1] && varEstTran == varArrayEstTrans[3])
						{			
							if(varCont >= MaxNumIntentosAnular)
					{
						varTramaAuditAux = varTramaAudit;
						varCont = parseInt(varCont) + 1;
						RSExecute(serverURL,"GuardarAutorizacion",varTramaAudit,CallBack_GuardarAutorizacion,GuardarAutorizacionError,"X");
							   
					}
											
						}					
				  
						if(varEstTran == varArrayEstTrans[2])
						{
							 alert('Operacion Exitosa');
						}
						else if (varEstTran == varArrayEstTrans[3])
						{
						   alert('Operacion Rechazada');
						}		
					//PROY-31949 				
						document.getElementById("lblEnvioPos").innerHTML  = "";
					    f_CargarFormasPago();	
									
				}
				catch(err)
			    {
					alert(err.description);
					document.getElementById("lblEnvioPos").innerHTML  = "";
					f_CargarFormasPago();
					
				}
				   
			}

		
			function f_limpiarTabla()
			{
			  var table = document.getElementById('tbl_FormasPago')
				  rowCount =table.rows.length; 
			       // RECORRE LA TABLA
              for (var i = 1; i <= rowCount - 1; i++) {                  
               // RECORRE LAS FILAS
                  var row = table.rows[1];
                  table.deleteRow(1);
               }
			
			}
			function f_CargarFormasPago()
			{			
			 var NroPedido = document.getElementById("txtNroPedido").value;	
			 var TipoPago = document.getElementById("HidTipoPago").value;	
			 RSExecute(serverURL,"CargarFormasPago",NroPedido,TipoPago,CallBack_CargarFormasPago,Error_CargarFormasPago,"X");  
			
			}
			
			function Error_CargarFormasPago(response)
			{
			  var varRpta = response.return_value;
	          var res = (varRpta.replace("<BODY><SELECT>", "")).replace("</SELECT>", "").replace("\r\n","");        
	    
	          alert(res);
			}
							
			function CallBack_CargarFormasPago(response) 
	        {	
	         var varRpta = response.return_value;
			 var res = (varRpta.replace("<BODY><SELECT>", "")).replace("</SELECT>", "").replace("\r\n","");
		    
		     varRpta = res;	
		     
		    if( varRpta.substr(0,1) == '0')
			{
			  varRpta = varRpta.split("!")[1];		  
			  var tbody = document.getElementById('tbl_FormasPago').getElementsByTagName("TBODY")[0];
			  var table = document.getElementById('tbl_FormasPago')			  
              var filas = varRpta.split(";");
              var celdas;
              var tr;
              var td;
              var contFilasOk = 0;
              var contFilasEvaluar = 0;
              
              if (table.rows.length>1)
              {
                 f_limpiarTabla();
              }  
                 
              for (var i = 0 ; i< filas.length; i++)
              {  
                celdas = filas[i].split("|");
                
                					
                 tr = document.createElement("TR");
                 for (var j = 0 ; j<= 7; j++)
                 {  
                   if (j<=5)
                   { 
                   td = document.createElement("TD");                   
                   td.appendChild(document.createTextNode(celdas[j]));
                   tr.appendChild(td);
                   
                   }         
                   
                   if (j==6)
                   {
                        td = document.createElement("TD");
                       
                        
                         if (celdas[2]!= "")
						   {
								document.getElementById("HidIdCabez").value = celdas[15];
								 contFilasEvaluar = contFilasEvaluar + 1 ;
						   } 
                        
						if (celdas[5]== document.getElementById("HidTransacExitosa").value || celdas[5]== document.getElementById("HidTransacIncompleta").value)
						{  				   
						   
						   td.insertAdjacentHTML("afterbegin","<a><IMG style='CURSOR: hand' border='0'  src='../images/delete-icon_ena.png'></a>");
						   //td.appendChild(document.createTextNode("")); 
						   contFilasOk = contFilasOk  + 1; 
						}
						else
						{    
						     if (celdas[2]== "")
						     {
								td.appendChild(document.createTextNode("")); 
							 }
							 else
							 { 
							     td.insertAdjacentHTML("afterbegin","<a onclick=f_Evaluar_datos('EliminarPOS','"+celdas[2]+"','"+celdas[3]+"','"+celdas[8]+"','"+celdas[10]+"','"+celdas[11]+"','"+celdas[12]+"','"+celdas[13]+"','"+celdas[14]+"','"+celdas[16]+"');><IMG style='CURSOR: hand' border='0'  src='../images/delete-icon.png'></a>"); 
								 
						}
						}
						
							tr.appendChild(td);                   
                   }
                   if (j==7)
                   {
                       td = document.createElement("TD");
                       
                          
                          
                       if (celdas[2]== "")
						{  
						   td.appendChild(document.createTextNode("")); 
						}
						else   
					   { 
							if (celdas[4]== "ACEPTADO")
							{
							 td.insertAdjacentHTML("afterbegin","<a onclick=f_Evaluar_datos('ImprimirPOS','"+celdas[2]+"','"+celdas[3]+"','"+celdas[8]+"','"+celdas[10]+"','"+celdas[11]+"','"+celdas[12]+"','"+celdas[13]+"','"+celdas[14]+"','"+celdas[16]+"');><IMG style='CURSOR: hand' border='0'  src='../images/print-icon.png'></a>");
					 
		                       
							}
							else
							{   
							td.insertAdjacentHTML("afterbegin","<a><IMG style='CURSOR: hand' border='0'  src='../images/print-icon_ena.png'></a>");
									                       
							} 
					   }
							tr.appendChild(td); 
                   }  
                }
                tbody.appendChild(tr);
              }  
              
                document.getElementById('HidFlagGuardar').value = 0;
              
				if (contFilasEvaluar == 0)
				{
					blnClosingMod = false; 
					document.getElementById("HidFlagGuardar").value = '1';
					document.getElementById('btnGrabar').disabled = false;
					document.getElementById('btnCancelar').disabled = false;
				}
				else
				{
					document.getElementById('btnGrabar').disabled = true;
									                       
					if(contFilasOk == 0)
					{
					    document.getElementById('btnCancelar').disabled = false;
						blnClosingMod = false;
							} 
					else
					{
						document.getElementById('btnCancelar').disabled = true;
						blnClosingMod = true;
						
						if(contFilasEvaluar != contFilasOk)
						{
							document.getElementById('btnGrabar').disabled = true;
					   }
						else
						{
							document.getElementById('btnGrabar').disabled = false;
							document.getElementById('HidFlagGuardar').value = 1;
                   }  
                }
              }  
              
                            
               if (contFilasEvaluar == contFilasOk)
               {
                   document.getElementById('HidFlagGuardar').value = 1;
                   document.getElementById('btnGrabar').disabled = false;
                   
                   	
               }
               else
               {
                   document.getElementById('HidFlagGuardar').value = 0;
                   document.getElementById('btnGrabar').disabled = true;
               }
               
               if(contFilasEvaluar > 0 && contFilasOk >0)
               {
                   document.getElementById('btnCancelar').disabled = true;
               }
               else
               {
                  document.getElementById('btnCancelar').disabled = false;
               }
             }
             else
             {             
                alert(varRpta.split("!")[1]);
                document.getElementById('tbl_FormasPagoC').style.setAttribute('display', 'none');
                document.getElementById('HidFlagGuardar').value = 1;
             }
             
             
			}
			

		//PROY-27440 FIN
	
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
							    blnClosingMod = false; 				
								if (!f_Grabar()) event.returnValue = false;
								break;							
							case "btnCancelar":	
							    blnClosingMod = false;
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
			function e_mayuscula() {
				if ((event.keyCode>96&&event.keyCode<123)||(event.keyCode==241)||(event.keyCode==250)||(event.keyCode==243)||(event.keyCode==237)||(event.keyCode==233)||(event.keyCode==225))
					event.keyCode=event.keyCode-32;
				
			}
			function f_Validar() {// valida campos
				if (!ValidaFechaA('document.frmPrincipal.txtFecConta',false)) return false;		
				if (!ValidaAlfanumerico('document.frmPrincipal.txtNotaCre','el campo Nota de Crédito ',false)) return false;				
				if (!ValidaAlfanumerico('document.frmPrincipal.txtBolFact','el campo Factura/Boleta ',false)) return false;
				if (!ValidaDecimal('document.frmPrincipal.txtMonto','el campo Monto a Aplicar ',false)) return false;				
				return true;  
			};
			
			function f_ConsultaNC(){
					frmPrincipal.cmdConsultaNC.click();	
			}
			
			function f_Cancelar(){
                                //PROY-27440 INICIO
				f_validar_switch(0);
				document.getElementById('txtNotaCre').value = "";
				document.getElementById('txtNombCli').value = "";
				document.getElementById('txtBolFact').value = "";
				document.getElementById('txtFechPago').value = "";
				document.getElementById('txtNroPedido').value = "";
				document.getElementById('txtMonto').value = "0.00";
				document.getElementById('btnGrabar').disabled = false;
		                //PROY-27440 FIN
			}
			

		//-->
		</script>
	</HEAD>
	<body>
		<form id="frmPrincipal" method="post" runat="server">
			<DIV align="center">
				<table border="0" cellSpacing="0" cellPadding="0" align="center">
					<tr>
						<td>
							<table border="1" cellSpacing="0" cellPadding="0" width="700" align="center">
								<tr borderColor="#336699">
									<td align="center">
										<table border="0" cellSpacing="0" cellPadding="0" width="100%" align="center">
											<tr>
												<td height="32" vAlign="top" width="10"></td>
												<td style="PADDING-TOP: 4px" class="TituloRConsulta" height="32" vAlign="top" width="98%"
													align="center">Devoluciones</td>
												<td height="32" vAlign="top" width="10"></td>
											</tr>
										</table>
										<table border="0" cellSpacing="0" cellPadding="0" width="100%" align="center">
											<tr>
												<td vAlign="top" width="14"></td>
												<td style="PADDING-BOTTOM: 5px; PADDING-LEFT: 5px; PADDING-RIGHT: 5px; PADDING-TOP: 5px"
													width="98%">
													<table border="0" cellSpacing="0" cellPadding="0" width="680">
														<tr>
															<td height="4"></td>
														</tr>
														<tr>
															<td height="18">
																<table border="0" cellSpacing="1" cellPadding="0">
																	<tr class="Arial12b">
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
													<table border="0" cellSpacing="0" cellPadding="0" width="680">
														<tr>
															<td>
																<table border="1" cellSpacing="2" borderColor="white" cellPadding="0" width="100%">
																	<tr>
																		<td width="20">&nbsp;</td>
																		<td class="Arial12b" width="160">&nbsp;&nbsp;&nbsp;Fecha Contabilización (*):</td>
																		<td vAlign="middle" width="170"><input id="txtFecConta" class="clsInputEnable" tabIndex="4" maxLength="10" size="25" name="txtFecConta"
																				runat="server"> <A onmouseover="window.status='Date Picker';return true;" onmouseout="window.status='';return true;"
																				href="javascript:show_calendar('frmPrincipal.txtFecConta');"><IMG border="0" src="../../images/botones/btn_Calendario.gif"></A>
																		</td>
																	</tr>
																	<tr>
																		<td>&nbsp;</td>
																		<td class="Arial12b" width="170">&nbsp;&nbsp;&nbsp;Nota de Crédito (*):</td>
																		<td class="Arial12b"><input id="txtNotaCre" class="clsInputEnable" tabIndex="2" onkeypress="e_mayuscula();"
																				maxLength="20" size="30" name="txtNotaCre" runat="server"></td>
																		<td height="22" width="25">&nbsp;</td>
																		<td class="Arial12b" width="170"></td>
																		<td class="Arial12b"></td>
																	</tr>
																</table>
															</td>
														</tr>
														<!--Proy-27440-Inicio-->
														<tr>
															<td>
																<table border="1" cellSpacing="2" borderColor="white" cellPadding="0" width="100%">
																	<tr>
																		<td style="TEXT-ALIGN: center"><INPUT style="WIDTH: 98px" id="btnBuscar" class="BotonOptm" onclick=" blnClosingMod = false; f_ConsultaNC()"
																				value="Buscar" type="button"></td>
																	</tr>
																	<tr>
																		<td>&nbsp;</td>
																	</tr>
																</table>
															</td>
														</tr>
														<!--Proy-27440-Fin-->
													</table>
													<!--Proy-27440-Inicio-->
													<table border="0" cellSpacing="0" cellPadding="0" width="680">
														<tr>
															<td height="4"></td>
														</tr>
														<tr>
															<td height="18">
																<table border="0" cellSpacing="1" cellPadding="0">
																	<tr class="Arial12b">
																		<td width="200"><font color="#ff0000">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Datos 
																					del Documento Pagado</b></font></td>
																	</tr>
																</table>
															</td>
														</tr>
														<tr>
															<td height="4"></td>
														</tr>
													</table>
													<table border="0" cellSpacing="0" cellPadding="0" width="680">
														<tr>
															<td>
																<table border="1" cellSpacing="2" borderColor="white" cellPadding="0" width="100%">
																	<tr>
																		<td width="20">&nbsp;</td>
																		<td class="Arial12b" width="160">&nbsp;&nbsp;&nbsp;Nombre Cliente :</td>
																		<td width="170" colSpan="4"><input id="txtNombCli" class="clsInputDisable" tabIndex="4" readOnly maxLength="30" size="120"
																				name="txtFecConta" runat="server">
																		</td>
																	</tr>
																	<tr>
																		<td>&nbsp;</td>
																		<td class="Arial12b" width="170">&nbsp;&nbsp;&nbsp;Nro Fact SUNAT:</td>
																		<td class="Arial12b"><input id="txtBolFact" class="clsInputDisable" tabIndex="2" readOnly maxLength="20" onkeypress="e_mayuscula();" size="30"
																				name="txtBolFact" runat="server"></td>
																		<td height="22" width="25">&nbsp;</td>
																		<td class="Arial12b" width="170">&nbsp;&nbsp;&nbsp;Fecha :</td>
																		<td class="Arial12b"><input id="txtFechPago" class="clsInputDisable" tabIndex="2" readOnly maxLength="20" size="30"
																				name="txtBolFact" runat="server"></td>
																	</tr>
																	<tr>
																		<td>&nbsp;</td>
																		<td class="Arial12b" width="170">&nbsp;&nbsp;&nbsp;Nro Pedido :</td>
																		<td class="Arial12b"><input id="txtNroPedido" class="clsInputDisable" tabIndex="2" readOnly maxLength="16" size="30"
																				name="txtNroPedido" runat="server"></td>
																		<td height="22" width="25">&nbsp;</td>
																		<td class="Arial12b" width="170">&nbsp;&nbsp;&nbsp;Monto Pagado :</td>
																		<td class="Arial12b"><input id="txtMonto" class="clsInputDisable" tabIndex="2" readOnly maxLength="16" size="30"
																				name="txtMonto" runat="server"></td>
																	</tr>
																</table>
															</td>
														</tr>
													</table>
													<!--Proy-27440 --Inicio-->
													<table style="DISPLAY: none" id="tbl_FormasPagoC" border="0" cellSpacing="0" cellPadding="0"
														width="680">
														<tr>
															<td>
																<table border="0" cellSpacing="0" cellPadding="0" width="100%" align="center">
																	<tr>
																		<td height="18">
																			<table border="0" cellSpacing="1" cellPadding="0">
																				<tr class="Arial12b">
																					<td width="200"><font color="#ff0000">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Medios 
																								de Pago</b></font></td>
																				</tr>
																			</table>
																		</td>
																	</tr>
																	<tr>
																		<td height="10" align="center">
																			<asp:Label id="lblEnvioPos" runat="server" CssClass="TituloRConsulta"></asp:Label>
																		</td>
																	</tr>
																	<tr>
																		<td>
																			<table border="1" cellSpacing="1" borderColor="#ffffff" cellPadding="1" width="100%">
																				<tr class="Arial12B" height="21">
																					<td style="TEXT-ALIGN: center; WIDTH: 100%">
																						<table style="BORDER-COLLAPSE: collapse" id="tbl_FormasPago" class="tbl_pagos">
																							<tbody>
																								<tr>
																									<th style="WIDTH: 150px">
																										FORMA PAGO
																									</th>
																									<th style="WIDTH: 70px">
																										TIPO TARJETA
																									</th>
																									<th style="WIDTH: 150px">
																										NUMERO TARJETA
																									</th>
																									<th style="WIDTH: 60px">
																										MONTO PAGADO
																									</th>
																									<th style="WIDTH: 110px">
																										ESTADO ANULACION
																									</th>
																									<th style="WIDTH: 160px">
																										RESULTADO PROCESO
																									</th>
																									<th style="WIDTH: 25px">
																									</th>
																									<th style="WIDTH: 25px">
																									</th>
																								</tr>
																							</tbody>
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
													<!--Proy-27440 --Fin -->
												</td>
												<td vAlign="top" width="14"></td>
											</tr>
										</table>
										<table border="0" cellSpacing="0" cellPadding="0" width="100%" align="center">
											<tr>
												<td height="4"></td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td>
							<table border="0" cellSpacing="0" cellPadding="0" width="100%" align="center">
								<tr>
									<td height="10"></td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td>
							<table border="1" cellSpacing="0" borderColor="#336699" cellPadding="0" width="400" align="center">
								<tr>
									<td align="center">
										<table border="0" cellSpacing="2" cellPadding="0">
												<tr>
													<td width="28" align="center"></td>
													<td width="85" align="center"><asp:button id="btnGrabar" runat="server" Text="Grabar" CssClass="BotonOptm" Width="98px"></asp:button></td>
													<td width="28" align="center"></td>
													<td width="85" align="center"><INPUT style="WIDTH: 98px" id="btnCancelar" class="BotonOptm" onclick="f_Cancelar()" value="Cancelar"
															type="button" name="btnCancelar"></td>
													<td width="28" align="center"></td>
												</tr>
												<tr>
													<td>
														<p style="Z-INDEX: 0; DISPLAY: none"><asp:button id="cmdConsultaNC" runat="server" Text="Button"></asp:button></p>
													</td>
											</tr> <!--Proy-27440 -->
										</table> <!--Proy-27440 -->
									</td>
								</tr>
							</table>
									</td>
								</tr>
					<tr>
						<td><input id="hidMensajeNC" type="hidden" name="hidMensajeNC" runat="server"><input id="hidPedidoOrigenIot" type="hidden" name="hidPedidoOrigenIot" runat="server"><!--PROY-140379-->
						</td>
						<td>
							<!--PROY-27440 INI--><input id="HidPtoVenta" type="hidden" name="HidPtoVenta" runat="server">
							<input id="HidIntAutPos" type="hidden" name="HidIntAutPos" runat="server"> <input id="HidCodOpera" type="hidden" name="HidCodOpera" runat="server">
							<input id="HidDesOpera" type="hidden" name="HidDesOpera" runat="server"> <input id="HidTipoOpera" type="hidden" name="HidTipoOpera" runat="server">
							<input id="HidTipoTarjeta" type="hidden" name="HidTipoTarjeta" runat="server"> <input id="HidTipoPago" type="hidden" name="HidTipoPago" runat="server">
							<input id="HidEstTrans" type="hidden" name="HidEstTrans" runat="server"> <input id="HidTipoPOS" type="hidden" name="HidTipoPOS" runat="server">
							<input id="HidTipoTran" type="hidden" name="HidTipoTran" runat="server"> <input id="HidTipoMoneda" type="hidden" name="HidTipoMoneda" runat="server">
							<input id="HidTransMC" type="hidden" name="HidTransMC" runat="server"> <input id="HidMonedaMC" type="hidden" name="HidMonedaMC" runat="server">
							<input id="HidApliPOS" type="hidden" name="HidApliPOS" runat="server"> <input id="HidMonedaVisa" type="hidden" name="HidMonedaVisa" runat="server">
							<input id="HidFlagGuardar" type="hidden" name="HidFlagGuardar" runat="server"> <input id="HidCajero" type="hidden" name="HidCajero" runat="server">
							<input id="HidDatoPosVisa" type="hidden" name="HidDatoPosVisa" runat="server"> <input id="HidDatoPosMC" type="hidden" name="HidDatoPosMC" runat="server">
							<input id="HidDatoAuditPos" type="hidden" name="HidDatoAuditPos" runat="server">
							<input id="HidIdCabez" type="hidden" name="HidIdCabez" runat="server"> <input id="HidMsjCajero" type="hidden" name="HidMsjCajero" runat="server">
							<input id="HidMsjErrorGuardarTrans" type="hidden" name="HidMsjErrorGuardarTrans" runat="server">
							<input id="HidTransacExitosa" type="hidden" name="HidTransacExitosa" runat="server">
							<input id="HidTransacIncompleta" type="hidden" name="HidTransacIncompleta" runat="server">
							<input id="HidMsjValIP" type="hidden" name="HidMsjValIP" runat="server"> 
							<input id="HidMontoTotal" type="hidden" name="HidMontoTotal" runat="server"> 
							<input id="HidMontoEfectivo" type="hidden" name="HidMontoEfectivo" runat="server"> 
							<!--PROY-27440 FIN-->
                                                        <!--PROY-31949 INI--><input id="HidNumIntentosPago" type="hidden" name="HidNumIntentosPago" runat="server">
							<input id="HidNumIntentosAnular" type="hidden" name="HidNumIntentosAnular" runat="server">
							<input id="HidMsjErrorNumIntentos" type="hidden" name="HidMsjErrorNumIntentos" runat="server">
							<input id="HidMsjErrorTimeOut" type="hidden" name="HidMsjErrorTimeOut" runat="server">
							<input id="HidFlagCajaCerrada" type="hidden" name="HidFlagCajaCerrada" runat="server">
							<input id="HidMsjCajaCerrada" type="hidden" name="HidMsjCajaCerrada" runat="server">
							<!--PROY-31949 FIN-->
                                                 </td>
					</tr>
				</table>
				</TD></TR></TBODY></TABLE></DIV>
			</TABLE></form>
	</body>
</HTML>
