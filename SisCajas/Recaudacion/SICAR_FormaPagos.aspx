<%@ Page Language="vb" AutoEventWireup="false" Codebehind="SICAR_FormaPagos.aspx.vb" Inherits="SisCajas.SICAR_FormaPagos" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>SICAR_FormaPagos</title>
		<meta name="vs_snapToGrid" content="True">
		<meta name="vs_showGrid" content="True">
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<meta content="no-cache" http-equiv="Pragma">
		<META content="Mon, 06 Jan 1990 00:00:01 GMT" http-equiv="Expires">
		<LINK rel="styleSheet" type="text/css" href="../estilos/est_General.css">
		<script language="JavaScript" src="../librerias/ubigeo.js"></script>
		<script language="JavaScript" src="../librerias/date-picker.js"></script>
		<script language="JavaScript" src="../librerias/Lib_FuncValidacion.js"></script>
		<script language="JavaScript" src="../librerias/Lib_FuncGenerales.js"></script>
		<script language="JavaScript" src="../librerias/msrsclient.js"></script>
		<script language="JavaScript" src="../librerias/jquery-1.8.2.min.js"></script>
		<script language="JavaScript" src="../Scripts/form.js"></script>
		<script language="JavaScript" src="../Scripts/xml2json.js"></script>
		<script language="JavaScript" src="../Scripts/operacionPOS.js"></script>
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<style>.tbl_pagos { BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; FONT-SIZE: 10px; BORDER-LEFT: #999999 1px solid; BORDER-BOTTOM: #999999 1px solid; FONT-FAMILY: Arial; TEXT-DECORATION: none }
	.tbl_pagos TH { PADDING-BOTTOM: 5px; COLOR: #ffffff; PADDING-TOP: 5px; HEIGHT: 22px; BACKGROUND-COLOR: #21618c; TEXT-ALIGN: center }
	.tbl_pagos TD { BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; BORDER-BOTTOM: #999999 1px solid; TEXT-ALIGN: center }
		</style>
		<script type="text/javascript">

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

		//PROY-27440 INI//PROY-27440 INI
			var serverURL =  '../Pos/ProcesoPOS.aspx';
			var webServiceURL = '<%= System.Configuration.ConfigurationSettings.AppSettings("consWsLocal_Url") %>';
			var timeOutWsLocal = '<%= System.Configuration.ConfigurationSettings.AppSettings("consWsLocal_Url_TimeOut") %>';
			var tiposError = 'EXC_';
                        var MaxNumIntentosAnular;//PROY-31949 
			var sCodRespTarj;
			var varCont;
			var varTramaInsert;
			var varTramaAuditAux;
			var varIdTransAux;
			var varEstadoTrans;				
			//Mastercard
			var varTransMC;
			var varMonedaMC;
			var varApliMC;
			var varNroRefMC;
			var varMontoMC;
			var varPwdComercioMC = '';
			//VisaNet
			var varMonedaVisa;
			var varMontoVisa;
			var varNroRefVisa;
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
			var varCodOper;
			var varDescriOpe;
			var varMoneda;
			var	varNroPedido;
			var	varTipoPago; //DEVOLUCIONES
			var varIdRefAnu='';
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
			var varMontoOperacion;
			//
			//Parametros SICAR POS
			var varArrayEstTrans;
			var varArrayCodOpe;
			var varArrayDesOpe;
			var varArrayCodTarjeta;
			var varArrayTipoPOS;
			var varArrayTipoTran;
			var varArrayTipoOper;	
			//newa
			var varTipoReporte="";		
			var PROCESAR_ANULACION="1";
			var EXPORTAR_EXCEL="2";
			var CodPDV="";
			var CodCajero="";
			var varBolGetTarjeta;
			var varBolWsLocal;
			var varNroTienda;
			var varCodEstablec;
			var varCodigoCaja;
			var varNomPcPos;
			var varCodTerminal;
			var varIpPos;
			var varTipoOpe;
			var varNomCliente='';
			var varNroTelefono =''; 
			var varNroPedido =''; 
			var varIdCabecera='';
			var varIdCabez='';
			var arrayEventsPrint;
			var arrayEventsCan;
			var varBolFact='';
			var varBoolAut='0';
			var nroFilas;
			var nroIncompleta = 0;
			function f_Back(){
				window.history.back();
			}
			function f_InicializarDatos(strTipo){
				varTipoReporte=strTipo;
				CodPDV=document.getElementById("HidPtoVenta").value;
				CodCajero=document.getElementById("hdnUsuario").value;
				// 1-Procesar Anulacion // 2--Exportar Excel
				if(varTipoReporte==PROCESAR_ANULACION){
				document.getElementById("btnExportar").style.setAttribute('display', 'none');
				document.getElementById("btnGrabar").style.setAttribute('display', 'block');
				}
				if(varTipoReporte==EXPORTAR_EXCEL){
				document.getElementById("btnExportar").style.setAttribute('display', 'block');
				document.getElementById("btnGrabar").style.setAttribute('display', 'none');
				}
				
				// llamda a la funcion que cargara la data de los pagos
					f_CargarFormasPago();		
			}
			//ini POS
			function CallBack_GuardarAutorizacion(response)
			{         
				
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
			
			function f_Evaluar_datos(TRANSACCIONID,TIPO_TRANSACCION,NRO_TARJETA,MONTO,NRO_INTENTOS,CAJERO,MONEDA,NUMERO_TELEFONO,TIPO_TARJETA_POS,ID_REF,IP_CAJA,IDCAB)
			{
			        blnClosingMod = false; 
				NRO_INTENTOS = parseInt(NRO_INTENTOS) + 1;  //PROY-31949 
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
				//Caja Cerrada
				 if(document.getElementById("HidFlagCajaCerrada").value == '1')
				 {	  
					alert(document.getElementById("HidMsjCajaCerrada").value);
					document.getElementById("lblEnvioPos").innerHTML  = "";	
					return;
				}
				//ip
					if(IP_CAJA!=varArrayDatoPosVisa[7].split("=")[1]){
					alert(document.getElementById("HidMsjCajero").value)
					document.getElementById("lblEnvioPos").innerHTML  = "";		
					return;
				}
				//cajero
				
				if(CAJERO !=  document.getElementById("hdnUsuario").value)
				{				
					alert(document.getElementById("HidMsjCajero").value)
					document.getElementById("lblEnvioPos").innerHTML  = "";		
					return;
				}
				varNroTarjeta=NRO_TARJETA;
				varTransMonto=Number(MONTO).toFixed(2) ; 
				varMoneda=MONEDA;	
				varNroTelefono =  NUMERO_TELEFONO;	
				varValueTar = TIPO_TARJETA_POS;
				varValueTarAux = TIPO_TARJETA_POS;
				varNroRef = ID_REF;
				varCont = parseInt(NRO_INTENTOS);	
				varIdCabecera=IDCAB;
				varNroPedido = document.getElementById("txtNroPedido").value;
				varTipoPago = document.getElementById("HidTipoPago").value; //DEVOLUCIONES
				var CajeroSession=document.getElementById("hdnUsuario").value;			
				varNomCliente = document.getElementById("txtNombCli").value;
				varBolFact = document.getElementById("txtBolFact").value;
				var varTramaAudit = '';    
				
				varTramaAudit = 'NomCliente=' + varNomCliente + 
				'|NroTelefono=' + "" + 
				'|NroPedido=' + varBolFact + 
				'|IdTransaccion=' + TRANSACCIONID +
				'|nMonto=' + MONTO ;		
				/*if(CajeroSession!=CAJERO){
					
					alert("EL PAGO NO SE REALIZ");
				}*/
				if (NRO_INTENTOS <= MaxNumIntentosAnular || TIPO_TRANSACCION=="ImprimirPOS")//PROY-31949 
				{					
					f_EnvioPOS(TIPO_TRANSACCION);
				}
				else
				{  
					varBoolAut='1';
					varTramaAuditAux = varTramaAudit;
					RSExecute(serverURL,"GuardarAutorizacion",varTramaAudit,CallBack_GuardarAutorizacion,GuardarAutorizacionError,"X");												
				}
				
			}			
			
			function f_EnvioPOS(TIPO_TRANSACCION)
			{						
			document.getElementById("lblEnvioPos").innerHTML = "Enviando al POS...";
					BloquearBotones();
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
				varPwdComercioMC='';			  
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
				varNroRefMC = '';
				 var varArrayDatoPosVisa = document.getElementById("HidDatoPosVisa").value.split("|");
				var varArrayDatoPosMC = document.getElementById("HidDatoPosMC").value.split("|");
    
				varNroRegistro = ''; varNroTienda = '';
				varCodigoCaja = ''; varCodEstablec = '';
				varNomPcPos = ''; varCodTerminal = '';
				varIpPos = '';
				if(varValueTar=="VISA"){
					varNroRegistro = varArrayDatoPosVisa[0].substr(varArrayDatoPosVisa[0].indexOf("=")+1);
					varNroTienda = varArrayDatoPosVisa[1].substr(varArrayDatoPosVisa[1].indexOf("=")+1);
					varCodigoCaja = varArrayDatoPosVisa[2].substr(varArrayDatoPosVisa[2].indexOf("=")+1);
					varCodEstablec = varArrayDatoPosVisa[3].substr(varArrayDatoPosVisa[3].indexOf("=")+1);
					varNomPcPos = varArrayDatoPosVisa[4].substr(varArrayDatoPosVisa[4].indexOf("=")+1);
					varCodTerminal = varArrayDatoPosVisa[6].substr(varArrayDatoPosVisa[6].indexOf("=")+1);
					varIpPos = varArrayDatoPosVisa[7].substr(varArrayDatoPosVisa[7].indexOf("=")+1);
					}else{
		varNroRegistro = varArrayDatoPosMC[0].substr(varArrayDatoPosMC[0].indexOf("=") + 1);
        varNroTienda = varArrayDatoPosMC[1].substr(varArrayDatoPosMC[1].indexOf("=") + 1);
        varCodigoCaja = varArrayDatoPosMC[2].substr(varArrayDatoPosMC[2].indexOf("=") + 1);
        varCodEstablec = varArrayDatoPosMC[3].substr(varArrayDatoPosMC[3].indexOf("=") + 1);
        varNomPcPos = varArrayDatoPosMC[4].substr(varArrayDatoPosMC[4].indexOf("=") + 1);
        varCodTerminal = varArrayDatoPosMC[6].substr(varArrayDatoPosMC[6].indexOf("=") + 1);
        varIpPos = varArrayDatoPosMC[7].substr(varArrayDatoPosMC[7].indexOf("=") + 1);

					}
				switch (varValueTar)
				{
					case "VISA":
					varTipoPos= varArrayTipoPOS[0];
					varTipTarjeta = varArrayCodTarjeta[0];//VISA
					varValueTar = "VIS";
					break;
					
					case "MASTERCARD":		
					varTipoPos= varArrayTipoPOS[1];
					varTipTarjeta = varArrayCodTarjeta[1];//MASTERCARD
					varValueTar = "MCD";			
					break;
					
					case "AMEX":
					varTipoPos= varArrayTipoPOS[2];
					varTipTarjeta = varArrayCodTarjeta[1];
					varValueTar = "AMX";				
					break;
					
					case "DINERS":
					varTipoPos= varArrayTipoPOS[3];
					varTipTarjeta = varArrayCodTarjeta[1];
					varValueTar = "DIN";				
					break;
				}
				
				switch (TIPO_TRANSACCION)
				{					
					case "EliminarPOS":

					varCodOpe = varArrayCodOpe[2];//Anulacion
					varDescriOpe = varArrayDesOpe[2];
					varTipoTrans= varArrayTipoTran[1]//Anulacion ;
					varTipoOpeFi = varArrayTipoOper[0];//Financiera
					if(varValueTar == 'VIS')
					{  
						var varArrayMonedaVisa = document.getElementById("HidMonedaVisa").value.split("|");					
						varMonedaVisa = varArrayMonedaVisa[0];//SOLES VISA	
						varNroRefVisa=varNroRef;
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
					var varArrayTranMC = document.getElementById("HidTransMC").value.split("|");
					varPwdComercioMC = '';					
					varPwdComercioMC = varArrayTranMC[6];
				
						var varArrayTranMC = document.getElementById("HidTransMC").value.split("|");
						varTransMC = varArrayTranMC[1];//06
						varMonedaMC = '';
						varNroRefMC=varNroRef;
						if(varNroRef == '')
						{
							alert('No tiene numero de referencia para eliminar la transaccion');
							return;
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
						var varArrayTranMC = document.getElementById("HidTransMC").value.split("|");
						varTransMC = varArrayTranMC[4];//11
						varMonedaMC = '';	
						varNroRefMC=varNroRef;
						if(varNroRef == '')
						{
							alert('No tiene numero de referencia para re-imprimir la transaccion');
							return;
						}
					}
					break;
				}
				
				
				varEstadoTrans= varArrayEstTrans[0];//ESTADO PENDIENTE
				//varIpPOS = '192.168.1.1'; //HARDCODEADO
				varMoneda=document.getElementById('HidTipoMoneda').value;
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
				'|ipCaja=' + varIpPOS + 
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
				var varIdUnicoTrans = '';
				var varFlagPago='1';
				varIdRefAnu='';
				varRpta = res;
			 varSeriePOS = varCodTerminal;
			 nomEquipoPOS = varNomPcPos;
				if( varRpta.substr(0,1) == '0')
				{	
					var varIdTran =varRpta.split("|")[2];//varRpta=>rpta|mensaj|idtran|idcab recuperar el Id de Transaccion insertado
					
					
					//2 - EN PROCESO				   

					varEstadoTrans = '';
					varEstadoTrans = varArrayEstTrans[1];//EN PROCESO

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
					'|estadoTransaccion=' + varEstadoTrans +
					'|NroTienda=' + varNroTienda + '|CodigoCaja=' + varCodigoCaja + 
					'|CodEstablec=' + varCodEstablec + '|IpPos=' + varIpPos + '|IdCabez=' + varIdCabecera +
					'|FlagPago=' + varFlagPago + '|Pedido=' + varNroPedido + '|IdUnico=' + varIdUnicoTrans +
					'|TipoTrans=' + varTipoTrans + '|IdRefAnulador=' + varIdRefAnu +
					'|TipoPago=' + document.getElementById("HidTipoPago").value +
					'|ResTarjetaPos=';//PROY-31949 ;

					RSExecute(serverURL,"ActualizarTransaction",varTramaUpdate,
					varIdTran,CallBack_ActualizarTransaction1,GuardarTransactionError,"X");
							
                                        
					objEntityPOS =
					{
						monedaOperacion: '', 
						montoOperacion: varTransMonto, 
						CodigoTienda: varNroTienda,  // HARCODEADO
						NroPedido: '',
						ipAplicacion: '', 
						nombreAplicacion: '', 
						usuarioAplicacion: '',
						TrnsnId: varIdTran,
						tipoPos: '',
						CodigoCaja: varCodigoCaja // HARCODEADO
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
								ErrorVisaNet('',objEntityPOS);
							}
							else if(varValueTar=='AMX' || varValueTar=='MCD' || varValueTar=='DIN')
							{
								ErrorMasterCard('',objEntityPOS);
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
					alert('Error al registrar la transaccion en estado PENDIENTE');			
					f_CargarFormasPago();
					return;
					
				}
			}
	
			function CallBack_ActualizarTransaction(response) 
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
				f_CargarFormasPago();	
                return;
			}
	
			function CallService(tipoPOS,NameDoc,objEntityPOS)
			{  
			
				var entityOpe;
				var soapMSG;
				
				
				var varNroReferencia = '';
				
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
				varIpPOS = '';
				varTipOpePOS = '';
				varEstTran = '';
			
				var varDesRpta = '';
				var varCliente = '';		
				varCodOper = '';
					//Variables de auditoria Ini		
		var varArrayAudi = document.getElementById("HidDatoAuditPos").value.split("|");
		var VarToday = new Date();
		var varIdTransaccion = varNroPedido + '_' + formatDate(VarToday);
		var varIpApplicacion = varArrayAudi[0];
		var varNombreAplicacion = varArrayAudi[1];
		var varUsuarioAplicacion = varArrayAudi[2];
		//Variables de auditoria Fin
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
				CodigoTienda:objEntityPOS.CodigoTienda, 
				CodigoCaja:objEntityPOS.CodigoCaja, 
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
								SuccessVisaNet(objResponse);
							
							},
							error: function (request, status) 
					                {
						        varBolWsLocal = true;
							alert('Sin respuesta del POS, tiempo de espera superado.');  
							ErrorVisaNet(request,objEntityPOS);
					
          },
          timeout: Number(timeOutWsLocal)
        });
        return true;
				                      break;	
						case "02": //MASTER CARD
				varApliMC = document.getElementById("HidApliPOS").value;
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
									ErrorMasterCard(request,objEntityPOS);
            /*Fin Error*/
          },
					timeout: Number(timeOutWsLocal)
        });
        return true;
				break;
    }
						
			}
		function ErrorVisaNet(request,objEntityPOS)
	{
		
		try {
			
			varBolWsLocal = true;          
            
            var varClienteVisa = '';
            var varNumAutTransaccion = '';
            var varCodOperVisa = '';
            var varImpVoucher = '';
            varMontoOperacion = Number(objEntityPOS.montoOperacion).toFixed(2);;
            varNomEquipoPOS = varNomPcPos;
            var varNroPedido = document.getElementById("txtBolFact").value; 
			var varIdUnicoTrans = '';
			var varIdCabez = document.getElementById("HidIdCabez").value;
			var varFlagPago = '1';
			var varDesRpta = '';
					
						varNroReferencia = '';
						varNroReferencia = varNroRef;
						varNumAutTransaccion = '';
						varFechaExpiracion = '';
						varCodOperVisa = '';
						varSeriePOS = '';
						varImpVoucher = '';
						sCodRespTarj = '';
						varEstTran = varArrayEstTrans[3];//RECHAZADO
						varDesRpta = '';
						//VALIDACION CUARTA ANULACION ESTADO INCONPLETO
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
			varTramaUpdate = 'monedaOperacion=' + varMoneda + '|montoOperacion=' + varMontoOperacion + 
			'|nroRegistro=' + varNroRegistro + 
			'|numVoucher=' + varNroReferencia + 
			'|numAutTransaccion=' + varNumAutTransaccion + 
			'|codRespTransaccion=' + sCodRespTarj + 
			'|descTransaccion=' + varDesRpta + 
			'|codAprobTransaccion=' + varCodOperVisa + 
			'|tipoPos=' + varTipoPos + '|varNroTarjeta=' + varNroTarjeta + 
			'|fechaExpiracion=' + varFechaExpiracion + 
			'|nomCliente=' + varClienteVisa + 
			'|impVoucher=' + varImpVoucher + 
			'|seriePOS=' + varSeriePOS + '|nomEquipoPOS=' + varNomEquipoPOS + 
			'|estadoTransaccion=' + varEstTran +
			'|NroTienda=' + varNroTienda + '|CodigoCaja=' + varCodigoCaja + 
			'|CodEstablec=' + varCodEstablec + '|IpPos=' + varIpPos+ '|IdCabez=' + varIdCabez +
			'|FlagPago=' + varFlagPago + '|Pedido=' + varNroPedido + '|IdUnico=' + varIdUnicoTrans +
			'|TipoTrans=' + varTipoTrans + '|IdRefAnulador=' + varIdRefAnu +
			'|TipoPago=' + document.getElementById("HidTipoPago").value +
			'|ResTarjetaPos=';//PROY-31949 
			
			RSExecute(serverURL,"ActualizarTransaction",varTramaUpdate,
						objEntityPOS.TrnsnId,GuardarTransactionError,"X");
		
			var varNomCliente = document.getElementById("txtNombCli").value;
			var varNroTelefono = document.getElementById("txtNroPedido").value; 
			var varNroPedido = document.getElementById("txtBolFact").value; 
			
			var varTramaAudit = '';    
			varTramaAudit = 'NomCliente=' + varNomCliente + 
							'|NroTelefono=' + varNroTelefono + 
							'|NroPedido=' + varBolFact + '|IdTransaccion=' + 
							objEntityPOS.TrnsnId + '|nMonto=' + varMontoOperacion;			
					
					if(varTipoTrans == varArrayTipoTran[1] && varEstTran == varArrayEstTrans[3]){
							
						
							if(varCont >= MaxNumIntentosAnular && varBoolAut=='0'){
								varTramaAuditAux = varTramaAudit;
								varCont = parseInt(varCont) + 1;
								RSExecute(serverURL,"GuardarAutorizacion",varTramaAudit,CallBack_GuardarAutorizacion,GuardarAutorizacionError,"X");
							}
					}
			    f_CargarFormasPago();
		}
		catch(err){
			alert(err.description);
			f_CargarFormasPago();
		}
	}
			// SUCCES VISA
	

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
			var varCodRptaAudit = '';
			var varMsjRptaAudit = '';
			var varCodRptaWs = '';
			var varMsgAlert = '';
			var varPrintData = '';
			var varIdRefAnu='';
			var varIdUnicoTrans='';
			//var varNroPedido = varNroPedido; 
			var varIdUnicoTrans = '';
			var varFlagPago = '1';
			var varIdCabez = varIdCabecera;
			var varIdRefAnu ='';
			varNroReferencia = varNroRef;
			varCodRptaAudit = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.AuditResponse.CodigoRespuesta;
			varMsjRptaAudit = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.AuditResponse.mensajeRespuesta;	
			varCodRptaWs = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.CodigoRespuesta;
						
			/*VALIDAR OPCION RE-IMPRESION*/
			if (varTipoTrans == '3'){				            										
				varNroTarjeta =varNroTarjeta;
				varNroReferencia = '';
				varNumAutTransaccion = '';
				varFechaExpiracion = '';
				varCodOperVisa = '';
				varSeriePOS = '';				
				varImpVoucher = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.ImprimirData;
				
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
				
				sCodRespTarj = '';
				
				if(varCodRptaWs == '1'){
					varEstTran = varArrayEstTrans[3];//RECHAZADO
					varMsgAlert = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.ImprimirData;
					alert(varMsgAlert);
				}
				else{
					varEstTran = varArrayEstTrans[2];//ACEPTADO								
				}
				
				
				if (varImpVoucher == 'OPER. CANCELADA'){
					varDesRpta = varImpVoucher;
				}
				else{
					varDesRpta = '';
				}
			}
			else
			{
				if ((varCodRptaAudit !='0' && typeof varCodRptaWs == 'undefined')|| varCodRptaWs == '1') 
				{
						varDesRpta = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.ImprimirData;
						varNroTarjeta = varNroTarjeta;
						varNroReferencia = '';
						varNroReferencia = varNroRefVisa;
						varNumAutTransaccion = '';
						varFechaExpiracion = '';
						sCodRespTarj = '';
						varCodOperVisa = '';
						varSeriePOS = '';
						
						if (typeof jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.ImprimirData == 'undefined'){
							varImpVoucher = '';
							varMsgAlert = varMsjRptaAudit;
						}							
						else{
							varImpVoucher = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.ImprimirData;
							varMsgAlert = varImpVoucher;
						}						
						varEstTran = varArrayEstTrans[3];//RECHAZADO
						
						
				}
				else
				{
					/*Ouput Visa Ini(Venta&Anulacion)*/
					sCodRespTarj = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.CodigoRespuesta;
					varDesRpta = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.DescripcionRespuesta;
					varClienteVisa = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.NombreCliente;   
					varNumAutTransaccion = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.NumeroAutorizacion;
					varNroRefVisa = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.NumeroReferencia;
					varNroTarjeta = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.NumeroTarjeta;     
					varFechaExpiracion = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.FechaExpiracion;
					varCodOperVisa = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.CodigoOperacion;
					varSeriePOS = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.Terminal;
					varImpVoucher = '';
					varImpVoucher = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.ImprimirData;
					varNroReferencia = varNroRefVisa;
					
					if (varCodOpe == '04')
						varIdRefAnu = varNroReferencia;
					else
						varIdRefAnu = '';
					
					
					if (typeof jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.IdUnico == 'undefined')					
						varIdUnicoTrans = '';
					else
						varIdUnicoTrans = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.IdUnico;
						
					
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
					
					/*Codigo de Rpta VISA*/
					if (sCodRespTarj == '00' || sCodRespTarj == '11'){
						varEstTran = varArrayEstTrans[2];//ACEPTADO
					}
					else{
						varEstTran = varArrayEstTrans[3];//RECHAZADO						
						varMsgAlert = varDesRpta;
						varNroTarjeta = '';
					}
					
					/*Ouput Visa Fin (Venta&Anulacion)*/
				}
			}
			varMontoOperacion = ''; varMontoOperacion = Number(objEntityPOS.montoOperacion).toFixed(2);;
			varNomEquipoPOS = ''; varNomEquipoPOS = varNomPcPos;
			
			//VALIDACION CUARTA ANULACION ESTADO INCONPLETO
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
			varTramaUpdate = 'monedaOperacion=' + varMoneda + '|montoOperacion=' + varMontoOperacion + 
			'|nroRegistro=' + varNroRegistro + 
			'|numVoucher=' + varNroRefVisa + 
			'|numAutTransaccion=' + varNumAutTransaccion + 
			'|codRespTransaccion=' + sCodRespTarj + 
			'|descTransaccion=' + varDesRpta + 
			'|codAprobTransaccion=' + varCodOperVisa + 
			'|tipoPos=' + varTipoPos + '|varNroTarjeta=' + varNroTarjeta + 
			'|fechaExpiracion=' + varFechaExpiracion + 
			'|nomCliente=' + varClienteVisa + 
			'|impVoucher=' + varImpVoucher + 
			'|seriePOS=' + varSeriePOS + '|nomEquipoPOS=' + varNomEquipoPOS + 
			'|estadoTransaccion=' + varEstTran +
			'|NroTienda=' + varNroTienda + '|CodigoCaja=' + varCodigoCaja + 
			'|CodEstablec=' + varCodEstablec + '|IpPos=' + varIpPos + '|IdCabez=' + varIdCabez +
			'|FlagPago=' + varFlagPago + '|Pedido=' + varNroPedido + '|IdUnico=' + varIdUnicoTrans +
			'|TipoTrans=' + varTipoTrans + '|IdRefAnulador=' + varIdRefAnu +
			'|TipoPago=' + document.getElementById("HidTipoPago").value +
			'|ResTarjetaPos=';//PROY-31949 ;
			
			var varRpta = RSExecute(serverURL,"ActualizarTransaction",varTramaUpdate,objEntityPOS.TrnsnId,CallBack_ActualizarTransactionSV);
			
			//ANULACION MASTERCARD Y VISANET
			if (varCodOpe == '04' && (varEstTran == '3' || varEstTran == '5')){
				varNroTarjeta = '';
				
			}
			
			varNameTipoPOS = '';
			
			
			varMontoOperacion =Number(objEntityPOS.montoOperacion).toFixed(2);;
			
		        var varNomCliente = document.getElementById("txtNombCli").value;
			var varNroTelefono = document.getElementById("txtNroPedido").value; 
			var varNroPedido = document.getElementById("txtBolFact").value; 
			
			var varTramaAudit = '';    
			varTramaAudit = 'NomCliente=' + varNomCliente + 
							'|NroTelefono=' + varNroTelefono + 
							'|NroPedido=' + varBolFact + '|IdTransaccion=' + 
							objEntityPOS.TrnsnId + '|nMonto=' + varMontoOperacion;	
			
					 if(varTipoTrans == varArrayTipoTran[1] && varEstTran == varArrayEstTrans[3]){
			
						if(varCont >= MaxNumIntentosAnular && varBoolAut=='0'){
							varTramaAuditAux = varTramaAudit;
							varCont = parseInt(varCont) + 1;
							RSExecute(serverURL,"GuardarAutorizacion",varTramaAudit,CallBack_GuardarAutorizacion,GuardarAutorizacionError,"X");							
							nroIncompleta += 1;						
							f_CargarFormasPago();
							return;
						}
					}
				
					   
					f_CargarFormasPago();
			  
				}
				catch(err)
			    {
			    
					alert(err.description);
					f_CargarFormasPago();
				}	
			}

	function CallBack_ActualizarTransaction1(response) 
	{
		var varRpta = response.return_value;
		
		var res = (varRpta.replace("<BODY><SELECT>", "")).replace("</SELECT>", "").replace("\r\n","");
		
		varRpta = res;
	}
		function CallBack_ActualizarTransactionSM(response) 
	{
		var varRpta = response.return_value;
		
		var res = (varRpta.replace("<BODY><SELECT>", "")).replace("</SELECT>", "").replace("\r\n","");
		
		varRpta = res;
		f_CargarFormasPago();
		
	}
			function CallBack_ActualizarTransactionEM(response) 
	{
		var varRpta = response.return_value;
		
		var res = (varRpta.replace("<BODY><SELECT>", "")).replace("</SELECT>", "").replace("\r\n","");
		
		varRpta = res;
		f_CargarFormasPago();
		
	}
				function CallBack_ActualizarTransactionSV(response) 
	{
		var varRpta = response.return_value;
		
		var res = (varRpta.replace("<BODY><SELECT>", "")).replace("</SELECT>", "").replace("\r\n","");
		
		varRpta = res;
		f_CargarFormasPago();
		
	}
					function CallBack_ActualizarTransactionEV(response) 
	{
		var varRpta = response.return_value;
		
		var res = (varRpta.replace("<BODY><SELECT>", "")).replace("</SELECT>", "").replace("\r\n","");
		
		varRpta = res;
		f_CargarFormasPago();
		
	}
	function CallBack_ActualizarTransaction2(response) 
	{
		var varRpta = response.return_value;
		
		var res = (varRpta.replace("<BODY><SELECT>", "")).replace("</SELECT>", "").replace("\r\n","");
		
		varRpta = res;
	}
			// fin POS
			
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
			
			 var NroPedido = document.getElementById("txtBolFact").value;	
			 var TipoPago = document.getElementById("HidTipoPago").value;
			 var FechaPago = document.getElementById("txtFechPago").value;		
			 RSExecute(serverURL,"CargarFormasPagoRecaudacion",NroPedido,TipoPago,CodCajero,CodPDV,FechaPago,CallBack_CargarFormasPago,Error_CargarFormasPago,"X");  
			
			}
			
			function Error_CargarFormasPago(response)
			{
			  var varRpta = response.return_value;
	          var res = (varRpta.replace("<BODY><SELECT>", "")).replace("</SELECT>", "").replace("\r\n","");        
	    
	          alert(res);
			}
			function BloquearBotones()
		{
			var tableReg =  document.getElementById('tbl_FormasPago');
			var cellsOfRow="";
			var found=false;
			var compareWith="";
			arrayEventsPrint=new Array();
			arrayEventsCan=new Array();
			for (var i = 1; i < tableReg.rows.length; i++)
			{
				cellsOfRow = tableReg.rows[i].getElementsByTagName('td');
				found = false;
				for (var j = 0; j < cellsOfRow.length && !found; j++)
				{
				if(cellsOfRow[6].ID!="" && found==false && cellsOfRow[6].ID!=undefined)
					{
					
					var arrayElem=cellsOfRow[6].ID.split("|");
						if (cellsOfRow[6].children.item(0).tagName=='A')
					{
						arrayEventsCan.push(arrayElem[2]+"|"+cellsOfRow[6].children.item(0).href);
						cellsOfRow[6].children.item(0).href="javascript:void(0)";
						(cellsOfRow[6].children.item(0)).children.item(0).src="../images/delete-icon_ena.png";
						cellsOfRow[6].children.item(0).style.cursor = "default";
					}
					}
					if(cellsOfRow[7].ID!="" && found==false && cellsOfRow[7].ID!=undefined)
					{
						var arrayElem=cellsOfRow[7].ID.split("|");
						if (cellsOfRow[7].children.item(0).tagName=='A')
							{
								arrayEventsPrint.push(arrayElem[2]+"|"+cellsOfRow[7].children.item(0).href);
								cellsOfRow[7].children.item(0).href="javascript:void(0)";
								(cellsOfRow[7].children.item(0)).children.item(0).src="../images/print-icon_ena.png";
								cellsOfRow[7].children.item(0).style.cursor = "default";
								
							}
						found = true;
					}
			
				}
				
			}
			
		}	
		function DesBloquearBotones()
		{
			var tableReg =  document.getElementById('tbl_FormasPago');
			var cellsOfRow="";
			var found=false;
			var compareWith="";
			for (var i = 1; i < tableReg.rows.length; i++)
			{
				cellsOfRow = tableReg.rows[i].getElementsByTagName('td');
				found = false;
				for (var j = 0; j < cellsOfRow.length && !found; j++)
				{
				if(cellsOfRow[6].ID!="" && found==false && cellsOfRow[6].ID!=undefined)
					{
					
					var arrayElem=cellsOfRow[6].ID.split("|");
					var arrEventsCan=arrayEventsCan[i-1].split("|");
						if (cellsOfRow[6].children.item(0).tagName=='A' && arrEventsCan[0]==arrayElem[2])
							{
								
								cellsOfRow[6].children.item(0).href=arrEventsCan[1];
								(cellsOfRow[6].children.item(0)).children.item(0).src="../images/delete-icon.png";
								cellsOfRow[6].children.item(0).style.cursor = "pointer";
							}
					}
					var arrayEvenPrint=arrayEventsPrint[i-1].split("|");
					if(cellsOfRow[7].ID!="" && found==false && cellsOfRow[7].ID!=undefined)
					{
							var arrayElem=cellsOfRow[7].ID.split("|");
							if (cellsOfRow[7].children.item(0).tagName=='A'&& arrayEvenPrint[0]==arrayElem[2])
								{						
									cellsOfRow[7].children.item(0).href=arrayEventsPrint[1];
									(cellsOfRow[7].children.item(0)).children.item(0).src="../images/print-icon.png";
									cellsOfRow[7].children.item(0).style.cursor = "pointer";
									
								}
								found = true;
					}
			
				}
				
			}
			 arrayEventsPrint=new Array();
			arrayEventsCan=new Array();
		}		
			function CallBack_CargarFormasPago(response) 
	        {	
	            document.getElementById("lblEnvioPos").innerHTML  = "";	
	            
	        var msjCaja=document.getElementById('hidMsjCajero').value;
	         var varRpta = response.return_value;
	          var varUsuCodCaja = document.getElementById('hdnUsuario').value;
			 var res = (varRpta.replace("<BODY><SELECT>", "")).replace("</SELECT>", "").replace("\r\n","");
		
				varRpta = res;	
			
				if( varRpta.split("|")[0]=="SD" && PROCESAR_ANULACION==varTipoReporte)
				{ 
					document.getElementById('HidSR').value="1"; document.getElementById('btnGrabar').click(); 
					return;
				} 
				
				if( varRpta.split("|")[0]=="SD" && EXPORTAR_EXCEL==varTipoReporte)
				{ 
					alert("Sin Datos para mostrar."); window.history.back(); 
					return;
				}
				if(varRpta.split("|")[0]=="Error")
				{
					var msjError=varRpta.split("|")[1];
					alert("Verifique los datos del Pago.");
					window.history.back();
					return;
				}			  
				
			  var tbody = document.getElementById('tbl_FormasPago').getElementsByTagName("TBODY")[0];
			  var table = document.getElementById('tbl_FormasPago');		  
              var filas = varRpta.split(";");
              var celdas;
              var tr;
              var td;
              var contFilasOk = 0;
              var contFilasE = 0;
              var contFilasEvaluar = 0;
              
              if (table.rows.length>1)
              {
                 f_limpiarTabla();
              }  
                 
               
			  //contFilasEvaluar = filas.length ;
                           
              for (var i = 0 ; i< filas.length; i++)
              {  
                celdas = filas[i].split("|");
               var IdCabez= celdas.pop();
               var nroPedido=celdas[13].toString();
                               //AGREGAR VALIDACION DE CAJA
               /*    if(!(celdas[16]==varUsuCodCaja)){
                   alert(msjCaja);
                   window.history.back();
					  return;
				}
                   //AGREGAR VALIDACION DE CAJA
                //AGREGAR VALIDACION DE IP
                   if(!(celdas[17]==document.getElementById("HidDatoAuditPos").value.split("|")[0].toString())){
                   alert(msjCaja);
                   window.history.back();
					  return;
				}*/
                   //AGREGAR VALIDACION DE IP
               if(celdas[0].toString()=="EFECTIVO"){
					contFilasE++;
				}
               document.getElementById("txtNroPedido").value=nroPedido;	
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
						
						 if (celdas[1]== "" || varTipoReporte==EXPORTAR_EXCEL)
						   {
							td.appendChild(document.createTextNode(""))
						   }
						   else
						   {
						    contFilasEvaluar = contFilasEvaluar + 1 ;
							if (celdas[5]== "TRANSACCION INCOMPLETA" || celdas[5]== "TRANSACCION EXITOSA")
							{   
							    contFilasOk = contFilasOk  + 1; 
								td.insertAdjacentHTML("afterbegin","<a><IMG style='CURSOR: hand' border='0'  src='../images/delete-icon_ena.png'></a>");
						}
						else
						{
							td.setAttribute('ID', 'TD|1|'+ celdas[8]);
								td.insertAdjacentHTML("afterbegin","<a onclick=f_Evaluar_datos('"+celdas[8]+"','EliminarPOS','"+celdas[2]+"','"+celdas[3]+"','"+celdas[9]+"','"+celdas[11]+"','"+celdas[12]+"','"+celdas[13]+"','"+celdas[14]+"','"+celdas[15]+"','"+celdas[17]+"','"+IdCabez+"');><IMG style='CURSOR: hand' border='0'  src='../images/delete-icon.png'></a>"); 
							}  						 
						}
							tr.appendChild(td);                   
                   }
                   if (j==7)
                   {
                       
                         td = document.createElement("TD");
                       
						 if (celdas[1]== "" || varTipoReporte==EXPORTAR_EXCEL)
							{
							td.appendChild(document.createTextNode(""))
							}
							else
						{  
                            
							if ( celdas[5]== "TRANSACCION EXITOSA")
						{  
							    td.setAttribute('ID', 'TD|1|'+ celdas[8]);						
								td.insertAdjacentHTML("afterbegin","<a onclick=f_Evaluar_datos('"+celdas[8]+"','ImprimirPOS','"+celdas[2]+"','"+celdas[3]+"','"+celdas[9]+"','"+celdas[11]+"','"+celdas[12]+"','"+celdas[13]+"','"+celdas[14]+"','"+celdas[15]+"','"+celdas[17]+"','"+IdCabez+"');><IMG style='CURSOR: hand' border='0'  src='../images/print-icon.png'></a>"); 
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

              //if(contFilasEvaluar==1 && contFilasE==1 && varTipoReporte!=EXPORTAR_EXCEL){
			  //document.getElementById('HidSR').value="1"; document.getElementById('btnGrabar').click(); 
			  //	return;
              //}else{
				if(document.getElementById('HidIpData') == "E"){
					window.history.back();
				}
              //}
              
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
               
					
               if (varTipoReporte == EXPORTAR_EXCEL)
               {                  
                   
                   document.getElementById('btnCancelar').disabled = false;
                   blnClosingMod = false;
               }
				
					document.getElementById("pnFormaPago").style.display = "block";

					
				
			}
		
		function SuccessMasterCard(objResponse,NameDoc)
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
			var varCodRptaAudit = '';
			var varMsjRptaAudit = '';
			var varCodRptaWs = '';
			var varMsgAlert = '';
			var varPrintData = '';
			varIdRefAnu='';
			var varIdUnicoTrans='';
			
			var varNroPedido = document.getElementById("txtBolFact").value; 
			var varIdUnicoTrans = '';
			var varFlagPago = '1';
			var varIdCabez = document.getElementById("HidIdCabez").value;
			
			varCodRptaAudit =isEmptyValue(jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.AuditResponse.CodigoRespuesta);
			varMsjRptaAudit =isEmptyValue(jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.AuditResponse.mensajeRespuesta);	
			varCodRptaWs =isEmptyValue(jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.CodigoRespuesta);
			
			/*VALIDAR OPCION RE-IMPRESION*/
			if (varTipoTrans == '3' ){			
			  if(varCodRptaAudit='0' && varCodRptaWs=='00'){
				sCodRespTarj = isEmptyValue(jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.CodigoRespuesta);
				varDesRpta = isEmptyValue(jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.DescripcionRespuesta);					
				//varNroTarjeta = document.getElementById(varNameDoc).value;
				varNroReferencia = '';
				varNroReferencia = varNroRef;;
				varNumAutTransaccion = '';//NumeroAutorizacion
				varNumAutTransaccion = isEmptyValue(jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.NumeroAutorizacion);
				varFechaExpiracion = '';
				varCodOperVisa = '';
				varSeriePOS = '';				
				varImpVoucher = isEmptyValue(jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.ImprimirData);
				
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
								
					varEstTran = varArrayEstTrans[2];//ACEPTADO								
				}else
				{
				sCodRespTarj = isEmptyValue(jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.CodigoRespuesta);
				varDesRpta = isEmptyValue(jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.DescripcionRespuesta);					
				//varNroTarjeta = document.getElementById(varNameDoc).value;
				varNroReferencia =varNroRefMC;
				varNumAutTransaccion = '';//NumeroAutorizacion
				varNumAutTransaccion = '';
				varFechaExpiracion = '';
				varCodOperVisa = '';
				varSeriePOS = '';				
				varImpVoucher = '';
				varNroRefVisa=varNroReferencia;
					varEstTran = varArrayEstTrans[3];//RECHAZADO
					varMsgAlert=varDesRpta;
				}			
			}
			else
			{
				if ((varCodRptaAudit == '0' && varCodRptaWs == '77')|| varCodRptaWs == '1') 
				{
					varDesRpta = isEmptyValue(jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.DescripcionRespuesta);
					varNroTarjeta = varNroTarjeta;
					varNroReferencia = '';
					varNroReferencia = varNroRefMC;
					varNumAutTransaccion = '';
					varFechaExpiracion = '';
					sCodRespTarj = '';
					varCodOperVisa = '';
					varSeriePOS = '';
					varNroRefVisa=varNroReferencia;
					varImpVoucher = isEmptyValue(jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.DescripcionRespuesta);
					varMsgAlert = varImpVoucher;
					varEstTran = varArrayEstTrans[3];//RECHAZADO
					
				}
				else
				{
					if (typeof jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.CodigoAdquiriente == 'undefined')					
						varIdUnicoTrans = '';
					else
						varIdUnicoTrans = isEmptyValue(jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.CodigoAdquiriente);
				
					/*Ouput Visa Ini(Venta&Anulacion)*/
					/*CodigoAprobacion*/
					sCodRespTarj = isEmptyValue(jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.CodigoRespuesta);
					varDesRpta = isEmptyValue(jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.DescripcionRespuesta);
					varClienteVisa = isEmptyValue(jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.NombreCliente);   
					varNumAutTransaccion = isEmptyValue(jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.NumeroAutorizacion);					
					
					
					if (varCodOpe == '04'){
						varNroRefVisa = '';
						varNroRefVisa = varNroRefMC;
						varNroRefVisa = trim1(varNroRefVisa);
						varIdRefAnu = isEmptyValue(jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.NumeroReferencia);
						varIdRefAnu = (varIdRefAnu == null) ? '' : String(varIdRefAnu).replace("REF","");
					}
					else{
						varNroRefVisa = isEmptyValue(jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.NumeroReferencia);
						varNroRefVisa = (varNroRefVisa == null) ? '' : String(varNroRefVisa).replace("REF","");	
						varNroRefVisa = trim1(varNroRefVisa);
					}
					
					varNroTarjeta = isEmptyValue(jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.NumeroTarjeta);
					varFechaExpiracion = '';
					varCodOperVisa = isEmptyValue(jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.CodigoAprobacion);
					varSeriePOS = '';
					varImpVoucher = '';
					varImpVoucher = isEmptyValue(jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.ImprimirData);
					varNroReferencia = varNroRefVisa;
					
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
						
					/*Codigo de Rpta VISA*/
					if (sCodRespTarj == '00' || sCodRespTarj == '11'){
						varEstTran = varArrayEstTrans[2];//ACEPTADO
					}
					else{
						varEstTran = varArrayEstTrans[3];//RECHAZADO						
						varMsgAlert = varDesRpta;
						varNroTarjeta = '';
					}
					/*Ouput Visa Fin (Venta&Anulacion)*/					
				}
			}
			
			varMontoOperacion = ''; varMontoOperacion = Number(objEntityPOS.montoOperacion).toFixed(2);;
			varNomEquipoPOS = ''; varNomEquipoPOS = varNomPcPos;
			

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
			varTramaUpdate = 'monedaOperacion=' + varMoneda + '|montoOperacion=' + varMontoOperacion + 
			'|nroRegistro=' + varNroRegistro + 
			'|numVoucher=' + varNroRef + 
			'|numAutTransaccion=' + varNumAutTransaccion + 
			'|codRespTransaccion=' + sCodRespTarj + 
			'|descTransaccion=' + varDesRpta + 
			'|codAprobTransaccion=' + varCodOperVisa + 
			'|tipoPos=' + varTipoPos + '|varNroTarjeta=' + varNroTarjeta + 
			'|fechaExpiracion=' + varFechaExpiracion + 
			'|nomCliente=' + varClienteVisa + 
			'|impVoucher=' + varImpVoucher + 
			'|seriePOS=' + varSeriePOS + '|nomEquipoPOS=' + varNomEquipoPOS + 
			'|estadoTransaccion=' + varEstTran +
			'|NroTienda=' + varNroTienda + '|CodigoCaja=' + varCodigoCaja + 
			'|CodEstablec=' + varCodEstablec + '|IpPos=' + varIpPos + '|IdCabez=' + varIdCabez +
			'|FlagPago=' + varFlagPago + '|Pedido=' + varNroPedido + '|IdUnico=' + varIdUnicoTrans + 
			'|TipoTrans=' + varTipoTrans + '|IdRefAnulador=' + varIdRefAnu +
			'|TipoPago=' + document.getElementById("HidTipoPago").value +
			'|ResTarjetaPos=';//PROY-31949 ;;
			
			
			var varRpta = RSExecute(serverURL,"ActualizarTransaction",varTramaUpdate,
			objEntityPOS.TrnsnId,CallBack_ActualizarTransactionSM);
			
		
			varNameTipoPOS = '';
					
			varMontoOperacion = document.getElementById("txtMonto").value;
			
			var varNomCliente = document.getElementById("txtNombCli").value;
			var varNroTelefono = document.getElementById("txtNroPedido").value; 
			var varNroPedido = document.getElementById("txtBolFact").value; 
			
			var varTramaAudit = '';    
			varTramaAudit = 'NomCliente=' + varNomCliente + 
							'|NroTelefono=' + varNroTelefono + 
							'|NroPedido=' + varBolFact + '|IdTransaccion=' + 
							objEntityPOS.TrnsnId + '|nMonto=' + varMontoOperacion;	
			
			
					 if(varTipoTrans == varArrayTipoTran[1] && varEstTran == varArrayEstTrans[3]){
			
						if(varCont >= MaxNumIntentosAnular && varBoolAut=='0'){
							varTramaAuditAux = varTramaAudit;
							varCont = parseInt(varCont) + 1;
							RSExecute(serverURL,"GuardarAutorizacion",varTramaAudit,CallBack_GuardarAutorizacion,GuardarAutorizacionError,"X");							
							nroIncompleta += 1;
							f_CargarFormasPago();
							return;
						}
					}
			f_CargarFormasPago();		
		}
		catch(err) {
			alert(err.description);
			f_CargarFormasPago();
		}	
	}


	function ErrorMasterCard(request,objEntityPOS)
	{
		try {
			var varClienteVisa = '';
			var varNumAutTransaccion = '';
			var varCodOperVisa = '';
			var varImpVoucher = '';      
      
			var varNroPedido = document.getElementById("txtBolFact").value; 
			var varIdUnicoTrans = '';
			var varIdCabez = document.getElementById("HidIdCabez").value;
			var varFlagPago = '1';
			var varDesRpta = '';
			
			varMontoOperacion = Number(objEntityPOS.montoOperacion).toFixed(2);;
			varNomEquipoPOS = varNomPcPos;
		      
			varNroReferencia = '';
			varNroReferencia = varNroRefMC;
			varNumAutTransaccion = '';
			varFechaExpiracion = '';
			varCodOperVisa = '';
			varSeriePOS = '';
			varImpVoucher = '';
			sCodRespTarj = '';
			
			//VALIDACION CUARTA ANULACION ESTADO INCONPLETO
            //PROY-31949 
             varEstTran = varArrayEstTrans[3];
            
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
			varTramaUpdate = 'monedaOperacion=' + varMoneda + '|montoOperacion=' + varMontoOperacion + 
			'|nroRegistro=' + varNroRegistro + 
			'|numVoucher=' + varNroReferencia + 
			'|numAutTransaccion=' + varNumAutTransaccion + 
			'|codRespTransaccion=' + sCodRespTarj + 
			'|descTransaccion=' + varDesRpta + 
			'|codAprobTransaccion=' + varCodOperVisa + 
			'|tipoPos=' + varTipoPos + '|varNroTarjeta=' + varNroTarjeta + 
			'|fechaExpiracion=' + varFechaExpiracion + 
			'|nomCliente=' + varClienteVisa + 
			'|impVoucher=' + varImpVoucher + 
			'|seriePOS=' + varSeriePOS + '|nomEquipoPOS=' + varNomEquipoPOS + 
			'|estadoTransaccion=' + varEstTran +
			'|NroTienda=' + varNroTienda + '|CodigoCaja=' + varCodigoCaja + 
			'|CodEstablec=' + varCodEstablec + '|IpPos=' + varIpPos + '|IdCabez=' + varIdCabez +
			'|FlagPago=' + varFlagPago + '|Pedido=' + varNroPedido + '|IdUnico=' + varIdUnicoTrans +
			'|TipoTrans=' + varTipoTrans + '|IdRefAnulador=' + varIdRefAnu +
			'|TipoPago=' + document.getElementById("HidTipoPago").value +
			'|ResTarjetaPos=';//PROY-31949 ;;
			
			RSExecute(serverURL,"ActualizarTransaction",varTramaUpdate,
						objEntityPOS.TrnsnId,GuardarTransactionError,"X");
						
			var varNomCliente = document.getElementById("txtNombCli").value;
			var varNroTelefono = document.getElementById("txtNroPedido").value; 
			var varNroPedido = document.getElementById("txtBolFact").value; 
			
			var varTramaAudit = '';    
			varTramaAudit = 'NomCliente=' + varNomCliente + 
			'|NroTelefono=' + varNroTelefono + 
			'|NroPedido=' + varBolFact + '|IdTransaccion=' + 
			objEntityPOS.TrnsnId + '|nMonto=' + varMontoOperacion;			
			
					if(varTipoTrans == varArrayTipoTran[1] && varEstTran == varArrayEstTrans[3]){
					
							
						if(varCont >= MaxNumIntentosAnular && varBoolAut=='0')
						{
							varTramaAuditAux = varTramaAudit;
							varCont = parseInt(varCont) + 1;
							RSExecute(serverURL,"GuardarAutorizacion",varTramaAudit,CallBack_GuardarAutorizacion,GuardarAutorizacionError,"X");							
							f_CargarFormasPago();
							return;
						}
					}
				f_CargarFormasPago();			
		}
		catch(err){
			alert(err.description);
			f_CargarFormasPago();
		}	
	}
			
		function f_Exportar()
		{			
			
			var varNroPedido=getValue("txtNroPedido");
			var varCodComercio=getValue("txtNombCli");
			var varFactura=getValue("txtBolFact");
			var varFecha=getValue("txtFechPago");
			var varMonto=getValue("txtMonto");
								
					document.all.ifraExcel.src="SICAR_FormaPagos_Excel.aspx?pnropedid="+varNroPedido+"&pnombrecli="+varCodComercio+"&pidtransac="+varFactura+"&pfecha="+varFecha+"&pmonto="+varMonto;
		}
		
		function isEmptyValue(val) {
    return (val == 'undefined' || val == null || val.length <= 0) ? '' : val;
}

function trim1 (string) {
		return string.replace(/^\s\s*/, '').replace(/\s\s*$/, '');
	}
		//PROY-27440 FIN
		</script>
	</HEAD>
	<body leftMargin="0" topMargin="0" marginheight="0" marginwidth="0">
		<form id="frmPrincipal" method="post" name="frmPrincipal" runat="server">
			<DIV ID="pnFormaPago" style="DISPLAY: none">
				<table border="0" cellSpacing="0" cellPadding="0" width="750">
					<tr>
						<td>
							<table border="1" cellSpacing="0" cellPadding="0" width="700" align="center">
								<tr borderColor="#336699">
									<td align="center">
										<table border="0" cellSpacing="0" cellPadding="0" width="100%" align="center">
											<tr>
												<td height="32" vAlign="top" width="10"></td>
												<td style="PADDING-TOP: 4px" class="TituloRConsulta" height="32" vAlign="top" width="98%"
													align="center"><B>Formas de Pago del Documento Pagado</B></td>
												<td height="32" vAlign="top" width="10"></td>
											</tr>
										</table>
										<table border="0" cellSpacing="0" cellPadding="0" width="100%" align="center">
											<tr>
												<td vAlign="top" width="14"></td>
												<td style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px; PADDING-BOTTOM: 5px; PADDING-TOP: 5px"
													width="98%">
													<!--Proy-27440-Inicio-->
													<table id="tbl_DatosPagoC" border="0" cellSpacing="0" cellPadding="0" width="680">
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
																		<td class="Arial12b"><input id="txtBolFact" class="clsInputDisable" tabIndex="2" readOnly maxLength="20" size="30"
																				name="txtBolFact" runat="server"></td>
																		<td height="22" width="25">&nbsp;</td>
																		<td class="Arial12b" width="170">&nbsp;&nbsp;&nbsp;Fecha :</td>
																		<td class="Arial12b"><input id="txtFechPago" class="clsInputDisable" tabIndex="2" readOnly maxLength="20" size="30"
																				name="txtFechaPago" runat="server"></td>
																	</tr>
																	<tr>
																		<td>&nbsp;</td>
																		<td class="Arial12b" width="170">&nbsp;&nbsp;&nbsp;Nro Pedido :</td>
																		<td class="Arial12b"><input id="txtNroPedido" class="clsInputDisable" tabIndex="2" readOnly maxLength="16" size="30"
																				name="txtMonto" runat="server"></td>
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
													<table id="tbl_FormasPagoC" border="0" cellSpacing="0" cellPadding="0" width="680">
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
																		<td>
																			<table border="1" cellSpacing="1" borderColor="#ffffff" cellPadding="1" width="100%">
																				<tr class="Arial12B" height="21">
																					<td style="WIDTH: 100%; TEXT-ALIGN: center">
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
																				<tr>
																					<td height="10" align="center">
																						<asp:Label id="lblEnvioPos" runat="server" CssClass="TituloRConsulta"></asp:Label>
																					</td>
																				</tr>
																				<tr>
																					<td><IFRAME style="DISPLAY: none" id="ifraExcel"></IFRAME>
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
												<td align="center"><INPUT style="WIDTH: 98px" id="btnExportar" class="BotonOptm" onclick="f_Exportar();" value="Exportar"
														type="button" name="btExportar"></td>
												<td width="28" align="center"></td>
												<td width="85" align="center"><INPUT style="WIDTH: 98px" id="btnCancelar" class="BotonOptm" value="Cerrar" type="button"
														name="btnCancelar" runat="server"></td>
												<td width="28" align="center"></td>
											</tr> <!--Proy-27440 -->
										</table> <!--Proy-27440 -->
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						</TD>
						<td>
							<!--PROY-27440 INI--><input id="HidPtoVenta" type="hidden" name="HidPtoVenta" runat="server">
							<input id="HidIntAutPos" type="hidden" name="HidIntAutPos" runat="server"> <input id="HidCodOpera" type="hidden" name="HidCodOpera" runat="server">
							<input id="HidDesOpera" type="hidden" name="HidDesOpera" runat="server"> <input id="HidTipoOpera" type="hidden" name="HidTipoOpera" runat="server">
							<input id="HidTipoTarjeta" type="hidden" name="HidTipoTarjeta" runat="server"> <input id="HidTipoPago" type="hidden" name="HidTipoPago" runat="server">
							<input id="HidEstTrans" type="hidden" name="HidEstTrans" runat="server"> <input id="HidTipoPOS" type="hidden" name="HidTipoPOS" runat="server">
							<input id="HidTipoTran" type="hidden" name="HidTipoTran" runat="server"> <input id="HidTipoMoneda" type="hidden" name="HidTipoMoneda" runat="server">
							<input id="HidTransMC" type="hidden" name="HidTransMC" runat="server"> <input id="HidMonedaMC" type="hidden" name="HidMonedaMC" runat="server">
							<input id="HidApliPOS" type="hidden" name="HidApliPOS" runat="server"> <input id="HidMonedaVisa" type="hidden" name="HidMonedaVisa" runat="server">
							<input id="HidFlagGuardar" type="hidden" name="HidFlagGuardar" runat="server"> <input id="HidTipoReporte" type="hidden" name="HidTipoReporte" runat="server">
							<input id="hdnFechaBusqueda" type="hidden" name="hdnFechaBusqueda" runat="server">
							<input id="hidEstadoServicioPago" type="hidden" name="hidEstadoServicioPago" runat="server">
							<input id="hdnUsuario" type="hidden" name="hdnUsuario" runat="server"> <input id="hdnBinAdquiriente" type="hidden" name="hdnBinAdquiriente" runat="server">
							<input id="hdnCodComercio" type="hidden" name="hdnCodComercio" runat="server"> <input id="intCanal" type="hidden" name="intCanal" runat="server">
							<input id="intAccion" type="hidden" name="intAccion" runat="server"> <input id="hdnRutaLog" type="hidden" name="hdnRutaLog" runat="server">
							<input id="hdnDetalleLog" type="hidden" name="hdnDetalleLog" runat="server"> <input id="hdnPuntoDeVenta" type="hidden" name="hdnPuntoDeVenta" runat="server">
							<input id="numeroDocumentoHiden" type="hidden" name="numeroDocumentoHiden" runat="server">
							<input id="hdnTipoCambioPago" type="hidden" name="hdnTipoCambioPago" runat="server">
							<input id="HidDatoPosVisa" type="hidden" name="HidDatoPosVisa" runat="server"><input id="HidDatoPosMC" type="hidden" name="HidDatoPosMC" runat="server">
							<input id="HidIdCabez" type="hidden" name="HidIdCabez" runat="server"> <input id="HidDatoAuditPos" type="hidden" name="HidDatoAuditPos" runat="server">
							<input id="HidSR" type="hidden" name="HidSR" value="0" runat="server"><input id="HidIpData" type="hidden" name="HidIpData" value="N" runat="server"><!--PROY-27440 FIN-->
							<input id="hidMsjCajero" type="hidden" name="hidMsjCajero" value="0" runat="server">
							<input id="HidNumIntentosAnular" type="hidden" name="HidNumIntentosAnular" runat="server">
							<input id="HidMsjErrorNumIntentos" type="hidden" name="HidMsjErrorNumIntentos" runat="server">
							<input id="HidMsjErrorTimeOut" type="hidden" name="HidMsjErrorTimeOut" runat="server">
							<input id="HidFlagCajaCerrada" type="hidden" name="HidFlagCajaCerrada" runat="server">
							<input id="HidMsjCajaCerrada" type="hidden" name="HidMsjCajaCerrada" runat="server"></td>
					</tr>
				</table>
				</TD></TR></TBODY></TABLE>
			</DIV>
		</form>
	</body>
</HTML>
