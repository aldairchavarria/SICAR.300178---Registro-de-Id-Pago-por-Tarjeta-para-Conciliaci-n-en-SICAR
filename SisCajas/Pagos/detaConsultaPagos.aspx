<%@ Page Language="vb" AutoEventWireup="false" Codebehind="detaConsultaPagos.aspx.vb" Inherits="SisCajas.detaConsultaPagos" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Aplicativo TIM</title>
		<meta http-equiv="Pragma" content="no-cache">
		<META HTTP-EQUIV="Expires" CONTENT="Mon, 06 Jan 1990 00:00:01 GMT">
		<link href="../estilos/est_General.css" rel="styleSheet" type="text/css">
			<!--PROY-27440 INI-->
			<script language="JavaScript" src="../librerias/msrsclient.js"></script>
			<script language="JavaScript" src="../Scripts/jquery-1.1.js"></script>
			<script language="JavaScript" src="../Scripts/form.js"></script>
			<script language="JavaScript" src="../Scripts/xml2json.js"></script>
			<script language="JavaScript" src="../Scripts/operacionPOS.js"></script>
			<!--PROY-27440 FIN-->
			<script language="JavaScript" src="../librerias/date-picker.js"></script>
			<script language="JavaScript" src="../librerias/Lib_FuncValidacion.js"></script>
			<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
			<script LANGUAGE="JavaScript">
	//PROY-27440 INI
	var serverURL =  '../Pos/ProcesoPOS.aspx';
	var webServiceURL = '<%= System.Configuration.ConfigurationSettings.AppSettings("consWsLocal_Url") %>';
	var timeOutWsLocal = '<%= System.Configuration.ConfigurationSettings.AppSettings("consWsLocal_Url_TimeOut") %>';
	var contTransOK = 0;//PROY-31949 
	var contTransEvaluar = 0;//PROY-31949 
	var MaxNumIntentosAnular;//PROY-31949 
	var tiposError = 'EXC_';
	var varNameTipoPOS;
	var varTipTarjeta;
	var varNameDoc = '';
	var varMoneda;
	var varTransMonto;
	var varTipoPos;
	var varValueTar;
	var varTipoTrans;
	var varArrayEstTrans;
	var varTipoOpeFi;
	var varNomEquipoPOS = '';  //varNomEquipoPOS   nomEquipoPOS
	var varContIntento0 = 0;
	var varContIntento1 = 0;
	var varContIntento2 = 0;
	var varContIntento3 = 0;
	var varContIntento4 = 0;
	var varTramaAuditAux;
	var varArrayStdAnul;
	var varNroRegistro;
	var varNroTienda;
	var varCodEstablec;
	var varCodigoCaja;
	var varNomPcPos;
	var varCodTerminal;
	var varIpPos;
	var NTarjeta;
	var NReferencia;
	//Mastercard
	var varTransMC;
	var varApliMC;
	var varMontoMC;	
	var varPwdComercioMC = '';	
	var varMonedaMCD;
	
	//VisaNet
	var varIdRefAnu;
	var varCodOpe;
	//var varNroRefVisa;
	var NroFila;
	var varEstdAnul;
	var varDescTran;
	var varMontoVisa;
	var varMonedaVisa;
	var varMonPOSVISA;
	//PROY-27440 FIN

	//PROY-31949 
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
        //PROY-31949 
	function popup(url) {
	msg= window.open(url,"popi","toolbar=no,left=58,top=50,width=680,height=480,directories=no,status=no,scrollbars=yes,resize=no,menubar=no");
	}

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
					    blnClosingMod = false; //PROY-31949 					
						if (!f_Grabar()) event.returnValue = false;
						break;								
					case "btnCancelar":
						blnClosingMod = false; //PROY-31949 
					break;							
				}
				break;
		}
	}

//PROY-27440 INI
	function f_Grabar(){
	//	var strCod = "";
	//	var strMon = "";			
	//		for (var i = 1; i < dgrDetalle.rows.length; i++){
	//				if (dgrDetalle.rows[i].cells[0].children.chkSel.checked){
	//			    
	//					if (strCod=="")
	//					{
	//						strCod = dgrDetalle.rows[i].cells[0].children.chkSel.value;					
	//						strMon = dgrDetalle.rows[i].cells[3].innerText;
	//					}	
	//					else
	//					{
	//						strCod = strCod+";"+dgrDetalle.rows[i].cells[0].children.chkSel.value;	
	//						strMon = strMon+";"+dgrDetalle.rows[i].cells[3].innerText;
	//					}	
	//				}	
	//		}
	//	if (strCod==""){
	//		alert("No ha seleccionado ningún detalle...!!!");
	//		return false;
	//	}
	//	else{
	//		frmPrincipal.txtViasPago.value = strCod;
	//		frmPrincipal.txtMontos.value = strMon;			
			return true;
	//	}		
		}
//PROY-27440 FIN


       	//PROY-27440 INI	
		function f_EnvioPOS(Indice,NroReferencia,NroTarjeta,Equival,Monto,TagOpcion)
		{	
		 //PROY-31949 
	         MaxNumIntentosAnular = Number(document.getElementById("HidNumIntentosAnular").value);
	          
		 if(document.getElementById("HidFlagCajaCerrada").value == '1')
		 {	  
			alert(document.getElementById("HidMsjCajaCerrada").value);	
			f_Contar_TransaccionesExitosas();		
			return;
		 }
            //PROY-31949 
		    
		    
		    //Valida el Ip desconfigurado
			if(document.getElementById("HidIdCabez").value.length >0 && document.getElementById("HidDatoPosVisa").value.length <=0 ||
			   document.getElementById("HidIdCabez").value.length >0 && document.getElementById("HidDatoPosMC").value.length <= 0)	
			{	
                    alert(document.getElementById("hidMsjIpDesconfigurado").value);	
				    f_Contar_TransaccionesExitosas(); //PROY-31949 			   
					return;
				}
					    
			varTransMC = '';  varApliMC = '';  
	
			varTipoTrans = '';
			varValueTar = Equival;
			var varTramaInsert='';
			varCodOpe='';
			var varDescriOpe='';
			var varCodPtaWS='';
			varTransMonto= Monto
			varApliMC = document.getElementById("HidApliPOS").value;
			NroFila = '';
			NReferencia = NroReferencia;
			NTarjeta=NroTarjeta;
			NroFila = Indice;
			var varNroPedido = document.getElementById("txtNumPedido").value
			var varArrayCodOpe=document.getElementById("HidCodOpera").value.split("|");
			var varArrayDesOpe=document.getElementById("HidDesOpera").value.split("|");
			var varArrayCodTarjeta=document.getElementById("HidTipoTarjeta").value.split("|");
			var varArrayTipoPOS=document.getElementById("HidTipoPOS").value.split("|");
			var varArrayTipoTran=document.getElementById("HidTipoTran").value.split("|");
			var varArrayOpeFi = document.getElementById("HidTipoOpera").value.split("|");
			var varTipoPago = document.getElementById("HidTipoPago").value; //Documentos Pagados
			var varNroTelefono =  document.getElementById("hidNroTelefono").value;
			varArrayEstTrans=document.getElementById("HidEstTrans").value.split("|");	
			var varEstadoTrans= varArrayEstTrans[0];//PENDIENTE
			var varArrayDatoPosVisa = document.getElementById("HidDatoPosVisa").value.split("|");
			var varArrayDatoPosMC = document.getElementById("HidDatoPosMC").value.split("|");
			varArrayStdAnul = document.getElementById("HidEstAnulGrilla").value.split("|");
			var varIdCabecera = document.getElementById("HidIdCabez").value;		
			var varArrayMonedas = document.getElementById("HidTipoMoneda").value.split("|");
			varMonedaVisa = varArrayMonedas[0];//SOLES VISA
			varMonedaMCD = varArrayMonedas[1];//SOLES MCD
			var varArrayTranMC = document.getElementById("HidTransMC").value.split("|");
			varMonPOSVISA = document.getElementById("HidTipMonPOSVISA").value;
					    
					    
			switch (TagOpcion)
						{
				case "Eliminar POS":
					varCodOpe = varArrayCodOpe[2];//Anulacion
					varDescriOpe = varArrayDesOpe[2];
					varTipoTrans= varArrayTipoTran[1] ;//ANULACION DE PAGO
					if(varValueTar == 'VIS')
					{					
						varTipoOpeFi = varArrayOpeFi[0]; // Financiera
						varMoneda = varMonedaVisa; //1 Soles para Tabla Transacciones
						if(NroReferencia == ''){
							alert('No tiene Numero de Referencia para eliminar la transaccion');
							f_Contar_TransaccionesExitosas();//PROY-31949 
							return;
						}
						else
						{
							alert("Nº Referencia: " + NReferencia );
						}
						}	
						else
						{
					    varTipoOpeFi = varArrayOpeFi[0]; // Financiera
						varTransMC = varArrayTranMC[1];//06 ANulacion MCD
						varPwdComercioMC = varArrayTranMC[6]; //299999 ANulacion MCD
						varMoneda = varMonedaVisa; //1 Soles para Tabla Transacciones
						if(NroReferencia == ''){
							alert('No tiene Numero de Referencia para eliminar la transaccion');
							f_Contar_TransaccionesExitosas();//PROY-31949 
							return;
						}
					}				
					break;
				case "Imprimir POS":
					varCodOpe = varArrayCodOpe[2];//Anulacion
					varDescriOpe = varArrayDesOpe[2];
					varTipoTrans= varArrayTipoTran[2];//3 REEIMPRESION DE VOUCHER
					    
					if(varValueTar == 'VIS')
						{
						varMoneda = varMonedaVisa;//SOLES VISA
						varTipoOpeFi = varArrayOpeFi[1]; //No Financiera
						}	
						else
						{
						var varArrayTranMC = document.getElementById("HidTransMC").value.split("|");
						varTransMC = varArrayTranMC[4];// 11 Impresión MCD
						varPwdComercioMC = varArrayTranMC[6] //299999 ANulacion MCD
						varTipoOpeFi = varArrayOpeFi[1]; //No Financiera
						varMoneda = varMonedaVisa //1 Soles para Tabla Transacciones
						if(NroReferencia == ''){
							alert('No tiene Numero de Referencia para Re-Imprimir la transaccion');
							f_Contar_TransaccionesExitosas();//PROY-31949 
							return;
						}
						
					}				
					break;
			}
			varNroRegistro = ''; varNroTienda = ''; varCodigoCaja = ''; varCodEstablec = '';
			varNomPcPos = ''; varCodTerminal = ''; 	varIpPos = '';
			
			
			switch (varValueTar)
			{
				case "VIS"://VISA
					varTipoPos= varArrayTipoPOS[0]; //VISA
					varTipTarjeta = varArrayCodTarjeta[0];//01
					varNroRegistro = varArrayDatoPosVisa[0].substr(varArrayDatoPosVisa[0].indexOf("=")+1);
					varNroTienda = varArrayDatoPosVisa[1].substr(varArrayDatoPosVisa[1].indexOf("=")+1);
					varCodigoCaja = varArrayDatoPosVisa[2].substr(varArrayDatoPosVisa[2].indexOf("=")+1);
					varCodEstablec = varArrayDatoPosVisa[3].substr(varArrayDatoPosVisa[3].indexOf("=")+1);
					varNomPcPos = varArrayDatoPosVisa[4].substr(varArrayDatoPosVisa[4].indexOf("=")+1);
					varCodTerminal = varArrayDatoPosVisa[6].substr(varArrayDatoPosVisa[6].indexOf("=")+1);
					varIpPos = varArrayDatoPosVisa[7].substr(varArrayDatoPosVisa[7].indexOf("=")+1);		
						
					break;
				case "MCD"://MC			
					varTipoPos= varArrayTipoPOS[1];
					varTipTarjeta = varArrayCodTarjeta[1];//MASTERCARD				
					varNroRegistro = varArrayDatoPosMC[0].substr(varArrayDatoPosMC[0].indexOf("=")+1);
					varNroTienda = varArrayDatoPosMC[1].substr(varArrayDatoPosMC[1].indexOf("=")+1);
					varCodigoCaja = varArrayDatoPosMC[2].substr(varArrayDatoPosMC[2].indexOf("=")+1);
					varCodEstablec = varArrayDatoPosMC[3].substr(varArrayDatoPosMC[3].indexOf("=")+1);
					varNomPcPos = varArrayDatoPosMC[4].substr(varArrayDatoPosMC[4].indexOf("=")+1);
					varCodTerminal = varArrayDatoPosMC[6].substr(varArrayDatoPosMC[6].indexOf("=")+1);
					varIpPos = varArrayDatoPosMC[7].substr(varArrayDatoPosMC[7].indexOf("=")+1);
					break;
				case "AMX":
					varTipoPos= varArrayTipoPOS[2];
					varTipTarjeta = varArrayCodTarjeta[1];//MASTERCARD				
					varNroRegistro = varArrayDatoPosMC[0].substr(varArrayDatoPosMC[0].indexOf("=")+1);
					varNroTienda = varArrayDatoPosMC[1].substr(varArrayDatoPosMC[1].indexOf("=")+1);
					varCodigoCaja = varArrayDatoPosMC[2].substr(varArrayDatoPosMC[2].indexOf("=")+1);
					varCodEstablec = varArrayDatoPosMC[3].substr(varArrayDatoPosMC[3].indexOf("=")+1);
					varNomPcPos = varArrayDatoPosMC[4].substr(varArrayDatoPosMC[4].indexOf("=")+1);
					varCodTerminal = varArrayDatoPosMC[6].substr(varArrayDatoPosMC[6].indexOf("=")+1);
					varIpPos = varArrayDatoPosMC[7].substr(varArrayDatoPosMC[7].indexOf("=")+1);
					break;
				case "DIN":
					varTipoPos= varArrayTipoPOS[3];
					varTipTarjeta = varArrayCodTarjeta[1];//MASTERCARD				
					varNroRegistro = varArrayDatoPosMC[0].substr(varArrayDatoPosMC[0].indexOf("=")+1);
					varNroTienda = varArrayDatoPosMC[1].substr(varArrayDatoPosMC[1].indexOf("=")+1);
					varCodigoCaja = varArrayDatoPosMC[2].substr(varArrayDatoPosMC[2].indexOf("=")+1);
					varCodEstablec = varArrayDatoPosMC[3].substr(varArrayDatoPosMC[3].indexOf("=")+1);
					varNomPcPos = varArrayDatoPosMC[4].substr(varArrayDatoPosMC[4].indexOf("=")+1);
					varCodTerminal = varArrayDatoPosMC[6].substr(varArrayDatoPosMC[6].indexOf("=")+1);
					varIpPos = varArrayDatoPosMC[7].substr(varArrayDatoPosMC[7].indexOf("=")+1);
					break;
			}
						
				if(document.getElementById("hidUsuario").value  != document.getElementById("hidCajeroPOS").value ||
				   document.getElementById("hidIpLocal").value  != document.getElementById("hidIPTransaccion").value ) 
				{	alert(document.getElementById("hidMsjCajero").value);	
				        f_Contar_TransaccionesExitosas();//PROY-31949 	
					return;
				}	
							
				varTramaInsert = '';
				varTramaInsert = 'codOperacion=' + varCodOpe + '|desOperacion=' + varDescriOpe + 
				'|tipoOperacion=' + varTipoOpeFi + '|montoOperacion=' + Monto + 
				'|monedaOperacion=' + varMoneda + '|tipoTarjeta=' + varTipTarjeta + 
				'|tipoPago=' + varTipoPago + '|estadoTransaccion=' + varEstadoTrans + 
				'|tipoPos=' + varTipoPos + '|tipoTransaccion=' + varTipoTrans + '|ipCaja=' + varIpPos+ 
				'|NroRegistro=' + varNroRegistro + '|NroTienda=' + varNroTienda + '|CodigoCaja=' + varCodigoCaja +
				'|CodEstablec=' + varCodEstablec + '|NomPcPos=' + varNomPcPos + '|CodTerminal=' + varCodTerminal +
				'|IdCabecera=' + varIdCabecera +
				'|nroTarjeta=' + NTarjeta + 
				'|nroRef=' + NroReferencia;		//1 - PENDIENTE	
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
			var varIdCabez = varArrayRpta[3];
			var varFlagPago = '1';
			
			document.getElementById("HidIdCabez").value = varIdCabez;
	
		
			//2 - EN PROCESO			
			var varNumVoucher=NReferencia
			var varNumAutTransaccion='';
			var varCodRespTransaccion='';
			var varDescTransaccion='';
			var varCodAprobTransaccion='0';
			var varFechaExpiracion = '';
			var varNomCliente = document.getElementById("txtCliente").value;
			var varImpVoucher = '';
			var varSeriePOS = '';
			var varIdUnicoTrans = varCodTerminal;
			var nomEquipoPOS = varNomPcPos; 
			var varNroPedido = document.getElementById("txtNumPedido").value;  
			var varIdUnicoTrans = '';
			
					
			varEstadoTrans = '';
			varEstadoTrans = varArrayEstTrans[1];//EN PROCESO
			varTramaUpdate = '';
			varTramaUpdate = 'monedaOperacion=' + varMoneda + '|montoOperacion=' + varTransMonto + 
			'|nroRegistro=' + varNroRegistro + '|numVoucher=' + varNumVoucher + 
			'|numAutTransaccion=' + varNumAutTransaccion + '|codRespTransaccion=' + varCodRespTransaccion + 
			'|descTransaccion=' + varDescTransaccion + '|codAprobTransaccion=' +  varCodAprobTransaccion + 
			'|tipoPos=' + varTipoPos + '|varNroTarjeta=' + NTarjeta + 
			'|fechaExpiracion=' + varFechaExpiracion + '|nomCliente=' + varNomCliente + 
			'|impVoucher=' + varImpVoucher + '|seriePOS=' + varSeriePOS + 
			'|nomEquipoPOS=' + nomEquipoPOS + '|estadoTransaccion=' + varEstadoTrans +
			'|NroTienda=' + varNroTienda + '|CodigoCaja=' + varCodigoCaja + 
			'|CodEstablec=' + varCodEstablec + '|IpPos=' + varIpPos + '|IdCabez=' + varIdCabez +
			'|FlagPago=' + varFlagPago + '|Pedido=' + varNroPedido + '|IdUnico=' + varIdUnicoTrans+
			'|TipoTrans=' + varTipoTrans + '|IdRefAnulador=' + NReferencia +
			'|TipoPago=' + document.getElementById("HidTipoPago").value+
			'|ResTarjetaPos='; //PROY-31949 
			
			RSExecute(serverURL,"ActualizarTransaction",varTramaUpdate,varIdTran,CallBack_ActualizarTransaction1,GuardarTransactionError,"X");
						
			objEntityPOS={
			monedaOperacion: '', 
			montoOperacion: varTransMonto, 
			CodigoTienda: varNroTienda, 
			NroPedido: '',
			ipAplicacion: '', 
			nombreAplicacion: '', 
			usuarioAplicacion: '',
			TrnsnId: varIdTran,
			tipoPos: '',
			CodigoCaja: varCodigoCaja};
				
				if (varValueTar == 'VIS'){//VISA
					CallService(varTipTarjeta,varNameDoc,objEntityPOS);
					
				}
				else if(varValueTar=='AMX' || varValueTar=='MCD' || varValueTar=='DIN'){//MASTERCARD
					CallService(varTipTarjeta,varNameDoc,objEntityPOS);
				}
			} // FIN if(varRpta.substr(0,1)
			else {
				alert('Error al registrar la transaccion en estado PENDIENTE');
				f_Contar_TransaccionesExitosas(); //PROY-31949 
				return;
					}	
			}
		
		function CallBack_ActualizarTransaction1(response) 
		{
			var varRpta = response.return_value;
			var res = (varRpta.replace("<BODY><SELECT>", "")).replace("</SELECT>", "").replace("\r\n","");
			varRpta = res;
						}	
		function GuardarTransactionError(co) 
		{
			
			if (co.message) {
				alert("Context:" + co.context + "\nError: " + co.message);
			}
			varRptTtipoPos='1|Error';		
		}
		
		function CallBack_ActualizarTransaction(response) 
		{
			var varRpta = response.return_value;
						}	
		
		function CallService(tipoPOS,NameDoc,objEntityPOS){
			
		document.getElementById("lblEnvioPos").innerHTML  = "Enviando al POS........";
		varBolWsLocal = false;		
		var entityOpe;
		var soapMSG;
		var varMontoOperacion;		
		var varEstTran;
		var varFechaExpiracion;
		var varSeriePOS = '';		
		var VarToday = '';			
		varTipOpePOS = '';
		varEstTran = '';
		//Variables de auditoria Ini		
		var varArrayAudi = document.getElementById("HidDatoAuditPos").value.split("|");
		var varNroPedido = document.getElementById("txtNumPedido").value;
		var VarToday = new Date();
		var varIdTransaccion = varNroPedido + '_' + formatDate(VarToday);
		var varIpApplicacion = varArrayAudi[0];
		var varNombreAplicacion = varArrayAudi[1];
		var varUsuarioAplicacion = varArrayAudi[2];
		//Variables de auditoria Fin
		
		switch (tipoPOS) {
		
			case "01": //VISANET        
				if (varTipoTrans == '3'){
					varMoneda = '';
					varMontoVisa = '' ;
					}	
		else{
					varMontoVisa = Number(objEntityPOS.montoOperacion).toFixed(2);
			}
				entityOpe = { 
				TipoOperacion: varTipoOpeFi, SalidaMensaje: '', RutaArchivoINI: '', 
				TipoMoneda: varMonPOSVISA, Monto: varMontoVisa, 
				CodigoTienda: objEntityPOS.CodigoTienda, 
				CodigoCaja: objEntityPOS.CodigoCaja, 
				Empresa: '',Funcion: '', TipoPS: '', CapturaTarjeta: '',
				Cuotas: '', Diferido: '', Nombre: '', Valor: '',
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
				timeout: timeOutWsLocal,
				processData: false,
				contentType: "text/xml; charset=\"utf-8\"",
				async: true,
				cache: false,
				success: function (objResponse, status) {
														  SuccessVisaNet(objResponse,NameDoc);
						},
						 error: function (request, status) { /*Inicio Error*/
						varBolWsLocal = true;
						
						ErrorVisaNet(request,NameDoc,objEntityPOS); /*Fin Error*/
					  },
					  timeout: Number(timeOutWsLocal)
					});
			return true;
			break;
			case "02": //MASTER CARD
				if (varTipoTrans == '3'){
					varMoneda = '';
					varMontoMC = '' ;
		}
		else{
					varMontoMC = Number(objEntityPOS.montoOperacion).toFixed(2);
				}
				entityOpe = { 
							IdTransaccion: varIdTransaccion,
							IpApplicacion: varIpApplicacion,
							NombreAplicacion: varNombreAplicacion,
							UsuarioAplicacion: varUsuarioAplicacion,
							Aplicacion: varApliMC, 
							Transaccion: varTransMC, 
							Monto: varMontoMC, 
							TipoMoneda: varMonedaMCD,
							DataAdicional: NReferencia, 
							CodigoServicio: '', ClaveComercio: varPwdComercioMC,
							Dni: '', Ruc: '', Producto: '', 
							OpeMonto: '', Nombre: '', Valor: ''
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
						async: true,
						cache: false,
						success: function (objResponse, status) { /*Inicio success*/
								SuccessMasterCard(objResponse,NameDoc); 	/*Fin success*/	
					},
				error: function (request, status) {
						/*Inicio Error*/						
            varBolWsLocal = true;
            alert('Sin respuesta del POS, tiempo de espera superado.');
            ErrorMasterCard(request,NameDoc,objEntityPOS);
            /*Fin Error*/
			  },
						timeout: Number(timeOutWsLocal)
			});
			return true;
			break;
		}//fin switch				
	} //Fin CallService
	
//SuccessMastercard INI
	function SuccessMasterCard(objResponse,NameDoc)
	{
		try { 
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
			var varFechaExpiracion = '';
			
			var varNroPedido = document.getElementById("txtNumPedido").value; 
			var varIdUnicoTrans = '';
			var varFlagPago = '1';
			var varIdCabez = document.getElementById("HidIdCabez").value;
			
			varCodRptaAudit = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.AuditResponse.CodigoRespuesta;
			varMsjRptaAudit = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.AuditResponse.mensajeRespuesta;	
			varCodRptaWs = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.CodigoRespuesta;
			var varIsIncompleto = '';
			
			/*VALIDAR OPCION RE-IMPRESION*/
			if (varTipoTrans == '3'){ 			            										
				varCodOperVisa = '';
				varSeriePOS = '';			
				varNroReferencia = NReferencia; 
				varNroTarjeta = NTarjeta;
	                if(varCodRptaWs != '00'){
						sCodRespTarj = '';
						varEstTran = varArrayEstTrans[3];//RECHAZADO
                     }
                     else{
						varEstTran = varArrayEstTrans[2];//3 - ACEPTADO
                        sCodRespTarj = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.CodigoRespuesta;
						varDesRpta = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.DescripcionRespuesta;
						varImpVoucher = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.ImprimirData;
						
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
			else //else 1
			{
                if ((varCodRptaAudit == '0' && varCodRptaWs == '77')|| varCodRptaWs == '1')  //Anulación Fallida
				    {
                     sCodRespTarj = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.CodigoRespuesta;
					 varDesRpta = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.DescripcionRespuesta;
					 varNroTarjeta = NTarjeta;
					 varNumAutTransaccion = '';
					 varSeriePOS = '';
					 varImpVoucher = '';
					 varEstTran = varArrayEstTrans[3];// 4 (RECHAZADO)
					 varEstdAnul = varArrayStdAnul[1]; // GRILLA: ESTADO ANUL --> RECHAZADO 
					 varDescTran = document.getElementById("hidAnulacionRechazada").value;	//GRILLA: Resultado Proceso -> TRANSACCION RECHAZADO
					} // Fin Anulación Fallida
				else if	(varCodRptaAudit == '0'  && varCodRptaWs=='00') //Anulacion Exitosa
				{
						sCodRespTarj = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.CodigoRespuesta;
						varDesRpta = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.DescripcionRespuesta;
						varCodOperVisa = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.CodigoAprobacion;
						varNumAutTransaccion = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.NumeroAutorizacion;
						varIdRefAnu = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.NumeroReferencia;
						varIdRefAnu = (varIdRefAnu == null) ? '' : String(varIdRefAnu).replace("REF","");
						varNroTarjeta = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.NumeroTarjeta;
						varIdUnicoTrans = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.CodigoAdquiriente;varClienteVisa = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.NombreCliente;   
						varSeriePOS = '';
						varImpVoucher = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.ImprimirData;

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
					   
					   varEstTran = varArrayEstTrans[2];// 3 (ACEPTADO)
					   varEstdAnul = varArrayStdAnul[0]; //  GRILLA: ESTADO ANUL --> ACEPTADO 
					   varDescTran = document.getElementById("hidAnulacionExitosa").value;	//GRILLA: Resultado Proceso -> TRANSACCION EXITOSA
				} //Fin Else if Anulacion Exitosa
		     } // fin else 1
			varMontoOperacion = '';
			varMontoOperacion = objEntityPOS.montoOperacion
		 //cambio INI
		 if (varCodRptaWs != '00' && varTipoTrans != '3')  //No se Anuló y No es Re Impresión
			{ 
			varIdRefAnu = '';
		   	document.getElementById('HidContIntento' + NroFila).value = parseInt(document.getElementById('HidContIntento' + NroFila).value)+1;
		   	if(parseInt(document.getElementById('HidContIntento' + NroFila).value) > MaxNumIntentosAnular && varTipoTrans == 2 ) //PROY-31949 
		   	   {
		   		document.getElementById('HidContIntento' + NroFila).value = parseInt(document.getElementById('HidContIntento' + NroFila).value)+1; //PROY-31949 
		   		varIsIncompleto = 'SI';
		   		varEstTran = varArrayEstTrans[4];
		   		varEstdAnul = varArrayStdAnul[1]; // GRILLA: ESTADO ANUL --> RECHAZADO
		   		varDescTran = document.getElementById("hidAnulacionIncompleta").value;	//GRILLA: Resultado Proceso -> TRANSACCION INCOMPLETO
		   	   } 
			}
		document.getElementById('HidFila' + NroFila).value = 'EstadoAnu=' + varEstdAnul + '|ResulProce=' + varDescTran + 
		'|HidContIntento=' + document.getElementById('HidContIntento' + NroFila).value + '|IsIncompleto='+varIsIncompleto; 		

			varTramaUpdate = '';
			varTramaUpdate = 'monedaOperacion=' + varMonedaVisa + '|montoOperacion=' + varMontoOperacion + '|nroRegistro=' + varNroRegistro + 
			'|numVoucher=' + NReferencia + '|numAutTransaccion=' + varNumAutTransaccion + 
			'|codRespTransaccion=' + sCodRespTarj + '|descTransaccion=' + varDesRpta + 
			'|codAprobTransaccion=' + varCodOperVisa + 	'|tipoPos=' + varTipoPos + '|varNroTarjeta=' + varNroTarjeta + 
			'|fechaExpiracion=' + varFechaExpiracion + 	'|nomCliente=' + varClienteVisa + 
			'|impVoucher=' + varImpVoucher + '|seriePOS=' + varSeriePOS + '|nomEquipoPOS=' + varNomEquipoPOS + 
			'|estadoTransaccion=' + varEstTran +
			'|NroTienda=' + varNroTienda + '|CodigoCaja=' + varCodigoCaja + 
			'|CodEstablec=' + varCodEstablec + '|IpPos=' + varIpPos+ '|IdCabez=' + varIdCabez +
			'|FlagPago=' + varFlagPago + '|Pedido=' + varNroPedido + '|IdUnico=' + varIdUnicoTrans + 
			'|TipoTrans=' + varTipoTrans + '|IdRefAnulador=' + varIdRefAnu +
			'|TipoPago=' + document.getElementById("HidTipoPago").value+
			'|ResTarjetaPos='; //PROY-31949 
			RSExecute(serverURL,"ActualizarTransaction",varTramaUpdate,
			objEntityPOS.TrnsnId,CallBack_ActualizarTransaction2,GuardarTransactionError,"X");
	        //Auditoría INI
			if (varIsIncompleto == 'SI')
			{
			var varTramaAudit = '';    
			var varNroTelefono= '';
			var varNomCliente = document.getElementById("txtCliente").value;
			var varNroPedido = document.getElementById("txtNumPedido").value;
			if  (!(document.getElementById("hidNroTelefono").value == '')  || !(document.getElementById("hidNroTelefono").value == "")) 
				{
				varNroTelefono= document.getElementById("hidNroTelefono").value;
				}
					
			varTramaAudit = 'NomCliente=' + varNomCliente + '|NroTelefono=' + varNroTelefono + '|NroPedido=' + varNroPedido + 
							'|IdTransaccion=' + objEntityPOS.TrnsnId + '|nMonto=' + varMontoOperacion ;
			varTramaAuditAux = varTramaAudit;
			RSExecute(serverURL,"GuardarAutorizacion",varTramaAudit,CallBack_GuardarAutorizacion,GuardarAutorizacionError,"X");
			}
			//Auditoría FIN
			document.getElementById("lblEnvioPos").innerHTML  = "";
			f_UpdateAnulPOS(NroFila);
			f_Contar_TransaccionesExitosas(); //PROY-31949 
	}
	catch(err) { 
		alert(err.description);
		document.getElementById("lblEnvioPos").innerHTML  = "";
		}
	} //SuccessMastercard FIN


function ErrorMasterCard(request,NameDoc,objEntityPOS)
	{
		try {
			alert('Sin respuesta del POS, tiempo de espera superado.');
            var varClienteVisa = '';
            var varNumAutTransaccion = '';
            var varCodOperVisa = '';
            var varImpVoucher = '';
            varMontoOperacion = objEntityPOS.montoOperacion;
            var varNroPedido = document.getElementById("txtNumPedido").value; 
            var varIdUnicoTrans = '';
			var varIdCabez = document.getElementById("HidIdCabez").value;
			var varFlagPago = '1';
                        var varIsIncompleto='';
			varNumAutTransaccion = '';
			varFechaExpiracion = '';
			varCodOperVisa = '';
			varSeriePOS = '';
			varImpVoucher = '';
			sCodRespTarj = '';
			varEstTran = varArrayEstTrans[3];//RECHAZADO
			varEstdAnul = varArrayStdAnul[1]; // GRILLA: ESTADO ANUL --> RECHAZADO 
			varDescTran = document.getElementById("hidAnulacionRechazada").value;	//GRILLA: Resultado Proceso -> TRANSACCION RECHAZADO
			varDesRpta = '';
            //nuevo cod ini
			document.getElementById('HidContIntento' + NroFila).value = parseInt(document.getElementById('HidContIntento' + NroFila).value)+1;
			if(parseInt(document.getElementById('HidContIntento' + NroFila).value) > MaxNumIntentosAnular && varTipoTrans == 2 ) //PROY-31949 
			   {
				document.getElementById('HidContIntento' + NroFila).value = parseInt(document.getElementById('HidContIntento' + NroFila).value)+1; //PROY-31949 
				varIsIncompleto = 'SI';
				varEstTran = varArrayEstTrans[4];
			    varEstdAnul = varArrayStdAnul[1]; // GRILLA: ESTADO ANUL --> RECHAZADO
			    varDescTran = document.getElementById("hidAnulacionIncompleta").value;	//GRILLA: Resultado Proceso -> TRANSACCION INCOMPLETO
			    } //nuevo cod fin

			document.getElementById('HidFila' + NroFila).value = 'EstadoAnu=' + varEstdAnul + '|ResulProce=' + varDescTran + 
		     '|HidContIntento=' + document.getElementById('HidContIntento' + NroFila).value + '|IsIncompleto='+varIsIncompleto; 
	  varTramaUpdate = '';
						varTramaUpdate = 'monedaOperacion=' + varMoneda + '|montoOperacion=' + varMontoOperacion + 
						'|nroRegistro=' + varNroRegistro + 
						'|numVoucher=' + NReferencia + 
						'|numAutTransaccion=' + varNumAutTransaccion + 
						'|codRespTransaccion=' + sCodRespTarj + 
						'|descTransaccion=' + varDesRpta + 
						'|codAprobTransaccion=' + varCodOperVisa + 
						'|tipoPos=' + varTipoPos + '|varNroTarjeta=' + NTarjeta + 
						'|fechaExpiracion=' + varFechaExpiracion + 
						'|nomCliente=' + varClienteVisa + 
						'|impVoucher=' + varImpVoucher + 
						'|seriePOS=' + varSeriePOS + '|nomEquipoPOS=' + varNomEquipoPOS + 
						'|estadoTransaccion=' + varEstTran +
						'|NroTienda=' + varNroTienda + '|CodigoCaja=' + varCodigoCaja + 
						'|CodEstablec=' + varCodEstablec + '|IpPos=' + varIpPos + '|IdCabez=' + varIdCabez +
						'|FlagPago=' + varFlagPago + '|Pedido=' + varNroPedido + '|IdUnico=' + varIdUnicoTrans +
						'|TipoTrans=' + varTipoTrans + '|IdRefAnulador=' + NReferencia +
						'|TipoPago=' + document.getElementById("HidTipoPago").value+
						'|ResTarjetaPos='; //PROY-31949 

				RSExecute(serverURL,"ActualizarTransaction",varTramaUpdate,
						objEntityPOS.TrnsnId,CallBack_ActualizarTransaction2,GuardarTransactionError,"X");
								
				if (varIsIncompleto == 'SI') 
				{
				var varTramaAudit = '';    
				var varNroTelefono= '';
				var varNomCliente = document.getElementById("txtCliente").value;
				var varNroPedido = document.getElementById("txtNumPedido").value;
				if  (!(document.getElementById("hidNroTelefono").value == '')  || !(document.getElementById("hidNroTelefono").value == "")) 
					{
					varNroTelefono= document.getElementById("hidNroTelefono").value;
					}
						
				varTramaAudit = 'NomCliente=' + varNomCliente + '|NroTelefono=' + varNroTelefono + '|NroPedido=' + varNroPedido + 
								'|IdTransaccion=' + objEntityPOS.TrnsnId + '|nMonto=' + varMontoOperacion ;
				varTramaAuditAux = varTramaAudit;
				RSExecute(serverURL,"GuardarAutorizacion",varTramaAudit,CallBack_GuardarAutorizacion,GuardarAutorizacionError,"X");
				} 		//Auditoría FIN
			document.getElementById("lblEnvioPos").innerHTML  = "";
			f_UpdateAnulPOS(NroFila);
			f_Contar_TransaccionesExitosas(); //PROY-31949 
		}
		catch(err){
			alert(err.description);
			document.getElementById("lblEnvioPos").innerHTML  = "";	
		}	
	}//ErrorMasterCard FIN

	function ErrorVisaNet(request,NameDoc,objEntityPOS)
	{
		try {
			alert('Sin respuesta del POS, tiempo de espera superado.');
            var varClienteVisa = '';
            var varNumAutTransaccion = '';
            var varCodOperVisa = '';
            var varImpVoucher = '';
            varMontoOperacion = objEntityPOS.montoOperacion;
            var varNroPedido = document.getElementById("txtNumPedido").value; 
            var varIdUnicoTrans = '';
			var varIdCabez = document.getElementById("HidIdCabez").value;
			var varFlagPago = '1';
			var varIsIncompleto='';
            varEstdAnul = varArrayStdAnul[1]; // GRILLA: ESTADO ANUL --> RECHAZADO			
			varNumAutTransaccion = '';
			varFechaExpiracion = '';
			varCodOperVisa = '';
			varSeriePOS = '';
			varImpVoucher = '';
			sCodRespTarj = '';
			varEstTran = varArrayEstTrans[3];//RECHAZADO
			varDesRpta = '';
			varDescTran = document.getElementById("hidAnulacionRechazada").value;	//GRILLA: Resultado Proceso -> TRANSACCION RECHAZADO
			
            //nuevo cod ini
			document.getElementById('HidContIntento' + NroFila).value = parseInt(document.getElementById('HidContIntento' + NroFila).value)+1;
			if(parseInt(document.getElementById('HidContIntento' + NroFila).value) > MaxNumIntentosAnular && varTipoTrans == 2 ) //PROY-31949 
			   {
				document.getElementById('HidContIntento' + NroFila).value = parseInt(document.getElementById('HidContIntento' + NroFila).value)+1; //PROY-31949 
				varIsIncompleto = 'SI';
				varEstTran = varArrayEstTrans[4];
			    varEstdAnul = varArrayStdAnul[1]; // GRILLA: ESTADO ANUL --> RECHAZADO
			    varDescTran = document.getElementById("hidAnulacionIncompleta").value;	//GRILLA: Resultado Proceso -> TRANSACCION INCOMPLETO
			    } //nuevo cod fin
			
			document.getElementById('HidFila' + NroFila).value = 'EstadoAnu=' + varEstdAnul + '|ResulProce=' + varDescTran + 
		     '|HidContIntento=' + document.getElementById('HidContIntento' + NroFila).value + '|IsIncompleto='+varIsIncompleto; 
						
			  varTramaUpdate = '';
						varTramaUpdate = 'monedaOperacion=' + varMoneda + '|montoOperacion=' + varMontoOperacion + 
						'|nroRegistro=' + varNroRegistro + 
						'|numVoucher=' + NReferencia + 
						'|numAutTransaccion=' + varNumAutTransaccion + 
						'|codRespTransaccion=' + sCodRespTarj + 
						'|descTransaccion=' + varDesRpta + 
						'|codAprobTransaccion=' + varCodOperVisa + 
						'|tipoPos=' + varTipoPos + '|varNroTarjeta=' + NTarjeta + 
						'|fechaExpiracion=' + varFechaExpiracion + 
						'|nomCliente=' + varClienteVisa + 
						'|impVoucher=' + varImpVoucher + 
						'|seriePOS=' + varSeriePOS + '|nomEquipoPOS=' + varNomEquipoPOS + 
						'|estadoTransaccion=' + varEstTran +
						'|NroTienda=' + varNroTienda + '|CodigoCaja=' + varCodigoCaja + 
						'|CodEstablec=' + varCodEstablec + '|IpPos=' + varIpPos + '|IdCabez=' + varIdCabez +
						'|FlagPago=' + varFlagPago + '|Pedido=' + varNroPedido + '|IdUnico=' + varIdUnicoTrans +
						'|TipoTrans=' + varTipoTrans + '|IdRefAnulador=' + NReferencia +
						'|TipoPago=' + document.getElementById("HidTipoPago").value+
						'|ResTarjetaPos='; //PROY-31949 

				RSExecute(serverURL,"ActualizarTransaction",varTramaUpdate,
						objEntityPOS.TrnsnId,CallBack_ActualizarTransaction2,GuardarTransactionError,"X");
								
				if (varIsIncompleto == 'SI') //Auditoría INI
				{
				var varTramaAudit = '';    
				var varNroTelefono= '';
				var varNomCliente = document.getElementById("txtCliente").value;
				var varNroPedido = document.getElementById("txtNumPedido").value;
				if  (!(document.getElementById("hidNroTelefono").value == '')  || !(document.getElementById("hidNroTelefono").value == "")) 
					{
					varNroTelefono= document.getElementById("hidNroTelefono").value;
					}
						
				varTramaAudit = 'NomCliente=' + varNomCliente + '|NroTelefono=' + varNroTelefono + '|NroPedido=' + varNroPedido + 
								'|IdTransaccion=' + objEntityPOS.TrnsnId + '|nMonto=' + varMontoOperacion ;
				varTramaAuditAux = varTramaAudit;
				RSExecute(serverURL,"GuardarAutorizacion",varTramaAudit,CallBack_GuardarAutorizacion,GuardarAutorizacionError,"X");
				} 		//Auditoría FIN
			document.getElementById("lblEnvioPos").innerHTML  = "";
			f_UpdateAnulPOS(NroFila);
			f_Contar_TransaccionesExitosas(); //PROY-31949 
		}
		catch(err){
			alert(err.description);
			document.getElementById("lblEnvioPos").innerHTML  = "";
		}
	} //ErrorVisaNet fin
			
	function CallBack_GuardarAutorizacion(response)
	{ var varRpta = response.return_value;
	  var res = (varRpta.replace("<BODY><SELECT>", "")).replace("</SELECT>", "").replace("\r\n","");
	  varRpta = res;	
	  document.getElementById("hidAutorizacion").value = varRpta ; 
	  if (varRpta != "1"){
		alert('Ud. no está autorizado a realizar esta operación. Comuniquese con el administrador');
		RSExecute(serverURL,"GuardarAutorizacion",varTramaAuditAux,CallBack_GuardarAutorizacion,GuardarAutorizacionError,"X");
		return;	
	}
	   else
	   {	   
		varEstdAnul = varArrayStdAnul[1]; // GRILLA: ESTADO ANUL --> INCOMPLETO  
		varDescTran = document.getElementById("hidAnulacionIncompleta").value;	//GRILLA: Resultado Proceso -> TRANSACCION INCOMPLETO
		f_UpdateAnulPOS(NroFila); 
	   }
	} // FIN CallBack_GuardarAutorizacion
	

	function GuardarAutorizacionError(co)
	{
		
		if (co.message) {
			alert("Context:" + co.context + "\nError: " + co.message);
		}
	} // FIN GuardarAutorizacionError
	

function SuccessVisaNet(objResponse,NameDoc)
	{
	try { 
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
			var varIsIncompleto='';
			
			var varNroPedido = document.getElementById("txtNumPedido").value;
			var varIdUnicoTrans = '';
			var varFlagPago = '1';
			var varIdCabez = document.getElementById("HidIdCabez").value;
			varCodRptaAudit = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.AuditResponse.CodigoRespuesta;
			varMsjRptaAudit = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.AuditResponse.mensajeRespuesta;	
			varCodRptaWs = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.CodigoRespuesta;

			/*VALIDAR OPCION RE-IMPRESION*/
			if (varTipoTrans == '3'){ 
				varNroTarjeta = NTarjeta;
				//varNroRefVisa
				varNumAutTransaccion = '';
				varFechaExpiracion = '';
				varCodOperVisa = '';
				varSeriePOS = '';
				varImpVoucher = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.ImprimirData;
				sCodRespTarj = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.CodigoRespuesta;
				varDesRpta = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.DescripcionRespuesta;
				
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
				if(varCodRptaWs != '0'){
					varEstTran = varArrayEstTrans[3];//RECHAZADO				
				}
				else{
					varEstTran = varArrayEstTrans[2];//3 - ACEPTADO
		}
		}
			else //else 1
			{
			
                if (varCodRptaWs == 'undefined' || 
			   tiposError.indexOf(jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.CodigoRespuesta)>-1 ) 
				    {                    
					 if (varCodRptaWs == 'undefined')
					 {varDesRpta = '';
					 varImpVoucher = '';
					 }
					 else {
					 varDesRpta = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.DescripcionRespuesta;
								
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
					 }
                     
					 varNroTarjeta = NTarjeta;
					 varNumAutTransaccion = '';
					 varFechaExpiracion = '';
					 sCodRespTarj = '';
					 varCodOperVisa = '';
					 varSeriePOS = '';
					 varImpVoucher = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.ImprimirData;
					 varEstTran = varArrayEstTrans[3];// 4 (RECHAZADO)
					 varEstdAnul = varArrayStdAnul[1]; // GRILLA: ESTADO ANUL --> RECHAZADO 
					 varDescTran = document.getElementById("hidAnulacionRechazada").value;	//GRILLA: Resultado Proceso -> TRANSACCION RECHAZADO
					}
				else if (varCodRptaWs == '00')
				{
					sCodRespTarj = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.CodigoRespuesta;
					 varDesRpta = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.DescripcionRespuesta;
					 varClienteVisa = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.NombreCliente;   
					 varNumAutTransaccion = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.NumeroAutorizacion;
					 NReferencia = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.NumeroReferencia;
					 varNroTarjeta = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.NumeroTarjeta;     
					 varFechaExpiracion = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.FechaExpiracion;
					 varCodOperVisa = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.CodigoOperacion;
					 varSeriePOS = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.Terminal;
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
					
					   varEstTran = varArrayEstTrans[2];// 3 (ACEPTADO)
					   varEstdAnul = varArrayStdAnul[0];//  GRILLA: ESTADO ANUL --> ACEPTADO 
					   varDescTran = document.getElementById("hidAnulacionExitosa").value;	//GRILLA: Resultado Proceso -> TRANSACCION ACEPTADO			
					 
				} //Fin Else if
		        else { 
				//  1 OPERACION CANCELADA
						varDesRpta = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.DescripcionRespuesta; 
						varNroTarjeta = NTarjeta;
						varNumAutTransaccion = '';
						varFechaExpiracion = '';
						sCodRespTarj = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.CodigoRespuesta;
						varCodOperVisa = '';
						varSeriePOS = '';
						varImpVoucher = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.ImprimirData;
						varEstTran = varArrayEstTrans[3];// 4 (RECHAZADO)
						varEstdAnul = varArrayStdAnul[1]; // GRILLA: ESTADO ANUL --> RECHAZADO 
						varDescTran = document.getElementById("hidAnulacionRechazada").value;	//GRILLA: Resultado Proceso -> TRANSACCION RECHAZADO
					 }
			}
					
			varMontoOperacion = '';
			varMontoOperacion = objEntityPOS.montoOperacion
		 if (varCodRptaWs != '00' && varTipoTrans != '3')  //No se Anuló y No es Re Impresión
			{
			//nuevo cod ini
			document.getElementById('HidContIntento' + NroFila).value = parseInt(document.getElementById('HidContIntento' + NroFila).value)+1;
			if(parseInt(document.getElementById('HidContIntento' + NroFila).value) > MaxNumIntentosAnular && varTipoTrans == 2 ) //PROY-31949 
			   {
				document.getElementById('HidContIntento' + NroFila).value = parseInt(document.getElementById('HidContIntento' + NroFila).value)+1; //PROY-31949 
				varIsIncompleto = 'SI';
				varEstTran = varArrayEstTrans[4];
			    varEstdAnul = varArrayStdAnul[1]; // GRILLA: ESTADO ANUL --> RECHAZADO
			    varDescTran = document.getElementById("hidAnulacionIncompleta").value;	//GRILLA: Resultado Proceso -> TRANSACCION INCOMPLETO
			   } //nuevo cod fin
			}
		
		document.getElementById('HidFila' + NroFila).value = 'EstadoAnu=' + varEstdAnul + '|ResulProce=' + varDescTran + 
		'|HidContIntento=' + document.getElementById('HidContIntento' + NroFila).value + '|IsIncompleto='+varIsIncompleto; 
				
			varTramaUpdate = '';
			varTramaUpdate = 'monedaOperacion=' + varMonedaVisa + '|montoOperacion=' + varMontoOperacion + '|nroRegistro=' + varNroRegistro + 
			'|numVoucher=' + NReferencia + '|numAutTransaccion=' + varNumAutTransaccion + 
			'|codRespTransaccion=' + sCodRespTarj + '|descTransaccion=' + varDesRpta + 
			'|codAprobTransaccion=' + varCodOperVisa + 	'|tipoPos=' + varTipoPos + '|varNroTarjeta=' + varNroTarjeta + 
			'|fechaExpiracion=' + varFechaExpiracion + 	'|nomCliente=' + varClienteVisa + 
			'|impVoucher=' + varImpVoucher + '|seriePOS=' + varSeriePOS + '|nomEquipoPOS=' + varNomEquipoPOS + 
			'|estadoTransaccion=' + varEstTran +
			'|NroTienda=' + varNroTienda + '|CodigoCaja=' + varCodigoCaja + 
			'|CodEstablec=' + varCodEstablec + '|IpPos=' + varIpPos + '|IdCabez=' + varIdCabez +
			'|FlagPago=' + varFlagPago + '|Pedido=' + varNroPedido + '|IdUnico=' + varIdUnicoTrans +
			'|TipoTrans=' + varTipoTrans + '|IdRefAnulador=' + NReferencia +
			'|TipoPago=' + document.getElementById("HidTipoPago").value+
			'|ResTarjetaPos='; //PROY-31949 
			RSExecute(serverURL,"ActualizarTransaction",varTramaUpdate,
					objEntityPOS.TrnsnId,CallBack_ActualizarTransaction2,GuardarTransactionError,"X");
			
			//Auditoría INI
			if (varIsIncompleto == 'SI')
			{
			var varTramaAudit = '';    
			var varNroTelefono= '';
			var varNomCliente = document.getElementById("txtCliente").value;
			var varNroPedido = document.getElementById("txtNumPedido").value;
			if  (!(document.getElementById("hidNroTelefono").value == '')  || !(document.getElementById("hidNroTelefono").value == "")) 
				{
				varNroTelefono= document.getElementById("hidNroTelefono").value;
		}
			
			varTramaAudit = 'NomCliente=' + varNomCliente + '|NroTelefono=' + varNroTelefono + '|NroPedido=' + varNroPedido + 
							'|IdTransaccion=' + objEntityPOS.TrnsnId + '|nMonto=' + varMontoOperacion ;
			varTramaAuditAux = varTramaAudit;
			RSExecute(serverURL,"GuardarAutorizacion",varTramaAudit,CallBack_GuardarAutorizacion,GuardarAutorizacionError,"X");
			}
			//Auditoría FIN
			
			document.getElementById("lblEnvioPos").innerHTML  = "";
			f_UpdateAnulPOS(NroFila);
			f_Contar_TransaccionesExitosas(); //PROY-31949 
			}
	catch(err) { 
		alert(err.description);
		document.getElementById("lblEnvioPos").innerHTML  = "";
		}
	} //FIn Succes Visa
	
	function f_UpdateAnulPOS(Indice)
	{ Indice = parseInt(Indice) + 1;
	  if (varTipoTrans != '3'){ 
	  var varCelda = '';
	  var varAncla = '';
	  var varPropiedad = '';
	  dgrDetalle.rows[Indice].cells[4].innerHTML=varEstdAnul; // ESTADO ANUL --> ACEPTADO/RECHAZADO 
	  dgrDetalle.rows[Indice].cells[5].innerHTML=varDescTran; // TRANSACCION RECHAZADO/ACEPTADO/INCOMPLETO

	  		
	  
	  	if (varDescTran == document.getElementById("hidAnulacionIncompleta").value)
		   {
			dgrDetalle.rows[Indice].cells[6].innerHTML = '<IMG style="CURSOR: hand"  border="0" src="../images/delete-icon_ena.png">';
			dgrDetalle.rows[Indice].cells[7].innerHTML =  '<IMG style="CURSOR: hand"  border="0" src="../images/print-icon_ena.png">';
			//document.getElementById("btnCancelar").style.display = "none"; //wnd* //PROY-31949 
			}
		else if (varDescTran == document.getElementById("hidAnulacionExitosa").value)
			{
				dgrDetalle.rows[Indice].cells[6].innerHTML = '<IMG style="CURSOR: hand"  border="0" src="../images/delete-icon_ena.png">';
				
				varCelda = dgrDetalle.rows[Indice].cells[7]; 
				varAncla = varCelda.children[0];
				varPropiedad =   varAncla.children[0];
				varPropiedad.src="../images/print-icon.png";
				//document.getElementById("btnCancelar").style.display = "none"; //wnd* //PROY-31949 
	}
		else if (varDescTran == document.getElementById("hidAnulacionRechazada").value)
			{
			 varCelda = dgrDetalle.rows[Indice].cells[6]; 
			 
			 
			 varAncla = varCelda.children[0];
			 varPropiedad =   varAncla.children[0];
			 varPropiedad.src="../images/delete-icon.png";
			 
			 varCelda = dgrDetalle.rows[Indice].cells[7]; 
			 varAncla = varCelda.children[0];
			 varPropiedad =   varAncla.children[0];
			 varPropiedad.src="../images/print-icon_ena.png";
			}		
	  } 
		}
			
	function f_Exportar()
	{
		var nroPedido = document.getElementById("txtNumPedido").value
		document.all.ifraExcel.src="FormaPagoExcel.aspx?NroPedido="+nroPedido;
	}
	
function f_CargaEstados()
	{
	var varCelda = '';
	var varAncla = '';
	var varPropiedad = '';
	
	for (var i = 1; i <=dgrDetalle.rows.length-1; i++)
		{
		var vNroFila  = i-1;
		if (document.getElementById('HidFila' + vNroFila).value !=''){			
			var vArray =  document.getElementById('HidFila' + vNroFila ).value.split("|");  // ESTADO-TRANSACCION-INTENTO-ISINCOMPL	
			
			dgrDetalle.rows[i].cells[4].innerText =vArray[0].substr(vArray[0].indexOf("=")+1); // ESTADO ANUL --> ACEPTADO/RECHAZADO 
			dgrDetalle.rows[i].cells[5].innerText =vArray[1].substr(vArray[1].indexOf("=")+1); // TRANSACCION RECHAZADO/ACEPTADO/INCOMPLETO 
			var varTransaccion = vArray[1].substr(vArray[1].indexOf("=")+1) 
			
			if (varTransaccion == document.getElementById("hidAnulacionIncompleta").value)
				{
				dgrDetalle.rows[i].cells[6].innerHTML = '<IMG style="CURSOR: hand"  border="0" src="../images/delete-icon_ena.png">';
				dgrDetalle.rows[i].cells[7].innerHTML =  '<IMG style="CURSOR: hand"  border="0" src="../images/print-icon_ena.png">';
				}
			else if (varTransaccion == document.getElementById("hidAnulacionExitosa").value )
				{
				dgrDetalle.rows[i].cells[6].innerHTML = '<IMG style="CURSOR: hand"  border="0" src="../images/delete-icon_ena.png">';
				
				varCelda = dgrDetalle.rows[i].cells[7]; 
				varAncla = varCelda.children[0];
				varPropiedad =   varAncla.children[0];
				varPropiedad.src = "../images/print-icon.png";
				//document.getElementById("btnCancelar").style.display = "none"; //wnd*//PROY-31949 
				}
			else if (varTransaccion == document.getElementById("hidAnulacionRechazada").value)
				{
					varCelda = dgrDetalle.rows[i].cells[6]; 
					varAncla = varCelda.children[0];
					varPropiedad =   varAncla.children[0];
					varPropiedad.src = "../images/delete-icon.png";
					
					varCelda = dgrDetalle.rows[i].cells[7]; 
					varAncla = varCelda.children[0];
					varPropiedad =  varAncla.children[0];
					varPropiedad.src = "../images/print-icon_ena.png";
				}
		 }
		 else
		 {		
		    var varTipoTarjeta = dgrDetalle.rows[i].cells[1].innerText;
			var  varTransaccion  = dgrDetalle.rows[i].cells[5].innerHTML;
			
			if (varTipoTarjeta == 'VISA' || varTipoTarjeta =='MASTERCARD')
			{
				if (varTransaccion == document.getElementById("hidAnulacionIncompleta").value)
					{
					 dgrDetalle.rows[i].cells[6].innerHTML = '<IMG style="CURSOR: hand"  border="0" src="../images/delete-icon_ena.png">';
					 dgrDetalle.rows[i].cells[7].innerHTML =  '<IMG style="CURSOR: hand"  border="0" src="../images/print-icon_ena.png">';
					 //document.getElementById("btnCancelar").style.display = "none"; //wnd*//PROY-31949 
					}
				else if (varTransaccion == document.getElementById("hidAnulacionExitosa").value )
					{
					dgrDetalle.rows[i].cells[6].innerHTML = '<IMG style="CURSOR: hand"  border="0" src="../images/delete-icon_ena.png">';

					varCelda = dgrDetalle.rows[i].cells[7]; 
					varAncla = varCelda.children[0];
					varPropiedad = varAncla.children[0];
					varPropiedad.src="../images/print-icon.png";
					//document.getElementById("btnCancelar").style.display = "none"; //wnd*//PROY-31949 
					}
				else 
					{
					varCelda = dgrDetalle.rows[i].cells[6]; 
					varAncla = varCelda.children[0];
					varPropiedad = varAncla.children[0];
					varPropiedad.src="../images/delete-icon.png";
					
					varCelda = dgrDetalle.rows[i].cells[7]; 
					varAncla = varCelda.children[0];
					varPropiedad = varAncla.children[0];
					varPropiedad.src="../images/print-icon_ena.png";
					}
			}			
		 }//Fin Else
		} //Fin for
		f_Contar_TransaccionesExitosas(); //PROY-31949 
	}

	function f_isFormPago()
	{
		var IsFormPago = document.getElementById("hidIsVerFormPag").value;
		if (IsFormPago == "0"){  // Visualizar Forma de Pago
			document.getElementById("btnGrabar").style.display = "none";
			document.getElementById("btnCancelar").innerText = "Cerrar";  
			document.getElementById("btnExportar").style.display = "block";
		}
		else{  //Anulacion de Pago
			document.getElementById("btnExportar").style.display = "none";			
            if (document.getElementById("HidIntAutPos").value == 1) 
			 {f_CargaEstados();
			 }
		}
	}

	function CallBack_ActualizarTransaction2(response) 
	{
		var varRpta = response.return_value;

		var res = (varRpta.replace("<BODY><SELECT>", "")).replace("</SELECT>", "").replace("\r\n","");

		varRpta = res;
	}
	//PROY-31949 
	function f_Contar_TransaccionesExitosas()
	{
		contTransEvaluar = 0;
		contTransOK = 0;
		document.getElementById("lblEnvioPos").innerHTML  = "";	
		
		for (var i = 1; i <=dgrDetalle.rows.length-1; i++)
		{
			var ResTransaccion  = dgrDetalle.rows[i].cells[5].innerHTML;
			var NroTarjeta  = dgrDetalle.rows[i].cells[2].innerHTML;
			if(ResTransaccion == document.getElementById("hidAnulacionIncompleta").value || ResTransaccion == document.getElementById("hidAnulacionExitosa").value )
			{
				contTransOK = contTransOK + 1;
			}
			if (NroTarjeta != '&nbsp;')
			{  
				contTransEvaluar = contTransEvaluar + 1;
			}
		}
		
		document.getElementById("HidGuardarTrans").value = '0';
		if (contTransEvaluar == 0)
		{
			blnClosingMod = false; 
			document.getElementById("HidGuardarTrans").value = '1';
		}
		else
		{
			document.getElementById("btnGrabar").disabled= true;
			
			if(contTransOK == 0)
			{
				blnClosingMod = false;
			}
			else
			{
				document.getElementById("btnCancelar").disabled= true;
				blnClosingMod = true;
				
				if(contTransEvaluar != contTransOK)
				{
					document.getElementById("btnGrabar").disabled= true;
				}	
				else
				{
					document.getElementById("btnGrabar").disabled= false;
					document.getElementById("HidGuardarTrans").value = '1';
				}
			}		
		}
		
	
	}
        //PROY-31949 
	//PROY-27440 FIN

	
			</script>
			<meta http-equiv="pragma" content="no-cache">
	</HEAD>

	<body topmargin="0" leftmargin="15" marginwidth="0" marginheight="0" onload="f_isFormPago();">

		<form name="frmPrincipal" id="frmPrincipal" method="post" runat="server">
			<table width="100%" cellpadding="0" cellspacing="0" border="0" style="margi-left:10px;">
				<tr>
					<td width="810" valign="top">
						<!-- Inicio Cuerpo Principal-->
						<table height="14" cellSpacing="0" cellPadding="0" width="100%" border="0" name="Contenedor">
							<tr>
								<td align="center"></td>
							</tr>
						</table>
						<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="100%" align="center"
							border="1" name="Contenedor">
							<tr>
								<td align="center">
									<table width="810" border="0" cellspacing="0" cellpadding="0" align="center">
										<tr>
											<td width="10" valign="top" height="32"></td>
											<td width="98%" height="32" align="center" class="TituloRConsulta" valign="top">Consulta 
												Detalle Pagos</td>
											<td valign="top" width="14" height="32"></td>
										</tr>
									</table>
									<table width="810" border="0" cellspacing="0" cellpadding="0" align="center">
										<tr>
											<td valign="top" width="10"></td>
											<td valign="middle" width="98%">
												<table border="0" cellspacing="2" cellpadding="0" align="center" class="Arial12B" width="715"
													height="88">
													<tr>
														<td width="25" height="4"></td>
													</tr>
													<tr>
														<td height="18" colspan="6">
															<table border="0" cellspacing="1" cellpadding="0" bgcolor="#bfbee9">
																<tr class="Arial12b" bgcolor="white">
																	<td width="200">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Factura</b></td>
																</tr>
															</table>
														</td>
													</tr>
													<tr>
														<td width="25" height="19">&nbsp;</td>
														<td width="180" class="Arial12b">&nbsp;&nbsp;&nbsp;Nro. Factura SAP :</td>
														<td width="131" class="Arial12b"><input class="clsInputDisable" type="text" id="txtNumFact" size="30" maxlength="15" tabindex="1"
																runat="server" readonly NAME="txtNumFact"></td>
														<td height="24" width="46">&nbsp;</td>
														<td width="159" class="Arial12b">&nbsp;&nbsp;Nro. Factura SUNAT :</td>
														<td width="147" class="Arial12b"><input class="clsInputDisable" type="text" id="txtNumFactSunat" maxlength="15" tabindex="2"
																runat="server" readonly NAME="txtNumFactSunat"></td>
													</tr>
													<tr>
														<td width="25" height="19">&nbsp;</td>
														<td width="180" class="Arial12b">&nbsp;&nbsp;&nbsp;Cliente :</td>
														<td width="131" class="Arial12b" colspan="4"><input class="clsInputDisable" type="text" id="txtCliente" size="110" maxlength="15" tabindex="3"
																runat="server" readonly NAME="txtCliente"></td>
													</tr>
												</table>
												<hr>
												<table border="0" cellspacing="2" cellpadding="0" align="center" class="Arial12B" width="90%">
													<tr>
														<td width="25" height="19">&nbsp;</td>
														<td width="172" class="Arial12b" height="19">&nbsp;&nbsp;&nbsp;Total Factura :</td>
														<td width="488" class="Arial12b" height="19"><input name="txtNeto" type="text" class="clsInputDisable" id="txtNeto" tabindex="4" runat="server"
																size="30" maxlength="30" readonly></td>
														<td width="7" height="19" colspan="3"></td>
													<tr>
														<td width="25">&nbsp;</td>
														<td width="172" class="Arial12b">&nbsp;&nbsp;&nbsp;Cuota Inicial:</td>
														<td class="Arial12b"><input name="txtCuotaIni" type="text" class="clsInputDisable" id="txtCuotaIni" runat="server"
																tabindex="4" size="30" maxlength="30" readonly>
														</td>
														<td height="18" colspan="3"></td>
													<tr>
														<td width="25">&nbsp;</td>
														<td width="172" class="Arial12b">&nbsp;&nbsp;Saldo a Pagar:</td>
														<td class="Arial12b"><input name="txtSaldo" type="text" class="clsInputDisable" id="txtSaldo" tabindex="5" runat="server"
																size="30" maxlength="30" readonly></td>
														<td height="18" colspan="3"></td>
													</tr>
												</table>
											</td>
											<td valign="top" width="10" align="right"></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<table height="5" cellSpacing="0" cellPadding="0" width="100%" border="0" name="Contenedor">
							<tr>
								<td align="center"></td>
							</tr>
						</table>
						<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="100%" align="center"
							border="1" name="Contenedor">
							<tr>
								<td align="center">
									<TABLE width="810" border="0" cellspacing="0" cellpadding="0" align="center">
										<tr>
											<td height="5"></td>
										</tr>
									</TABLE>
									<table width="810" border="0" cellspacing="0" cellpadding="0" align="center">
										<tr>
											<td valign="top" width="14"></td>
											<td width="98%">
												<table width="780" border="1" cellspacing="0" cellpadding="0" bordercolor="#d0d8f0" bgcolor="#ffffff"
													align="center">
													<tr>
														<td>
															<div style="BORDER-RIGHT:1px; BORDER-TOP:1px; OVERFLOW-Y:scroll; Z-INDEX:1; LEFT:0px; OVERFLOW-X:scroll; BORDER-LEFT:1px; WIDTH:775px; BORDER-BOTTOM:1px; POSITION:relative; HEIGHT:176px; TEXT-ALIGN:center"
																class="frame2">
																<asp:DataGrid id="dgrDetalle" runat="server" CssClass="Arial11B" AutoGenerateColumns="False" BorderColor="White"
																	BorderWidth="0" CellSpacing="1">
																	<AlternatingItemStyle BackColor="#DDDEE2"></AlternatingItemStyle>
																	<ItemStyle BackColor="#E9EBEE"></ItemStyle>
																	<HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderColor="#999999" CssClass="Arial12B"></HeaderStyle>
																	<Columns>	<%--PROY-27440 INI --%>
																		<asp:TemplateColumn Visible="false">
																			<%--PROY-27440 FIN --%>	<HeaderStyle Wrap="False" Width="50px"></HeaderStyle>
																			<ItemTemplate>
																				<INPUT id=chkSel type="checkbox" checked="true" disabled="true" value='<%# DataBinder.Eval(Container,"DataItem.DEPAV_FORMAPAGO") %>'>
																			</ItemTemplate>
																		</asp:TemplateColumn>
																		<asp:BoundColumn DataField="DEPAV_DESCPAGO" HeaderText="Forma Pago">
																			<%--PROY-27440 INI --%>
																			<HeaderStyle Wrap="False" Width="120px"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="INDICE" HeaderText="Ind" Visible="false">
																			<HeaderStyle Wrap="False" Width="24px"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="EQUIVALENCIA" HeaderText="Equivalencia" Visible="false">
																			<HeaderStyle Wrap="False" Width="67px"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="TRANSACCION_ID" HeaderText="TRANSACCION_ID" Visible="false">
																			<HeaderStyle Wrap="False" Width="70px"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="NRO_REFERENCIAPOS" HeaderText="REF POS" Visible="false">
																			<HeaderStyle Wrap="False" Width="40px"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="TIPO_TARJETA" HeaderText="Tipo Tarjeta" Visible="true">
																			<HeaderStyle Wrap="False" Width="80px"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="NRO_TARJETA" HeaderText="Nro. Tarjeta/Documento">
																			<HeaderStyle Wrap="False" Width="160px"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="DEPAN_IMPORTE" HeaderText="Monto Pagado">
																			<HeaderStyle Wrap="False" Width="86px"></HeaderStyle>
                                                                                                                                                <%--PROY27440 FIN --%>
																		</asp:BoundColumn>
																		<%--PROY-27440 INI --%>
																		<asp:BoundColumn DataField="ESTADO_ANULACION" HeaderText="Estado Anulacion">
																			<HeaderStyle Wrap="False" Width="130px"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="RESULTADO_PROCESO" HeaderText="Resultado Proceso">
																			<HeaderStyle Wrap="False" Width="130px"></HeaderStyle>
																		</asp:BoundColumn>
																		<%--PROY27440 FIN --%>
																		<asp:BoundColumn Visible="False" DataField="DEPAV_FORMAPAGO" HeaderText="VIAPAGO"></asp:BoundColumn>
																		<%--PROY-27440 INI --%>
																		<asp:TemplateColumn >
																			<ItemTemplate >
																				<A id="idEnvio" onclick="blnClosingMod = false; f_EnvioPOS('<%# DataBinder.Eval(Container,"DataItem.INDICE") %>','<%# DataBinder.Eval(Container,"DataItem.NRO_REFERENCIAPOS") %>','<%# DataBinder.Eval(Container,"DataItem.NRO_TARJETA") %>','<%# DataBinder.Eval(Container,"DataItem.EQUIVALENCIA") %>','<%# DataBinder.Eval(Container,"DataItem.DEPAN_IMPORTE") %>','Eliminar POS');">
																					<IMG style="CURSOR: hand" id="icoTransEn" border="0" alt="Eliminar POS" src="../images/delete-icon.png"></A>
																			</ItemTemplate>
																		</asp:TemplateColumn>
																		<asp:TemplateColumn >
																			<ItemTemplate >
																				<A id="idImprimir" onclick="blnClosingMod = false; f_EnvioPOS('<%# DataBinder.Eval(Container,"DataItem.INDICE") %>','<%# DataBinder.Eval(Container,"DataItem.NRO_REFERENCIAPOS") %>','<%# DataBinder.Eval(Container,"DataItem.NRO_TARJETA") %>','<%# DataBinder.Eval(Container,"DataItem.EQUIVALENCIA") %>','<%# DataBinder.Eval(Container,"DataItem.DEPAN_IMPORTE") %>','Imprimir POS');">
																					<IMG style="CURSOR: hand" id="icoTransEL" border="0" alt="Imprimir POS" src="../images/print-icon.png"></A>
																			</ItemTemplate>
																		</asp:TemplateColumn>
																		<%--PROY-27440 FIN --%>
																	</Columns>
																</asp:DataGrid>
															</div>
															<!--</div>-->
														</td>
													</tr>
												</table>
											</td>
											<td valign="top" width="14" align="right"></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						        <!--PROY-27440 INI-->				
						<table border="0" cellSpacing="0" cellPadding="0" width="100%" align="center">
			                         <tr>
					           <td height="10" align="center">

						        <asp:Label id="lblEnvioPos" runat="server" CssClass="TituloRConsulta"></asp:Label>
					            </td>
				                  </tr>
			                         </table>
			      </td>
			      </tr>
						        <!--PROY-27440 FIN-->
				<tr>
					<td width="810" align="center">
						<br>
						<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="400" align="center"
							border="1">
							<tr>
								<td align="center">
									<table cellSpacing="2" cellPadding="0" border="0">
										<tr>
											<td align="center" width="28"></td>
											<td align="center" width="60"><asp:button id="btnGrabar" runat="server" Width="100px" CssClass="BotonOptm" Text="Grabar"></asp:button></td>
											<%--PROY-27440 INI --%>
											<td align="center" width="60"><input name="btnExportar" style="WIDTH: 100px; DISPLAY: none" type="button" class="BotonOptm"
													onClick="javascript:f_Exportar();" value="Exportar Excel"></td>
											<%--PROY-27440 FIN --%>
											<td align="center" width="28"></td>
											<td align="center" width="60"><asp:button id="btnCancelar" runat="server" Width="100px" CssClass="BotonOptm" Text="Cancelar"></asp:button></td>
											<td align="center" width="28"></td>
											<%--PROY-27440 INI --%>
											<td><iframe style="DISPLAY: none" id="ifraExcel" height="10" width="2"></iframe></td>
											<%--PROY-27440 FIN --%>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				</td> </tr>
			</table>
			<%--PROY-27440 INI --%>
			<script language="JavaScript" type="text/javascript">
						var esNavegador, esIExplorer;

						esNavegador = (navigator.appName == "Netscape") ? true : false;
						esIExplorer = ((navigator.appName.indexOf("Microsoft") != -1) || (navigator.appName.indexOf("MSIE") != -1)) ? true : false;

						if (esIExplorer) {
						}
			</script>
			<%--PROY-27440 FIN --%>
			</TD></TR></TABLE>
			<script language="JavaScript" type="text/javascript">
var esNavegador, esIExplorer;
  esNavegador = (navigator.appName == "Netscape") ? true : false;
  esIExplorer = ((navigator.appName.indexOf("Microsoft") != -1) || (navigator.appName.indexOf("MSIE") != -1)) ? true : false;

function e_mayuscula(){
	if (event.keyCode>96&&event.keyCode<123)
		event.keyCode=event.keyCode-32;
}

function e_numero(){
	if ( !( (event.keyCode>=48) && (event.keyCode<=57)) && !(event.keyCode==13) )
		event.keyCode=0;
}

if (esIExplorer) {
	//document.onkeypress = e_envio;
}
			</script>
			<INPUT id="txtFecha" type="hidden" runat="server" NAME="txtFecha"><INPUT id="txtOfiVta" type="hidden" runat="server" NAME="txtOfiVta">
			<INPUT id="txtViasPago" type="hidden" runat="server" NAME="txtViasPago"> <INPUT id="txtFechaPago" type="hidden" runat="server" NAME="txtFechaPago">
			<INPUT id="txtMontos" type="hidden" runat="server" NAME="txtMontos"><INPUT id="txtNumPedido" type="hidden" runat="server" NAME="txtNumPedido">

                        	<!--PROY-27440 INI-->
                        <input style="DISPLAY: none" id="HidCodOpera" name="HidCodOpera" runat="server">
			<input style="DISPLAY: none" id="HidDesOpera" name="HidDesOpera" runat="server">
			<input style="DISPLAY: none" id="HidTipoTarjeta" name="HidTipoTarjeta" runat="server">
			<input style="DISPLAY: none" id="HidTipoPOS" name="HidTipoPOS" runat="server"> 
			<input style="DISPLAY: none" id="HidTipoTran" name="HidTipoTran" runat="server">
			<input style="DISPLAY: none" id="HidEstTrans" name="HidEstTrans" runat="server">
			<input style="DISPLAY: none" id="HidApliPOS" name="HidApliPOS" runat="server"> 
			<input style="DISPLAY: none" id="HidTransMC" name="HidTransMC" runat="server">
			<input style="DISPLAY: none" id="HidTipoMoneda" name="HidTipoMoneda" runat="server">
			<input style="DISPLAY: none" id="HidPtoVenta" name="HidPtoVenta" runat="server">
			<input style="DISPLAY: none" id="HidTipoOpera" name="HidTipoOpera" runat="server">
			<input style="DISPLAY: none" id="HidTipoPago" name="HidTipoPago" runat="server">
			<input style="DISPLAY: none" id="HidEstAnulGrilla" name="HidEstAnulGrilla" runat="server">
			<input style="DISPLAY: none" id="hidIsVerFormPag" name="hidIsVerFormPag" runat="server">
			<input style="DISPLAY: none" id="hidNroTelefono" name="hidNroTelefono" runat="server">
			<input style="DISPLAY: none" id="hidAutorizacion" name="hidAutorizacion" runat="server">
			<input style="DISPLAY: none" id="HidDatoPosVisa" name="HidDatoPosVisa" runat="server">
			<input style="DISPLAY: none" id="HidDatoPosMC" name="HidDatoPosMC" runat="server">
			<input style="DISPLAY: none" id="HidIntAutPos" name="HidIntAutPos" runat="server">
			<input style="DISPLAY: none" id="HidFila0" name="HidFila0" runat="server">
			<input style="DISPLAY: none" id="HidFila1" name="HidFila1" runat="server">
			<input style="DISPLAY: none" id="HidFila2" name="HidFila2" runat="server">
			<input style="DISPLAY: none" id="HidFila3" name="HidFila3" runat="server">
			<input style="DISPLAY: none" id="HidFila4" name="HidFila4" runat="server">
			<input style="DISPLAY: none" id="HidContIntento0" name="HidContIntento0" runat="server">
			<input style="DISPLAY: none" id="HidContIntento1" name="HidContIntento1" runat="server">
			<input style="DISPLAY: none" id="HidContIntento2" name="HidContIntento2" runat="server">
			<input style="DISPLAY: none" id="HidContIntento3" name="HidContIntento3" runat="server">
			<input style="DISPLAY: none" id="HidContIntento4" name="HidContIntento4" runat="server">
			<input style="DISPLAY: none" id="HidIdCabez" name="HidIdCabez" runat="server">
			<input style="DISPLAY: none" id="HidDatoAuditPos" name="HidDatoAuditPos" runat="server">
			<input style="DISPLAY: none" id="hidAnulacionExitosa" name="hidAnulacionExitosa" runat="server">
			<input style="DISPLAY: none" id="hidAnulacionRechazada" name="hidAnulacionRechazada" runat="server">
			<input style="DISPLAY: none" id="hidAnulacionIncompleta" name="hidAnulacionIncompleta" runat="server">
			<input style="DISPLAY: none" id="hidCajeroPOS" name="hidCajeroPOS" runat="server">
			<input style="DISPLAY: none" id="hidUsuario" name="hidUsuario" runat="server">
			<input style="DISPLAY: none" id="hidMsjCajero" name="hidMsjCajero" runat="server">
			<input style="DISPLAY: none" id="hidMsjIpDesconfigurado" name="hidMsjIpDesconfigurado" runat="server">
			<input style="DISPLAY: none" id="hidIPTransaccion" name="hidIPTransaccion" runat="server">
			<input style="DISPLAY: none" id="hidIpLocal" name="hidIpLocal" runat="server">
			<input style="DISPLAY: none" id="HidTipMonPOSVISA" name="HidTipMonPOSVISA" runat="server">
			<!--PROY-27440 FIN-->
                        <!--PROY-31949 INICIO-->
			<input id="HidNumIntentosAnular" type="hidden" name="HidNumIntentosAnular" runat="server">
			<input id="HidMsjErrorNumIntentos" type="hidden" name="HidMsjErrorNumIntentos" runat="server">
			<input id="HidMsjErrorTimeOut" type="hidden" name="HidMsjErrorTimeOut" runat="server">
			<input id="HidMsjNumIntentosPago" type="hidden" name="HidMsjNumIntentosPago" runat="server">
			<input id="HidFlagCajaCerrada" type="hidden" name="HidFlagCajaCerrada" runat="server">
			<input id="HidMsjCajaCerrada" type="hidden" name="HidMsjCajaCerrada" runat="server">
			<input id="HidGuardarTrans" type="hidden" name="HidGuardarTrans" value="0" runat="server">
		        <!--PROY-31949 FIN-->
		</form>
	</body>
</HTML>
