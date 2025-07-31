<%@ Page Language="vb" AutoEventWireup="false" Codebehind="detDocRecargaDTH.aspx.vb" Inherits="SisCajas.detDocRecargaDTH" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>detDocRecargaDTH</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<meta http-equiv="Cache-Control" content="no-cache">
		<meta http-equiv="Pragma" content="no-cache">
		<meta http-equiv="Expires" content="0">
		<LINK href="../estilos/est_General.css" type="text/css" rel="styleSheet">
		<script language="JavaScript" src="../librerias/date-picker.js"></script>
		<script language="JavaScript" src="../librerias/Lib_FuncValidacion.js"></script>
		<script type="text/javascript" src="../librerias/Lib_Redondeo.js"></script>
		<!--PROY-27440 INI-->
		<script language="JavaScript" src="../librerias/msrsclient.js"></script>
		<script language="JavaScript" src="../librerias/jquery-1.8.2.min.js"></script> <!--PROY-31949-->
		<script language="JavaScript" src="../Scripts/form.js"></script>
		<script language="JavaScript" src="../Scripts/xml2json.js"></script>
		<script language="JavaScript" src="../Scripts/operacionPOS.js"></script>
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<!--PROY-27440 FIN-->
		<script language="javascript">
                //Proy-31949 Inicio
                jQuery.support.cors = true;
                var MaxNumIntentosPago; 
                var MaxNumIntentosAnular;
                var varNroTarjeta;
                var varNroRef;			
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
                var varLnkPrintPos;
                var varNameImgPrint;
                //Proy-31949 Fin
		
                //Regularizacion 20.12.2012		
			//PROY-27440 INI
			var varArrayEstTrans;
			var serverURL =  '../Pos/ProcesoPOS.aspx';
			var webServiceURL = '<%= System.Configuration.ConfigurationSettings.AppSettings("consWsLocal_Url") %>';
			var timeOutWsLocal = '<%= System.Configuration.ConfigurationSettings.AppSettings("consWsLocal_Url_TimeOut") %>';
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
			var varTipoTrans;
			var varNameTdoc;
    var varResulCajero;
	var varIntAutPos;
	var varIntAutPosMC;
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
	var varIdRefAnu;
	/*CNH INI*/
	var varNameTxtDoc;
	/*CNH FIN*/
		    
			//PROY-27440 INI
			function f_datos_POS()
			{
		varIntAutPos = document.getElementById("HidIntAutPos").value;
		
			if(varIntAutPos != '1'){
			return;
		}
				var suma;
				suma = 0.0;
				
				for (var i=1; i<=3; i ++)
				{
					if (document.getElementById('HidFila' + i).value.length >0){
						var varArray = document.getElementById('HidFila' + i ).value.split("|");
						var varMonto = varArray[0].substr(varArray[0].indexOf("=")+1);
						var varTarjeta = varArray[1].substr(varArray[1].indexOf("=")+1);
						var varComboIndex = varArray[2].substr(varArray[2].indexOf("=")+1);
						
						document.getElementById('txtMonto' + i).value = varMonto;
						document.getElementById('txtDocumento' + i).value = varTarjeta;
						document.getElementById('ddlTipoDocumento' + i).selectedIndex = varComboIndex;
						document.getElementById('txtMonto' + i).disabled = true;
						document.getElementById('txtDocumento' + i).disabled = true;
						document.getElementById('ddlTipoDocumento' + i).disabled = true;
						
						//Proy-31949 Inicio
						$('#LnkPos'+i).fadeOut('fast');
						$('#LnkPrintPos'+i).fadeIn('fast');
						$('#LnkDelPos'+i).fadeIn('fast');
				
						var intRow = i + 1;
						FN_LoadPOS(intRow,'+');
						//Proy-31949 Fin				
						
						suma=suma +(eval(varMonto)*1);				
					}
				}	
				
				if(suma>0){
			//document.frmPrincipal.txtSaldo.value = Math.round((document.frmPrincipal.txtNeto.value*1 - suma)*100)/100; VALIDAR
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
				
						//Proy-31949 Inicio
						$('#'+varNameLinkPOS).fadeIn('slow');
						$('#'+varNameLinkDelPOS).fadeIn('slow');
						$(varLnkPrintPos).fadeOut('fast');
						//Proy-31949 Fin
				
				/*validacion IAPOS CNH*/			
				if(varIntAutPos == "1"){
					document.getElementById(varNameTxtDoc).value = "";
					document.getElementById(varNameTxtDoc).disabled = true;
				}
			}
			else{
						//Proy-31949 Inicio
						$('#'+varNameLinkPOS).fadeOut('fast');
						$('#'+varNameLinkDelPOS).fadeOut('fast');
						$(varLnkPrintPos).fadeOut('fast');
						//Proy-31949 Fin
			}
		}
		else
		{
			/*validacion IAPOS CNH*/							
			document.getElementById(varNameTxtDoc).value = "";
			document.getElementById(varNameTxtDoc).disabled = false;			
			
					//Proy-31949 Inicio
					$('#'+varNameLinkPOS).fadeOut('fast');
					$('#'+varNameLinkDelPOS).fadeOut('fast');
					$(varLnkPrintPos).fadeOut('fast');
					//Proy-31949 Fin
				}
			}
	
	function ObtenerTipoPosError(co) 
	{
		if (co.message) {
			alert("Context:" + co.context + "\nError: " + co.message);
		}
		varRptTtipoPos='1|Error';
		
				//Proy-31949 Inicio
				$('#'+varNameLinkPOS).fadeOut('fast');
				$('#'+varNameLinkDelPOS).fadeOut('fast');
				$(varLnkPrintPos).fadeOut('fast');
				//Proy-31949 Fin
	 }
			
	function f_ConsultaFP()
	{
		for(i=1; i<=3; i++)
		{   
			eval("vv=document.frmPrincipal.ddlTipoDocumento"+i+".value")
			if (vv !="")
			{
				if(i==1){document.frmPrincipal.hidF1.value=vv}
				if(i==2){document.frmPrincipal.hidF2.value=vv}
				if(i==3){document.frmPrincipal.hidF3.value=vv}
			}
		}
	} 
	
			//PROY-31949 INICIO
	function f_bloqueo_fila(objCombo,intFila)
	{	
				varIntPos = 0;
		switch (intFila)
		{
			case 1:
						varNameLinkPOS="LnkPos1";
						varNameImgPOS="icoTranPos1";
						varNameLinkDelPOS="LnkDelPos1";
						varNameImgDelPOS="icoDelPos1";
				varNameTipoPOS = "HidTipPOS1";
				varNameTdoc = "txtDocumento1";
				//CNH
				varNameTxtDoc = "txtDocumento1";				
						varLnkPrintPos='#LnkPrintPos1';
						varNameImgPrint='#icoPrintPOS1';
						ContPago1 = 1;
			break;
			case 2:
						varNameLinkPOS="LnkPos2";
						varNameImgPOS="icoTranPos2";
						varNameLinkDelPOS="LnkDelPos2";
						varNameImgDelPOS="icoDelPos2";
				varNameTipoPOS = "HidTipPOS2";
				varNameTdoc = "txtDocumento2";
				//CNH
				varNameTxtDoc = "txtDocumento2";
						varLnkPrintPos='#LnkPrintPos2';
						varNameImgPrint='#icoPrintPOS2';
						ContPago2 = 1;
			break;
			case 3:
						varNameLinkPOS="LnkPos3";
						varNameImgPOS="icoTranPos3";
						varNameLinkDelPOS="LnkDelPos3";
						varNameImgDelPOS="icoDelPos3";
				varNameTipoPOS = "HidTipPOS3";
				varNameTdoc = "txtDocumento3";
				//CNH
				varNameTxtDoc = "txtDocumento3";
						varLnkPrintPos='#LnkPrintPos3';
						varNameImgPrint='#icoPrintPOS3';
						ContPago3 = 1;
					break;
				}
				
				$('#LnkPos'+intFila).fadeOut('fast');
				$('#LnkPrintPos'+intFila).fadeOut('fast');
				$('#'+varNameLinkDelPOS).fadeOut('fast');
				document.getElementById(varNameTxtDoc).value = "";
				document.getElementById(varNameTxtDoc).disabled = false;
				
				varBolGetTarjeta = false;
				varIntAutPos = document.getElementById("HidIntAutPos").value;
				var TipTarjetaSel;
				
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
					$('#LnkPos'+intFila).fadeIn('slow');
					$('#LnkPrintPos'+intFila).fadeOut('fast');
					$('#'+varNameLinkDelPOS).fadeIn('slow');
					document.getElementById(varNameTxtDoc).value = "";
					document.getElementById(varNameTxtDoc).disabled = true;		
	}
			}
			//PROY-31949 FIN
	
			function f_activar_fila(intFila,bolEnable)
			{
				document.getElementById("lblEnvioPos").innerHTML  = "";
				document.getElementById("txtMonto" + intFila).disabled = bolEnable;
				//document.getElementById("txtDocumento" + intFila).disabled = bolEnable;
				//CNH
				document.getElementById("txtDocumento" + intFila).disabled = true;
				document.getElementById("ddlTipoDocumento" + intFila).disabled = bolEnable;
				
				var objMonto=document.getElementById("txtMonto" + intFila);
		var objTipo=document.getElementById("ddlTipoDocumento" + intFila);
		var objDoc=document.getElementById("txtDocumento" + intFila);
				
				//Proy-31949 Inicio
				$('#LnkPos'+intFila).fadeIn('slow');
				$('#LnkPos'+intFila).prop('disabled',false);
				document.getElementById("icoTranPos" + intFila).src = "../images/send-icon.png";
		
				$('#LnkPrintPos'+intFila).fadeOut('fast');
				
				document.getElementById(varNameLinkDelPOS).disabled=false;
				document.getElementById("icoDelPos" + intFila).src = "../images/delete-icon.png";
				//Proy-31949 Fin
			}
		
			function f_EnvioPOS(intFila,TagOpcion)
			{
				//Proy-31949-Inicio
				if(document.getElementById("HidFlagCajaCerrada").value=='1'){
					alert(document.getElementById("HidMsjCajaCerrada").value); 
					return; 
				}
			  
				MaxNumIntentosPago = Number(document.getElementById("HidNumIntentosPago").value);
				MaxNumIntentosAnular = Number(document.getElementById("HidNumIntentosAnular").value);
				//Proy-31949-Fin
			  
				varTransMC = ''; 
				varMonedaMC = '';
				varApliMC = ''; 
				varNroRefMC = ''; 
				varNroRefVisa = ''; 
				varIdRefAnu ='';			  
				varTransVisa = '';
				varMonedaVisa = '';
				var varTramaInsert='';
				varCodOpe = '';
				var varDescriOpe = '';
				varTipoTrans = '';
				var TipTarjeta = '';
				var varCodPtaWS = '';
				
				varArrayCodOpe=document.getElementById("HidCodOpera").value.split("|");//Proy-31949
				varArrayDesOpe=document.getElementById("HidDesOpera").value.split("|");//Proy-31949
				varArrayCodTarjeta=document.getElementById("HidTipoTarjeta").value.split("|");//Proy-31949
				varArrayTipoPOS=document.getElementById("HidTipoPOS").value.split("|");//Proy-31949
				varArrayTipoTran=document.getElementById("HidTipoTran").value.split("|");//Proy-31949
				varArrayEstTrans=document.getElementById("HidEstTrans").value.split("|");	
				
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
				varCodOpe = varArrayCodOpe[1]; //RECAUDACION
				varDescriOpe = varArrayDesOpe[1];
						varTipoTrans= varArrayTipoTran[0];//PAGO
						varNroTarjeta=document.getElementById("txtDocumento" + intFila).value;//Proy-31949
						varNroRef='';//Proy-31949
						
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
						varNroTarjeta = document.getElementById("txtDocumento" + intFila).value;//Proy-31949
						eval("IntentosAnular=ContAnular"+intFila);//Proy-31949				
				
						if(varValueTar == 'VIS')
						{
							var varArrayMonedaVisa = document.getElementById("HidMonedaVisa").value.split("|");
							varMonedaVisa = varArrayMonedaVisa[0];//SOLES VISA
							
							if(document.getElementById("HidFila" + intFila).value == ''){
								if(IntentosAnular <= MaxNumIntentosAnular){
								alert('No tiene numero de referencia para eliminar la transaccion');
									document.getElementById("lblEnvioPos").innerHTML  = "";
								}
								return;
							}
							else{
								var varArrayFila = document.getElementById("HidFila" + intFila).value.split("|");
								varNroRefVisa = varArrayFila[3].substr(varArrayFila[3].indexOf("=")+1);
								varNroRef = varNroRefVisa;//Proy-31949
								if(IntentosAnular <= MaxNumIntentosAnular){
						alert('Nro de referencia: ' + varNroRefVisa);
							}
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
								if(IntentosAnular <=  MaxNumIntentosAnular){
								alert('No tiene numero de referencia para eliminar la transaccion');
								}
								return;
							}
							else{
								var varArrayFila = document.getElementById("HidFila" + intFila).value.split("|");
								varNroRefMC = varArrayFila[3].substr(varArrayFila[3].indexOf("=")+1);
								varNroRef = varNroRefMC;//Proy-31949
								if(IntentosAnular <= MaxNumIntentosAnular){
									alert('Nro de referencia: ' + varNroRefMC);
								}
							}
						}				
						break;
					case "Imprimir POS":
				varCodOpe = varArrayCodOpe[1];//RECAUDACION
				varDescriOpe = varArrayDesOpe[1];
						varTipoTrans= varArrayTipoTran[2];//REEIMPRESION DE VOUCHER
						varNroTarjeta = document.getElementById("txtDocumento" + intFila).value;//Proy-31949
						
						if(varValueTar == 'VIS')
						{
						    var varArrayFila = document.getElementById("HidFila" + intFila).value.split("|"); //PROY-31949 
							var varArrayMonedaVisa = document.getElementById("HidMonedaVisa").value.split("|");
							varMonedaVisa = varArrayMonedaVisa[0];//SOLES VISA
							varNroRef = varArrayFila[3].substr(varArrayFila[3].indexOf("=")+1);//Proy-31949
						}
						else
						{
							var varArrayTranMC = document.getElementById("HidTransMC").value.split("|");
							varTransMC = varArrayTranMC[4];//11
							varMonedaMC = '';
							
							if(document.getElementById("HidFila" + intFila).value == ''){
								alert('No tiene numero de referencia para re-imprimir la transaccion');
								document.getElementById("lblEnvioPos").innerHTML  = "";//Proy-31949
								return;
							}
							else{
								var varArrayFila = document.getElementById("HidFila" + intFila).value.split("|");
								varNroRefMC = varArrayFila[3].substr(varArrayFila[3].indexOf("=")+1);
								varNroRef = varNroRefMC;//Proy-31949
							}
						}				
						break;
				}
		
				var varNameDelFila; 
				var varNameTipoDoc; 
				var varNameMonto;
				var vaNameLink;
				varNameTipoPOS='';
				varNameDoc=''
		
				switch (intFila)
				{
					case 1:
						varNameLinkPOS="LnkPos1";
						varNameImgPOS="icoTranPos1";
						varNameLinkDelPOS="LnkDelPos1";
						varNameImgDelPOS="icoDelPos1";
						varNameDelFila="ImgEliminar1";
						varNameTipoDoc="ddlTipoDocumento1";
						varNameDoc="txtDocumento1";
						varNameMonto="txtMonto1";
						vaNameLink="IdLink1";
						varNameTipoPOS = "HidTipPOS1";
						varLnkPrintPos='#LnkPrintPos1';//Proy-31949
						varNameImgPrint='#icoPrintPOS1';//Proy-31949
					break;
					case 2:
						varNameLinkPOS="LnkPos2";
						varNameImgPOS="icoTranPos2";
						varNameLinkDelPOS="LnkDelPos2";
						varNameImgDelPOS="icoDelPos2";
						varNameDelFila="ImgEliminar2";
						varNameTipoDoc="ddlTipoDocumento2";
						varNameDoc="txtDocumento2";
						varNameMonto="txtMonto2";
						vaNameLink="IdLink2";
						varNameTipoPOS = "HidTipPOS2";
						varLnkPrintPos='#LnkPrintPos2';//Proy-31949
						varNameImgPrint='#icoPrintPOS2';//Proy-31949
					break;
					case 3:
						varNameLinkPOS="LnkPos3";varNameImgPOS="icoTranPos3";
						varNameLinkDelPOS="LnkDelPos3";varNameImgDelPOS="icoDelPos3";
						varNameDelFila="ImgEliminar3";
						varNameTipoDoc="ddlTipoDocumento3";
						varNameDoc="txtDocumento3";
						varNameMonto="txtMonto3";
						vaNameLink="IdLink3";
						varNameTipoPOS = "HidTipPOS3";
						varLnkPrintPos='#LnkPrintPos3';//Proy-31949
						varNameImgPrint='#icoPrintPOS3';//Proy-31949
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
				//document.getElementById(vaNameLink).href = "javascript:void(0)";
				
				switch (TagOpcion)
				{
					case "Envio POS":
						//Proy-31949 Inicio
						document.getElementById(varNameLinkPOS).disabled = true;
						document.getElementById(varNameImgPOS).src = "../images/send-icon_ena.png";				
						document.getElementById(varNameLinkDelPOS).disabled = true;
						document.getElementById(varNameImgDelPOS).src = "../images/delete-icon_ena.png";
						//Proy-31949 Fin
						break;
					case "Eliminar POS":				
						//Proy-31949 Inicio
						$(varLnkPrintPos).prop('disabled',true);
						$(varNameImgPrint).prop('src','../images/print-icon_ena.png');
						document.getElementById(varNameLinkDelPOS).disabled = true;
						document.getElementById(varNameImgDelPOS).src = "../images/delete-icon_ena.png";				
						//Proy-31949 Fin
						break;
					case "Imprimir POS":
						//Proy-31949 Inicio
						$(varLnkPrintPos).prop('disabled',true);
						$(varNameImgPrint).prop('src','../images/print-icon_ena.png');
						document.getElementById(varNameLinkDelPOS).disabled = true;
						document.getElementById(varNameImgDelPOS).src = "../images/delete-icon_ena.png";				
						//Proy-31949 Fin
						break;
				}
				
				var EntitySaveTransac;
				var EntityUpdateTransac;
				var soapMSG
				varTipoPos='';	
		var varArrayDatoPosVisa = document.getElementById("HidDatoPosVisa").value.split("|");
		var varArrayDatoPosMC = document.getElementById("HidDatoPosMC").value.split("|");
				varNroRegistro = ''; 
				varNroTienda = '';
				varCodigoCaja = ''; 
				varCodEstablec = '';
				varNomPcPos = ''; 
				varCodTerminal = '';
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
				
				var idRecaudacion = document.getElementById("hidTextIdentificador").value;//Proy-31949
				var varNroPedido = '';
				var varNroTelefono = '';
				varNroPedido = document.getElementById("hidTextIdentificador").value; 
				varNroTelefono = document.getElementById("hidTextIdentificador").value; 
				varTransMonto= '';
				varTransMonto = document.getElementById(varNameMonto).value;
				varMoneda='';
				varMoneda = document.getElementById("HidTipoMoneda").value;
				var varTipoPago = document.getElementById("HidTipoPago").value; //DOCUMENTOS POR PAGAR
				var varEstadoTrans= varArrayEstTrans[0];//PENDIENTE
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
				RSExecute(serverURL,"GuardarTransaction",varTramaInsert,varNroTelefono,idRecaudacion,CallBack_GuardarTransaction,GuardarTransactionError,"X");
	}
				
			//Proy-31949 Inicio
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
					document.getElementById("ddlTipoDocumento"+ varIntFila).value = varRespuesta.split(";")[0] ;
					varNameTipoPOS = "HidTipPOS" + varIntFila; 
						
					document.getElementById(varNameTipoPOS).value = varRespuesta.split(";")[1];

					document.getElementById("HidFila" + varIntFila).value = 'Monto=' + varMontoOperacion + 
					'|Tarjeta=' + varNroTarjeta + 
					'|ComboIndex=' + document.getElementById("ddlTipoDocumento" + varIntFila).selectedIndex + 
					'|NroReferncia=' + varNroRefVisa;	
				}
				else if (varCodRespuesta == "2")
				{
					alert(varRespuesta);
				}	
	}
			//Proy-31949 Fin
				
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
					var varNumVoucher='';
			var varNumAutTransaccion = '';
					var varCodRespTransaccion='';
					var varDescTransaccion='';
			var varCodAprobTransaccion = '';
					var varFechaExpiracion = '';
					var varNomCliente = '';
					var varImpVoucher = '';
			var varSeriePOS = varCodTerminal;
			var nomEquipoPOS = varNomPcPos;
			var varNroPedido = '';
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
					'|TipoPago=' + document.getElementById("HidTipoPago").value; +
					'|ResTarjetaPos=';			
			
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
						CodigoCaja: varCodigoCaja
					};
					
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
					document.getElementById("lblEnvioPos").innerHTML  = "";
			return;
		}
	}
			
	function GuardarTransactionError(co) 
	{
		if (co.message) {
			alert("Context:" + co.context + "\nError: " + co.message);
		}
		varRptTtipoPos='1|Error';		
				//Proy-31949 Inicio
				$('#'+varNameLinkPOS).fadeOut('slow');
				$('#'+varNameLinkDelPOS).fadeOut('slow');
				$(varLnkPrintPos).fadeOut('slow');
				//Proy-31949 Fin
	}
			
	function BloqueoCancelar()
	{
	try
	{
		var contPagosPos = 0;
	
		for (var i=1; i<=3; i ++)
		{
			if(document.getElementById('HidFila' + i).value.length >0)
			{
				contPagosPos = contPagosPos + 1;
			}
		}
		if (contPagosPos == 0)
		{
			document.getElementById("btnCancelar").disabled = false;
						//Proy-31949 Inicio
						blnClosingMod=false;
						//Proy-31949 Fin
		}
		else
		{
					document.getElementById("btnCancelar").disabled = true;
						//Proy-31949 Inicio
						blnClosingMod=true;
						//Proy-31949 Fin
				}
			}
	catch(err)
				{}
					}			
			
	function ErrorMasterCard(request,NameDoc,objEntityPOS)
	{
		try {
			var varClienteVisa = '';
      var varNumAutTransaccion = '';
      var varCodOperVisa = '';
      var varImpVoucher = '';      
			var varNroPedido = ''; 
			var varIdUnicoTrans = '';
			var varIdCabez = document.getElementById("HidIdCabez").value;
			var varFlagPago = '1';
					
			varMontoOperacion = objEntityPOS.montoOperacion;
      varNomEquipoPOS = varNomPcPos;
      varNroTarjeta = document.getElementById(varNameDoc).value;
      varNroReferencia = '';
					varNroReferencia = varNroRef;
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
					'|ResTarjetaPos=';					
			
			RSExecute(serverURL,"ActualizarTransaction",varTramaUpdate,
						objEntityPOS.TrnsnId,CallBack_ActualizarTransaction2,GuardarTransactionError,"X");
						
			if(varEstTran == '4' && varCodOpe == '05' &&  varTipoTrans == '1')
					{
				f_activar_fila(varIntFila,false);
			}
			else{
						//Proy-31949 Inicio
						document.getElementById("lblEnvioPos").innerHTML  = "";
						
						$('#LnkPos'+varIntFila).fadeOut('fast');
						$('#LnkPos'+varIntFila).prop('disabled',false);
						
						$(varLnkPrintPos).fadeIn('fast');
						$(varLnkPrintPos).prop('disabled',false);
						$(varNameImgPrint).prop('src','../images/print-icon.png');
						
						document.getElementById(varNameImgDelPOS).src = "../images/delete-icon.png";
						document.getElementById(varNameLinkDelPOS).disabled=false;						
						//Proy-31949 Fin
					}						
					
					//Proy-31949 Inicio
					if(varEstTran == varArrayEstTrans[4] && varTipoTrans == varArrayTipoTran[1])
					{
						document.getElementById("txtDocumento" + varIntFila).value = '';
						document.getElementById("HidFila" + varIntFila).value = '';
						document.getElementById("HidFila" + varIntFila).value = '';
						document.getElementById("txtDocumento" + varIntFila).disabled = true;
						varNroTarjeta = '';
						f_activar_fila(varIntFila,false);
					
						$('#LnkPos'+varIntFila).fadeIn('slow');
						$('#LnkPos'+varIntFila).prop('disabled',false);
					
						$('#LnkPrintPos'+varIntFila).fadeOut('fast');
						
						$('#LnkDelPos'+varIntFila).fadeIn('slow');
						$('#LnkDelPos'+varIntFila).prop('disabled',false);
					}
					//Proy-31949 Fin							
							
			var varIndex = NameDoc.substr(NameDoc.length -1,1);
			var varNomCliente = document.getElementById("txtIdentificadorCliente").value;
					var varNroTelefono = document.getElementById("txtIdentificadorCliente").value; 
					var varNroPedido = document.getElementById("txtIdentificadorCliente").value; 
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
						
							eval("document.getElementById('txtDocumento'+"+varIntFila+").disabled = false");
							$('#LnkPos'+varIntFila).fadeOut('fast');
							$('#LnkPrintPos'+varIntFila).fadeOut('fast');
							$('#LnkDelPos'+varIntFila).fadeOut('fast');		
					}
						eval("ContPago"+varIntFila+"=ContPago"+varIntFila+"+1");			
			}			
					//Proy-31949-Fin		
		}
		catch(err){
			alert(err.description);
					f_activar_fila(varIntFila,false);
					document.getElementById("lblEnvioPos").innerHTML  = "";
		}	
	}
			
	function ErrorVisaNet(request,NameDoc,objEntityPOS)
	{
		try {
			var varClienteVisa = '';
      var varNumAutTransaccion = '';
      var varCodOperVisa = '';
      var varImpVoucher = '';
			var varNroPedido = ''; 
			var varIdUnicoTrans = '';
			var varIdCabez = document.getElementById("HidIdCabez").value;
			var varFlagPago = '1';

      varMontoOperacion = objEntityPOS.montoOperacion;
      varNomEquipoPOS = varNomPcPos;
      varNroTarjeta = document.getElementById(varNameDoc).value;
      varNroReferencia = '';
					varNroReferencia = varNroRef;
			varNumAutTransaccion = '';
			varFechaExpiracion = '';
			varCodOperVisa = '';
			varSeriePOS = '';
			varImpVoucher = '';
			sCodRespTarj = '';
			
					//Proy-31949 Inicio
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
					//Proy-31949 Fin
			
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
					'|ResTarjetaPos=';						
						
			RSExecute(serverURL,"ActualizarTransaction",varTramaUpdate,
						objEntityPOS.TrnsnId,CallBack_ActualizarTransaction2,GuardarTransactionError,"X");
						
			if(varEstTran == '4' && varCodOpe == '05' &&  varTipoTrans == '1')
			{
				f_activar_fila(varIntFila,false);
						}
						else{
						//Proy-31949 Inicio
				document.getElementById("lblEnvioPos").innerHTML  = "";
						
						$('#LnkPos'+varIntFila).fadeOut('fast');
						
						$(varLnkPrintPos).fadeIn('fast');
						$(varLnkPrintPos).prop('disabled',false);
						$(varNameImgPrint).prop('src','../images/print-icon.png');
						
				document.getElementById(varNameImgDelPOS).src = "../images/delete-icon.png";
						document.getElementById(varNameLinkDelPOS).disabled=false;
						//Proy-31949 Fin
						}
						
					//Proy-31949 Inicio
					if(varEstTran == varArrayEstTrans[4] && varTipoTrans == varArrayTipoTran[1])
					{
						document.getElementById("txtDocumento" + varIntFila).value = '';
						document.getElementById("HidFila" + varIntFila).value = '';
						document.getElementById("HidFila" + varIntFila).value = '';
						document.getElementById("txtDocumento" + varIntFila).disabled = true;
						varNroTarjeta = '';
						f_activar_fila(varIntFila,false);
						
						$('#LnkPos'+varIntFila).fadeIn('slow');
						$('#LnkPos'+varIntFila).prop('disabled',false);
						
						$('#LnkPrintPos'+varIntFila).fadeOut('fast');
						
						$('#LnkDelPos'+varIntFila).fadeIn('slow');
						$('#LnkDelPos'+varIntFila).prop('disabled',false);
					}
					//Proy-31949 Fin
								
					var varIndex = NameDoc.substr(NameDoc.length -1,1);
					var varNomCliente = document.getElementById("hidTextIdentificador").value;
					var varNroTelefono = document.getElementById("hidTextIdentificador").value;
					var varNroPedido = document.getElementById("hidTextIdentificador").value;
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
							
							eval("document.getElementById('txtDocumento'+"+varIntFila+").disabled = false");
							$('#LnkPos'+varIntFila).fadeOut('fast');
							$('#LnkPrintPos'+varIntFila).fadeOut('fast');
							$('#LnkDelPos'+varIntFila).fadeOut('fast');		
					}
						eval("ContPago"+varIntFila+"=ContPago"+varIntFila+"+1");			
			}
					//Proy-31949-Fin
		}
		catch(err){
			alert(err.description);
					document.getElementById("lblEnvioPos").innerHTML  = "";
					f_activar_fila(varIntFila,false);
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
					var varNroPedido = document.getElementById("txtIdentificadorCliente").value; 
			var varIdUnicoTrans = '';
			var varFlagPago = '1';
			var varIdCabez = document.getElementById("HidIdCabez").value;
					var varRespTipTarPos = '';//Proy-31949
		            
			varCodRptaAudit = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.AuditResponse.CodigoRespuesta;
			varMsjRptaAudit = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.AuditResponse.mensajeRespuesta;	
			varCodRptaWs = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.CodigoRespuesta;
			
								/*VALIDAR OPCION RE-IMPRESION*/
			if (varTipoTrans == '3'){				            			
									varNroTarjeta = document.getElementById(varNameDoc).value;
									varNroReferencia = '';
						varNroReferencia = varNroRef;
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
					else{
						if((varCodRptaAudit !='0' && typeof varCodRptaWs == 'undefined')|| varCodRptaWs == '1') {
										varDesRpta = jsonObj.Envelope.Body.RespuestaPeticionOperacionVisaNet.ImprimirData;
										varNroTarjeta = document.getElementById(varNameDoc).value;
										varNroReferencia = '';
							varNroReferencia = varNroRef;
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
							varNroRef=varNroRefVisa;
										
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
					'|ResTarjetaPos=';					
														
					var varRpta = RSExecute(serverURL,"ActualizarTransaction",varTramaUpdate,objEntityPOS.TrnsnId);
			
			if(varEstTran == '4' && varCodOpe == '05' &&  varTipoTrans == '1')
			{
				//RECHAZADO VENTA
				f_activar_fila(varIntFila,false);
				alert(varMsgAlert);				
			}
			else if ((varEstTran == '3' || varEstTran == '' || varEstTran == '4')  && varCodOpe == '05'){
				//ACEPTADO VENTA
						//Proy-31949 Inicio
			document.getElementById("lblEnvioPos").innerHTML  = "";
			
						$('#LnkPos'+varIntFila).fadeOut('fast');
			
						$(varLnkPrintPos).fadeIn('slow');
						$(varLnkPrintPos).prop('disabled',false);
						$(varNameImgPrint).prop('src','../images/print-icon.png');
			
						$('#'+varNameLinkDelPOS).fadeIn('fast');
						document.getElementById(varNameImgDelPOS).src = "../images/delete-icon.png";
						document.getElementById(varNameLinkDelPOS).disabled=false;
			
						FN_LoadPOS(varIntFila,'+');
						//Proy-31949 Fin					
					}			
					//ANULACION MASTERCARD Y VISANET					
			if (varCodOpe == '04' && (varEstTran == '3' || varEstTran == '5')){
				document.getElementById("HidFila" + varIntFila).value = '';
				f_activar_fila(varIntFila,false);
				document.getElementById("txtDocumento" + varIntFila).value = '';
				document.getElementById("HidFila" + varIntFila).value = '';
				varNroTarjeta = '';
				document.getElementById("txtDocumento" + varIntFila).disabled = true;
			
						//Proy-31949 Inicio
						$('#LnkPos'+varIntFila).fadeIn('slow');
						document.getElementById("LnkPos" + varIntFila).disabled = false;
						document.getElementById("icoTranPos" + varIntFila).src = "../images/send-icon.png";
			
						$(varLnkPrintPos).fadeOut('fast');
			
						$('#'+varNameLinkDelPOS).fadeIn('fast');
						document.getElementById(varNameLinkDelPOS).disabled = false;
						document.getElementById(varNameImgDelPOS).src = "../images/delete-icon.png";
			
						varNroTarjeta = '';
						if(varEstTran == '3')
						{varCont1 = 0; varCont2 = 0;varCont3 = 0;}
			
						FN_LoadPOS(varIntFila,'-');
						//Proy-31949 Fin	
					}					
					//ANULACION CANCELADA
			else if(varCodOpe == '04' && varEstTran == '4')
			{
						//Proy-31949 Inicio
				document.getElementById("lblEnvioPos").innerHTML  = "";
						
						$('#LnkPos'+varIntFila).fadeOut('fast');
						
						$(varLnkPrintPos).fadeIn('slow');
						$(varLnkPrintPos).prop('disabled',false);
						$(varNameImgPrint).prop('src','../images/print-icon.png');
						
						$('#'+varNameLinkDelPOS).fadeIn('fast');
						document.getElementById(varNameLinkDelPOS).disabled=false;						
				document.getElementById(varNameImgDelPOS).src = "../images/delete-icon.png";
						//Proy-31949 Fin
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
				'|ComboIndex=' + document.getElementById("ddlTipoDocumento" + varIndex).selectedIndex + 
				'|NroReferncia=' + varNroReferencia;
			}
			
			var varNomCliente = document.getElementById("txtIdentificadorCliente").value;
					var varNroTelefono = document.getElementById("txtIdentificadorCliente").value; 
					var varNroPedido = document.getElementById("txtIdentificadorCliente").value;
			
			var varTramaAudit = '';    
			varTramaAudit = 'NomCliente=' + varNomCliente + '|NroTelefono=' + varNroTelefono + 
			'|NroPedido=' + varNroPedido + '|IdTransaccion=' + objEntityPOS.TrnsnId + '|nMonto=' + varMontoOperacion ;
											
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
							
							eval("document.getElementById('txtDocumento'+"+varIntFila+").disabled = false");
							$('#LnkPos'+varIntFila).fadeOut('fast');
							$('#LnkPrintPos'+varIntFila).fadeOut('fast');
							$('#LnkDelPos'+varIntFila).fadeOut('fast');		
					}
						eval("ContPago"+varIntFila+"=ContPago"+varIntFila+"+1");			
			}
					//Proy-31949-Fin
		}
		catch(err) {
					document.getElementById("lblEnvioPos").innerHTML  = "";
			alert(err.description);
					f_activar_fila(varIntFila,false);//Proy-31949
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
					var varNroPedido = document.getElementById("txtIdentificadorCliente").value; 
			var varIdUnicoTrans = '';
			var varFlagPago = '1';
			var varIdCabez = document.getElementById("HidIdCabez").value;
					var varRespTipTarPos = '';//Proy-31949
			
			varCodRptaAudit = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.AuditResponse.CodigoRespuesta;
			varMsjRptaAudit = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.AuditResponse.mensajeRespuesta;	
			varCodRptaWs = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.CodigoRespuesta;
			
			/*VALIDAR OPCION RE-IMPRESION*/
			if (varTipoTrans == '3'){			
				sCodRespTarj = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.CodigoRespuesta;
				varDesRpta = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.DescripcionRespuesta;					
								varNroTarjeta = document.getElementById(varNameDoc).value;
								varNroReferencia = '';
						varNroReferencia = varNroRef;
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
							varNroReferencia = varNroRef;
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
								varNroRef = trim1(varNroRef);
						varIdRefAnu = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.NumeroReferencia;
						varIdRefAnu = (varIdRefAnu == null) ? '' : String(varIdRefAnu).replace("REF","");						
					}
					else{
						varNroRefVisa = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.NumeroReferencia;
						varNroRefVisa = (varNroRefVisa == null) ? '' : String(varNroRefVisa).replace("REF","");
								varNroRef = trim1(varNroRefVisa);
								varRespTipTarPos = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.IdTarjeta;//Proy-31949
					}
		        
								varNroTarjeta = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.NumeroTarjeta;
					varFechaExpiracion = '';
					varCodOperVisa = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.CodigoAprobacion;
					varSeriePOS = '';
					varImpVoucher = '';
					varImpVoucher = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.ImprimirData;
							varNroReferencia = varNroRef;
								
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
					'|ResTarjetaPos=' + varRespTipTarPos;//Proy-31949									
			
					var varRpta = RSExecute(serverURL,"ActualizarTransaction",varTramaUpdate,objEntityPOS.TrnsnId,CallBack_ActTransMCD);
								
			if(varEstTran == '4' && varCodOpe == '05' &&  varTipoTrans == '1')
			{
				//RECHAZADO VENTA
				f_activar_fila(varIntFila,false);
				alert(varMsgAlert);				
			}
			else if ((varEstTran == '3' || varEstTran == '' || varEstTran == '4')  && varCodOpe == '05'){
				//ACEPTADO VENTA
						//Proy-31949 Inicio
				document.getElementById("lblEnvioPos").innerHTML  = "";
						
						$('#LnkPos'+varIntFila).fadeOut('fast');
						
						$(varLnkPrintPos).fadeIn('slow');
						$(varLnkPrintPos).prop('disabled',false);
						$(varNameImgPrint).prop('src','../images/print-icon.png');
						
						$('#'+varNameLinkDelPOS).fadeIn('fast');
				document.getElementById(varNameImgDelPOS).src = "../images/delete-icon.png";
						document.getElementById(varNameLinkDelPOS).disabled=false;
						
						FN_LoadPOS(varIntFila,'+');	
						//Proy-31949 Fin
			}			
			//ANULACION MASTERCARD Y VISANET
			if (varCodOpe == '04' && (varEstTran == '3' || varEstTran == '5')){
				document.getElementById("HidFila" + varIntFila).value = '';
				f_activar_fila(varIntFila,false);
				document.getElementById("txtDocumento" + varIntFila).value = '';
				document.getElementById("HidFila" + varIntFila).value = '';
				varNroTarjeta = '';
				document.getElementById("txtDocumento" + varIntFila).disabled = true;
				
						//Proy-31949
						$('#LnkPos' + varIntFila).fadeIn('slow');
						$('#LnkPos' + varIntFila).prop('disabled',false);
						document.getElementById("icoTranPos" + varIntFila).src = "../images/send-icon.png";
						
						$(varLnkPrintPos).fadeOut('fast');
						$(varLnkPrintPos).prop('disabled',false);
						$(varNameImgPrint).prop('src','../images/print-icon.png');
						
						$('#'+varNameLinkDelPOS).fadeIn('fast');
						document.getElementById(varNameImgDelPOS).src = "../images/delete-icon.png";
						document.getElementById(varNameLinkDelPOS).disabled=false;
						
						varNroTarjeta = '';
						if(varEstTran == '3')
						{varCont1 = 0; varCont2 = 0;varCont3 = 0;}
						
						FN_LoadPOS(varIntFila,'-');
						//Proy-31949 Fin	
							}
			//ANULACION CANCELADA
			else if(varCodOpe == '04' && varEstTran == '4')
			{
						//Proy-31949 Inicio
				document.getElementById("lblEnvioPos").innerHTML  = "";

						$('#LnkPos'+varIntFila).fadeOut('fast');
						
						$(varLnkPrintPos).fadeIn('slow');
						$(varLnkPrintPos).prop('disabled',false);
						$(varNameImgPrint).prop('src','../images/print-icon.png');
						
						$('#'+varNameLinkDelPOS).fadeIn('fast');
						document.getElementById(varNameLinkDelPOS).disabled=false;
				document.getElementById(varNameImgDelPOS).src = "../images/delete-icon.png";
						//Proy-31949 Fin
				}
		    
			varNameTipoPOS = '';
				document.getElementById(NameDoc).value = varNroTarjeta;
					//var varIndex = NameDoc.substr(NameDoc.length -1,1);      
				varMontoOperacion = document.getElementById("txtMonto" + varIndex).value;
		    
			if(sCodRespTarj == '00' &&  varTipoTrans == '1' )
			{
				//VENTA
				document.getElementById("HidFila" + varIndex).value = 'Monto=' + varMontoOperacion + 
				'|Tarjeta=' + varNroTarjeta + 
				'|ComboIndex=' + document.getElementById("ddlTipoDocumento" + varIndex).selectedIndex + 
				'|NroReferncia=' + varNroReferencia;
			}
		    
				var varNomCliente = document.getElementById("txtNombreCliente").value;
				var varNroTelefono = document.getElementById("hidTextIdentificador").value; 
				var varNroPedido = document.getElementById("hidTextIdentificador").value; 
				
				var varTramaAudit = '';    
				varTramaAudit = 'NomCliente=' + varNomCliente + '|NroTelefono=' + varNroTelefono + 
				'|NroPedido=' + varNroPedido + '|IdTransaccion=' + objEntityPOS.TrnsnId + '|nMonto=' + varMontoOperacion ;
				
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
							
							eval("document.getElementById('txtDocumento'+"+varIntFila+").disabled = false");
							$('#LnkPos'+varIntFila).fadeOut('fast');
							$('#LnkPrintPos'+varIntFila).fadeOut('fast');
							$('#LnkDelPos'+varIntFila).fadeOut('fast');		
						}
						eval("ContPago"+varIntFila+"=ContPago"+varIntFila+"+1");			
							}	
					//Proy-31949-Fin							
		}
		catch(err) {
					document.getElementById("lblEnvioPos").innerHTML  = "";
					f_activar_fila(varIntFila,false);//Proy-31949
			alert(err.description);
		}	
	}
	
			function CallService(tipoPOS,NameDoc,objEntityPOS){
		varBolWsLocal = false;
		varIdRefAnu = '';
		var entityOpe;
		var soapMSG;
		var soapDataUpdate;
		var varMontoOperacion;		
		var varFechaExpiracion;
		var varSeriePOS;
		var varNomEquipoPOS;
		var VarTrnsnId = '';
		var VarToday = '';	
		var varNroReferencia = '';
				varTipOpePOS = ''; 
				varEstTran = '';
		
		//Variables de auditoria Ini		
		var varArrayAudi = document.getElementById("HidDatoAuditPos").value.split("|");
		var varNroPedido = '';
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
						BloqueoCancelar();
						/*Fin success*/
							},error: function (request, status) {
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
						BloqueoCancelar();
						/*Fin success*/
							},error: function (request, status) {
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
				if (co.message) {
					alert("Context:" + co.context + "\nError: " + co.message);
				}
			}
			//PROY-27440 FIN
		
        	function f_Grabar() {
        		blnClosingMod = false;
				if ( document.getElementById("txtMonto1") == null ) {
					alert('Ingresar el Monto a Pagar.');
					return false;
				}
				if (! f_Validar()) return false;				
				
				CalculoVuelto();

				if ( frmPrincipal.txtVuelto.value*1 < 0 ) {
					alert('Se debe realizar el Pago Completo de la Recarga Virtual DTH.');
					return false;
				}
				
				if (! f_ValidarTarjeta()) return false;
				document.getElementById('hidNroFilas').value = f_NumeroFilas();
				
				if (document.getElementById('hidFlagCliente').value == "N") {
					if (document.getElementById('hidTipoDocCliente').value == "06") {
						//if ( !ValidaCampo('document.frmPrincipal.txtRazonSocial','Debe ingresar Razón Social.') ) return false;
						if (!ValidaAlfanumerico('document.frmPrincipal.txtNombres','el campo Razón Social ',false)) return false;
					} else {
						/*
						if ( !ValidaCampo('document.frmPrincipal.txtNombres','Debe ingresar nombres del Cliente.') ) return false;
						if ( !ValidaCampo('document.frmPrincipal.txtApellidosMat','Debe ingresar apellido Materno del Cliente.') ) return false;					
						if ( !ValidaCampo('document.frmPrincipal.txtApellidosPat','Debe ingresar apellido Paterno del Cliente.') ) return false;					
						*/
						if (!ValidaAlfanumerico('document.frmPrincipal.txtNombres','el campo nombres ',false)) return false;
						if (!ValidaAlfanumerico('document.frmPrincipal.txtApellidosMat','el campo apellido Materno ',false)) return false;
						if (!ValidaAlfanumerico('document.frmPrincipal.txtApellidosPat','el campo apellido Paterno ',false)) return false;
					}
				}
				
				if (confirm("¿Esta seguro que desea realizar el Pago?")) {
					HabilitarBotones(false);
					return true;					
				} else {
					return false;
				}
			}

			function f_Validar() {

				var i;
				for ( i=0; i<document.frmPrincipal.elements.length; i++ ) {
					if ( document.frmPrincipal.elements[i].name.substring(0,8) == "txtMonto" ) {
						if ( document.frmPrincipal.elements[i].value != "" ) {
							if ( !ValidaDecimal("document.frmPrincipal." + document.frmPrincipal.elements[i].name,'El campo Monto a Pagar debe tener el formato 0.00',false) ) return false;
						}
					}

					if ( document.frmPrincipal.elements[i].name.substring(0,16) == "ddlTipoDocumento" ) {
						if ( document.frmPrincipal.elements[i+2].value != "" ) {
							if ( document.frmPrincipal.elements[i].value == "" ) {
								
								document.frmPrincipal.elements[i].focus();
								alert('Debe de seleccionar una forma de pago.');
								return false;
							}
						}
						if ( document.frmPrincipal.elements[i].value != "" ) {
							if ( document.frmPrincipal.elements[i+2].value == "" ) {

								document.frmPrincipal.elements[i+2].focus();
								alert('Debe ingresar un monto de pago.');
								return false;
							}
						}
					}
				}
				return true;
			}

			function f_NumeroFilas(){
				var i, contador;
				contador = 0;			
				for( i=1; i<4; i++ ) {
					eval("monto=document.frmPrincipal.txtMonto" + i + ".value");	
					if( monto != '' )
						contador = contador + 1;
				}

				return contador;
			}
			
			function f_ValidarTarjeta() {
                                 //PROY-27440
	                         var varIntAutPos = document.getElementById("HidIntAutPos").value;

	 	                 if(varIntAutPos != '1')
		                 {	
	                         //PROY-27440
				var i;
				for( i=0; i < document.frmPrincipal.elements.length; i++ ) {
					if( document.frmPrincipal.elements[i].name.substring(0,16) == "ddlTipoDocumento" ) {
						if ( document.frmPrincipal.elements[i].value != "" ) {
							if ( document.frmPrincipal.hidTarjetaCredito.value.indexOf(document.frmPrincipal.elements[i].value) >= 0 ) {
								if ( document.frmPrincipal.elements[i+1].value != "" ){
									if ( document.frmPrincipal.hidBIN.value.indexOf(document.frmPrincipal.elements[i+1].value.substr(0,4) ) < 0 ) {
										return confirm('El prefijo de la tarjeta no se encuentra registrado. Desea Continuar ?');
									}
								}
								else{
									document.frmPrincipal.elements[i+1].focus();
									alert('Debe ingresar el número de la tarjeta ...');
									return false;
								}
							}
						}
					}
				}
			//PROY-27440
		        } 
                        else
		        {
			        for(i=1; i<4; i++)
			        {
				        eval("cboTipotar=document.frmPrincipal.ddlTipoDocumento"+i+".value");
							        
				        if (cboTipotar != "" && document.frmPrincipal.hidTarjetaCredito.value.indexOf(cboTipotar)>=0)
				        {
			                eval("numTar=document.frmPrincipal.txtDocumento"+i+".value");
					        if(numTar == "" )
					        {
						        alert('Debe ingresar el número de la tarjeta...');
						        return false;
					        }					 
				        }
			        }		        
		        
				for(i=1; i<4; i++)
				{
					eval("cboTipotar=document.frmPrincipal.ddlTipoDocumento"+i+".value");
								
					if (cboTipotar != "" && document.frmPrincipal.hidTarjetaCredito.value.indexOf(cboTipotar)>=0)
					{
						eval("numTar=document.frmPrincipal.txtDocumento"+i+".value");
						if(numTar != "" )
						{					
							if (document.frmPrincipal.hidBIN.value.indexOf(numTar.substr(0,4))< 0 )
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

			function ValidaNumero(obj){
				var keyAscii = window.event.keyCode;
				if ( keyAscii == 13 ) return;
				if ( !(keyAscii >= 46 && keyAscii <= 57) | (keyAscii == 46 && obj.value.indexOf(".") >= 0) ) {
					window.event.keyCode = 0;
				}
				else
				{
					if ( obj.value.indexOf(".") >= 0 ) {
						if ( keyAscii != 46  && obj.value.substring(obj.value.indexOf(".")+1,obj.value.length).length > 1 )
							window.event.keyCode = 0;
					}
				}
			}

			function f_Recalcular(obj){
				var suma;
				suma = 0.0;

				for( i=1; i<4; i++ ) {
					eval("monto=document.frmPrincipal.txtMonto"+i+".value");
					if( monto != '' ) {
						suma = suma + (monto*1);
					}
				}
				suma= Math.round(suma*100)/100;
				CalculoVuelto();
			}

			function CalculoVuelto(){
				var i, suma;
				suma = 0.0;

				for( i=1; i<4; i++ ){
					eval("monto=document.frmPrincipal.txtMonto"+i+".value");

					if( monto != '' ) {
						eval("tipoDoc=document.frmPrincipal.ddlTipoDocumento"+i+".value");
						if ( tipoDoc == 'ZEFE' )
							suma = suma + (monto*1);
					}
				}
				suma= Math.round(suma*100)/100;

				if ( frmPrincipal.txtRecibidoSoles.value == "" )
					frmPrincipal.txtRecibidoSoles.value = "0.00";
				if ( frmPrincipal.txtRecibidoUsd.value == "" )
					frmPrincipal.txtRecibidoUsd.value = "0.00";

				var tc = document.all.lblTC.innerText*1;
				//var vuelto = Math.round((frmPrincipal.txtRecibidoSoles.value * 1 + frmPrincipal.txtRecibidoUsd.value * tc - suma)*100)/100;//aotane 05.08.2013	
				var vuelto = Math.round((frmPrincipal.txtRecibidoSoles.value * 1 + frmPrincipal.txtRecibidoUsd.value * tc - suma)*1000)/1000;//aotane 05.08.2013	

				if ( vuelto < 0 )
					document.getElementById("tdVuelto").innerHTML = "<FONT color=FF0000>Faltante:</FONT>";
				else
					document.getElementById("tdVuelto").innerText = "Vuelto:";

				//frmPrincipal.txtVuelto.value = vuelto;				
				var vueltoRedondeo = Math.round(RedondeaInmediatoSuperior(vuelto)*100)/100;//aotane 05.08.2013		
				frmPrincipal.txtVuelto.value = vueltoRedondeo;//aotane 05.08.2013	
			}
			
			function HabilitarBotones(flag) {
				if ( flag ) {
					document.getElementById('tdBotones').style.display = 'block';
					document.getElementById('tdProcesar').style.display = 'none';
				} else {
					document.getElementById('tdBotones').style.display = 'none';
					document.getElementById('tdProcesar').style.display = 'block';				
				}
			}
			
			function Inicio() {
				var mensajeError;
				mensajeError = document.getElementById('hidMensaje').value;
									
				if ( mensajeError != "" ) {
					HabilitarBotones(true);
					alert(mensajeError);
				}
				document.getElementById('hidMensaje').value = "";
				
				// Validacion Datos del Cliente
				if (document.getElementById('hidFlagCliente').value == "N") {
					document.getElementById('trNombreCliente').style.display = 'none';
					if (document.getElementById('hidTipoDocCliente').value == "06") {
						document.getElementById('trRazonSocial').style.display = 'block';
					} else {
						document.getElementById('trNombres').style.display = 'block'; }
				}
			}
			function ValidaCampo (control, mensaje) {
				var cadena;
				var flag = true;
				eval("cadena = " + control + ".value");
				cadena = trim(cadena);
				if ( cadena == '00' || cadena == ''){
					eval("" + control + ".focus()");
					alert(mensaje);
					flag = false;
				}
				return flag;
			}
			//nhuaringa 21032012
			function obtieneFecVec(){
				document.getElementById('hidFecVencimiento').value = document.getElementById('tdFechavencimiento').innerText;
				//alert(document.getElementById('hidFecVencimiento').value);
			}

			//Proy-31949 Inicio
			var arrPOSValidation = [2];
			arrPOSValidation[0]=0;
			arrPOSValidation[1]=0;
			arrPOSValidation[2]=0;
			var blnClosingMod = false;
			window.onbeforeunload = FN_ExitModal;
			
			function FN_LoadPOS(intRow,strOption){	
				var intTotPOS=0;
				
				intRow=parseInt(intRow)-1;
						
				if(strOption=='+'){
					arrPOSValidation[intRow]=parseInt(arrPOSValidation[intRow])+1;
				}
				else{
					arrPOSValidation[intRow]=parseInt(arrPOSValidation[intRow])-1;
				}
				
				for(var row=0;row<3;row++){
					intTotPOS=intTotPOS+parseInt(arrPOSValidation[row]);
				}
				if(intTotPOS>0){
					blnClosingMod=true;
				}
				else{
					blnClosingMod=false;
				}
			}
			
			function FN_ExitModal(){  
				///control de cerrar la ventana///
				if(blnClosingMod == true) 
				{ 
					FN_ExitModal = false
					return "Tiene Transacciones realizadas con el POS, debe grabar o anular la Transacción antes de salir.";
				}
				else{
					return;
				}
			}
			
			$(document).on('click','#LnkPos1',function(){
				f_EnvioPOS(1,'Envio POS');
				return false;
			});
			$(document).on('click','#LnkPos2',function(){
				f_EnvioPOS(2,'Envio POS');
				return false;
			});
			$(document).on('click','#LnkPos3',function(){
				f_EnvioPOS(3,'Envio POS');
				return false;
			});
			$(document).on('click','#LnkDelPos1',function(){
				f_EnvioPOS(1,'Eliminar POS');
				return false;
			});
			$(document).on('click','#LnkDelPos2',function(){
				f_EnvioPOS(2,'Eliminar POS');
				return false;
			});
			$(document).on('click','#LnkDelPos3',function(){
				f_EnvioPOS(3,'Eliminar POS');
				return false;
			});
			$(document).on('click','#LnkPrintPos1',function(){
				f_EnvioPOS(1,'Imprimir POS')
				return false;
			});
			$(document).on('click','#LnkPrintPos2',function(){
				f_EnvioPOS(2,'Imprimir POS')
				return false;
			});
			$(document).on('click','#LnkPrintPos3',function(){
				f_EnvioPOS(3,'Imprimir POS')
				return false;
			});
			//Proy-31949 Fin
						
			//INICIATIVA-318 INI
			function f_validaCajaCerrada() {
				document.getElementById('tdBotones').style.disabled = true; //INC000004584664
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
				document.getElementById('tdProcesar').style.display = 'block'; //INC000004584664
				
			}
			//INICIATIVA-318 FIN

		</script>
	</HEAD>
	<body onload="f_datos_POS();Inicio();obtieneFecVec();RedondearInicio2();">
		<form id="frmPrincipal" method="post" runat="server">
			<table border="0" cellSpacing="0" cellPadding="0" width="750">
				<tr>
					<td>
						<table class="tabla_borde" cellSpacing="2" cellPadding="1" width="750">
							<tr height="25">
								<td class="TituloRConsulta" align="center">Recaudación - Recarga Virtual DTH</td>
							</tr>
							<tr>
								<td width="750">
									<table class="Arial12b" cellSpacing="0" cellPadding="2" width="730">
										<tr height="25">
											<td class="Arial12br" width="550" colSpan="3">Datos Cliente</td>
											<td class="Arial12br" width="150" colSpan="2" align="right">Tipo de Cambio:
												<asp:label id="lblTC" runat="server" Font-Bold="True"></asp:label></td>
										</tr>
										<tr id="trNombreCliente">
											<td width="150">Nombre Cliente</td>
											<td width="10">:</td>
											<td width="550" colSpan="3"><asp:textbox id="txtNombreCliente" runat="server" CssClass="clsInputDisable" ReadOnly="True"
													Width="570px"></asp:textbox></td>
										</tr>
										<tr style="DISPLAY: none" id="trNombres">
											<td colSpan="5">
												<table class="Arial12b" cellSpacing="0" cellPadding="2">
													<tr>
														<td style="WIDTH: 130px" width="130">Nombres</td>
														<td width="5">:</td>
														<td width="100" colSpan="3"><asp:textbox id="txtNombres" onpaste="return false;" runat="server" CssClass="clsInputEnable"
																Width="200px" MaxLength="30"></asp:textbox></td>
													</tr>
													<tr>
														<td style="WIDTH: 137px" width="137">Apellido Materno</td>
														<td width="5">:</td>
														<td width="100" colSpan="3"><asp:textbox id="txtApellidosMat" onpaste="return false;" runat="server" CssClass="clsInputEnable"
																Width="300px" MaxLength="50"></asp:textbox></td>
													</tr>
													<tr>
														<td style="WIDTH: 130px" width="130">Apellido Paterno</td>
														<td width="5">:</td>
														<td width="100" colSpan="3"><asp:textbox id="txtApellidosPat" onpaste="return false;" runat="server" CssClass="clsInputEnable"
																Width="300px" MaxLength="50"></asp:textbox></td>
													</tr>
												</table>
											</td>
										</tr>
										<tr style="DISPLAY: none" id="trRazonSocial">
											<td colSpan="5">
												<table class="Arial12b" cellSpacing="0" cellPadding="2">
													<tr>
														<td style="WIDTH: 137px" width="137">Razón Social</td>
														<td width="5">:</td>
														<td width="100" colSpan="3"><asp:textbox id="txtRazonSocial" runat="server" CssClass="clsInputEnable" Width="300px" MaxLength="50"></asp:textbox></td>
													</tr>
												</table>
											</td>
										</tr>
										<tr>
											<td width="150">Cantidad Documentos</td>
											<td width="10">:</td>
											<td width="400"><asp:textbox id="txtNumeroDocumentos" runat="server" CssClass="clsInputDisable" ReadOnly="True"
													Width="50px"></asp:textbox></td>
											<td width="100">Valor Deuda:</td>
											<td width="50"><asp:textbox id="txtValorDeuda" runat="server" CssClass="clsInputDisable" ReadOnly="True" Width="50px"></asp:textbox></td>
										</tr>
									</table>
									<br>
									<table class="Arial12b" cellPadding="3">
										<colgroup>
											<col align="center" width="200">
											<col align="center" width="300">
											<col align="center" width="200">
										</colgroup>
										<thead>
											<tr>
												<td class="Arial12br" colSpan="3">Pagos</td>
											</tr>
											<tr align="center">
												<td class="ColumnHeader">Forma de Pago</td>
												<td class="ColumnHeader">Nro. Tarjeta/Nro Cheque</td>
												<td class="ColumnHeader">Monto a Pagar</td>
											</tr>
										</thead>
										<tbody>
											<tr class="RowEven">
												<td style="HEIGHT: 20px">
													<!--PROY-27440 INI--><asp:dropdownlist id="ddlTipoDocumento1" runat="server" CssClass="clsSelectEnable" onChange="javascript:MontoEfectivoRestriccion(f_CalculoDeuda_TablaMiles('tabla','5','2'),'txtMonto1','txtMonto1','txtMonto2','txtMonto3','ddlTipoDocumento1');RedondearEfectivo('txtMonto1','ddlTipoDocumento1');f_bloqueo_fila(this,1);"></asp:dropdownlist>
													<!--PROY-27440 FIN--></td>
												<td style="HEIGHT: 20px"><asp:textbox id="txtDocumento1" runat="server" CssClass="clsInputEnable" Width="147px"></asp:textbox></td>
												<td style="HEIGHT: 20px"><asp:textbox id="txtMonto1" onkeyup="javascript:MontoEfectivoRestriccion(f_CalculoDeuda_TablaMiles('tabla','5','2'),'txtMonto1','txtMonto1','txtMonto2','txtMonto3','ddlTipoDocumento1');RedondearEfectivo('txtMonto1','ddlTipoDocumento1');"
														runat="server" CssClass="clsInputEnable" Width="147px"></asp:textbox>
													<!--PROY-27440 INI--><A style="DISPLAY: none" id="LnkPos1" runat="server"><IMG style="CURSOR: hand" id="icoTranPos1" border="0" alt="Envio POS" src="../images/send-icon.png"
															runat="server"> </A>
													<!--PROY-31949 INI--><a style="DISPLAY: none" id="LnkPrintPos1"><IMG style="CURSOR: hand" id="icoPrintPOS1" border="0" alt="Imprimir" src="../images/print-icon.png">
													</a>
													<!--PROY-31949 INI--><A style="DISPLAY: none" id="LnkDelPos1"><IMG style="CURSOR: hand" id="icoDelPos1" border="0" alt="Eliminar POS" src="../images/delete-icon.png">
													</A><input id="HidTipPOS1" type="hidden" name="HidTipPOS1" runat="server"> 
													<!--PROY-27440 FIN--></td>
											</tr>
											<tr class="RowOdd">
												<td>
													<!--PROY-27440 INI--><asp:dropdownlist id="ddlTipoDocumento2" runat="server" CssClass="clsSelectEnable" onChange="javascript:MontoEfectivoRestriccion(f_CalculoDeuda_TablaMiles('tabla','5','2'),'txtMonto2','txtMonto1','txtMonto2','txtMonto3','ddlTipoDocumento2');RedondearEfectivo('txtMonto2','ddlTipoDocumento2');f_bloqueo_fila(this,2);"></asp:dropdownlist>
													<!--PROY-27440 FIN--></td>
												<td><asp:textbox id="txtDocumento2" runat="server" CssClass="clsInputEnable" Width="147px"></asp:textbox></td>
												<td><asp:textbox id="txtMonto2" onkeyup="javascript:MontoEfectivoRestriccion(f_CalculoDeuda_TablaMiles('tabla','5','2'),'txtMonto2','txtMonto1','txtMonto2','txtMonto3','ddlTipoDocumento2');RedondearEfectivo('txtMonto2','ddlTipoDocumento2');"
														runat="server" CssClass="clsInputEnable" Width="147px"></asp:textbox>
													<!--PROY-27440 INI--><A style="DISPLAY: none" id="LnkPos2"><IMG style="CURSOR: hand" id="icoTranPos2" border="0" alt="Envio POS" src="../images/send-icon.png">
													</A>
													<!--PROY-31949 INI--><a style="DISPLAY: none" id="LnkPrintPos2"><IMG style="CURSOR: hand" id="icoPrintPOS2" border="0" alt="Imprimir" src="../images/print-icon.png">
													</a>
													<!--PROY-31949 INI--><A style="DISPLAY: none" id="LnkDelPos2"><IMG style="CURSOR: hand" id="icoDelPos2" border="0" alt="Eliminar POS" src="../images/delete-icon.png">
													</A><input id="HidTipPOS2" type="hidden" name="HidTipPOS2" runat="server"> 
													<!--PROY-27440 FIN--></td>
											</tr>
											<tr class="RowEven">
												<td>
													<!--PROY-27440 INI--><asp:dropdownlist id="ddlTipoDocumento3" runat="server" CssClass="clsSelectEnable" onChange="javascript:MontoEfectivoRestriccion(f_CalculoDeuda_TablaMiles('tabla','5','2'),'txtMonto3','txtMonto1','txtMonto2','txtMonto3','ddlTipoDocumento3');RedondearEfectivo('txtMonto3','ddlTipoDocumento3');f_bloqueo_fila(this,3);"></asp:dropdownlist>
													<!--PROY-27440 FIN--></td>
												<td><asp:textbox id="txtDocumento3" runat="server" CssClass="clsInputEnable" Width="147px"></asp:textbox></td>
												<td><asp:textbox id="txtMonto3" onkeyup="javascript:MontoEfectivoRestriccion(f_CalculoDeuda_TablaMiles('tabla','5','2'),'txtMonto3','txtMonto1','txtMonto2','txtMonto3','ddlTipoDocumento3');RedondearEfectivo('txtMonto3','ddlTipoDocumento3');"
														runat="server" CssClass="clsInputEnable" Width="147px"></asp:textbox>
													<!--PROY-27440 INI--><A style="DISPLAY: none" id="LnkPos3"><IMG style="CURSOR: hand" id="icoTranPos3" border="0" alt="Envio POS" src="../images/send-icon.png">
													</A>
													<!--PROY-31949 INI--><a style="DISPLAY: none" id="LnkPrintPos3"><IMG style="CURSOR: hand" id="icoPrintPOS3" border="0" alt="Imprimir" src="../images/print-icon.png">
													</a>
													<!--PROY-31949 INI--><A style="DISPLAY: none" id="LnkDelPos3"><IMG style="CURSOR: hand" id="icoDelPos3" border="0" alt="Eliminar POS" src="../images/delete-icon.png">
													</A><input id="HidTipPOS3" type="hidden" name="HidTipPOS3" runat="server"> 
													<!--PROY-27440 FIN--></td>
											</tr>
											<!--PROY-27440 INI-->
											<tr>
												<td colSpan="7" align="center"><asp:label id="lblEnvioPos" runat="server" CssClass="TituloRConsulta"></asp:label></td>
											</tr>
											<!--PROY-27440 FIN--></tbody></table>
									<br>
									<table id="tabla" class="Arial12b" cellPadding="3" width="730">
										<thead>
											<tr>
												<td class="Arial12br" colSpan="7">Documentos</td>
											</tr>
											<tr>
												<td style="HEIGHT: 17px" class="ColumnHeader">Tipo Documento</td>
												<td style="HEIGHT: 17px" class="ColumnHeader">Número Documento</td>
												<td style="HEIGHT: 17px" class="ColumnHeader">Descripción Servicio</td>
												<td style="HEIGHT: 17px" class="ColumnHeader">Fecha Vigencia</td>
												<td style="HEIGHT: 17px" class="ColumnHeader">Moneda</td>
												<td style="HEIGHT: 17px" class="ColumnHeader">Total</td>
											</tr>
										</thead>
										<tbody>
											<%
										Dim arrRecibo, strClaseFila
										Dim cteRECIBO_CODIGOSERVICIO, _
											cteRECIBO_DESCRIPCIONSERVICIO, _
											cteRECIBO_MONEDASERVICIO, _
											cteRECIBO_TIPODOCUMENTO, _
											cteRECIBO_NUMERODOCUMENTO, _
											cteRECIBO_RECHAEMISION, _
											cteRECIBO_FECHAEMISION, _
											cteRECIBO_FECHAVENCIMIENTO, _
											cteRECIBO_MONTODOCUMENTO
											
											'''TODO: CAMBIADO POR JYMMY TORRES
											'''INDICES

											'cteRECIBO_CODIGOSERVICIO = 1
											'cteRECIBO_DESCRIPCIONSERVICIO = 2
											'cteRECIBO_MONEDASERVICIO = 3
											'cteRECIBO_TIPODOCUMENTO = 4
											'cteRECIBO_NUMERODOCUMENTO = 5
											'cteRECIBO_FECHAEMISION = 6
											'cteRECIBO_FECHAVENCIMIENTO = 7
											'cteRECIBO_MONTODOCUMENTO = 8
											
										cteRECIBO_CODIGOSERVICIO = 0
                                        cteRECIBO_DESCRIPCIONSERVICIO = 1
                                        cteRECIBO_MONEDASERVICIO = 2
                                        cteRECIBO_TIPODOCUMENTO = 3
                                        cteRECIBO_NUMERODOCUMENTO = 4
                                        cteRECIBO_FECHAEMISION = 5
                                        cteRECIBO_FECHAVENCIMIENTO = 6
                                        cteRECIBO_MONTODOCUMENTO = 7

										if not Recibos is nothing andalso Recibos.length>0 then
											'CARIAS : ordenamiento por Fec.Ven
											dim strTempo, strFila1, strFila2
											dim i,j
											for i = 0 to Me.Recibos.Length - 1
												for j= i + 1 to Me.Recibos.Length-1
													strFila1 = Me.Recibos(i).split(";")
													strFila2 = Me.Recibos(j).split(";")
													if strFila1(cteRECIBO_FECHAVENCIMIENTO) > strFila2(cteRECIBO_FECHAVENCIMIENTO) then
													strTempo = Me.Recibos(i)
													Me.Recibos(i) = Me.Recibos(j)
													Me.Recibos(j) = strTempo
													end if
												next
											next
											'CARIAS : fin de bloque

											for i = 0 to Me.Recibos.length-1
												if len(trim(Me.Recibos(i))) > 0 then
													arrRecibo = Me.Recibos(i).split(";")
													if i mod 2 = 0 then
														strClaseFila = "RowOdd"
													else
														strClaseFila = "RowEven"
													end if
													%>
											<tr class="<%=strClaseFila%>">
												<td><%=arrRecibo(cteRECIBO_TIPODOCUMENTO)%></td>
												<td><%=arrRecibo(cteRECIBO_NUMERODOCUMENTO)%></td>
												<td>SERVICIO DTH</td>
												<%-- nhuaringa21032012 --%>
												<td id="tdFechavencimiento"><%=FormatoFecha(arrRecibo(cteRECIBO_FECHAVENCIMIENTO),0)%></td>
												<td><%=FormatoMoneda(arrRecibo(cteRECIBO_MONEDASERVICIO),COD_MONEDA_SOLES)%></td>
												<td align="right"><%=FormatoMonto(arrRecibo(cteRECIBO_MONTODOCUMENTO)).tostring("N2")%>
													<input id="strRecibos" type="hidden" name="strRecibos" runat="server"></td>
											</tr>
											<%
												end if
											next
										end if
										%>
										</tbody>
									</table>
								</td>
							</tr>
						</table>
						<table cellSpacing="0" cellPadding="0" width="750" align="center">
							<tr height="2">
								<td>&nbsp;</td>
							</tr>
						</table>
						<table class="tabla_borde" cellSpacing="0" cellPadding="0" width="750" align="center">
							<tr height="2">
								<td colSpan="5">&nbsp;</td>
							</tr>
							<tr>
								<td class="Arial12b" width="30">&nbsp;</td>
								<td class="Arial12b" width="150">Importe Recibido Soles:</td>
								<td class="Arial12b" width="150"><asp:textbox id="txtRecibidoSoles" runat="server" CssClass="clsInputEnable" Width="150px"></asp:textbox></td>
								<td id="tdVuelto" class="Arial12b" width="100">Vuelto:</td>
								<td class="Arial12b" width="150"><asp:textbox id="txtVuelto" runat="server" CssClass="clsInputDisable" ReadOnly="True" Width="150px"></asp:textbox></td>
							</tr>
							<tr>
								<td class="Arial12b" width="30">&nbsp;</td>
								<td class="Arial12b" width="150">Importe Recibido Dolares:</td>
								<td class="Arial12b" width="150"><asp:textbox id="txtRecibidoUsd" runat="server" CssClass="clsInputEnable" Width="150px"></asp:textbox></td>
								<td class="Arial12b" width="100">&nbsp;</td>
								<td class="Arial12b" width="150">&nbsp;</td>
							</tr>
							<tr height="2">
								<td colSpan="5">&nbsp;</td>
							</tr>
						</table>
						<div id="divBotones">
							<table class="tabla_borde" cellSpacing="0" cellPadding="2" width="400" align="center">
								<tr id="tdBotones" align="center">
									<td width="100">&nbsp;</td>
									<td width="200"><asp:button id="btnGrabar" runat="server" CssClass="BotonOptm" Width="100px" Text="Grabar"></asp:button></td>
									&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<td width="100"><asp:button id="btnCancelar" runat="server" CssClass="BotonOptm" Width="100px" Text="Cancelar"></asp:button></td>
									<td width="100">&nbsp;</td>
								</tr>
								<tr style="DISPLAY: none" id="tdProcesar" align="center">
									<td class="Arial12br" colSpan="4">Espere&nbsp;Por&nbsp;Favor!&nbsp;El&nbsp;pago&nbsp;se&nbsp;está&nbsp;procesando&nbsp;...</td>
								</tr>
							</table>
						</div>
					</td>
				</tr>
			</table>
			<input id="txtIdentificadorCliente" type="hidden" name="txtIdentificadorCliente" runat="server">
			<input id="hidTipoIdentificador" type="hidden" name="hidTipoIdentificador" runat="server">
			<input id="hidTextIdentificador" type="hidden" name="hidTextoIdentificador" runat="server">
			<input id="hidNumeroTrace" type="hidden" name="hidNumeroTrace" runat="server"> <input id="hidTarjetaCredito" type="hidden" name="hidTarjetaCredito" runat="server">
			<input id="hidBIN" type="hidden" name="hidBIN" runat="server"> <input id="hidTipoDocCliente" type="hidden" name="hidTipoDocCliente" runat="server">
			<input id="hidNroDocCliente" type="hidden" name="hidNroDocCliente" runat="server">
			<input id="hidMonto" type="hidden" name="hidMonto" runat="server"> <input id="hidMensaje" type="hidden" name="hidMensaje" runat="server">
			<input id="hidNroFilas" type="hidden" name="hidNroFilas" runat="server"> <input id="hidNroPedido" type="hidden" name="hidNroPedido" runat="server">
			<input id="hidFlagCliente" type="hidden" name="hidFlagCliente" runat="server"> <input id="hidNroRecarga" type="hidden" name="hidNroRecarga" runat="server">
			<%-- nhuaringa21032012 --%>
			<input id="hidFecVencimiento" type="hidden" name="hidFlagCliente" runat="server">
			<!--PROY-27440 INI--><input id="HidPtoVenta" type="hidden" name="HidPtoVenta" runat="server">
			<input id="HidIntAutPos" type="hidden" name="HidIntAutPos" runat="server"> <input id="HidGrabAuto" type="hidden" name="HidGrabAuto" runat="server">
			<input id="HidCodOpera" type="hidden" name="HidCodOpera" runat="server"> <input id="HidDesOpera" type="hidden" name="HidDesOpera" runat="server">
			<input id="HidTipoOpera" type="hidden" name="HidTipoOpera" runat="server"> <input id="HidTipoTarjeta" type="hidden" name="HidTipoTarjeta" runat="server">
			<input id="HidTipoPago" type="hidden" name="HidTipoPago" runat="server"> <input id="HidEstTrans" type="hidden" name="HidEstTrans" runat="server">
			<input id="HidTipoPOS" type="hidden" name="HidTipoPOS" runat="server"> <input id="HidTipoTran" type="hidden" name="HidTipoTran" runat="server">
			<input id="HidFila1" type="hidden" name="HidFila1" runat="server"> <input id="HidFila2" type="hidden" name="HidFila2" runat="server">
			<input id="HidFila3" type="hidden" name="HidFila3" runat="server"> <input id="HidFila4" type="hidden" name="HidFila4" runat="server">
			<input id="HidFila5" type="hidden" name="HidFila5" runat="server"> <input id="HidTipoMoneda" type="hidden" name="HidTipoMoneda" runat="server">
			<input id="HidTransMC" type="hidden" name="HidTransMC" runat="server"> <input id="HidMonedaMC" type="hidden" name="HidMonedaMC" runat="server">
			<input id="HidApliPOS" type="hidden" name="HidApliPOS" runat="server"> <input id="HidMonedaVisa" type="hidden" name="HidMonedaVisa" runat="server">
			<input id="HidDatoPosVisa" type="hidden" name="HidDatoPosVisa" runat="server"> <input id="HidDatoPosMC" type="hidden" name="HidDatoPosMC" runat="server">
			<input id="HidDatoAuditPos" type="hidden" name="HidDatoAuditPos" runat="server">
			<input id="HidIdCabez" type="hidden" name="HidIdCabez" runat="server"> <input id="Hidden1" type="hidden" name="HidTipPOS1" runat="server">
			<input id="Hidden2" type="hidden" name="HidTipPOS2" runat="server"> <input id="Hidden3" type="hidden" name="HidTipPOS3" runat="server">
			<input id="hidF1" type="hidden" name="hidF1" runat="server"> <input id="hidF2" type="hidden" name="hidF2" runat="server">
			<input id="hidF3" type="hidden" name="hidF3" runat="server"> 
			<!--PROY-27440 FIN-->
			<!--PROY-31949 INI--><input id="HidNumIntentosPago" type="hidden" name="HidNumIntentosPago" runat="server">
			<input id="HidNumIntentosAnular" type="hidden" name="HidNumIntentosAnular" runat="server">
			<input id="HidMsjErrorNumIntentos" type="hidden" name="HidMsjErrorNumIntentos" runat="server">
			<input id="HidMsjErrorTimeOut" type="hidden" name="HidMsjErrorTimeOut" runat="server">
			<input id="HidMsjNumIntentosPago" type="hidden" name="HidMsjNumIntentosPago" runat="server">
			<input id="HidFlagCajaCerrada" type="hidden" name="HidFlagCajaCerrada" runat="server">
			<input id="HidMsjCajaCerrada" type="hidden" name="HidMsjCajaCerrada" runat="server">
			<input id="HidMedioPagoPermitidas" type="hidden" name="HidMedioPagoPermitidas" runat="server">
			<!--PROY-31949 FIN--></form>
		<script language="javascript">

			var esNavegador, esIExplorer;
			esNavegador = (navigator.appName == "Netscape") ? true : false;
			esIExplorer = ((navigator.appName.indexOf("Microsoft") != -1) || (navigator.appName.indexOf("MSIE") != -1)) ? true : false;

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
				case 1 : if (document.frmPrincipal.ddlTipoDocumento1.value == "ZDIN")
							largo = 13;
						break;
				case 2 : if (document.frmPrincipal.ddlTipoDocumento2.value == "ZDIN")
							largo = 13;
						break;
				case 3 : if (document.frmPrincipal.ddlTipoDocumento3.value == "ZDIN")
							largo = 13;
						break;
			}


				if (this.value.length > largo)
				event.keyCode=0;

			}

			if (esIExplorer) {
				document.frmPrincipal.txtDocumento1.onkeypress = f_LecBanda;
				document.frmPrincipal.txtDocumento2.onkeypress = f_LecBanda;
				document.frmPrincipal.txtDocumento3.onkeypress = f_LecBanda;
			}
		</script>
	</body>
</HTML>
