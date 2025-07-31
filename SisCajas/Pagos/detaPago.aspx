<%@ Page Language="vb" AspCompat="true" AutoEventWireup="false" Codebehind="detaPago.aspx.vb" Inherits="SisCajas.detaPago" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Aplicativo PVU</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta content="no-cache" http-equiv="Pragma">
		<META content="Mon, 06 Jan 1990 00:00:01 GMT" http-equiv="Expires">
		<LINK rel="styleSheet" type="text/css" href="../estilos/est_General.css">
			<script language="JavaScript" src="../librerias/date-picker.js"></script>
			<script language="JavaScript" src="../librerias/Lib_FuncValidacion.js"></script>
			<!--PROY-27440 INI-->
			<script language="JavaScript" src="../librerias/msrsclient.js"></script>
			<script language="JavaScript" src="../librerias/jquery-1.8.2.min.js"></script>
			<script language="JavaScript" src="../Scripts/form.js"></script>
			<script language="JavaScript" src="../Scripts/xml2json.js"></script>
			<script language="JavaScript" src="../Scripts/operacionPOS.js"></script>
			<!--PROY-27440 FIN-->
			<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
			<script language="JavaScript">

                        //PROY-31949 - INICIO 
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
			$(document).on('click','#btnCargarDocumento',function(){ blnClosingMod = false; });
			
			//PROY-31949 - FIN	
<!--
	var gbFlag=0;
	
	//PROY-27440 INI
	var varArrayEstTrans;
	var serverURL =  '../Pos/ProcesoPOS.aspx';
	var webServiceURL = '<%= System.Configuration.ConfigurationSettings.AppSettings("consWsLocal_Url") %>';
	var timeOutWsLocal = '<%= System.Configuration.ConfigurationSettings.AppSettings("consWsLocal_Url_TimeOut") %>';
        //PROY-31949 - I
        var MaxNumIntentosPago; 
	var MaxNumIntentosAnular;
	var varNroTarjeta;
	var varNroRef;
	//PROY-31949 - F
	var sCodRespTarj;
	var tiposError = 'EXC_';
	
	var varRptTtipoPos;
	var varNameLinkPOS;
	var varNameImgPOS;
	var varNameLinkDelPOS;
	var varNameImgDelPOS;
	var varNameTipoPOS;
	var varTipTarjeta;
	var varNameDoc;
	var varMoneda;
	var varTransMonto;
	var varTipoPos;
	var varIntFila;
	var varValueTar;
	var varCont1 = 0;
	var varCont2 = 0;
	var varCont3 = 0;
	var varCont4 = 0;
	var varCont5 = 0;
	
	//Proy-31949-Inicio
	var ContPago1 = 1;
	var ContPago2 = 1;
	var ContPago3 = 1;
	var ContPago4 = 1;
	var ContPago5 = 1;
	
	var ContAnular1 = 1;
	var ContAnular2 = 1;
	var ContAnular3 = 1;
	var ContAnular4 = 1;
	var ContAnular5 = 1;	
	
	var varArrayCodOpe;
	var varArrayDesOpe;
	var varArrayCodTarjeta;
	var varArrayTipoPOS;
	var varArrayTipoTran;
	//Proy-31949-Fin
	
	
	var varTipoTrans;
	
	var varIntAutPos;
	//Mastercard
	var varTransMC;
	var varMonedaMC;
	var varApliMC;
	var varNroRefMC;
	var varMontoMC;
	var varPwdComercioMC = '';
	
	//VisaNet
	var varTransVisa;
	var varMonedaVisa;
	var varNroRefVisa;
	var varMontoVisa;
	
	var varTramaUpdateAux;
	var varTramaAuditAux;
	var varIdTransAux;
	var varCodOpe;
	var varTipoOpeFi;
	var varEstTran;
	var varBolGetTarjeta;
	var varBolWsLocal;
	
	var varNroRegistro;
	var varNroTienda;
	var varCodEstablec;
	var varCodigoCaja;
	var varNomPcPos;
	var varCodTerminal;
	var varIpPos;
	var varIntPos; //PROY-31949 
	var varIdRefAnu;
	/*CNH INI*/
	var varNameTxtDoc;
	/*CNH FIN*/	
	//PROY-27440 FIN
	
	window.onload = window_onload;
	
	//PROY-27440 INI
	function f_datos_POS()
	{
		varIntAutPos = document.getElementById("HidIntAutPos").value;
		
		if(varIntAutPos != '1'){
			return;
		}
		var suma;
		suma = 0.0;
		
		for (var i=1; i<=5; i ++)
		{
					
			if (document.getElementById('HidFila' + i).value.length >0){			
				var varArray = document.getElementById('HidFila' + i ).value.split("|");
				var varMonto = varArray[0].substr(varArray[0].indexOf("=")+1);
				var varTarjeta = varArray[1].substr(varArray[1].indexOf("=")+1);
				var varComboIndex = varArray[2].substr(varArray[2].indexOf("=")+1);
				
				document.getElementById('txtMonto' + i).value = varMonto;
				document.getElementById('txtDoc' + i).value = varTarjeta;
				document.getElementById('cboTipDocumento' + i).selectedIndex = varComboIndex;
				
				document.getElementById('txtMonto' + i).disabled = true;
				document.getElementById('txtDoc' + i).disabled = true;
				document.getElementById('cboTipDocumento' + i).disabled = true;
				
				var varNameLinkPOS="LnkPos" + i;
				var varNameImgPOS="icoTranPos" + i;
				var varNameLinkDelPOS="LnkDelPos" + i;
				var varNameImgDelPOS="icoDelPos" + i;
				var varNameTipoPOS = "HidTipPOS" + i;
				var vaNameLink = "IdLink" + i;
				var varNameDelFila ="ImgEliminar" + i;
				
				
				document.getElementById(vaNameLink).disabled = true;
				//document.getElementById(vaNameLink).style.visibility = "hidden";
				//document.getElementById(vaNameLink).href = "";
				
				document.getElementById(varNameDelFila).src = "../images/botones/ico_eliminar_ena.gif";
				document.getElementById(varNameDelFila).disabled = true;				
				
				document.getElementById(varNameLinkPOS).style.visibility = "visible";
				document.getElementById(varNameImgPOS).style.visibility = "visible";
		  
				
				document.getElementById(varNameLinkDelPOS).style.visibility = "visible";
				document.getElementById(varNameImgDelPOS).style.visibility = "visible";
		                //PROY-31949 
				//document.getElementById(varNameLinkPOS).href= "javascript:f_EnvioPOS(" + i + ",'Imprimir POS')";
				eval("document.getElementById('"+"LnkPos" + i +"').onclick = function(){blnClosingMod = false; f_EnvioPOS(" + i + ",'Imprimir POS');}"); //PROY-31949 
				//PROY-31949 
				document.getElementById(varNameImgPOS).src = "../images/print-icon.png";
				document.getElementById(varNameImgPOS).alt="Imprimir";
				
				suma=suma +(eval(varMonto)*1);
				
			}
		}	
		if(suma>0){
			//document.frmPrincipal.txtSaldo.value = Math.round((document.frmPrincipal.txtNeto.value*1 - suma)*100)/100;
		}
		BloqueoCancelar();
	}
	function CallBack_ObtenerTipoPos(response) 
	{
		varBolGetTarjeta = false;
	
		var varRpta = response.return_value;
		var res = (varRpta.replace("<BODY><SELECT>", "")).replace("</SELECT>", "").replace("\r\n","");
		
		varRpta = res;
				
		if(varRpta.substr(0,1) == '0'){
			var varArryRpta = varRpta.split('|');
			var varTipoPos = varArryRpta[1].substr(0,varArryRpta[1].indexOf('#'));				
			document.getElementById(varNameTipoPOS).value = varTipoPos;
			
			if(varTipoPos=='AMX' || varTipoPos=='MCD' || varTipoPos=='VIS' || varTipoPos=='DIN' ){
				varBolGetTarjeta = true;
			
				//"AMX"://AMERICAN EXPRESS //"MCD"://MASTER CARD		
				//"VIS"://VISA //"DIN"://DINERS
				
				document.getElementById(varNameLinkPOS).style.visibility = "visible";
				document.getElementById(varNameImgPOS).style.visibility = "visible";
				
				document.getElementById(varNameLinkDelPOS).style.visibility = "visible";
				document.getElementById(varNameImgDelPOS).style.visibility = "visible";
				/*validacion IAPOS CNH*/			
				if(varIntAutPos == "1"){
					document.getElementById(varNameTxtDoc).value = "";
					document.getElementById(varNameTxtDoc).disabled = true;
				}
			}
			else{				
				/*validacion IAPOS CNH*/							
				document.getElementById(varNameTxtDoc).value = "";
				document.getElementById(varNameTxtDoc).disabled = false;
				document.getElementById(varNameLinkPOS).style.visibility = "hidden";
				document.getElementById(varNameImgPOS).style.visibility = "hidden";				
				document.getElementById(varNameLinkDelPOS).style.visibility = "hidden";
				document.getElementById(varNameImgDelPOS).style.visibility = "hidden";
			}
		}
		else
		{
			
			/*validacion IAPOS CNH*/							
			document.getElementById(varNameTxtDoc).value = "";
			document.getElementById(varNameTxtDoc).disabled = false;
			document.getElementById(varNameLinkPOS).style.visibility = "hidden";
			document.getElementById(varNameImgPOS).style.visibility = "hidden";				
			document.getElementById(varNameLinkDelPOS).style.visibility = "hidden";
			document.getElementById(varNameImgDelPOS).style.visibility = "hidden";
		}		
	}
	function ObtenerTipoPosError(co) 
	{
		
		if (co.message) {
			alert("Context:" + co.context + "\nError: " + co.message);
		}
		varRptTtipoPos='1|Error';
		
		document.getElementById(varNameLinkPOS).style.visibility = "hidden";
		document.getElementById(varNameImgPOS).style.visibility = "hidden";				
		document.getElementById(varNameLinkDelPOS).style.visibility = "hidden";
		document.getElementById(varNameImgDelPOS).style.visibility = "hidden";
	 }
	function f_bloqueo_fila(objCombo,intFila)
	{
		varIntPos = 0;
		switch (intFila)
		{
			case 1:
				varNameLinkPOS="LnkPos1";varNameImgPOS="icoTranPos1";
				varNameLinkDelPOS="LnkDelPos1";varNameImgDelPOS="icoDelPos1";
				varNameTipoPOS = "HidTipPOS1";
				//CNH
				varNameTxtDoc = "txtDoc1";
				ContPago1 = 1; //PROY-31949 
			break;
			case 2:
				varNameLinkPOS="LnkPos2";varNameImgPOS="icoTranPos2";
				varNameLinkDelPOS="LnkDelPos2";varNameImgDelPOS="icoDelPos2";
				varNameTipoPOS = "HidTipPOS2";
				//CNH
				varNameTxtDoc = "txtDoc2";
				ContPago2 = 1; //PROY-31949 
			break;
			case 3:
				varNameLinkPOS="LnkPos3";varNameImgPOS="icoTranPos3";
				varNameLinkDelPOS="LnkDelPos3";varNameImgDelPOS="icoDelPos3";
				varNameTipoPOS = "HidTipPOS3";
				//CNH
				varNameTxtDoc = "txtDoc3";
				ContPago3 = 1; //PROY-31949 
			break;
			case 4:
				varNameLinkPOS="LnkPos4";varNameImgPOS="icoTranPos4";
				varNameLinkDelPOS="LnkDelPos4";varNameImgDelPOS="icoDelPos4";
				varNameTipoPOS = "HidTipPOS4";
				//CNH
				varNameTxtDoc = "txtDoc4";
				ContPago4 = 1; //PROY-31949 
			break;
			case 5:
				varNameLinkPOS="LnkPos5";varNameImgPOS="icoTranPos5";
				varNameLinkDelPOS="LnkDelPos5";varNameImgDelPOS="icoDelPos5";
				varNameTipoPOS = "HidTipPOS5";
				//CNH
				varNameTxtDoc = "txtDoc5";
				ContPago5 = 1; //PROY-31949 
			break;
		}
		
                //PROY-31949 - I

		document.getElementById(varNameLinkPOS).style.visibility = "hidden";
		document.getElementById(varNameImgPOS).style.visibility = "hidden";				
		document.getElementById(varNameLinkDelPOS).style.visibility = "hidden";
		document.getElementById(varNameImgDelPOS).style.visibility = "hidden";
		varBolGetTarjeta = false;
		document.getElementById(varNameTxtDoc).value = "";
		document.getElementById(varNameTxtDoc).disabled = false;	
				
		varIntAutPos = document.getElementById("HidIntAutPos").value;
	
		//CNH
		
		var TipTarjetaSel;

		f_ConsultaNC();
		
		if(varIntAutPos != '1'){			
			return;
		}
		
		if(document.getElementById("HidMedioPagoPermitidas").value.indexOf(objCombo.value)< 0)
		{				
			return;		
		}
		else
		{	var TarjetasPOS = document.getElementById("HidMedioPagoPermitidas").value.split("|");
			var varArrayDatoPosVisa = document.getElementById("HidDatoPosVisa").value.split("|");
			var varArrayDatoPosMC = document.getElementById("HidDatoPosMC").value.split("|");
			
			for(i=0; i<TarjetasPOS.length; i++)
			{
			 if (objCombo.value == TarjetasPOS[i].split(";")[0])
			 {
				TipTarjetaSel = TarjetasPOS[i].split(";")[1];
			 }
			}
			document.getElementById(varNameTipoPOS).value = TipTarjetaSel;
			
			switch (TipTarjetaSel)
			{
				case "VIS"://VISA
				    if (varArrayDatoPosVisa.length > 0)
					varIntPos = varArrayDatoPosVisa[8].substr(varArrayDatoPosVisa[8].indexOf("=")+1);				
					else
					varIntPos = 0;
					break;
				case "MCD"://MC	
					if (varArrayDatoPosMC.length > 0)		
					varIntPos = varArrayDatoPosMC[8].substr(varArrayDatoPosMC[8].indexOf("=")+1);				
					else
					varIntPos = 0;
					break;
				case "AMX":			
					if (varArrayDatoPosMC.length > 0)		
					varIntPos = varArrayDatoPosMC[8].substr(varArrayDatoPosMC[8].indexOf("=")+1);				
					else
					varIntPos = 0;
					break;
				case "DIN":			
					if (varArrayDatoPosMC.length > 0)		
					varIntPos = varArrayDatoPosMC[8].substr(varArrayDatoPosMC[8].indexOf("=")+1);				
					else
					varIntPos = 0;
					break;
			}
			 		
		}
		
		if(varIntPos != '1'){			
			return;
		}
		
		else
		{
		varBolGetTarjeta = true;
	        document.getElementById(varNameLinkPOS).style.visibility = "visible";
		document.getElementById(varNameImgPOS).style.visibility = "visible";			
		document.getElementById(varNameLinkDelPOS).style.visibility = "visible";
		document.getElementById(varNameImgDelPOS).style.visibility = "visible";
		document.getElementById(varNameTxtDoc).value = "";
		document.getElementById(varNameTxtDoc).disabled = true;		
		}
		//PROY-31949 - F
	
		//varRptTtipoPos='';  //PROY-31949 
		//RSExecute(serverURL,"ObtenerTipoPos",objCombo.value,document.getElementById("HidPtoVenta").value,CallBack_ObtenerTipoPos,ObtenerTipoPosError,"X"); //PROY-31949 
	}
	
	function f_activar_fila(intFila,bolEnable)
	{
		document.getElementById("lblEnvioPos").innerHTML  = "";
			
		document.getElementById("txtMonto" + intFila).disabled = bolEnable;
		//document.getElementById("txtDoc" + intFila).disabled = bolEnable;
		//CNH
		document.getElementById("txtDoc" + intFila).disabled = true;
		document.getElementById("cboTipDocumento" + intFila).disabled = bolEnable;
		
		var objMonto=document.getElementById("txtMonto" + intFila);
		var objTipo=document.getElementById("cboTipDocumento" + intFila);
		var objDoc=document.getElementById("txtDoc" + intFila);
		
		document.getElementById("icoTranPos" + intFila).src = "../images/send-icon.png";
		document.getElementById("icoTranPos" + intFila).alt = "Envio POS";
		//PROY-31949 
		//document.getElementById("LnkPos" + intFila).href="javascript:f_EnvioPOS(" + intFila + ",'Envio POS')"; //PROY-31949 
		eval("document.getElementById('"+"LnkPos" + intFila +"').onclick = function(){blnClosingMod = false; f_EnvioPOS(" + intFila + ",'Envio POS');}"); //PROY-31949 
		//PROY-31949 
		document.getElementById("IdLink" + intFila).disabled = bolEnable;
		
		document.getElementById("ImgEliminar" + intFila).src = "../images/botones/ico_eliminar.gif";
		document.getElementById("ImgEliminar" + intFila).disabled = true;
		
		document.getElementById("icoDelPos" + intFila).src = "../images/delete-icon.png";
	}
		
	function f_EnvioPOS(intFila,TagOpcion)
	{
		varTransMC = ''; varMonedaMC = '';
	  varApliMC = ''; varNroRefMC = ''; 
	  varNroRefVisa = ''; varIdRefAnu ='';
	  varNroRef = '';
	  //PROY-31949 
	  MaxNumIntentosPago = Number(document.getElementById("HidNumIntentosPago").value);//PROY-31949 
	  MaxNumIntentosAnular = Number(document.getElementById("HidNumIntentosAnular").value);//PROY-31949 
	  
	  if(document.getElementById("HidFlagCajaCerrada").value == '1')
	  {	  
		alert(document.getElementById("HidMsjCajaCerrada").value);
		return;
	  }	  
	  
	    //PROY-31949 
	  varTransVisa = '';
	  varMonedaVisa = '';
	
	
		var varTramaInsert='';
	
		varCodOpe = '';
		var varDescriOpe='';
		varTipoTrans = '';
		var TipTarjeta='';
		var varCodPtaWS='';
		
	        //PROY-31949 	
	        varArrayCodOpe=document.getElementById("HidCodOpera").value.split("|");
	        varArrayDesOpe=document.getElementById("HidDesOpera").value.split("|");
	        varArrayCodTarjeta=document.getElementById("HidTipoTarjeta").value.split("|");
	        varArrayTipoPOS=document.getElementById("HidTipoPOS").value.split("|");
	        varArrayTipoTran=document.getElementById("HidTipoTran").value.split("|");
	        //PROY-31949 	
		varArrayEstTrans=document.getElementById("HidEstTrans").value.split("|");	
		
		
		//if(varNameTipoPOS == null || varNameTipoPOS == '') 
		varNameTipoPOS = "HidTipPOS" + intFila;
		
		varValueTar = '';
		varValueTar = document.getElementById(varNameTipoPOS).value;
		
		varApliMC = '';
		varApliMC = document.getElementById("HidApliPOS").value;
		
		varNroRefMC = '';
		varNroRefVisa = '';
		
		varPwdComercioMC = '';
		
		switch (TagOpcion)
		{
			case "Envio POS":
				varCodOpe = varArrayCodOpe[0]; //Venta
				varDescriOpe = varArrayDesOpe[0];
				varTipoTrans= varArrayTipoTran[0];//PAGO
				
				if(varValueTar == 'VIS')
				{
					var varArrayMonedaVisa = document.getElementById("HidMonedaVisa").value.split("|");					
					varMonedaVisa = varArrayMonedaVisa[0];//SOLES VISA
				}
				else
				{
					var varArrayTranMC = document.getElementById("HidTransMC").value.split("|");
					varTransMC = varArrayTranMC[0];//01					
					var varArrayMonedaMC = document.getElementById("HidMonedaMC").value.split("|");					
					varMonedaMC = varArrayMonedaMC[0];//SOLES MC
				}
				break;
			case "Eliminar POS":
				varCodOpe = varArrayCodOpe[2];//Anulacion
				varDescriOpe = varArrayDesOpe[2];
				varTipoTrans= varArrayTipoTran[1] ;//ANULACION DE PAGO
                                //PROY-31949 
				varNroTarjeta = document.getElementById("txtDoc" + intFila).value; 				
				eval("IntentosAnular=ContAnular"+intFila); 
                                //PROY-31949 
				
				if(varValueTar == 'VIS')
				{
					var varArrayMonedaVisa = document.getElementById("HidMonedaVisa").value.split("|");
					varMonedaVisa = varArrayMonedaVisa[0];//SOLES VISA
					
					if(document.getElementById("HidFila" + intFila).value == ''){
                                                //PROY-31949 
					        if(IntentosAnular <= MaxNumIntentosAnular) 
						{
						alert('No tiene numero de referencia para eliminar la transaccion');
						}
                                                //PROY-31949 
						
						return;
					}
					else{
						var varArrayFila = document.getElementById("HidFila" + intFila).value.split("|");
						varNroRefVisa = varArrayFila[3].substr(varArrayFila[3].indexOf("=")+1);
                                                //PROY-31949 
						varNroRef = varNroRefVisa; 
						if(IntentosAnular <= MaxNumIntentosAnular)
						{	
						alert('Nro de referencia: ' + varNroRefVisa);
					}
						//PROY-31949 
					     }
				}
				else
				{
					var varArrayTranMC = document.getElementById("HidTransMC").value.split("|");
					varPwdComercioMC = '';					
					varPwdComercioMC = varArrayTranMC[6]
				
					var varArrayTranMC = document.getElementById("HidTransMC").value.split("|");
					varTransMC = varArrayTranMC[1];//06
					varMonedaMC = '';
					
					if(document.getElementById("HidFila" + intFila).value == ''){
                                                //PROY-31949 
					        if(IntentosAnular <=  MaxNumIntentosAnular)
						{	
						alert('No tiene numero de referencia para eliminar la transaccion');
						}
                                                //PROY-31949 
						return;
					}
					else{
						var varArrayFila = document.getElementById("HidFila" + intFila).value.split("|");
						varNroRefMC = varArrayFila[3].substr(varArrayFila[3].indexOf("=")+1);
						varNroRef = varNroRefMC; //PROY-31949 
						
					}
				}				
				break;
			case "Imprimir POS":
				varCodOpe = varArrayCodOpe[0];//Venta
				varDescriOpe = varArrayDesOpe[0];
				varTipoTrans= varArrayTipoTran[2];//REEIMPRESION DE VOUCHER
				varNroTarjeta = document.getElementById("txtDoc" + intFila).value; //PROY-31949 
				
				if(varValueTar == 'VIS')
				{
				    var varArrayFila = document.getElementById("HidFila" + intFila).value.split("|"); //PROY-31949 
					var varArrayMonedaVisa = document.getElementById("HidMonedaVisa").value.split("|");
					varMonedaVisa = varArrayMonedaVisa[0];//SOLES VISA
					varNroRef = varArrayFila[3].substr(varArrayFila[3].indexOf("=")+1); //PROY-31949 
				}
				else
				{
					var varArrayTranMC = document.getElementById("HidTransMC").value.split("|");
					varTransMC = varArrayTranMC[4];//11
					varMonedaMC = '';
					
					if(document.getElementById("HidFila" + intFila).value == ''){
						alert('No tiene numero de referencia para re-imprimir la transaccion');
						return;
					}
					else{
						var varArrayFila = document.getElementById("HidFila" + intFila).value.split("|");
						varNroRefMC = varArrayFila[3].substr(varArrayFila[3].indexOf("=")+1);
						varNroRef = varNroRefMC; //PROY-31949 
					}
				}				
				break;
		}
		
		var varNameLinkDelPOS;
		var varNameDelFila; var varNameTipoDoc; var varNameMonto;
		var vaNameLink;
		
		varNameTipoPOS = '';
		varNameDoc = ''
		
		switch (intFila)
		{
			case 1:
				varNameLinkPOS="LnkPos1";varNameImgPOS="icoTranPos1";
				varNameLinkDelPOS="LnkDelPos1";varNameImgDelPOS="icoDelPos1";
				varNameDelFila="ImgEliminar1";
				varNameTipoDoc="cboTipDocumento1";
				varNameDoc="txtDoc1";
				varNameMonto="txtMonto1";
				vaNameLink="IdLink1";
				varNameTipoPOS = "HidTipPOS1";
			break;
			case 2:
				varNameLinkPOS="LnkPos2";varNameImgPOS="icoTranPos2";
				varNameLinkDelPOS="LnkDelPos2";varNameImgDelPOS="icoDelPos2";
				varNameDelFila="ImgEliminar2";
				varNameTipoDoc="cboTipDocumento2";
				varNameDoc="txtDoc2";
				varNameMonto="txtMonto2";
				vaNameLink="IdLink2";
				varNameTipoPOS = "HidTipPOS2";
			break;
			case 3:
				varNameLinkPOS="LnkPos3";varNameImgPOS="icoTranPos3";
				varNameLinkDelPOS="LnkDelPos3";varNameImgDelPOS="icoDelPos3";
				varNameDelFila="ImgEliminar3";
				varNameTipoDoc="cboTipDocumento3";
				varNameDoc="txtDoc3";
				varNameMonto="txtMonto3";
				vaNameLink="IdLink3";
				varNameTipoPOS = "HidTipPOS3";
			break;
			case 4:
				varNameLinkPOS="LnkPos4";varNameImgPOS="icoTranPos4";
				varNameLinkDelPOS="LnkDelPos4";varNameImgDelPOS="icoDelPos4";
				varNameDelFila="ImgEliminar4";
				varNameTipoDoc="cboTipDocumento4";
				varNameDoc="txtDoc4";
				varNameMonto="txtMonto4";
				vaNameLink="IdLink4";
				varNameTipoPOS = "HidTipPOS4";
			break;
			case 5:
				varNameLinkPOS="LnkPos5";varNameImgPOS="icoTranPos5";
				varNameLinkDelPOS="LnkDelPos5";varNameImgDelPOS="icoDelPos5";
				varNameDelFila="ImgEliminar5";
				varNameTipoDoc="cboTipDocumento5";
				varNameDoc="txtDoc5";
				varNameMonto="txtMonto5";
				vaNameLink="IdLink5";
				varNameTipoPOS = "HidTipPOS5";
			break;
		}
		
		if(document.getElementById(varNameMonto).value ==''){
			alert('Debe ingresar un monto de pago');
			document.getElementById(varNameMonto).focus();
			return;
		}
		
		if(Number(document.getElementById(varNameMonto).value)==0){
			alert('Debe ingresar un monto mayor a cero.');
			document.getElementById(varNameMonto).focus();
			return;
		}
		
		document.getElementById("lblEnvioPos").innerHTML  = "Enviando al POS........";
		document.getElementById(varNameTipoDoc).disabled = true;
		document.getElementById(varNameDoc).disabled = true;
		document.getElementById(varNameMonto).disabled = true;
		document.getElementById(vaNameLink).disabled = true;
		//document.getElementById(vaNameLink).href = "javascript:void(0)";
		
		switch (TagOpcion)
		{
			case "Envio POS":
				document.getElementById(varNameLinkPOS).href = "javascript:void(0)";
				document.getElementById(varNameImgPOS).src = "../images/send-icon_ena.png";				
				document.getElementById(varNameLinkPOS).href = "javascript:void(0)";
				document.getElementById(varNameImgDelPOS).src = "../images/delete-icon_ena.png";
				break;
			case "Eliminar POS":				
				document.getElementById(varNameLinkPOS).href = "javascript:void(0)";
				document.getElementById(varNameImgPOS).src = "../images/print-icon_ena.png";
				document.getElementById(varNameLinkPOS).href = "javascript:void(0)";
				document.getElementById(varNameImgDelPOS).src = "../images/delete-icon_ena.png";				
				break;
			case "Imprimir POS":
				document.getElementById(varNameLinkPOS).href = "javascript:void(0)";
				document.getElementById(varNameImgPOS).src = "../images/print-icon_ena.png";
				document.getElementById(varNameLinkPOS).href = "javascript:void(0)";
				document.getElementById(varNameImgDelPOS).src = "../images/delete-icon_ena.png";				
				break;
		}
		
		document.getElementById(varNameDelFila).src = "../images/botones/ico_eliminar_ena.gif";
		document.getElementById(varNameDelFila).disabled = true;		
		
		
		
		var EntitySaveTransac;
		var EntityUpdateTransac;
		var soapMSG
		varTipoPos='';
		
		var varArrayDatoPosVisa = document.getElementById("HidDatoPosVisa").value.split("|");
		var varArrayDatoPosMC = document.getElementById("HidDatoPosMC").value.split("|");
		
		
		varNroRegistro = ''; varNroTienda = '';
		varCodigoCaja = ''; varCodEstablec = '';
		varNomPcPos = ''; varCodTerminal = '';
		varIpPos = '';
		
		
		switch (varValueTar)
		{
			case "VIS"://VISA
				varTipoPos= varArrayTipoPOS[0];
				varTipTarjeta = varArrayCodTarjeta[0];//VISA
				
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
		
		var varArrayOpeFi = document.getElementById("HidTipoOpera").value.split("|");
		
		if (varTipoTrans == '3'){
			varTipoOpeFi = varArrayOpeFi[1];
		}
		else{
			varTipoOpeFi = varArrayOpeFi[0];
		}
		
		
		
		
		var varNroPedido = '';
		var varNroTelefono = '';
		
		varNroPedido = document.getElementById("hidDocSap").value; 
		varNroTelefono = document.getElementById("hidNumeroTelefono").value; 
		
		
		varTransMonto= '';
		varTransMonto = document.getElementById(varNameMonto).value;
		
		
		
		varMoneda='';
		varMoneda = document.getElementById("HidTipoMoneda").value;
		var varTipoPago = document.getElementById("HidTipoPago").value; //DOCUMENTOS POR PAGAR
		var varEstadoTrans= varArrayEstTrans[0];//PENDIENTE
		
		//var varNroTarjeta = ''; //PROY-31949 
		//var varNroRef = '';//PROY-31949 
		var varIdCabecera = document.getElementById("HidIdCabez").value;		
		
		varTramaInsert = '';
		varTramaInsert = 'codOperacion=' + varCodOpe + '|desOperacion=' + varDescriOpe + 
		'|tipoOperacion=' + varTipoOpeFi + '|montoOperacion=' + varTransMonto + 
		'|monedaOperacion=' + varMoneda + '|tipoTarjeta=' + varTipTarjeta + 
		'|tipoPago=' + varTipoPago + '|estadoTransaccion=' + varEstadoTrans + 
		'|tipoPos=' + varTipoPos + '|tipoTransaccion=' + varTipoTrans + '|ipCaja=' + varIpPos + 
		'|NroRegistro=' + varNroRegistro + '|NroTienda=' + varNroTienda + '|CodigoCaja=' + varCodigoCaja +
		'|CodEstablec=' + varCodEstablec + '|NomPcPos=' + varNomPcPos + '|CodTerminal=' + varCodTerminal +
		'|IdCabecera=' + varIdCabecera +
		'|nroTarjeta=' + varNroTarjeta + 
		'|nroRef=' + varNroRef;		
			
		varIntFila = 0;
		varIntFila = intFila;		
		
		//1 - PENDIENTE	
		RSExecute(serverURL,"GuardarTransaction",varTramaInsert,
		varNroTelefono,varNroPedido,CallBack_GuardarTransaction,GuardarTransactionError,"X");
	}
	//PROY-31949 
	function CallBack_ActTransMCD(response) 
	{
		var varRpta = response.return_value;
		var res = (varRpta.replace("<BODY><SELECT>", "")).replace("</SELECT>", "").replace("\r\n","");
		varRpta = res;
		var varArrayRpta = varRpta.split("|");
		var varCodRespuesta = varArrayRpta[2];
		var varRespuesta = varArrayRpta[3];
		if (varCodRespuesta == "0")
		{}
		else if (varCodRespuesta == "1")
		{
			document.getElementById("cboTipDocumento"+ varIntFila).value = varRespuesta.split(";")[0] ; 
		        varNameTipoPOS = "HidTipPOS" + varIntFila;
				
                        document.getElementById(varNameTipoPOS).value = varRespuesta.split(";")[1];		
	
			document.getElementById("HidFila" + varIntFila).value = 'Monto=' + varMontoOperacion + 
			'|Tarjeta=' + varNroTarjeta + 
			'|ComboIndex=' + document.getElementById("cboTipDocumento" + varIntFila).selectedIndex + 
			'|NroReferncia=' + varNroRefVisa;	
		}
		else if (varCodRespuesta == "2")
		{
			alert(varRespuesta);
		}
	}
	//PROY-31949 
	function CallBack_ActualizarTransaction1(response) 
	{
		var varRpta = response.return_value;
		var res = (varRpta.replace("<BODY><SELECT>", "")).replace("</SELECT>", "").replace("\r\n","");
		varRpta = res;
	}
	function CallBack_ActualizarTransaction2(response) 
	{
		var varRpta = response.return_value;
		var res = (varRpta.replace("<BODY><SELECT>", "")).replace("</SELECT>", "").replace("\r\n","");
		varRpta = res;
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
		
			//var varIdTran = varRpta.substr(varRpta.lastIndexOf("|")+1);
						
			//2 - EN PROCESO
			var varNumVoucher = '';
			var varNumAutTransaccion = '';
			var varCodRespTransaccion = '';
			var varDescTransaccion = '';
			var varCodAprobTransaccion = '';
			//var varNroTarjeta='';//PROY-31949 
			var varFechaExpiracion = '';
			var varNomCliente = '';
			var varImpVoucher = '';
			var varSeriePOS = varCodTerminal;
			var nomEquipoPOS = varNomPcPos;
			var varNroPedido = document.getElementById("hidDocSap").value; 
			var varIdUnicoTrans = '';
			
			varEstadoTrans = '';
			varEstadoTrans = varArrayEstTrans[1];//EN PROCESO
			
			
			varTramaUpdate = '';
			varTramaUpdate = 'monedaOperacion=' + varMoneda + '|montoOperacion=' + varTransMonto + 
			'|nroRegistro=' + varNroRegistro + '|numVoucher=' + varNumVoucher + 
			'|numAutTransaccion=' + varNumAutTransaccion + '|codRespTransaccion=' + varCodRespTransaccion + 
			'|descTransaccion=' + varDescTransaccion + '|codAprobTransaccion=' +  varCodAprobTransaccion + 
			'|tipoPos=' + varTipoPos + '|varNroTarjeta=' + varNroTarjeta + 
			'|fechaExpiracion=' + varFechaExpiracion + '|nomCliente=' + varNomCliente + 
			'|impVoucher=' + varImpVoucher + '|seriePOS=' + varSeriePOS + 
			'|nomEquipoPOS=' + nomEquipoPOS + '|estadoTransaccion=' + varEstadoTrans +
			'|NroTienda=' + varNroTienda + '|CodigoCaja=' + varCodigoCaja + 
			'|CodEstablec=' + varCodEstablec + '|IpPos=' + varIpPos + '|IdCabez=' + varIdCabez +
			'|FlagPago=' + varFlagPago + '|Pedido=' + varNroPedido + '|IdUnico=' + varIdUnicoTrans +
			'|TipoTrans=' + varTipoTrans + '|IdRefAnulador=' + varIdRefAnu +
			'|TipoPago=' + document.getElementById("HidTipoPago").value +
			'|ResTarjetaPos='; //PROY-31949 
			
			
			
			
			RSExecute(serverURL,"ActualizarTransaction",varTramaUpdate,
			varIdTran,CallBack_ActualizarTransaction1,GuardarTransactionError,"X");
			
			
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
				
                        //PROY-31949 - INICIO	
        
			eval("IntentosPagar=ContPago"+varIntFila);
			eval("IntentosAnular=ContAnular"+varIntFila);
	        
	                 
			if(varTipoTrans == varArrayTipoTran[0]) // Pago
			{
				if(IntentosPagar <= MaxNumIntentosPago)
				{
					if (varValueTar == 'VIS')
					{//VISA
				CallService(varTipTarjeta,varNameDoc,objEntityPOS);
			}
					else if(varValueTar=='AMX' || varValueTar=='MCD' || varValueTar=='DIN')
					{//MASTERCARD
				CallService(varTipTarjeta,varNameDoc,objEntityPOS);
			}
		}
				else
				{
					if (varValueTar == 'VIS')
					{//VISA
						ErrorVisaNet('',varNameDoc,objEntityPOS);
					}
					else if(varValueTar=='AMX' || varValueTar=='MCD' || varValueTar=='DIN')
					{//MASTERCARD
						ErrorMasterCard('',varNameDoc,objEntityPOS);
					}						
				}
			} //Fin Pago 1
			else if(varTipoTrans == varArrayTipoTran[1]) //Anulacion
			{
				if(IntentosAnular <= MaxNumIntentosAnular)
				{
					if (varValueTar == 'VIS')
					{//VISA
				CallService(varTipTarjeta,varNameDoc,objEntityPOS);
			}
					else if(varValueTar=='AMX' || varValueTar=='MCD' || varValueTar=='DIN')
					{//MASTERCARD
					CallService(varTipTarjeta,varNameDoc,objEntityPOS);
					}
				}
				else
				{
					if (varValueTar == 'VIS')
					{//VISA
					ErrorVisaNet('',varNameDoc,objEntityPOS);
					}
					else if(varValueTar=='AMX' || varValueTar=='MCD' || varValueTar=='DIN')
					{//MASTERCARD
					ErrorMasterCard('',varNameDoc,objEntityPOS);
					}						
				}	
			}//Fin Anular
			else // Otras Operaciones
			{
				if (varValueTar == 'VIS')
				{//VISA
					CallService(varTipTarjeta,varNameDoc,objEntityPOS);
				}
				else if(varValueTar=='AMX' || varValueTar=='MCD' || varValueTar=='DIN')
				{//MASTERCARD
				CallService(varTipTarjeta,varNameDoc,objEntityPOS);
			}
		}
			//PROY-31949 - FIN	
					
		}
		else {
			alert('Error al registrar la transaccion en estado PENDIENTE');
			f_activar_fila(varIntFila,false);
			return;
		}
	}
	function GuardarTransactionError(co) 
	{
		
		if (co.message) {
			alert("Context:" + co.context + "\nError: " + co.message);
		}
		varRptTtipoPos='1|Error';		
		document.getElementById(varNameLinkPOS).style.visibility = "hidden";
		document.getElementById(varNameImgPOS).style.visibility = "hidden";				
		document.getElementById(varNameLinkDelPOS).style.visibility = "hidden";
		document.getElementById(varNameImgDelPOS).style.visibility = "hidden";
	}
	function BloqueoCancelar()
	{
		try{
		    
		    var contPagosPos = 0;
		    
			for (var i=1; i<=5; i ++){
				if(document.getElementById('HidFila' + i).value.length >0){
					
					contPagosPos = contPagosPos + 1;
					
				}
			}
			
			if (contPagosPos == 0)
			{
				document.getElementById("btnCancelar").disabled = false;
				blnClosingMod = false;//PROY-31949 
			}
			else
			{
					document.getElementById("btnCancelar").disabled = true;
				blnClosingMod = true;//PROY-31949 
			}
			
		}
		catch(err){
			
		}	
	}
	function ErrorMasterCard(request,NameDoc,objEntityPOS)
	{
		try {
			
			
			var varClienteVisa = '';
      var varNumAutTransaccion = '';
      var varCodOperVisa = '';
      var varImpVoucher = '';      
      
			var varNroPedido = document.getElementById("hidDocSap").value; 
			var varIdUnicoTrans = '';
			var varIdCabez = document.getElementById("HidIdCabez").value;
			var varFlagPago = '1';
			
			varMontoOperacion = objEntityPOS.montoOperacion;
      varNomEquipoPOS = varNomPcPos;
                        //varNroTarjeta = document.getElementById(varNameDoc).value;//PROY-31949 
      
      varNroReferencia = '';
			varNroReferencia = varNroRefVisa;
			varNumAutTransaccion = '';
			varFechaExpiracion = '';
			varCodOperVisa = '';
			varSeriePOS = '';
			varImpVoucher = '';
			sCodRespTarj = '';
			
                        //Proy-31949-Inicio
			varDesRpta = document.getElementById("HidMsjErrorTimeOut").value;
			varEstTran = varArrayEstTrans[3]; 
			
			eval("IntentosPagar=ContPago"+varIntFila);
			eval("IntentosAnular=ContAnular"+varIntFila);
			
			if(varTipoTrans == varArrayTipoTran[1])
			{ 
				if(IntentosAnular > MaxNumIntentosAnular)
			{ 
				varEstTran = varArrayEstTrans[4];//INCONPLETO
					varDesRpta = document.getElementById("HidMsjErrorNumIntentos").value;					
					eval("ContAnular"+varIntFila+"=1");
					eval("document.frmPrincipal.HidFila"+varIntFila+".value=''")					
					BloqueoCancelar();	
			}
				else
				{
				varEstTran = varArrayEstTrans[3];//RECHAZADO
					varDesRpta = document.getElementById("HidMsjErrorTimeOut").value;					
			}			
			}			
			
			//Proy-31949-Fin
			
			varTramaUpdate = '';
			varTramaUpdate = 'monedaOperacion=' + varMoneda + '|montoOperacion=' + varMontoOperacion + 
			'|nroRegistro=' + varNroRegistro + 
			'|numVoucher=' + varNroRef + //PROY-31949 
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
			'|TipoPago=' + document.getElementById("HidTipoPago").value+
			'|ResTarjetaPos='; //PROY-31949 	
			
			RSExecute(serverURL,"ActualizarTransaction",varTramaUpdate,
						objEntityPOS.TrnsnId,CallBack_ActualizarTransaction2,GuardarTransactionError,"X");
						
			if(varEstTran == '4' && varCodOpe == '01' &&  varTipoTrans == '1')
			{
				f_activar_fila(varIntFila,false);
			}
			else{
				document.getElementById("lblEnvioPos").innerHTML  = "";
                                //Proy-31949-Inicio
				//document.getElementById(varNameLinkPOS).href = "javascript:f_EnvioPOS(" + varIntFila + ",'Imprimir POS')";
				eval("document.getElementById('"+ varNameLinkPOS +"').onclick = function(){blnClosingMod = false; f_EnvioPOS(" + varIntFila + ",'Imprimir POS');}");
				//Proy-31949-FIN
				document.getElementById(varNameImgPOS).src = "../images/print-icon.png";
				document.getElementById(varNameImgDelPOS).src = "../images/delete-icon.png";
				document.getElementById(varNameImgPOS).alt="Imprimir";
			}
			//PROY-31949 
			if(varEstTran == varArrayEstTrans[4] && varTipoTrans == varArrayTipoTran[1])
			{
				document.getElementById("txtDoc" + varIntFila).value = '';
				document.getElementById("HidFila" + varIntFila).value = '';
				f_activar_fila(varIntFila,false); //PROY-31949
				document.getElementById("icoTranPos" + varIntFila).src = "../images/send-icon.png";
				document.getElementById("icoTranPos" + varIntFila).alt = "Envio POS";
				//document.getElementById("LnkPos" + varIntFila).href = "javascript:f_EnvioPOS(" + varIntFila + ",'Envio POS')";
				eval("document.getElementById('"+ "LnkPos" + varIntFila +"').onclick = function(){blnClosingMod = false; f_EnvioPOS(" + varIntFila + ",'Envio POS');}");
				document.getElementById("HidFila" + varIntFila).value = '';
				varNroTarjeta = '';
				//CNH
				document.getElementById("txtDoc" + varIntFila).disabled = true;
			}
			//PROY-31949 
			var varIndex = NameDoc.substr(NameDoc.length -1,1);
			var varNomCliente = document.getElementById("txtNomCliente").value;
			var varNroTelefono = document.getElementById("hidNumeroTelefono").value; 
			var varNroPedido = document.getElementById("hidDocSap").value; 
			
			var varTramaAudit = '';    
			varTramaAudit = 'NomCliente=' + varNomCliente + 
			'|NroTelefono=' + varNroTelefono + 
			'|NroPedido=' + varNroPedido + '|IdTransaccion=' + 
			objEntityPOS.TrnsnId + '|nMonto=' + varMontoOperacion;			
			varIdTransAux = objEntityPOS.TrnsnId;			
			
			//Proy-31949-Inicio			
						
			if(varTipoTrans == "2" && varEstTran == varArrayEstTrans[3])
			{			
				if(IntentosAnular >= MaxNumIntentosAnular)
			{
							varTramaAuditAux = varTramaAudit;
							RSExecute(serverURL,"GuardarAutorizacion",varTramaAudit,CallBack_GuardarAutorizacion,GuardarAutorizacionError,"X");
					eval("ContAnular"+varIntFila+"=ContAnular"+varIntFila+"+1");
							return;
						}	
				eval("ContAnular"+varIntFila+"=ContAnular"+varIntFila+"+1");
					}
			
			if(varTipoTrans == "1" && varEstTran == varArrayEstTrans[3])
			{				
				if(IntentosPagar >= MaxNumIntentosPago)
				{
				alert(document.getElementById("HidMsjNumIntentosPago").value);
				
				eval("document.getElementById('txtDoc'+"+varIntFila+").disabled = false");
				eval("document.getElementById('LnkPos'+"+varIntFila+").style.visibility = 'hidden'");	
				eval("document.getElementById('LnkDelPos'+"+varIntFila+").style.visibility = 'hidden'");
				eval("document.getElementById('icoTranPos'+"+varIntFila+").style.visibility = 'hidden'");	
				eval("document.getElementById('icoDelPos'+"+varIntFila+").style.visibility = 'hidden'");	
		
					}
				eval("ContPago"+varIntFila+"=ContPago"+varIntFila+"+1");			
			}			
			//Proy-31949-Fin	
			
		}
		catch(err){
			alert(err.description);
			f_activar_fila(varIntFila,false);//Proy-31949-Inicio
		}	
	}
	function ErrorVisaNet(request,NameDoc,objEntityPOS)
	{
		try {
			
			
			
			var varClienteVisa = '';
      var varNumAutTransaccion = '';
      var varCodOperVisa = '';
      var varImpVoucher = '';      
      
			var varNroPedido = document.getElementById("hidDocSap").value; 
			var varIdUnicoTrans = '';
			var varIdCabez = document.getElementById("HidIdCabez").value;
			var varFlagPago = '1';

      
      
      varMontoOperacion = objEntityPOS.montoOperacion;
      varNomEquipoPOS = varNomPcPos;
      varNroTarjeta = document.getElementById(varNameDoc).value;
      
      varNroReferencia = '';
			varNroReferencia = varNroRefVisa;
			varNumAutTransaccion = '';
			varFechaExpiracion = '';
			varCodOperVisa = '';
			varSeriePOS = '';
			varImpVoucher = '';
			sCodRespTarj = '';
			
                        //Proy-31949-Inicio
			varDesRpta = document.getElementById("HidMsjErrorTimeOut").value;
			varEstTran = varArrayEstTrans[3]; 
						
			eval("IntentosPagar=ContPago"+varIntFila);
			eval("IntentosAnular=ContAnular"+varIntFila);
			
			if(varTipoTrans == varArrayTipoTran[1])
			{ 
				if(IntentosAnular > MaxNumIntentosAnular)
			{ 
				varEstTran = varArrayEstTrans[4];//INCONPLETO
					varDesRpta = document.getElementById("HidMsjErrorNumIntentos").value;					
					eval("ContAnular"+varIntFila+"=1");
					eval("document.frmPrincipal.HidFila"+varIntFila+".value=''")					
					BloqueoCancelar();	
			}
				else
				{
				varEstTran = varArrayEstTrans[3];//RECHAZADO
					varDesRpta = document.getElementById("HidMsjErrorTimeOut").value;					
			}			
			}			
			
			//Proy-31949-Fin		
			
			
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
			'|TipoPago=' + document.getElementById("HidTipoPago").value+
			'|ResTarjetaPos='; //Proy-31949-Inicio		
			
				
			RSExecute(serverURL,"ActualizarTransaction",varTramaUpdate,
						objEntityPOS.TrnsnId,CallBack_ActualizarTransaction2,GuardarTransactionError,"X");
						
			if(varEstTran == '4' && varCodOpe == '01' &&  varTipoTrans == '1')
			{
				f_activar_fila(varIntFila,false);
			}
			else{
				document.getElementById("lblEnvioPos").innerHTML  = "";
                                //Proy-31949-Inicio
				//document.getElementById(varNameLinkPOS).href = "javascript:f_EnvioPOS(" + varIntFila + ",'Imprimir POS')";
				eval("document.getElementById('"+ varNameLinkPOS +"').onclick = function(){blnClosingMod = false; f_EnvioPOS(" + varIntFila + ",'Imprimir POS');}");
				//Proy-31949-Fin
				document.getElementById(varNameImgPOS).src = "../images/print-icon.png";
				document.getElementById(varNameImgDelPOS).src = "../images/delete-icon.png";
				document.getElementById(varNameImgPOS).alt="Imprimir";
			}
			//Proy-31949-Inicio
			if(varEstTran == varArrayEstTrans[4] && varTipoTrans == varArrayTipoTran[1])
			{
				document.getElementById("txtDoc" + varIntFila).value = '';
				document.getElementById("HidFila" + varIntFila).value = '';
				f_activar_fila(varIntFila,false); //PROY-31949
				document.getElementById("icoTranPos" + varIntFila).src = "../images/send-icon.png";
				document.getElementById("icoTranPos" + varIntFila).alt = "Envio POS";
				//document.getElementById("LnkPos" + varIntFila).href = "javascript:f_EnvioPOS(" + varIntFila + ",'Envio POS')";
				eval("document.getElementById('"+ "LnkPos" + varIntFila +"').onclick = function(){blnClosingMod = false; f_EnvioPOS(" + varIntFila + ",'Envio POS');}");
				document.getElementById("HidFila" + varIntFila).value = '';
				varNroTarjeta = '';
				//CNH
				document.getElementById("txtDoc" + varIntFila).disabled = true;
			}
			//Proy-31949-FIN
			var varIndex = NameDoc.substr(NameDoc.length -1,1);
			var varNomCliente = document.getElementById("txtNomCliente").value;
			var varNroTelefono = document.getElementById("hidNumeroTelefono").value; 
			var varNroPedido = document.getElementById("hidDocSap").value; 
			
			var varTramaAudit = '';    
			varTramaAudit = 'NomCliente=' + varNomCliente + 
			'|NroTelefono=' + varNroTelefono + 
			'|NroPedido=' + varNroPedido + '|IdTransaccion=' + 
			objEntityPOS.TrnsnId + '|nMonto=' + varMontoOperacion;			
			varIdTransAux = objEntityPOS.TrnsnId;			
			
		//Proy-31949-Inicio			
						
			if(varTipoTrans == "2" && varEstTran == varArrayEstTrans[3])
			{			
				if(IntentosAnular >= MaxNumIntentosAnular)
			{
							varTramaAuditAux = varTramaAudit;
							RSExecute(serverURL,"GuardarAutorizacion",varTramaAudit,CallBack_GuardarAutorizacion,GuardarAutorizacionError,"X");
					eval("ContAnular"+varIntFila+"=ContAnular"+varIntFila+"+1");
							return;
						}	
				eval("ContAnular"+varIntFila+"=ContAnular"+varIntFila+"+1");
					}
			
			if(varTipoTrans == "1" && varEstTran == varArrayEstTrans[3])
			{				
				if(IntentosPagar >= MaxNumIntentosPago)
				{
				alert(document.getElementById("HidMsjNumIntentosPago").value);
				
				eval("document.getElementById('txtDoc'+"+varIntFila+").disabled = false");
				eval("document.getElementById('LnkPos'+"+varIntFila+").style.visibility = 'hidden'");	
				eval("document.getElementById('LnkDelPos'+"+varIntFila+").style.visibility = 'hidden'");
				eval("document.getElementById('icoTranPos'+"+varIntFila+").style.visibility = 'hidden'");	
				eval("document.getElementById('icoDelPos'+"+varIntFila+").style.visibility = 'hidden'");	
		
					}
				eval("ContPago"+varIntFila+"=ContPago"+varIntFila+"+1");			
			}
			//Proy-31949-Fin	
		
		
		}
		catch(err){
			alert(err.description);
			f_activar_fila(varIntFila,false);//Proy-31949
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
			var varCodRptaAudit = '';
			var varMsjRptaAudit = '';
			var varCodRptaWs = '';
			var varMsgAlert = '';
			var varPrintData = '';
			
			var varNroPedido = document.getElementById("hidDocSap").value; 
			var varIdUnicoTrans = '';
			var varFlagPago = '1';
			var varIdCabez = document.getElementById("HidIdCabez").value;
			var varRespTipTarPos = ''; //Proy-31949-Inicio
			
			
			varCodRptaAudit = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.AuditResponse.CodigoRespuesta;
			varMsjRptaAudit = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.AuditResponse.mensajeRespuesta;	
			varCodRptaWs = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.CodigoRespuesta;
						
			/*VALIDAR OPCION RE-IMPRESION*/
			if (varTipoTrans == '3'){				            										
				varNroTarjeta = document.getElementById(varNameDoc).value;
				varNroReferencia = '';
				varNroReferencia = varNroRefVisa;
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
				
				if(varCodRptaWs == '1' || varCodRptaWs == null || varCodRptaWs === undefined){//Proy-31949-Inicio
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
						varNroTarjeta = document.getElementById(varNameDoc).value;
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
						// varNroTarjeta = ''; //Proy-31949-Inicio
					}
					
					/*Ouput Visa Fin (Venta&Anulacion)*/
				}
			}
			varMontoOperacion = ''; varMontoOperacion = objEntityPOS.montoOperacion;
			varNomEquipoPOS = ''; varNomEquipoPOS = varNomPcPos;
			//Proy-31949-Inicio
			if(varEstTran == varArrayEstTrans[3] && varTipoTrans == varArrayTipoTran[1])
			{   
			//VALIDACION CUARTA ANULACION ESTADO INCONPLETO
								
				eval("IntentosPagar=ContPago"+varIntFila);
				eval("IntentosAnular=ContAnular"+varIntFila);
			
				if(varTipoTrans == varArrayTipoTran[1])
				{ 
					if(IntentosAnular > MaxNumIntentosAnular)
			{ 
				varEstTran = varArrayEstTrans[4];//INCONPLETO	
						varDesRpta = document.getElementById("HidMsjErrorNumIntentos").value;					
						eval("ContAnular"+varIntFila+"=1");
						eval("document.frmPrincipal.HidFila"+varIntFila+".value=''");					
						
			}
			
			}
			
			}
			//Proy-31949-Fin
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
			'|TipoPago=' + document.getElementById("HidTipoPago").value+
			'|ResTarjetaPos='; //Proy-31949-Inicio
			
			var varRpta = RSExecute(serverURL,"ActualizarTransaction",varTramaUpdate,
			objEntityPOS.TrnsnId);
			
			if(varEstTran == '4' && varCodOpe == '01' &&  varTipoTrans == '1')
			{
				//RECHAZADO VENTA
				f_activar_fila(varIntFila,false);
				alert(varMsgAlert);				
			}
			else if ((varEstTran == '3' || varEstTran == '' || varEstTran == '4')  && varCodOpe == '01'){
				//ACEPTADO VENTA
				document.getElementById("lblEnvioPos").innerHTML  = "";
				//document.getElementById(varNameLinkPOS).href = "javascript:f_EnvioPOS(" + varIntFila + ",'Imprimir POS')"; //Proy-31949-Inicio
				eval("document.getElementById('"+ varNameLinkPOS +"').onclick = function(){blnClosingMod = false; f_EnvioPOS(" + varIntFila + ",'Imprimir POS');}"); //Proy-31949-Inicio
				document.getElementById(varNameImgPOS).src = "../images/print-icon.png";
				document.getElementById(varNameImgDelPOS).src = "../images/delete-icon.png";
				document.getElementById(varNameImgPOS).alt="Imprimir";
			}			
			
			//ANULACION MASTERCARD Y VISANET
			if (varCodOpe == '04' && (varEstTran == '3' || varEstTran == '5')){
				//document.getElementById("HidFila" + varIntFila).value = ''; //Proy-31949-Inicio
				f_activar_fila(varIntFila,false);
				document.getElementById("txtDoc" + varIntFila).value = '';
				eval("document.frmPrincipal.HidFila"+varIntFila+".value=''"); //Proy-31949-Inicio	
				document.getElementById("icoTranPos" + varIntFila).src = "../images/send-icon.png";
				document.getElementById("icoTranPos" + varIntFila).alt = "Envio POS";
				//document.getElementById("LnkPos" + varIntFila).href = "javascript:f_EnvioPOS(" + varIntFila + ",'Envio POS')"; //Proy-31949-Inicio
				eval("document.getElementById('"+ "LnkPos" + varIntFila +"').onclick = function(){blnClosingMod = false; f_EnvioPOS(" + varIntFila + ",'Envio POS');}"); //Proy-31949-Inicio				
				varNroTarjeta = '';
				
				//CNH
				document.getElementById("txtDoc" + varIntFila).disabled = true;
				
			}
			//ANULACION CANCELADA
			else if(varCodOpe == '04' && varEstTran == '4')
			{
				document.getElementById("lblEnvioPos").innerHTML  = "";
				//document.getElementById(varNameLinkPOS).href = "javascript:f_EnvioPOS(" + varIntFila + ",'Imprimir POS')"; //Proy-31949-Inicio
				eval("document.getElementById('"+ varNameLinkPOS +"').onclick = function(){blnClosingMod = false; f_EnvioPOS(" + varIntFila + ",'Imprimir POS');}"); //Proy-31949-Inicio
				document.getElementById(varNameImgPOS).src = "../images/print-icon.png";
				document.getElementById(varNameImgDelPOS).src = "../images/delete-icon.png";
				document.getElementById(varNameImgPOS).alt="Imprimir";
			}
			
			varNameTipoPOS = '';
			
			
			document.getElementById(NameDoc).value = varNroTarjeta;
			var varIndex = NameDoc.substr(NameDoc.length -1,1);
			varMontoOperacion = document.getElementById("txtMonto" + varIndex).value;
			
			if(sCodRespTarj == '11')
			{
				//VENTA
				document.getElementById("HidFila" + varIndex).value = 'Monto=' + varMontoOperacion + 
				'|Tarjeta=' + varNroTarjeta + 
				'|ComboIndex=' + document.getElementById("cboTipDocumento" + varIndex).selectedIndex + 
				'|NroReferncia=' + varNroReferencia;
			}
			
			var varNomCliente = document.getElementById("txtNomCliente").value;
			var varNroTelefono = document.getElementById("hidNumeroTelefono").value; 
			var varNroPedido = document.getElementById("hidDocSap").value; 
			
			var varTramaAudit = '';    
			varTramaAudit = 'NomCliente=' + varNomCliente + '|NroTelefono=' + varNroTelefono + 
			'|NroPedido=' + varNroPedido + '|IdTransaccion=' + objEntityPOS.TrnsnId + '|nMonto=' + varMontoOperacion ;
			
			varIdTransAux = objEntityPOS.TrnsnId;
			
			//Proy-31949-Inicio	
			
			BloqueoCancelar();			
						
			if(varTipoTrans == "2" && varEstTran == varArrayEstTrans[3])
			{			
				if(IntentosAnular >= MaxNumIntentosAnular)
			{
							varTramaAuditAux = varTramaAudit;
							RSExecute(serverURL,"GuardarAutorizacion",varTramaAudit,CallBack_GuardarAutorizacion,GuardarAutorizacionError,"X");
					eval("ContAnular"+varIntFila+"=ContAnular"+varIntFila+"+1");
							return;
						}	
				eval("ContAnular"+varIntFila+"=ContAnular"+varIntFila+"+1");
					}
			
			if(varTipoTrans == "1" && varEstTran == varArrayEstTrans[3])
			{				
				if(IntentosPagar >= MaxNumIntentosPago)
				{
				alert(document.getElementById("HidMsjNumIntentosPago").value);
				
				eval("document.getElementById('txtDoc'+"+varIntFila+").disabled = false");
				eval("document.getElementById('LnkPos'+"+varIntFila+").style.visibility = 'hidden'");	
				eval("document.getElementById('LnkDelPos'+"+varIntFila+").style.visibility = 'hidden'");
				eval("document.getElementById('icoTranPos'+"+varIntFila+").style.visibility = 'hidden'");	
				eval("document.getElementById('icoDelPos'+"+varIntFila+").style.visibility = 'hidden'");	
		
					}
				eval("ContPago"+varIntFila+"=ContPago"+varIntFila+"+1");			
			}
			//Proy-31949-Fin
			
			
		}
		catch(err) {
			alert(err.description);
			f_activar_fila(varIntFila,false); //Proy-31949-Inicio
		}	
	}
	function trim1 (string) {
		return string.replace(/^\s\s*/, '').replace(/\s\s*$/, '');
	}
	
	function SuccessMasterCard(objResponse,NameDoc)
	{
	    var varIndex = NameDoc.substr(NameDoc.length -1,1); //PROY-31949
	    
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
			
			var varNroPedido = document.getElementById("hidDocSap").value; 
			var varIdUnicoTrans = '';
			var varFlagPago = '1';
			var varIdCabez = document.getElementById("HidIdCabez").value;
			var varRespTipTarPos = ''; //Proy-31949-Inicio
			
			varCodRptaAudit = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.AuditResponse.CodigoRespuesta;
			varMsjRptaAudit = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.AuditResponse.mensajeRespuesta;	
			varCodRptaWs = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.CodigoRespuesta;
			
			/*VALIDAR OPCION RE-IMPRESION*/
			if (varTipoTrans == '3'){			
			
				sCodRespTarj = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.CodigoRespuesta;
				varDesRpta = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.DescripcionRespuesta;					
				varNroTarjeta = document.getElementById(varNameDoc).value;
				varNroReferencia = '';
				varNroReferencia = varNroRefVisa;
				varNumAutTransaccion = '';//NumeroAutorizacion
				varNumAutTransaccion = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.NumeroAutorizacion;
				varFechaExpiracion = '';
				varCodOperVisa = '';
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
				
				if(varCodRptaWs == '1'){
					varEstTran = varArrayEstTrans[3];//RECHAZADO
					varMsgAlert = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.ImprimirData;
					alert(varMsgAlert);
				}
				else{
					varEstTran = varArrayEstTrans[2];//ACEPTADO								
				}				
			}
			else
			{
				if ((varCodRptaAudit == '0' && varCodRptaWs == '77')|| varCodRptaWs == '1') 
				{
					varDesRpta = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.DescripcionRespuesta;
					varNroTarjeta = document.getElementById(varNameDoc).value;
					varNroReferencia = '';
					varNroReferencia = varNroRefVisa;
					varNumAutTransaccion = '';
					varFechaExpiracion = '';
					sCodRespTarj = '';
					varCodOperVisa = '';
					varSeriePOS = '';
					
					varImpVoucher = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.DescripcionRespuesta;
					varMsgAlert = varImpVoucher;
					varEstTran = varArrayEstTrans[3];//RECHAZADO
				}
				else
				{
					if (typeof jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.CodigoAdquiriente == 'undefined')					
						varIdUnicoTrans = '';
					else
						varIdUnicoTrans = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.CodigoAdquiriente;
				
					/*Ouput Visa Ini(Venta&Anulacion)*/
					/*CodigoAprobacion*/
					sCodRespTarj = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.CodigoRespuesta;
					varDesRpta = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.DescripcionRespuesta;
					varClienteVisa = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.NombreCliente;   
					varNumAutTransaccion = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.NumeroAutorizacion;					
					
					
					if (varCodOpe == '04'){
						varNroRefVisa = '';
						varNroRefVisa = varNroRefMC;
						varNroRefVisa = trim1(varNroRefVisa);
						varIdRefAnu = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.NumeroReferencia;
						varIdRefAnu = (varIdRefAnu == null) ? '' : String(varIdRefAnu).replace("REF","");
					}
					else{
						varNroRefVisa = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.NumeroReferencia;
						varNroRefVisa = (varNroRefVisa == null) ? '' : String(varNroRefVisa).replace("REF","");
						varNroRefVisa = trim1(varNroRefVisa);
						varRespTipTarPos = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.IdTarjeta; //Proy-31949-Inicio
					}
					
					
					
					varNroTarjeta = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.NumeroTarjeta;
					varFechaExpiracion = '';
					varCodOperVisa = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.CodigoAprobacion;
					varSeriePOS = '';
					varImpVoucher = '';
					varImpVoucher = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.ImprimirData;
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
						// varNroTarjeta = ''; //Proy-31949-Inicio
					}
					/*Ouput Visa Fin (Venta&Anulacion)*/					
				}
			}
			
			varMontoOperacion = ''; varMontoOperacion = objEntityPOS.montoOperacion;
			varNomEquipoPOS = ''; varNomEquipoPOS = varNomPcPos;
			
			//Proy-31949-Inicio
			if(varEstTran == varArrayEstTrans[3] && varTipoTrans == varArrayTipoTran[1])
			{   
			//VALIDACION CUARTA ANULACION ESTADO INCONPLETO
								
				eval("IntentosPagar=ContPago"+varIntFila);
				eval("IntentosAnular=ContAnular"+varIntFila);
				
				if(varTipoTrans == varArrayTipoTran[1])
				{ 
					if(IntentosAnular > MaxNumIntentosAnular)
			{ 
				varEstTran = varArrayEstTrans[4];//INCONPLETO
						varDesRpta = document.getElementById("HidMsjErrorNumIntentos").value;					
						eval("ContAnular"+varIntFila+"=1");
						eval("document.frmPrincipal.HidFila"+varIntFila+".value=''")					
						BloqueoCancelar();	
			}
			
			}
			
			}
			//Proy-31949-Fin
				
			
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
			'|TipoPago=' + document.getElementById("HidTipoPago").value+
			'|ResTarjetaPos=' + varRespTipTarPos; //Proy-31949-Inicio
			
			RSExecute(serverURL,"ActualizarTransaction",varTramaUpdate,
			objEntityPOS.TrnsnId,CallBack_ActTransMCD,GuardarTransactionError,"X"); //Proy-31949-Inicio
			
			if(varEstTran == '4' && varCodOpe == '01' &&  varTipoTrans == '1')
			{
				//RECHAZADO VENTA
				f_activar_fila(varIntFila,false);
				alert(varMsgAlert);				
			}
			else if ((varEstTran == '3' || varEstTran == '' || varEstTran == '4')  && varCodOpe == '01'){
				//ACEPTADO VENTA
				document.getElementById("lblEnvioPos").innerHTML  = "";
				//document.getElementById(varNameLinkPOS).href = "javascript:f_EnvioPOS(" + varIntFila + ",'Imprimir POS')";
				eval("document.getElementById('"+ varNameLinkPOS +"').onclick = function(){blnClosingMod = false; f_EnvioPOS(" + varIntFila + ",'Imprimir POS');}"); //Proy-31949-Inicio
				document.getElementById(varNameImgPOS).src = "../images/print-icon.png";
				document.getElementById(varNameImgDelPOS).src = "../images/delete-icon.png";
				document.getElementById(varNameImgPOS).alt="Imprimir";
			}			
			//ANULACION MASTERCARD Y VISANET
			if (varCodOpe == '04' && (varEstTran == '3' || varEstTran == '5')){
				document.getElementById("HidFila" + varIntFila).value = '';
				f_activar_fila(varIntFila,false);
				document.getElementById("txtDoc" + varIntFila).value = '';
				document.getElementById("icoTranPos" + varIntFila).src = "../images/send-icon.png";
				document.getElementById("icoTranPos" + varIntFila).alt = "Envio POS";
				//document.getElementById("LnkPos" + varIntFila).href = "javascript:f_EnvioPOS(" + varIntFila + ",'Envio POS')";
				eval("document.getElementById('"+ "LnkPos" + varIntFila +"').onclick = function(){blnClosingMod = false; f_EnvioPOS(" + varIntFila + ",'Envio POS');}"); //Proy-31949-Inicio
				document.getElementById("HidFila" + varIntFila).value = '';
				varNroTarjeta = '';
				//CNH
				document.getElementById("txtDoc" + varIntFila).disabled = true;
				
			}
			//ANULACION CANCELADA
			else if(varCodOpe == '04' && varEstTran == '4')
			{
				document.getElementById("lblEnvioPos").innerHTML  = "";
				//document.getElementById(varNameLinkPOS).href = "javascript:f_EnvioPOS(" + varIntFila + ",'Imprimir POS')";
				eval("document.getElementById('"+ varNameLinkPOS +"').onclick = function(){blnClosingMod = false; f_EnvioPOS(" + varIntFila + ",'Imprimir POS');}"); //Proy-31949-Inicio
				document.getElementById(varNameImgPOS).src = "../images/print-icon.png";
				document.getElementById(varNameImgDelPOS).src = "../images/delete-icon.png";
				document.getElementById(varNameImgPOS).alt="Imprimir";
			}
			
			varNameTipoPOS = '';
			
			document.getElementById(NameDoc).value = varNroTarjeta;
			//var varIndex = NameDoc.substr(NameDoc.length -1,1); //Proy-31949-Inicio
			varMontoOperacion = document.getElementById("txtMonto" + varIndex).value;
			
			if(sCodRespTarj == '00' &&  varTipoTrans == '1' )
			{
				//VENTA
				document.getElementById("HidFila" + varIndex).value = 'Monto=' + varMontoOperacion + 
				'|Tarjeta=' + varNroTarjeta + 
				'|ComboIndex=' + document.getElementById("cboTipDocumento" + varIndex).selectedIndex + 
				'|NroReferncia=' + varNroReferencia;
			}
			
			var varNomCliente = document.getElementById("txtNomCliente").value;
			var varNroTelefono = document.getElementById("hidNumeroTelefono").value; 
			var varNroPedido = document.getElementById("hidDocSap").value; 
			
			var varTramaAudit = '';    
			varTramaAudit = 'NomCliente=' + varNomCliente + '|NroTelefono=' + varNroTelefono + 
			'|NroPedido=' + varNroPedido + '|IdTransaccion=' + objEntityPOS.TrnsnId + '|nMonto=' + varMontoOperacion ;
			
			varIdTransAux = objEntityPOS.TrnsnId;
			
			//Proy-31949-Inicio	
			BloqueoCancelar();		
						
			if(varTipoTrans == "2" && varEstTran == varArrayEstTrans[3])
			{			
				if(IntentosAnular >= MaxNumIntentosAnular)
			{
							varTramaAuditAux = varTramaAudit;
							RSExecute(serverURL,"GuardarAutorizacion",varTramaAudit,CallBack_GuardarAutorizacion,GuardarAutorizacionError,"X");
					eval("ContAnular"+varIntFila+"=ContAnular"+varIntFila+"+1");
							return;
						}	
				eval("ContAnular"+varIntFila+"=ContAnular"+varIntFila+"+1");
					}
			
			if(varTipoTrans == "1" && varEstTran == varArrayEstTrans[3])
			{				
				if(IntentosPagar >= MaxNumIntentosPago)
				{
				alert(document.getElementById("HidMsjNumIntentosPago").value);
				
				eval("document.getElementById('txtDoc'+"+varIntFila+").disabled = false");
				eval("document.getElementById('LnkPos'+"+varIntFila+").style.visibility = 'hidden'");	
				eval("document.getElementById('LnkDelPos'+"+varIntFila+").style.visibility = 'hidden'");
				eval("document.getElementById('icoTranPos'+"+varIntFila+").style.visibility = 'hidden'");	
				eval("document.getElementById('icoDelPos'+"+varIntFila+").style.visibility = 'hidden'");	
		
					}
				eval("ContPago"+varIntFila+"=ContPago"+varIntFila+"+1");			
			}			
			//Proy-31949-Fin
		}
		catch(err) {
				   
			alert(err.description);
			//PROY-31949 - Inicio
			f_activar_fila(varIntFila,false);		
			//PROY-31949 - Fin
		}	
	}

	function CallService(tipoPOS,NameDoc,objEntityPOS){
	
		varBolWsLocal = false;
		varIdRefAnu = '';
		
		var entityOpe;
		var soapMSG;
		var soapDataUpdate;
		// var varNroTarjeta; //Proy-31949-Inicio
		var varMontoOperacion;		
		var varFechaExpiracion;
		var varSeriePOS;
		var varNomEquipoPOS;
				
		var VarTrnsnId = '';
		var VarToday = '';	
		var varNroReferencia = '';
		
		varTipOpePOS = ''; varEstTran = '';
		
		//Variables de auditoria Ini		
		var varArrayAudi = document.getElementById("HidDatoAuditPos").value.split("|");
		var varNroPedido = document.getElementById("hidDocSap").value;
		var VarToday = new Date();
		var varIdTransaccion = varNroPedido + '_' + formatDate(VarToday);
		var varIpApplicacion = varArrayAudi[0];
		var varNombreAplicacion = varArrayAudi[1];
		var varUsuarioAplicacion = varArrayAudi[2];
		//Variables de auditoria Fin
		
    
    switch (tipoPOS) {
			case "01": //VISANET
				if (varTipoTrans == '3'){
					varMonedaVisa = '';
					varMontoVisa = '' ;
				}
				else{
					//varMontoVisa = parseFloat(objEntityPOS.montoOperacion);
					varMontoVisa = Number(objEntityPOS.montoOperacion).toFixed(2);
					
				}				
				
				entityOpe = { 
				TipoOperacion: varTipoOpeFi, SalidaMensaje: '', RutaArchivoINI: '', 
        TipoMoneda: varMonedaVisa, Monto: varMontoVisa, 
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
          processData: false,
          contentType: "text/xml; charset=\"utf-8\"",
          async: true,
          cache: false,
          success: function (objResponse, status) {
						/*Inicio success*/
						SuccessVisaNet(objResponse,NameDoc);
						
						var varGrabAutoc = document.getElementById("HidGrabAuto").value;
						var varPagoPen = document.getElementById("txtSaldo").value;
						//PAGO AUTOMATICO
						
						if(varGrabAutoc == "1")
						{
							if (varEstTran == '3' && varCodOpe == '01' && varBolGetTarjeta == true && Number(varPagoPen)== 0){
								//f_Grabar();//se asigna en CBH al btnGrabar						
								var clickButton = document.getElementById("<%= btnGrabar.ClientID %>");
								clickButton.click();
							}
						}
						//BloqueoCancelar(); //Proy-31949-Inicio
						/*Fin success*/
          },
          error: function (request, status) {
						/*Inicio Error*/
						varBolWsLocal = true;
            alert('Sin respuesta del POS, tiempo de espera superado.');            
                      
            ErrorVisaNet(request,NameDoc,objEntityPOS);
            /*Fin Error*/
          },
          timeout: Number(timeOutWsLocal)
        });
        return true;
				break;
			case "02": //MASTER CARD
			
				if (varTipoTrans == '3'){
					varMonedaMC = '';
					varMontoMC = '' ;
				}
				else{
					//varMontoMC = parseFloat(objEntityPOS.montoOperacion);
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
        TipoMoneda: varMonedaMC,
        DataAdicional: varNroRefMC, 
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
          success: function (objResponse, status) {
						/*Inicio success*/
						SuccessMasterCard(objResponse,NameDoc);
						
						var varGrabAutoc = document.getElementById("HidGrabAuto").value;
						var varPagoPen = document.getElementById("txtSaldo").value;
						//PAGO AUTOMATICO
						if(varGrabAutoc == "1")
						{
							if (varEstTran == '3' && varCodOpe == '01' && varBolGetTarjeta == true && Number(varPagoPen)== 0){
								//f_Grabar();	//se asigna en CBH al btnGrabar				
								var clickButton = document.getElementById("<%= btnGrabar.ClientID %>");
								clickButton.click();
							}
						}
						//BloqueoCancelar();//Proy-31949-Inicio
						/*Fin success*/
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
    }
	}
	function CallBack_GuardarAutorizacion(response)
	{
		var varRpta = response.return_value;
		var res = (varRpta.replace("<BODY><SELECT>", "")).replace("</SELECT>", "").replace("\r\n","");
		varRpta = res;		
		if (varRpta=='0'){
			alert('Ud. no está autorizado a realizar esta operación. Comuniquese con el administrador');
			RSExecute(serverURL,"GuardarAutorizacion",varTramaAuditAux,CallBack_GuardarAutorizacion,GuardarAutorizacionError,"X");
			return;	
		}
		else{
		
		    f_EnvioPOS(varIntFila,'Eliminar POS'); //PROY-31949
		
		}
	}
	function GuardarAutorizacionError(co)
	{
		if (co.message){
			alert("Context:" + co.context + "\nError: " + co.message);
		}
	}
	//PROY-27440 FIN
	
	
	
	function window_onload(){
	  if (document.getElementById("txtRecibidoPen") != null)
	  {
		frmPrincipal.txtRecibidoPen.focus();
		//frmPrincipal.txtRecibidoPen.select();
	  }	
	  if(document.getElementById("ddlTipDoc") != null){
		f_CambiaTipoDocDOL();
	  }
	}
 
	function f_Grabar(){
		 
	    var blnErr = false;
	    if (document.getElementById("txtMonto1") != null)
	    {
		  if (f_Validar()) {
		    //if (!ValidaNumRefSunat('document.frmPrincipal.txtNotaCred','el campo Numero de Nota de Credito ', true)) event.returnValue = false;
			CalculoVuelto(); 
			if (frmPrincipal.txtVuelto.value*1<0){
				alert('El importe recibido no puede ser menor al importe pagado..');
				event.returnValue = false;
				blnErr = true;
			}
				
			document.frmPrincipal.hidNumFilas.value = f_NumFilas();			
			if (! f_ValidarTarjeta())
			{
				//alert('entra en el validar tarjeta');
				event.returnValue = false;
				blnErr = true;			
			}
			
			if (! f_ValidarNC())
			{
				event.returnValue = false;
				blnErr = true;			
			}
			
			
			/*if(document.getElementById("txtNumSunat").value!=''){
				var numRefer = parseInt(document.getElementById("txtNumSunat").value);
				var numReferPropuesto = parseInt(document.getElementById("hidNroSunatEntero").value);
				if(numRefer <= numReferPropuesto){
					event.returnValue = false;
					blnErr = true;
					alert('Número de referencia invalido');
				}
			}*/
			
			if(!blnErr)
			{
				if(document.getElementById('hidFlagRenovacionRMP').value == "S"){
					alert("Por favor antes de Grabar realice el cobro del artículo");
					if(confirm("¿Está seguro de registrar el pago?"))
					{
						document.getElementById("Botones").style.display ="none"
						document.getElementById("divTitulo").style.display ="block"
			    	}
			    	else
			    	event.returnValue = false;			
				}else{
					document.getElementById("Botones").style.display ="none"
					document.getElementById("divTitulo").style.display ="block"
				}
			
			}
		  }
		  else
			event.returnValue = false;
		}	
		
	}

	function f_ValidarNC()
	{
		for(i=1; i<6; i++)
		{
		    eval("vv=document.frmPrincipal.cboTipDocumento"+i+".value")
			if (vv == 'ZNCR')
			{
				//document.frmPrincipal.txtMonto"+i+".value="";
				//alert('Forma de pago NC');	
				eval("ss=document.frmPrincipal.txtDoc"+i+".value")
				//alert(ss);
				if(ss==''){
					alert('Debe ingresar el número de correlativo generado de la nota de crèdito.');	
					return false;
				}
			}
		}	
		return true;
	}
	
	//***************************************************************************************//
	// CONSULTA DATOS DE LA NOTA DE CRÈDITO *************************************************//
	//***************************************************************************************//
	function f_ConsultaNC(){
		//alert('llama al evento para consultar los datos de la nc');
		document.frmPrincipal.hidNumFilas.value = f_NumFilasNC();
		
		//***********************
		for(i=1; i<6; i++)
		{
		    eval("vv=document.frmPrincipal.cboTipDocumento"+i+".value")
		    if (vv !=""){
				if(i==1){document.frmPrincipal.hidF1.value=vv}
				if(i==2){document.frmPrincipal.hidF2.value=vv}
				if(i==3){document.frmPrincipal.hidF3.value=vv}
				if(i==4){document.frmPrincipal.hidF4.value=vv}
				if(i==5){document.frmPrincipal.hidF5.value=vv}
			}
		}
		
				
		//***********************
		for(i=1; i<6; i++)
		{
		    eval("vv=document.frmPrincipal.cboTipDocumento"+i+".value")
		    if (vv == 'ZNCR')
			{
				frmPrincipal.cmdConsultaNC.click();	
				//return true;
			}
		}
		
	}
	
	function f_ValidarTarjeta() {					
	        //PROY-27440
	 var varIntAutPos = document.getElementById("HidIntAutPos").value;
		
	 	if(varIntAutPos != '1')
		{	
	        //PROY-27440			
		for(i=0; i<document.frmPrincipal.elements.length; i++){			
			if(document.frmPrincipal.elements[i].name.substring(0,15)=="cboTipDocumento"){						
				if (document.frmPrincipal.elements[i].value!=""){
					if (frmPrincipal.txtTarjCred.value.indexOf(document.frmPrincipal.elements[i].value)>=0) {
						if(document.frmPrincipal.elements[i+1].value!=""){ 
							if (frmPrincipal.txtBIN.value.indexOf(document.frmPrincipal.elements[i+1].value.substr(0,4))< 0 ){
								return confirm('El prefijo de la tarjeta no se encuentra registrado. Desea Continuar ?');
							}								
						}	
						else{							
							alert('Debe ingresar el número de la tarjeta...');
							return false
						}
					}
				}
			}
		}				
		        //PROY-27440
		} 
		else
		{
			for(i=1; i<6; i++)
			{
				eval("cboTipotar=document.frmPrincipal.cboTipDocumento"+i+".value");
							
				if (cboTipotar != "" && frmPrincipal.txtTarjCred.value.indexOf(cboTipotar)>=0)
				{
			        eval("numTar=document.frmPrincipal.txtDoc"+i+".value");
					if(numTar == "" )
					{
						alert('Debe ingresar el número de la tarjeta...');
						return false;
					}
				}
			}
		
			for(i=1; i<6; i++)
			{
				eval("cboTipotar=document.frmPrincipal.cboTipDocumento"+i+".value");

				if (cboTipotar != "" && frmPrincipal.txtTarjCred.value.indexOf(cboTipotar)>=0)
				{
					eval("numTar=document.frmPrincipal.txtDoc"+i+".value");
					if(numTar != "" )
					{					
						if (frmPrincipal.txtBIN.value.indexOf(numTar.substr(0,4))< 0 )
						{
							return confirm('El prefijo de la tarjeta no se encuentra registrado. Desea Continuar ?');
						}
					}
				}
		}

		}

		//PROY-27440				
        return true;
	}
	
	function CalculoVuelto(){
		suma = 0.0;
				
		for(i=1; i<6; i++){
			eval("ss=document.frmPrincipal.txtMonto"+i+".value");
			if(ss!='')
			{ 
			  eval("vv=document.frmPrincipal.cboTipDocumento"+i+".value")
			  if (vv == 'ZEFE')
			      suma = suma + (ss*1);	
		    }
		}
		suma = Math.round(suma*100)/100;
		
		if (frmPrincipal.txtRecibidoPen.value=="")	
			frmPrincipal.txtRecibidoPen.value = "0.00"
		if (frmPrincipal.txtRecibidoUsd.value=="") 
			frmPrincipal.txtRecibidoUsd.value = "0.00"
		//var vuelto = Math.round((frmPrincipal.txtRecibidoPen.value * 1 + frmPrincipal.txtRecibidoUsd.value * frmPrincipal.txtTipoCambio.value - suma)*100)/100;//aotane 05.08.2013		
		var vuelto = Math.round((frmPrincipal.txtRecibidoPen.value * 1 + frmPrincipal.txtRecibidoUsd.value * frmPrincipal.txtTipoCambio.value - suma)*1000)/1000;//aotane 05.08.2013		
		
			if (vuelto < 0)
			  document.getElementById("tdVuelto").innerHTML = "<FONT color=FF0000>Faltante:</FONT>";
			else
			  document.getElementById("tdVuelto").innerText = "Vuelto:";  
		//frmPrincipal.txtVuelto.value = vuelto;				
		var vueltoRedondeo = Math.round(RedondeaInmediatoSuperior(vuelto)*100)/100;//aotane 05.08.2013		
		frmPrincipal.txtVuelto.value = vueltoRedondeo;//aotane 05.08.2013
	}
	
	//PROY BUYBACK - INICIO
		function CalculoVueltoCV(){
		debugger;
		suma = 0.0;
				
		for(i=2; i<6; i++){
			eval("ss=document.frmPrincipal.txtMonto"+i+".value");
			if(ss!='')
			{ 
			  eval("vv=document.frmPrincipal.cboTipDocumento"+i+".value")
			  if (vv == 'ZEFE')
			      suma = suma + (ss*1);	
		    }
		}
		suma = Math.round(suma*100)/100;
		
		if (frmPrincipal.txtRecibidoPen.value=="")	
			frmPrincipal.txtRecibidoPen.value = "0.00"
		if (frmPrincipal.txtRecibidoUsd.value=="") 
			frmPrincipal.txtRecibidoUsd.value = "0.00"
			
		var vuelto = Math.round((frmPrincipal.txtRecibidoPen.value * 1 + frmPrincipal.txtRecibidoUsd.value * frmPrincipal.txtTipoCambio.value - suma)*1000)/1000;		
		
			if (vuelto < 0)
			  document.getElementById("tdVuelto").innerHTML = "<FONT color=FF0000>Faltante:</FONT>";
			else
			  document.getElementById("tdVuelto").innerText = "Vuelto:";  

		var vueltoRedondeo = Math.round(RedondeaInmediatoSuperior(vuelto)*100)/100;		
		frmPrincipal.txtVuelto.value = vueltoRedondeo;
	}
	//PROY BUYBACK - FIN
	
	function popup(url) {
	msg= window.open(url,"popi","toolbar=no,left=100,top=100,width=800,height=500,directories=no,status=no,scrollbars=yes,resize=no,menubar=no");
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

	//Se encarga de manejar el tamaño del campo numero de documento dependiendo de la eleccion del tipo de documento
	function textCounter(obj) {
		var maximo;
		switch (parseInt(document.frmPrincipal.cboTipDocumento.options[document.frmPrincipal.cboTipDocumento.selectedIndex].value)){
			case 6 : maximo = 11;	break;
			case 1 : maximo = 8;	break;
			default: maximo = 15;	break;
		}
		if (obj.value.length > maximo) // if too long...trim it!
			obj.value = obj.value.substring(0, maximo);
			// otherwise, update 'characters left' counter
	}

	function f_Nuevo(){
		document.frmPrincipal.action='addPago.asp';
		document.frmPrincipal.submit();
	}
		
	function f_Valida(){
		var valor;
		var i,j;
		var ret = true;
		for(i=0; i<document.frmPrincipal.elements.length; i++){
			if(document.frmPrincipal.elements[i].name.substring(0,15)=="cboTipDocumento"){
				valor = document.frmPrincipal.elements[i].value;
				if(valor != ''){
					for(j=0; j<document.frmPrincipal.elements.length; j++){
						if(document.frmPrincipal.elements[j].name.substring(0,15)=="cboTipDocumento"){
							if(i!=j){
							  if(valor == document.frmPrincipal.elements[j].value){
								ret = false;
							  }	
							}
						}
					}
				}
			}
		}
		return ret;
	}

	function f_Cancelar(){
		document.frmPrincipal.action='poolPagos.aspx';
		//document.frmPrincipal.action='Operaciones.asp?codOperacion=01&accionPago=1';
		document.frmPrincipal.submit();
	}
	
	function f_Borrar(camp1,camp2,camp3){
				
		//PROY-27440 INI
		var varIndex = camp1.name.substr(camp1.name.length-1);		
		
				
		 eval("vv=document.frmPrincipal.HidFila"+varIndex+".value")
		 if (vv != '')
		 {  BloqueoCancelar();	
			return;		 
		 }
		 else
		 {
		camp1.value='';
		camp2.value='';
		camp3.value='';
		eval("document.frmPrincipal.hidF"+varIndex+".value = ''");
		//ENVIO POS
		var varNameImgPOS="icoTranPos" + varIndex;
		var varNameLinkPOS="LnkPos" + varIndex;
		document.getElementById(varNameLinkPOS).style.visibility = "hidden";
		document.getElementById(varNameImgPOS).style.visibility = "hidden";
		
		//ELIMNAR POS
		var varNameImgDelPOS="icoDelPos" + varIndex;
		var varNameLinkDelPOS="LnkDelPos" + varIndex;
		document.getElementById(varNameImgDelPOS).style.visibility = "hidden";
		document.getElementById(varNameLinkDelPOS).style.visibility = "visible";
		
		//PROY-27440 FIN		
		BloqueoCancelar();	
		f_Recalcular(varIndex) //PROY-30166 -IDEA-38863
	   }
		
	}
	
	function f_Sumar(){
	 
		var ss, suma, tot;
		suma=0.0;
		if(f_Validar() == true){	
			for(i=1; i<6; i++){
				eval("ss=document.frmPrincipal.txtMonto"+i+".value");	
				if(ss!='')
				{	suma = suma + (ss*1);	}
			} 
			tot = document.frmPrincipal.hidSaldo.value;
			if(suma > tot) {
				alert("La suma del Monto a Pagar debe ser menor o igual al Saldo a Pagar")	
				document.frmPrincipal.txtMonto1.select()
				return false;
			}
			if (suma < 0.00) {
				alert("La suma del Monto a Pagar debe ser mayor o igual a la Cuota Inicial")
				document.frmPrincipal.txtMonto1.select()
				return false;
			}
			return true;
		}
	}
	
	function f_NumFilas(){
	 
		var cont;
		var chk = f_Validar();	
		cont=0;
		if(chk == true){
			for(i=1; i<6; i++){
				eval("ss=document.frmPrincipal.txtMonto"+i+".value");	
				if(ss!='')
				{	cont=cont + 1;	}
			}
		}
		return cont;
	}
	
	function f_NumFilasNC(){
		var cont;
		//var chk = f_Validar();	
		cont=0;
		//if(chk == true){
			for(i=1; i<6; i++){
				eval("ss=document.frmPrincipal.txtMonto"+i+".value");	
				if(ss!='')
				{	cont=cont + 1;	}
			}
		//}
		return cont;
	}
	
	function f_Validar() {	
		 		
		for(i=0; i<document.frmPrincipal.elements.length; i++){
			if(document.frmPrincipal.elements[i].name.substring(0,8)=="txtMonto"){
				if(document.frmPrincipal.elements[i].value!=""){
					if (!ValidaDecimal("document.frmPrincipal." + document.frmPrincipal.elements[i].name,'El campo Monto a Pagar debe tener el formato 0.00',false)) return false;
				}
			}
			valor=""
			
			if(document.frmPrincipal.elements[i].name.substring(0,15)=="cboTipDocumento"){
				
							
				if(document.frmPrincipal.elements[i+2].value!=""){
					if (document.frmPrincipal.elements[i].value==""){
					 alert('Debe de seleccionar una forma de pago');
					 return false;
					}
				}
				
				if(document.frmPrincipal.elements[i].value!=""){
					if(document.frmPrincipal.elements[i+2].value==""){
						alert('Debe ingresar un monto de pago');
						return false;
					}
				}
			}
		}
		
		if(document.frmPrincipal.hidFlagVentaCuota.value =='1'){
			var suma = 0;
			for(i=1; i<6; i++){
				eval("ss=document.frmPrincipal.txtMonto"+i+".value");	
				if(ss!=''){	
					suma = suma + (ss*1);	
				}
			} 			
			if(suma != document.frmPrincipal.hidMontoCuota.value){
				alert("La suma del Monto a Pagar debe igual a la Cuota Inicial S/ " + document.frmPrincipal.hidMontoCuota.value);
				return false;
			}
		}
						
        return true;
	}
	
	
	
	function f_Recalcular(index){
	
		//alert('f_Recalcular');
                // INI PROY-30166 - IDEA - 38934
		//var MontoFinal = (document.frmPrincipal.txtCuotaIni.value <= 0 ? MontoFinal = document.frmPrincipal.txtNeto.value : document.frmPrincipal.txtCuotaIni.value) ////PROY-30166 - IDEA - 38934
		var cuota = document.frmPrincipal.HidNroCuotas.value;
		if(cuota > 0 && document.frmPrincipal.txtCuotaIni.value >=0)
	    {
	      var MontoFinal=document.frmPrincipal.txtCuotaIni.value;
	    }
	   else
	    {
	       var MontoFinal=document.frmPrincipal.txtNeto.value;
		}
// FIN PROY-30166 - IDEA - 38934
		
		var suma;
		suma = 0.0;
		
		for(i=1; i<6; i++){
			eval("ss=document.frmPrincipal.txtMonto"+i+".value");
			if(ss!='')
			{	suma = suma + (ss*1);	}
		} 
		suma = Math.round(suma*100)/100;
		if (suma > MontoFinal){ //PROY-30166 - IDEA - 38934 - Se reemplaza document.frmPrincipal.txtNeto.value
			alert('El pago no puede ser mayor que el saldo')
			switch (index){
				case 1: document.frmPrincipal.txtMonto1.value=""; break;
				case 2: document.frmPrincipal.txtMonto2.value=""; break;
				case 3: document.frmPrincipal.txtMonto3.value=""; break;
				case 4: document.frmPrincipal.txtMonto4.value=""; break;
				case 5: document.frmPrincipal.txtMonto5.value=""; break;
			}
		} else {
			document.frmPrincipal.txtSaldo.value = Math.round((MontoFinal*1 - suma)*100)/100; //PROY-30166 - IDEA - 38934 - Se reemplaza document.frmPrincipal.txtNeto.value
		}
	}
	function f_CambiaTipoDocDOL(){
	
		document.frmPrincipal.hidTipoDocDOL.value = document.getElementById('ddlTipDoc').value;			
				
		switch (document.getElementById('ddlTipDoc').value)
		{
			case "0":{ 
						if(document.getElementById('hidFlagCargaDocDOL').value == "0" && document.getElementById('btnCargarDocumento').style.display == "none"){
							document.getElementById('btnCargarDocumento').style.display = "";
						}else{		
							if(document.getElementById('hidFlagCargaDocDOL').value == "1"){
								document.getElementById('btnCargarDocumento').style.display = "none";				
							}					
						}
						break;
					}
			case "1":{ 
						if(document.getElementById('hidFlagCargaDoc').value == "0" && document.getElementById('btnCargarDocumento').style.display == "none"){
							document.getElementById('btnCargarDocumento').style.display = "";					
						}else{
							if(document.getElementById('hidFlagCargaDoc').value == "1"){
								document.getElementById('btnCargarDocumento').style.display = "none";							
							}
						}						
						break;
					}
			case "2":{ 
						if(document.getElementById('hidFlagCargaDoc').value == "0" && document.getElementById('btnCargarDocumento').style.display == "none"){
							document.getElementById('btnCargarDocumento').style.display = "";					
						}else{
							if(document.getElementById('hidFlagCargaDoc').value == "1"){
								document.getElementById('btnCargarDocumento').style.display = "none";							
							}
						}						
						break;
					}
			default: {  					
						break; 
					}
		}				
	}
	
//-->
			</script>
			<meta content="no-cache" http-equiv="pragma">
	</HEAD>
	<body onload="f_datos_POS();" leftMargin="0" topMargin="0" marginwidth="0" marginheight="0">
		<div style="Z-INDEX: 1; WIDTH: 100px; POSITION: absolute" id="overDiv"></div>
		<form id="frmPrincipal" method="post" runat="server">
			<INPUT id="hidNumFilas" type="hidden" name="hidNumFilas" runat="server">
                        <INPUT id="HidNroCuotas" type="hidden" name="HidNroCuotas" runat="server">
			<table border="0" cellSpacing="0" cellPadding="0" width="975">
				<tr>
					<td vAlign="top" width="10">&nbsp;</td>
					<td vAlign="top" width="810">
						<!-- Inicio Cuerpo Principal-->
						<table border="0" cellSpacing="0" cellPadding="0" width="100%" height="14" name="Contenedor">
							<tr>
								<td align="center"></td>
							</tr>
						</table>
						<table border="1" cellSpacing="0" borderColor="#336699" cellPadding="0" width="100%" align="center"
							name="Contenedor">
							<tr>
								<td align="center">
									<table border="0" cellSpacing="0" cellPadding="0" width="100%" align="center">
										<tr>
											<td height="32" vAlign="top" width="14"></td>
											<td class="TituloRConsulta" height="32" vAlign="top" width="98%" align="center">Pagos 
												- Asignacion de Nro. Referencia</td>
											<td height="32" vAlign="top" width="14"></td>
										</tr>
									</table>
									<table border="0" cellSpacing="0" cellPadding="0" width="100%" align="center">
										<tr>
											<td vAlign="top" width="14"></td>
											<td width="98%">
												<table class="Arial12B" border="0" cellSpacing="2" cellPadding="0" width="80%" align="center">
													<tr id="RefProp" runat="server">
														<td>&nbsp;&nbsp;&nbsp;Nro. Referencia Propuesto :</td>
														<td><asp:textbox id="txtCorrelativo" runat="server" ReadOnly="True" Width="147px" CssClass="clsInputDisable"></asp:textbox></td>
													</tr>
													<tr id="RefImp" runat="server">
														<td>&nbsp;&nbsp;&nbsp;Nro. Referencia a Imprimir:</td>
														<td><asp:textbox id="txtCompleto" runat="server" ReadOnly="True" Width="147px" CssClass="clsInputDisable"></asp:textbox></td>
													</tr>
													<tr>
														<td>&nbsp;&nbsp;&nbsp;Nro. Referencia :</td>
														<td><asp:textbox id="txtNumSunat" runat="server" Width="147px" CssClass="clsInputEnable" MaxLength="7"></asp:textbox></td>
													</tr>
													<tr>
														<td>&nbsp;&nbsp;&nbsp;Imprimir Documento ?</td>
														<td><asp:checkbox id="chkImprimir" runat="server" Checked="True"></asp:checkbox></td>
													</tr>
												</table>
											</td>
											<td vAlign="top" width="14" align="right"></td>
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
						<div id="divPagos" runat="server">
							<table border="0" cellSpacing="0" cellPadding="0" width="100%" align="center">
								<tr>
									<td height="10"></td>
								</tr>
							</table>
							<table border="1" cellSpacing="0" borderColor="#336699" cellPadding="0" width="810" align="center"
								name="Contenedor">
								<tr>
									<td align="center">
										<table border="0" cellSpacing="0" cellPadding="0" width="100%" align="center">
											<tr>
												<td height="32" vAlign="top" width="10"></td>
												<td class="TituloRConsulta" height="32" vAlign="top" width="98%" align="center">Pagos 
													- Ingreso de Pagos</td>
												<td height="32" vAlign="top" width="14"></td>
											</tr>
										</table>
										<table border="0" cellSpacing="0" cellPadding="0" width="100%" align="center">
											<tr>
												<td vAlign="top" width="10"></td>
												<td vAlign="middle" width="100%">
													<table class="Arial12B" border="0" cellSpacing="2" cellPadding="0" width="90%" align="center">
														<tr>
															<td height="4"></td>
														</tr>
														<tr>
															<td width="25">&nbsp;</td>
															<td class="Arial12b" width="172">&nbsp;&nbsp;&nbsp;Cliente :</td>
															<td class="Arial12b" width="488" colSpan="4"><asp:textbox id="txtNomCliente" tabIndex="3" runat="server" ReadOnly="True" Width="266px" CssClass="clsInputDisable"></asp:textbox></td>
														</tr>
														<tr>
															<td height="18" width="25">&nbsp;</td>
															<td class="Arial12b" height="18" width="172">&nbsp;&nbsp;&nbsp;Total Factura :</td>
															<td class="Arial12b" height="18" width="488"><asp:textbox id="txtNeto" tabIndex="3" runat="server" ReadOnly="True" Width="147px" CssClass="clsInputDisable"
																	MaxLength="30"></asp:textbox></td>
															<td class="Arial12b" width="172">&nbsp;&nbsp;Tipo Cambio:</td>
															<td class="Arial12b"></td>
															<td height="18"><asp:textbox id="txtTipoCambio" tabIndex="5" runat="server" ReadOnly="True" Width="147px" CssClass="clsInputDisable"
																	MaxLength="30"></asp:textbox></td>
														<tr>
															<td width="25">&nbsp;</td>
															<td class="Arial12b" width="172">&nbsp;&nbsp;&nbsp;Cuota Inicial:</td>
															<td class="Arial12b"><asp:textbox id="txtCuotaIni" tabIndex="4" runat="server" ReadOnly="True" Width="147px" CssClass="clsInputDisable"
																	MaxLength="30"></asp:textbox></td>
															<td class="Arial12b" width="172">&nbsp;&nbsp;Pendiente de Pago:</td>
															<td class="Arial12b"></td>
															<td height="18"><asp:textbox id="txtSaldo" tabIndex="5" runat="server" ReadOnly="True" Width="147px" CssClass="clsInputDisable"
																	MaxLength="30"></asp:textbox></td>
														</tr>
														<TR>
															<TD width="25"></TD>
															<TD class="Arial12b" width="172"></TD>
															<TD class="Arial12b"></TD>
															<!--<TD class="Arial12b" width="172">&nbsp; Nota de Credito</TD>-->
															<TD class="Arial12b" width="172">&nbsp;</TD>
															<TD class="Arial12b"></TD>
															<TD height="18"><asp:textbox id="txtNotaCred" runat="server" Width="147px" CssClass="clsInputEnable" MaxLength="16"
																	Visible="False"></asp:textbox></TD>
														</TR>
													</table>
												</td>
												<td vAlign="top" width="4" align="right"></td>
											</tr>
										</table>
										<table border="0" cellSpacing="0" cellPadding="0" width="100%" align="center">
											<tr>
												<td height="10"></td>
											</tr>
										</table>
										<table border="0" cellSpacing="0" cellPadding="0" width="100%" align="center" name="Contenedor">
											<tr>
												<td align="center">
													<table border="0" cellSpacing="0" cellPadding="0" width="100%" align="center">
														<tr>
															<td vAlign="top" width="14"></td>
															<td width="98%">
																<table border="0" cellSpacing="0" cellPadding="0" width="780" align="center">
																	<tr>
																		<td>
																			<table border="1" cellSpacing="1" borderColor="#ffffff" cellPadding="1" width="100%">
																				<tr class="Arial12B" height="21">
																					<td height="22" borderColor="#999999" width="6%" align="center">&nbsp;</td>
																					<td height="22" borderColor="#999999" width="28%" align="center">Forma de Pago</td>
																					<td height="22" borderColor="#999999" width="31%" align="center">Nro. 
																						Tarjeta/Documento</td>
																					<td height="22" borderColor="#999999" width="35%" align="center">Monto a Pagar</td>
																				</tr>
																				<tr class="Arial12B" height="21">
																					<td bgColor="#dddee2" height="22" width="6%" align="center">
																						<!--PROY-27440 INI-->
                                                                                                                                                                                 <A id="IdLink1" onclick="blnClosingMod = false; BloqueoCancelar();">
																							<IMG id="ImgEliminar1" style="CURSOR: hand" border="0" name="icoeliminar" alt="Eliminar"
																								src="../images/botones/ico_eliminar.gif"></A> 
																						<!--PROY-27440 FIN-->
																					</td>
																					<td bgColor="#dddee2" height="22" width="28%" align="center">
																						<!--PROY-27440 INI-->
																						<asp:dropdownlist id="cboTipDocumento1" tabIndex="6" runat="server" CssClass="clsSelectEnable" onchange="f_bloqueo_fila(this,1);"
																							AutoPostBack="True"></asp:dropdownlist>
																						<!--PROY-27440 FIN-->
																					</td>
																					<td bgColor="#dddee2" height="22" width="31%" align="center"><asp:textbox id="txtDoc1" tabIndex="7" runat="server" Width="147px" CssClass="clsInputEnable"></asp:textbox></td>
																					<td bgColor="#dddee2" height="22" width="35%" align="center"><asp:textbox id="txtMonto1" tabIndex="8" runat="server" Width="147px" CssClass="clsInputEnable"></asp:textbox>
																						<!--PROY-27440 INI-->
																						<a runat="server" style="VISIBILITY:hidden" id="LnkPos1" onclick="blnClosingMod = false;f_EnvioPOS(1,'Envio POS');">
																							<img runat="server" style="CURSOR: hand" border="0" id="icoTranPos1" alt="Envio POS"
																								src="../images/send-icon.png"> </a><a style="VISIBILITY:hidden" id="LnkDelPos1" onclick="blnClosingMod = false;f_EnvioPOS(1,'Eliminar POS');">
																							<img style="CURSOR: hand" border="0" id="icoDelPos1" alt="Eliminar POS" src="../images/delete-icon.png">
																						</a><input id="HidTipPOS1" type="hidden" name="HidTipPOS1" runat="server"> 
																						<!--PROY-27440 FIN-->
																					</td>
																				</tr>
																				<tr class="Arial12B" height="21">
																					<td bgColor="#e9ebee" height="22" width="6%" align="center">
																						<!--PROY-27440 INI-->
																						<A id="IdLink2" onclick="blnClosingMod = false;javascript:f_Borrar(document.frmPrincipal.cboTipDocumento2,document.frmPrincipal.txtDoc2,document.frmPrincipal.txtMonto2);">
																							<IMG id="ImgEliminar2" style="CURSOR: hand" border="0" name="icoeliminar" alt="Eliminar"
																								src="../images/botones/ico_eliminar.gif"></A> 
																						<!--PROY-27440 FIN-->
																					</td>
																					<td bgColor="#e9ebee" height="22" width="28%" align="center">
																						<!--PROY-27440 INI-->
																						<asp:dropdownlist id="cboTipDocumento2" tabIndex="9" runat="server" CssClass="clsSelectEnable" onchange="f_bloqueo_fila(this,2);"
																							AutoPostBack="True"></asp:dropdownlist>
																						<!--PROY-27440 FIN-->
																					</td>
																					<td bgColor="#e9ebee" height="22" width="31%" align="center"><asp:textbox id="txtDoc2" tabIndex="10" runat="server" Width="147px" CssClass="clsInputEnable"></asp:textbox></td>
																					<td bgColor="#e9ebee" height="22" width="35%" align="center"><asp:textbox id="txtMonto2" tabIndex="8" runat="server" Width="147px" CssClass="clsInputEnable"></asp:textbox>
																						<!--PROY-27440 INI-->
																						<a style="VISIBILITY:hidden" id="LnkPos2" onclick="blnClosingMod = false;f_EnvioPOS(2,'Envio POS');">
																							<img style="CURSOR: hand" border="0" id="icoTranPos2" alt="Envio POS" src="../images/send-icon.png">
																						</a><a style="VISIBILITY:hidden" id="LnkDelPos2" onclick="blnClosingMod = false;f_EnvioPOS(2,'Eliminar POS');">
																							<img style="CURSOR: hand" border="0" id="icoDelPos2" alt="Eliminar POS" src="../images/delete-icon.png">
																						</a><input id="HidTipPOS2" type="hidden" name="HidTipPOS2" runat="server"> 
																						<!--PROY-27440 FIN-->
																					</td>
																				</tr>
																				<tr class="Arial12B" height="21">
																					<td bgColor="#dddee2" height="22" width="6%" align="center">
																						<!--PROY-27440 INI-->
																						<A id="IdLink3" onclick="blnClosingMod = false;f_Borrar(document.frmPrincipal.cboTipDocumento3,document.frmPrincipal.txtDoc3,document.frmPrincipal.txtMonto3);">
																							<IMG id="ImgEliminar3" style="CURSOR: hand" border="0" name="icoeliminar" alt="Eliminar"
																								src="../images/botones/ico_eliminar.gif"></A> 
																						<!--PROY-27440 FIN-->
																					</td>
																					<td bgColor="#dddee2" height="22" width="28%" align="center">
																						<!--PROY-27440 INI-->
																						<asp:dropdownlist id="cboTipDocumento3" tabIndex="12" runat="server" CssClass="clsSelectEnable" onchange="f_bloqueo_fila(this,3);"
																							AutoPostBack="True"></asp:dropdownlist>
																						<!--PROY-27440 FIN-->
																					</td>
																					<td bgColor="#dddee2" height="22" width="31%" align="center"><asp:textbox id="txtDoc3" tabIndex="13" runat="server" Width="147px" CssClass="clsInputEnable"></asp:textbox></td>
																					<td bgColor="#dddee2" height="22" width="35%" align="center"><asp:textbox id="txtMonto3" tabIndex="8" runat="server" Width="147px" CssClass="clsInputEnable"></asp:textbox>
																						<!--PROY-27440 INI-->
																						<a style="VISIBILITY:hidden" id="LnkPos3" onclick="blnClosingMod = false;f_EnvioPOS(3,'Envio POS');">
																							<img style="CURSOR: hand" border="0" id="icoTranPos3" alt="Envio POS" src="../images/send-icon.png">
																						</a><a style="VISIBILITY:hidden" id="LnkDelPos3" onclick="blnClosingMod = false;f_EnvioPOS(3,'Eliminar POS');">
																							<img style="CURSOR: hand" border="0" id="icoDelPos3" alt="Eliminar POS" src="../images/delete-icon.png">
																						</a><input id="HidTipPOS3" type="hidden" name="HidTipPOS3" runat="server"> 
																						<!--PROY-27440 FIN-->
																					</td>
																				</tr>
																				<tr class="Arial12B" height="21">
																					<td bgColor="#e9ebee" height="22" width="6%" align="center">
																						<!--PROY-27440 INI-->
																						<A id="IdLink4" onclick="blnClosingMod = false;javascript:f_Borrar(document.frmPrincipal.cboTipDocumento4,document.frmPrincipal.txtDoc4,document.frmPrincipal.txtMonto4);">
																							<IMG id="ImgEliminar4" style="CURSOR: hand" border="0" name="icoeliminar" alt="Eliminar"
																								src="../images/botones/ico_eliminar.gif"></A> 
																						<!--PROY-27440 FIN-->
																					</td>
																					<td bgColor="#e9ebee" height="22" width="28%" align="center">
																						<!--PROY-27440 INI-->
																						<asp:dropdownlist id="cboTipDocumento4" tabIndex="15" runat="server" CssClass="clsSelectEnable" onchange="f_bloqueo_fila(this,4);"
																							AutoPostBack="True"></asp:dropdownlist>
																						<!--PROY-27440 FIN-->
																					</td>
																					<td bgColor="#e9ebee" height="22" width="31%" align="center"><asp:textbox id="txtDoc4" tabIndex="16" runat="server" Width="147px" CssClass="clsInputEnable"></asp:textbox></td>
																					<td bgColor="#e9ebee" height="22" width="35%" align="center"><asp:textbox id="txtMonto4" tabIndex="8" runat="server" Width="147px" CssClass="clsInputEnable"></asp:textbox>
																						<!--PROY-27440 INI-->
																						<a style="VISIBILITY:hidden" id="LnkPos4" onclick="blnClosingMod = false;f_EnvioPOS(4,'Envio POS');">
																							<img style="CURSOR: hand" border="0" id="icoTranPos4" alt="Envio POS" src="../images/send-icon.png">
																						</a><a style="VISIBILITY:hidden" id="LnkDelPos4" onclick="blnClosingMod = false;f_EnvioPOS(4,'Eliminar POS');">
																							<img style="CURSOR: hand" border="0" id="icoDelPos4" alt="Eliminar POS" src="../images/delete-icon.png">
																						</a><input id="HidTipPOS4" type="hidden" name="HidTipPOS4" runat="server"> 
																						<!--PROY-27440 FIN-->
																					</td>
																				</tr>
																				<tr class="Arial12B" height="21">
																					<td bgColor="#dddee2" height="22" width="6%" align="center">
																						<!--PROY-27440 INI-->
																						<A id="IdLink5" onclick="blnClosingMod = false;javascript:f_Borrar(document.frmPrincipal.cboTipDocumento5,document.frmPrincipal.txtDoc5,document.frmPrincipal.txtMonto5);">
																							<IMG id="ImgEliminar5" style="CURSOR: hand" border="0" name="icoeliminar" alt="Eliminar"
																								src="../images/botones/ico_eliminar.gif"></A> 
																						<!--PROY-27440 FIN-->
																					</td>
																					<td bgColor="#dddee2" height="22" width="28%" align="center">
																						<!--PROY-27440 INI-->
																						<asp:dropdownlist id="cboTipDocumento5" tabIndex="18" runat="server" CssClass="clsSelectEnable" onchange="f_bloqueo_fila(this,5);"
																							AutoPostBack="True"></asp:dropdownlist>
																						<!--PROY-27440 FIN-->
																					</td>
																					<td bgColor="#dddee2" height="22" width="31%" align="center"><asp:textbox id="txtDoc5" tabIndex="19" runat="server" Width="147px" CssClass="clsInputEnable"></asp:textbox></td>
																					<td bgColor="#dddee2" height="22" width="35%" align="center"><asp:textbox id="txtMonto5" tabIndex="8" runat="server" Width="147px" CssClass="clsInputEnable"></asp:textbox>
																						<!--PROY-27440 INI-->
																						<a style="VISIBILITY:hidden" id="LnkPos5" onclick="blnClosingMod = false;f_EnvioPOS(5,'Envio POS');">
																							<img style="CURSOR: hand" border="0" id="icoTranPos5" alt="Envio POS" src="../images/send-icon.png">
																						</a><a style="VISIBILITY:hidden" id="LnkDelPos5" onclick="blnClosingMod = false;f_EnvioPOS(5,'Eliminar POS');">
																							<img style="CURSOR: hand" border="0" id="icoDelPos5" alt="DelPOS" src="../images/delete-icon.png">
																						</a><input id="HidTipPOS5" type="hidden" name="HidTipPOS5" runat="server"> 
																						<!--PROY-27440 FIN-->
																					</td>
																				</tr>
																			</table>
																		</td>
																	</tr>
																</table>
															</td>
															<td vAlign="top" width="14" align="right"></td>
														</tr>
													</table>
												</td>
											</tr>
										</table>
										<table border="0" cellSpacing="0" cellPadding="0" width="100%" align="center">
											<tr>
												<td height="10" align="center">
													<!--PROY-27440 INI-->
													<asp:Label id="lblEnvioPos" runat="server" CssClass="TituloRConsulta"></asp:Label>
													<!--PROY-27440 FIN-->
												</td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
							<table border="0" cellSpacing="0" cellPadding="0" width="100%" align="center">
								<tr>
									<td height="10"></td>
								</tr>
							</table>
							<table border="1" cellSpacing="0" borderColor="#336699" cellPadding="0" width="100%" align="center">
								<tr>
									<td>
										<table border="0" cellSpacing="0" cellPadding="0" width="100%" align="center">
											<tr>
												<td vAlign="top" width="10"></td>
												<td vAlign="middle" width="100%">
													<table class="Arial12B" border="0" cellSpacing="2" cellPadding="0" width="95%" align="center">
														<tr>
															<td width="200">Importe Recibido Soles:</td>
															<td><asp:textbox id="txtRecibidoPen" tabIndex="10" runat="server" CssClass="clsInputEnable"></asp:textbox>&nbsp;&nbsp;</td>
															<td id="tdVuelto" width="110">Vuelto:
															</td>
															<td><asp:textbox id="txtVuelto" runat="server" ReadOnly="True" CssClass="clsInputDisable"></asp:textbox>&nbsp;&nbsp;</td>
														</tr>
														<tr>
															<td width="200">Importe Recibido Dolares:</td>
															<td><asp:textbox id="txtRecibidoUsd" tabIndex="10" runat="server" CssClass="clsInputEnable"></asp:textbox></td>
															<td><asp:label id="lblTipDoc" text="Tipo Doc:" Runat="server" Visible="False"></asp:label><!--INICIATIVA-318--><!--Documento:--></td>
															<td><asp:dropdownlist id="ddlTipDoc" tabIndex="6" runat="server" CssClass="clsSelectEnable" Visible="false">
																	<asp:ListItem Value="">--Seleccione--</asp:ListItem>
																	<asp:ListItem Value="0">DOL</asp:ListItem>
																	<asp:ListItem Value="1">Titular TC</asp:ListItem>
																	<asp:ListItem Value="2">Otros</asp:ListItem>
																</asp:dropdownlist><!--INICIATIVA-318--><!--<input id="txtFile" size="34" type="file" name="txtFile" runat="server">-->&nbsp;<asp:button id="btnCargarDocumento" runat="server" Width="42px" CssClass="BotonOptm" Text="Cargar" Visible="False"></asp:button>
															</td>
														</tr>
														<tr>
															<td width="200"></td>
															<td></td>
															<td width="110">
															<td><asp:label style="DISPLAY: none" id="lblArchivoDOL" text="Ver" Runat="server"></asp:label><asp:imagebutton id="imgElimDoc" Runat="server" Visible="False" AlternateText="Eliminar Documento DOL"
																	ImageUrl="../../images/iconos/ico_eliminar.gif"></asp:imagebutton><br>
																<asp:label style="DISPLAY: none" id="lblArchivo" text="Ver" Runat="server"></asp:label></td>
														</tr>
													</table>
												</td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
						</div> <!-- divPagos --></td>
				</tr>
			</table>
			<table border="0" cellSpacing="0" cellPadding="0" width="100%" align="center">
				<tr>
					<td height="10"></td>
				</tr>
			</table>
			<div id="Botones">
				<table border="1" cellSpacing="0" borderColor="#336699" cellPadding="0" width="400" align="center">
					<tr>
						<td align="center">
							<table border="0" cellSpacing="2" cellPadding="0">
								<tr>
									<td width="28" align="center"></td>
									<td width="60" align="center"><asp:button id="btnGrabar" runat="server" Width="100px" CssClass="BotonOptm" Text="Grabar"></asp:button></td>
									<td width="28" align="center"></td>
									<td width="60" align="center"><asp:button id="btnCancelar" runat="server" Width="100px" CssClass="BotonOptm" Text="Cancelar"></asp:button></td>
									<td width="28" align="center"></td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</div>
			<div style="DISPLAY: none" id="divTitulo">
				<table border="0" cellSpacing="2" cellPadding="0" width="400" align="center">
					<tr>
						<td class="TituloRConsulta" width="28" align="center">El&nbsp;pago&nbsp;se&nbsp;está&nbsp;procesando</td>
					</tr>
				</table>
			</div>
			<p style="DISPLAY: none"><asp:textbox id="txtTarjCred" runat="server"></asp:textbox><asp:textbox id="txtBIN" runat="server"></asp:textbox><input id="txttest">
				<asp:button id="cmdConsultaNC" runat="server" Text="Button"></asp:button></p>
			<script language="JavaScript" type="text/javascript">
var esNavegador, esIExplorer;
  esNavegador = (navigator.appName == "Netscape") ? true : false;
  esIExplorer = ((navigator.appName.indexOf("Microsoft") != -1) || (navigator.appName.indexOf("MSIE") != -1)) ? true : false;

if (document.getElementById("txtMonto1") != null){
	//alert('Lllama => f_Recalcular(1)');
	f_Recalcular(1);
 }

function e_mayuscula(){
	if (event.keyCode>96&&event.keyCode<123)
		event.keyCode=event.keyCode-32;
}

function e_numero(){
	if ( !( (event.keyCode>=48) && (event.keyCode<=57)) && !(event.keyCode==13) )
		event.keyCode=0;
}

function f_LecBanda()
    {
     
      var largo;
      var intObj = parseFloat(this.id.substr(6,1));
      
      if ( !( (event.keyCode>=48) && (event.keyCode<=57))  )
		event.keyCode=0;
		
	  if (event.keyCode==13)
	    event.keyCode=0;	
		
      largo = 15;
     
	  switch (intObj)
	  {
	    case 1 : if (document.frmPrincipal.cboTipDocumento1.value == "ZDIN") 
	                largo = 13;
	             break;   
	    case 2 : if (document.frmPrincipal.cboTipDocumento2.value == "ZDIN") 
	                largo = 13;
	             break;
	    case 3 : if (document.frmPrincipal.cboTipDocumento3.value == "ZDIN") 
	                largo = 13;
	             break;
	    case 4 : if (document.frmPrincipal.cboTipDocumento4.value == "ZDIN") 
	                largo = 13;
	             break;
	    case 5 : if (document.frmPrincipal.cboTipDocumento5.value == "ZDIN") 
	                largo = 13;
	             break;
	  } 
	   
		
		if (this.value.length > largo)
		 event.keyCode=0;
		    
    }

function ValidaNumero(obj){
	var KeyAscii = window.event.keyCode;

	if (KeyAscii==13) return;	
	if (!(KeyAscii >= 46 && KeyAscii<=57) | (KeyAscii==46 && obj.value.indexOf(".")>=0) ){		
		window.event.keyCode = 0;
	}	
	else
	{	
		if (obj.value.indexOf(".")>=0 ){		
			if (KeyAscii!=46  && obj.value.substring(obj.value.indexOf(".")+1,obj.value.length).length>1)
				window.event.keyCode = 0;	
		}
	}
}
		
/*function CapturaTarjeta(obj)
{
   
}*/


if (esIExplorer) {
	//document.onkeypress = e_envio;
	//document.frmPrincipal.txtNumDocumento.onkeypress=e_mayuscula;
	if(typeof(document.frmPrincipal.txtDoc1)!="undefined"){
		//document.frmPrincipal.txtDoc1.onkeypress = f_LecBanda;
	}	
	if(typeof(document.frmPrincipal.txtDoc2)!="undefined"){
		//document.frmPrincipal.txtDoc2.onkeypress = f_LecBanda;
	}
	if(typeof(document.frmPrincipal.txtDoc3)!="undefined"){
		//document.frmPrincipal.txtDoc3.onkeypress = f_LecBanda;	
	}
	if(typeof(document.frmPrincipal.txtDoc4)!="undefined"){
		//document.frmPrincipal.txtDoc4.onkeypress = f_LecBanda;
	}
	if(typeof(document.frmPrincipal.txtDoc5)!="undefined"){
		//document.frmPrincipal.txtDoc5.onkeypress = f_LecBanda;	
	}
}



//INICIATIVA-318 INI
function f_validaCajaCerrada() {
	RSExecute(serverURL,"ValidarCajaCerrada",CallBack_ValidarCajaCerrada,"X");  
}

function CallBack_ValidarCajaCerrada(response){
	var varRpta = response.return_value;
	var res = (varRpta.replace("<BODY><SELECT>", "")).replace("</SELECT>", "").replace("\r\n","");
	
	var varArrayRpta = res.split("|");
	var varCodRpta = varArrayRpta[0];
	var varMsjRta = varArrayRpta[1];
	
	if(varCodRpta=="1"){
		alert(varMsjRta);
	}
}
//INICIATIVA - 529 INI
function f_autoAsignacionCaja() {
	RSExecute(serverURL,"AutoAsignacionCaja",CallBack_AutoAsignacionCaja,"X");  
}

function CallBack_AutoAsignacionCaja(response){
	var varRpta = response.return_value;
	var res = (varRpta.replace("<BODY><SELECT>", "")).replace("</SELECT>", "").replace("\r\n","");
	
	var varArrayRpta = res.split("|");
	var varCodRpta = varArrayRpta[0];
	var varMsjRta = varArrayRpta[1];
	
	if(varCodRpta=="0"){
		alert(varMsjRta);
	}
}
//INICIATIVA - 529 FIN
//INICIATIVA 318 FIN




			</script>
			<script runat="server">
			</script>
			<input id="hidTipoDocDOL" type="hidden" name="hidTipoDocDOL" runat="server"> <input id="hidFlagCargaDocDOL" type="hidden" name="hidFlagCargaDocDOL" runat="server">
			<input id="hidFlagCargaDoc" type="hidden" name="hidFlagCargaDoc" runat="server">
			<input id="hidF1" type="hidden" name="hidF1" runat="server"> <input id="hidF2" type="hidden" name="hidF2" runat="server">
			<input id="hidF3" type="hidden" name="hidF3" runat="server"> <input id="hidF4" type="hidden" name="hidF4" runat="server">
			<input id="hidF5" type="hidden" name="hidF5" runat="server"> <input id="hidFlagRenovacionRMP" type="hidden" name="hidFlagRenovacionRMP" runat="server">
			<input id="hidFlagVentaCuota" type="hidden" name="hidFlagVentaCuota" runat="server">
			<input id="hidNroSunatEntero" type="hidden" name="hidNroSunatEntero" runat="server">
			<input id="hidMensajeNC" type="hidden" name="hidMensajeNC" runat="server"> <input id="hidMontoCuota" type="hidden" name="hidMontoCuota" runat="server">
			<input id="hidDocSap" type="hidden" name="hidDocSap" runat="server"> <INPUT id="hidNumeroTelefono" type="hidden" name="hidNumeroTelefono" runat="server">
<input id="hidDESLOG" type="hidden" name="hidDESLOG" runat="server">
                        <!--PROY-27440 INI-->
			<input id="HidPtoVenta" type="hidden" name="HidPtoVenta" runat="server"> <input id="HidIntAutPos" type="hidden" name="HidIntAutPos" runat="server">
			<input id="HidGrabAuto" type="hidden" name="HidGrabAuto" runat="server"> <input id="HidCodOpera" type="hidden" name="HidCodOpera" runat="server">
			<input id="HidDesOpera" type="hidden" name="HidDesOpera" runat="server"> <input id="HidTipoOpera" type="hidden" name="HidTipoOpera" runat="server">
			<input id="HidTipoTarjeta" type="hidden" name="HidTipoTarjeta" runat="server"> <input id="HidTipoPago" type="hidden" name="HidTipoPago" runat="server">
			<input id="HidEstTrans" type="hidden" name="HidEstTrans" runat="server"> <input id="HidTipoPOS" type="hidden" name="HidTipoPOS" runat="server">
			<input id="HidTipoTran" type="hidden" name="HidTipoTran" runat="server"> <input id="HidFila1" type="hidden" name="HidFila1" runat="server">
			<input id="HidFila2" type="hidden" name="HidFila2" runat="server"> <input id="HidFila3" type="hidden" name="HidFila3" runat="server">
			<input id="HidFila4" type="hidden" name="HidFila4" runat="server"> <input id="HidFila5" type="hidden" name="HidFila5" runat="server">
			<input id="HidTipoMoneda" type="hidden" name="HidTipoMoneda" runat="server"> <input id="HidTransMC" type="hidden" name="HidTransMC" runat="server">
			<input id="HidMonedaMC" type="hidden" name="HidMonedaMC" runat="server"> <input id="HidApliPOS" type="hidden" name="HidApliPOS" runat="server">
			<input id="HidMonedaVisa" type="hidden" name="HidMonedaVisa" runat="server"> 
                        <input id="HidDatoPosVisa" type="hidden" name="HidDatoPosVisa" runat="server">
			<input id="HidDatoPosMC" type="hidden" name="HidDatoPosMC" runat="server"> 
                       <input id="HidDatoAuditPos" type="hidden" name="HidDatoAuditPos" runat="server">
			<input id="HidIdCabez" type="hidden" name="HidIdCabez" runat="server"> 
			<!--PROY-27440 FIN-->
	                <!--PROY-31949 INI--><input id="HidNumIntentosPago" type="hidden" name="HidNumIntentosPago" runat="server">
			<input id="HidNumIntentosAnular" type="hidden" name="HidNumIntentosAnular" runat="server">
			<input id="HidMsjErrorNumIntentos" type="hidden" name="HidMsjErrorNumIntentos" runat="server">
			<input id="HidMsjErrorTimeOut" type="hidden" name="HidMsjErrorTimeOut" runat="server">
			<input id="HidMsjNumIntentosPago" type="hidden" name="HidMsjNumIntentosPago" runat="server">
			<input id="HidFlagCajaCerrada" type="hidden" name="HidFlagCajaCerrada" runat="server">
			<input id="HidMsjCajaCerrada" type="hidden" name="HidMsjCajaCerrada" runat="server">
			<input id="HidMedioPagoPermitidas" type="hidden" name="HidMedioPagoPermitidas" runat="server">
			<!--PROY-31949 FIN-->
			<!--PROY-30166 - IDEA - 38934 - INICIO-->
			<input id="hidTotalSinInicial" type="hidden" name="hidTotalSinInicial" runat="server">
			<!--PROY-30166 - IDEA - 38934 - FIN-->
			<input id="hidCupon" type="hidden" name="hidCupon" runat="server"><!--PROY BUYBACK-->
			<input id="hidValorCupon" type="hidden" name="hidValorCupon" runat="server"><!--PROY BUYBACK-->
			<input id="hidCodigoCupon" type="hidden" name="hidCodigoCupon" runat="server"><!--PROY BUYBACK-->
                        <!--INICIATIVA-318 INI-->
			<input id="hddEnvioAutorizador" type="hidden" name="hddEnvioAutorizador" runat="server">
			<input id="hddComboAutorizador" type="hidden" name="hddComboAutorizador" runat="server">
			<input id="hddcboFormaPago" type="hidden" name="hddcboFormaPago" runat="server">
                        <!--INICIATIVA-318 FIN-->
		</form>
	</body>
</HTML>
