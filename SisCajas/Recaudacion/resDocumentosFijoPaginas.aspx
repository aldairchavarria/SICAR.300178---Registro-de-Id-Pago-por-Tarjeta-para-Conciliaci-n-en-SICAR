<%@ Page Language="vb" AutoEventWireup="false" Codebehind="resDocumentosFijoPaginas.aspx.vb" Inherits="SisCajas.resDocumentosFijoPaginas" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Documentos Cliente Fijo y Páginas</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="stylesheet" type="text/css" href="../Estilos/est_General.css">
		<script type="text/javascript" src="../librerias/Lib_FuncValidacion.js"></script>
		<script type="text/javascript" src="../librerias/Lib_FuncGenerales.js"></script>
		<script type="text/javascript" src="../librerias/Lib_Redondeo.js"></script>
		<!--PROY-27440 INI-->
		<script language="JavaScript" src="../librerias/jquery-1.8.2.min.js"></script> <!--PROY-31949-->
		<script language="JavaScript" src="../librerias/msrsclient.js"></script>
		<script language="JavaScript" src="../Scripts/form.js"></script>
		<script language="JavaScript" src="../Scripts/xml2json.js"></script>
		<script language="JavaScript" src="../Scripts/operacionPOS.js"></script>
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<!--PROY-27440 FIN-->
		<script language="javascript" type="text/javascript">
		    //Regularizacion 20.12.2012
			
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
			var varIdRefAnu;
			
			/*CNH INI*/
			var varNameTxtDoc;
			/*CNH FIN*/			
			//PROY-27440 FIN
			
			//PROY-27440 INI			
			function f_datos_POS()
			{
				varIntAutPos = document.getElementById("HidIntAutPos").value;
				
				if(varIntAutPos == '0'){
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
						document.getElementById('txtDoc' + i).value = varTarjeta;
						document.getElementById('cboTipDocumento' + i).selectedIndex = varComboIndex;
						
						document.getElementById('txtMonto' + i).disabled = true;
						document.getElementById('txtDoc' + i).disabled = true;
						document.getElementById('cboTipDocumento' + i).disabled = true;
						
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
						
						//Proy-31949 Inicio
						$('#'+varNameLinkPOS).fadeIn('slow');
						$('#'+varNameLinkDelPOS).fadeIn('slow');
						//Proy-31949 Fin
						
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
					
						//Proy-31949 Inicio
						$('#'+varNameLinkPOS).fadeOut('slow');
						$('#'+varNameLinkDelPOS).fadeOut('slow');
						//Proy-31949 Fin
					}
				}
				else
				{
					/*validacion IAPOS CNH*/							
					document.getElementById(varNameTxtDoc).value = "";
					document.getElementById(varNameTxtDoc).disabled = false;
				
					//Proy-31949 Inicio
					$('#'+varNameLinkPOS).fadeOut('slow');
					$('#'+varNameLinkDelPOS).fadeOut('slow');
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
				for(i=1; i<=1; i++)
				{
					eval("vv=document.frmPrincipal.cboTipDocumento"+i+".value")
					if (vv !="")
					{
						if(i==1){document.frmPrincipal.hidF1.value=vv}
					}
				}
			} 
			
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
						//CNH
						varNameTxtDoc = "txtDoc1";
						varLnkPrintPos='#LnkPrintPos1';//Proy-31949
						varNameImgPrint='#icoPrintPOS1';//Proy-31949
						ContPago1 = 1;//Proy-31949
					break;
					case 2:
						varNameLinkPOS="LnkPos2";
						varNameImgPOS="icoTranPos2";
						varNameLinkDelPOS="LnkDelPos2";
						varNameImgDelPOS="icoDelPos2";
						varNameTipoPOS = "HidTipPOS2";
						//CNH
						varNameTxtDoc = "txtDoc2";						
						varLnkPrintPos='#LnkPrintPos2';//Proy-31949
						varNameImgPrint='#icoPrintPOS2';//Proy-31949
						ContPago2 = 1;//Proy-31949
					break;
					case 3:
						varNameLinkPOS="LnkPos3";
						varNameImgPOS="icoTranPos3";
						varNameLinkDelPOS="LnkDelPos3";
						varNameImgDelPOS="icoDelPos3";
						varNameTipoPOS = "HidTipPOS3";
						//CNH
						varNameTxtDoc = "txtDoc3";						
						varLnkPrintPos='#LnkPrintPos3';//Proy-31949
						varNameImgPrint='#icoPrintPOS3';//Proy-31949
						ContPago3 = 1;//Proy-31949
					break;
				}
				
				$('#LnkPos'+intFila).fadeOut('slow');
				$('#LnkPrintPos'+intFila).fadeOut('fast');
				$('#'+varNameLinkDelPOS).fadeOut('fast');
				
				varBolGetTarjeta = false;
		        document.getElementById(varNameTxtDoc).value = "";
		        document.getElementById(varNameTxtDoc).disabled = false;
		        
		        varIntAutPos = document.getElementById("HidIntAutPos").value;
		        var TipTarjetaSel;
						
		        f_ConsultaFP();
		        
		        if(varIntAutPos != '1'){			
			        return;
		        }
		        
		        if(document.getElementById("HidMedioPagoPermitidas").value.indexOf(objCombo.value.substr(0,objCombo.value.length -2))< 0)
		        {				
			        return;		
		        }
		        else
		        {	
					var TarjetasPOS = document.getElementById("HidMedioPagoPermitidas").value.split("|");
			        var varArrayDatoPosVisa = document.getElementById("HidDatoPosVisa").value.split("|");
			        var varArrayDatoPosMC = document.getElementById("HidDatoPosMC").value.split("|");
						
			        for(i=0; i<TarjetasPOS.length; i++)
			        {
						if (objCombo.value.substr(0,objCombo.value.length -2) == TarjetasPOS[i].split(";")[0])
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
					$('#'+varNameLinkDelPOS).fadeIn('fast');
					document.getElementById(varNameTxtDoc).value = "";
					document.getElementById(varNameTxtDoc).disabled = true;		
			}
			}
			
			function f_activar_fila(intFila,bolEnable)
			{
				document.getElementById("lblEnvioPos").innerHTML  = "";
					
				//document.getElementById("txtMonto" + intFila).disabled = bolEnable;
				//document.getElementById("txtDoc" + intFila).disabled = bolEnable;
				//CNH
				document.getElementById("txtDoc" + intFila).disabled = true;				
				document.getElementById("cboTipDocumento" + intFila).disabled = bolEnable;
				
				var objMonto=document.getElementById("txtMonto" + intFila);
				var objTipo=document.getElementById("cboTipDocumento" + intFila);
				var objDoc=document.getElementById("txtDoc" + intFila);
				
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
				//Proy-31949-Inicio cuqui
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
				varIdRefAnu = '';			  
				varTransVisa = '';
				varMonedaVisa = '';
				var varTramaInsert='';
				varCodOpe = '';
				var varDescriOpe='';
				varTipoTrans = '';
				var TipTarjeta='';
				var varCodPtaWS='';
				
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
						varNroTarjeta=document.getElementById("txtDoc" + intFila).value;//Proy-31949
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
						varNroTarjeta = document.getElementById("txtDoc" + intFila).value;//Proy-31949
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
									document.getElementById("lblEnvioPos").innerHTML  = "";
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
						varNroTarjeta = document.getElementById("txtDoc" + intFila).value;//Proy-31949
						
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
						varLnkPrintPos='#LnkPrintPos1';//Proy-31949
						varNameImgPrint='#icoPrintPOS1';//Proy-31949
					break;
				}
				
				if(document.getElementById(varNameMonto).value ==''){
					alert('Debe ingresar un monto de pago');
					document.getElementById(varNameMonto).focus();
					return;
				}
				
				if(Number(document.getElementById(varNameMonto).value)==0){
					alert('Debe ingresar un monto mayor a cero.');
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
				
				var idRecaudacion = document.getElementById("hidIdentificador").value;//Proy-31949
				var varNroPedido = '';
				var varNroTelefono = '';
				varNroPedido = document.getElementById("hidIdentificador").value; 	
				varNroTelefono = document.getElementById("hidIdentificador").value; 
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
				RSExecute(serverURL,"GuardarTransaction",varTramaInsert, varNroTelefono,idRecaudacion,CallBack_GuardarTransaction,GuardarTransactionError,"X");
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
					var cboTipDocumento = document.getElementById("cboTipDocumento"+ varIntFila);
					var strOption = '';
					
					for(var row=0;row<cboTipDocumento.length;row++){
						strOption = cboTipDocumento[row].value;
						
						if(strOption.substr(0,strOption.length -2)==varRespuesta.split(";")[0]){
							break;
						}					
			}
				
				    var cboSel = document.getElementById("cboTipDocumento" + varIntFila).value
				    if( cboSel.substr(0,cboSel.length -2) != strOption.substr(0,strOption.length -2))
				    {					
					document.getElementById("cboTipDocumento"+ varIntFila).value = strOption;
					varNameTipoPOS = "HidTipPOS" + varIntFila;
					document.getElementById(varNameTipoPOS).value = varRespuesta.split(";")[1];

						document.getElementById("HidFila" + varIntFila).value = 'Monto=' + varMontoOperacion + 
						'|Tarjeta=' + varNroTarjeta + 
						'|ComboIndex=' + document.getElementById("cboTipDocumento" + varIntFila).selectedIndex + 
						'|NroReferncia=' + varNroRefVisa;
					}
				    

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
					var varNumVoucher = '';
					var varNumAutTransaccion = '';
					var varCodRespTransaccion = '';
					var varDescTransaccion = '';
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
					'|TipoPago=' + document.getElementById("HidTipoPago").value+
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
			    
					for (var i=1; i<=1; i ++)
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
						document.getElementById("txtDoc" + varIntFila).value = '';
						document.getElementById("HidFila" + varIntFila).value = '';
						document.getElementById("HidFila" + varIntFila).value = '';
						document.getElementById("txtDoc" + varIntFila).disabled = true;
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
					var varNomCliente = document.getElementById("txtNombreCliente").value;
					var varNroTelefono = document.getElementById("hidIdentificador").value; 
					var varNroPedido = document.getElementById("hidIdentificador").value;
					
					var varTramaAudit = '';    
					varTramaAudit = 'NomCliente=' + varNomCliente + 
					'|NroTelefono=' + varNroTelefono + 
					'|NroPedido=' + varNroTelefono + '|IdTransaccion=' + 
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
							$('#LnkPos'+varIntFila).fadeOut('fast');
							$('#LnkPrintPos'+varIntFila).fadeOut('fast');
							$('#LnkDelPos'+varIntFila).fadeOut('fast');		
							}
						eval("ContPago"+varIntFila+"=ContPago"+varIntFila+"+1");			
					}			
					//Proy-31949-Fin			
				}
				catch(err){
					document.getElementById("lblEnvioPos").innerHTML  = "";
					alert(err.description);
					f_activar_fila(varIntFila,false);//Proy-31949
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
						document.getElementById("txtDoc" + varIntFila).value = '';
						document.getElementById("HidFila" + varIntFila).value = '';
						document.getElementById("HidFila" + varIntFila).value = '';
						document.getElementById("txtDoc" + varIntFila).disabled = true;
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
					var varNroTelefono = document.getElementById("hidIdentificador").value; 
					var varNroPedido = document.getElementById("hidIdentificador").value;
					
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
							$('#LnkPos'+varIntFila).fadeOut('fast');
							$('#LnkPrintPos'+varIntFila).fadeOut('fast');
							$('#LnkDelPos'+varIntFila).fadeOut('fast');		
							}
						eval("ContPago"+varIntFila+"=ContPago"+varIntFila+"+1");			
					}
					//Proy-31949-Fin
				}
				catch(err){
					document.getElementById("lblEnvioPos").innerHTML  = "";
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
					var varNroPedido = document.getElementById("hidIdentificador").value;//Proy-31949
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
						
						if(varCodRptaWs == '1' || varCodRptaWs == null || varCodRptaWs === undefined){
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
					
					var varRpta = RSExecute(serverURL,"ActualizarTransaction",varTramaUpdate, objEntityPOS.TrnsnId);
					
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
						//Proy-31949 Inicio
						f_activar_fila(varIntFila,false);
						document.getElementById("HidFila" + varIntFila).value = '';
						document.getElementById("txtDoc" + varIntFila).value = '';
						document.getElementById("HidFila" + varIntFila).value = '';
						
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
						
						//CNH
						document.getElementById("txtDoc" + varIntFila).disabled = true;						
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
						'|ComboIndex=' + document.getElementById("cboTipDocumento" + varIndex).selectedIndex + 
						'|NroReferncia=' + varNroReferencia;
					}
					
					var varNomCliente = document.getElementById("txtIdentificadorCliente").value;
					var varNroTelefono = document.getElementById("hidIdentificador").value; 
					var varNroPedido = document.getElementById("hidIdentificador").value;
					
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
							
							eval("document.getElementById('txtDoc'+"+varIntFila+").disabled = false");
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
					var varNroPedido = document.getElementById("hidIdentificador").value;;
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
					
					var varRpta = RSExecute(serverURL,"ActualizarTransaction",varTramaUpdate, objEntityPOS.TrnsnId,CallBack_ActTransMCD);
					
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
					
						//Proy-31949 Inicio
						f_activar_fila(varIntFila,false);
						document.getElementById("HidFila" + varIntFila).value = '';
						document.getElementById("txtDoc" + varIntFila).value = '';
						document.getElementById("HidFila" + varIntFila).value = '';
						
						$('#LnkPos' + varIntFila).fadeIn('slow');
						$('#LnkPos' + varIntFila).prop('disabled',false);
						document.getElementById("icoTranPos" + varIntFila).src = "../images/send-icon.png";
						
						$(varLnkPrintPos).fadeOut('fast');
						$(varLnkPrintPos).prop('disabled',false);
						$(varNameImgPrint).prop('src','../images/print-icon.png');
						
						$('#'+varNameLinkDelPOS).fadeIn('fast');
						document.getElementById(varNameLinkDelPOS).disabled=false;
						document.getElementById(varNameImgDelPOS).src = "../images/delete-icon.png";
						
						FN_LoadPOS(varIntFila,'-');
						//Proy-31949 Fin
						
						varNroTarjeta = '';
						//CNH
						document.getElementById("txtDoc" + varIntFila).disabled = true;
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
						'|ComboIndex=' + document.getElementById("cboTipDocumento" + varIndex).selectedIndex + 
						'|NroReferncia=' + varNroReferencia;
					}
					
					var varNomCliente = document.getElementById("txtNombreCliente").value;
					var varNroTelefono = document.getElementById("hidIdentificador").value; 
					var varNroPedido = document.getElementById("hidIdentificador").value;
					
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
							
							eval("document.getElementById('txtDoc'+"+varIntFila+").disabled = false");
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
				var varIdTransaccion = document.getElementById("hidIdentificador").value + '_' + formatDate(VarToday);
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
			
		    //
			function calculaVuelto(){
				var monto;
				var val_fp;
				var codEfec = getValue('hidFPCodigoEfectivo');
				var suma = 0.0;
				var decimales = ''; //nhuaringa auto
				//--
				val_fp = getValue('cboTipDocumento1')
				if (val_fp == codEfec) 
				{
					monto = getValue('txtMonto1');
					if ((monto != '') && (!isNaN(monto)))
						suma = suma + monto*1;				
				}
				else
				{
					setValue('txtRecibidoPen','0.00');
					setValue('txtRecibidoUsd','0.00');
				}
				
				suma= Math.round(suma*100)/100;		
				var recPEN = getValue('txtRecibidoPen')*1;
				var recUSD = getValue('txtRecibidoUsd')*1
				if ( (recPEN == '') || (isNaN(recPEN)) ) 
				{
					setValue('txtRecibidoPen','0.00');
					recPEN = 0;
				}	
				if ( (recUSD == '') || (isNaN(recUSD)) ) 
				{
					setValue('txtRecibidoUsd','0.00');	
					recUSD = 0;
				}
					
				var tc = getValueHTML('lblTC')*1;
				//var vuelto = Math.round((recPEN*1 + recUSD*tc - suma)*100)/100;//aotane 05.08.2013		
				var vuelto = Math.round((recPEN*1 + recUSD*tc - suma)*1000)/1000;//aotane 05.08.2013		
				//nhuaringa auto
				
				//aotane 05.08.2013	
				/*if (vuelto.toString().indexOf('.') != -1)
				{
					decimales = (vuelto.toString().substring(vuelto.toString().indexOf('.') + 2))*1;	
				}
				else
				{
					decimales = 0;
				}				
				
				if ((decimales*1 < 5) && (decimales*1 > 0))
				{
					vuelto = Math.round((vuelto + (5-decimales*1)/100)*100)/100;
				}
				if (decimales*1 > 5)
				{
					vuelto = Math.round((vuelto + (10-decimales*1)/100)*100)/100;
				}*/
				
				if (vuelto < 0)
					document.getElementById("spnVuelto").innerHTML = "<FONT color=FF0000>Faltante:</FONT>";
				else
					document.getElementById("spnVuelto").innerText = "Vuelto:";  
				
				//frmPrincipal.txtVuelto.value = vuelto;				
				var vueltoRedondeo = Math.round(RedondeaInmediatoSuperior(vuelto)*100)/100;//aotane 05.08.2013		
				frmPrincipal.txtVuelto.value = vueltoRedondeo;//aotane 05.08.2013
			}


			function activarInactivarIngresoMonto(idChkSelec, idTxtActivar, idTxtDeuda)
			{
				//nhuaringa auto
				var Monto;
				var Monto1;
				var TotalRecibosSoles;
				var CodServ = getValue('hidCodigoServicio');
				var decimales;
				//----
				var marca = getValueCheck(idChkSelec);	

				//Proy-31949 Ini
				var hidFilaPOS = getValue('HidFila1');
				
				if(hidFilaPOS==''){
				if (!marca){//cuando se inactiva un chek
					setValue(idTxtActivar,'');
					setEnabled(idTxtActivar, false,'clsInputDisable');
					actualizaTotalesRecibos();
					var totalPorPagina = parseFloat(getValue('hiddTotal'),10);////hiddTotal  -->  hidPagina
					//nhuaringa auto
					if (CodServ != "103")
					{												
						for (var i = dgDocumentosPago.rows.length - 1; i > 0; i--){
							var fila = dgDocumentosPago.rows[i];				
							var chk = fila.cells[8].children(0);
							//inicio jtn
							var ultimoRecibo = fila.cells[7].children(0);
							setValue('hidUltimoRecibo',ultimoRecibo);
							//fin jtn
							if (chk.checked){
								var numeroIngresado = fila.cells[9].children(0).value*1;
								var numeroFila  = fila.cells[7].children(0).innerHTML*1;

								Monto1 = getValue('txtMonto1');
								TotalRecibosSoles = getValue('txtTotalRecibosSoles');
								//alert(numeroFila);////
								Monto = TotalRecibosSoles*1 - Monto1*1;
								val = dgDocumentosPago.rows[i].cells[7].children(0).innerHTML*1// - Monto; //  quitado para que no redondee
								val = val.toFixed(2);
								//alert('antes de ingresar valores');
								if(numeroIngresado != 0){
									fila.cells[9].children(0).value = (numeroIngresado < numeroFila)? numeroIngresado.toFixed(2) : val;
								}
							    //dgDocumentosPago.rows[i].cells[9].children(0).value = val;
							    //alert('antes de actualizatotalesrecibos');
								actualizaTotalesRecibos();
								break;
							}
							//setValue('txtTotalRecibosSoles', getValue('txtMonto1'));
							//setValue('txtRecibidoPen', getValue('txtMonto1'));
						}
					}else
					{
						actualizaTotalesRecibos();	
						TotalRecibosSoles = getValue('txtTotalRecibosSoles');
						decimales = (TotalRecibosSoles.toString().substring(TotalRecibosSoles.toString().indexOf('.') + 2))*1;				
						if ((decimales*1 > 0))
						{
							TotalRecibosSoles = Math.round((TotalRecibosSoles - (decimales*1)/100)*100)/100;
						}
						/*if (decimales*1 > 5)
						{
							TotalRecibosSoles = Math.round((TotalRecibosSoles - (decimales*1-5)/100)*100)/100;
						}*/
						setValue('txtMonto1',TotalRecibosSoles);
						setValue('txtTotalRecibosSoles',TotalRecibosSoles);
						calculaVuelto();
					}
				}
				else{//cuando se activa un chek
					setEnabled(idTxtActivar, true,'clsInputEnable');
					val = getValueHTML(idTxtDeuda);
					//nhuaringa auto
					if ((CodServ == "103") || (document.frmPrincipal.ChkMontoPagar.checked))
					{
						setValue(idTxtActivar,val);
						actualizaTotalesRecibos();
						setFocus(idTxtActivar,true);
					}
					else
					{
						var ContGrilla = 0;
						for (var i = 1; i < dgDocumentosPago.rows.length; i++){
							var fila = dgDocumentosPago.rows[i];
							var chk = fila.cells[8].children(0);
							///INICIO JYMMY ////////////////////
							if (chk.checked){
								ContGrilla = ContGrilla + 1;
								var ultimoRecibo = getValueHTML(fila.cells[3].children(0).id);
								setValue('hidUltimoRecibo',ultimoRecibo);
								var montoAsignar = 0;
								montoAsignar = parseFloat(fila.cells[7].children(0).innerHTML,10);
								if(montoAsignar < 0){
									setEnabled(idTxtActivar, false,'clsInputDisable');
									alert('Recibo no se ajusta');
									chk.checked=false;
									setFocus(idTxtActivar,true);
									return;
								}
								else{//cambiado por TS-JTN 2014.02.24
									var numeroIngresado = fila.cells[9].children(0).value*1;
									var numeroFila  = fila.cells[7].children(0).innerHTML*1;
									if(numeroIngresado != 0){
										if(numeroIngresado < numeroFila){
											fila.cells[9].children(0).value = numeroIngresado.toFixed(2);
										}
									}
									else{
										fila.cells[9].children(0).value = fila.cells[7].children(0).innerHTML;
									}
								}
							}
							///FIN JYMMY //////////////////////////////////////////////////////////////////////////////////////////
						}
							
							val = getValueHTML(idTxtDeuda);
							setValue(idTxtActivar,val);
							actualizaTotalesRecibos();
							
							Monto1 = getValue('txtMonto1');
							TotalRecibosSoles = getValue('txtTotalRecibosSoles');
							Monto = TotalRecibosSoles*1 - Monto1*1;
							val = getValueHTML(idTxtDeuda)*1 - Monto;
							val = val.toFixed(2);
							
							actualizaTotalesRecibos();
							setValue('txtTotalRecibosSoles',Monto1);
							setValue('txtRecibidoPen',Monto1);
							setFocus(idTxtActivar,true);
						}
					}
							
				} else {
					if (!marca) { //Cuando se inactiva el check
						setEnabled(idTxtActivar, false,'clsInputDisable');
						var totalPorPagina = parseFloat(getValue('hiddTotal'),10);

						if (CodServ != "103")
						{												
							for (var i = dgDocumentosPago.rows.length - 1; i > 0; i--){
								var fila = dgDocumentosPago.rows[i];				
								var chk = fila.cells[8].children(0);
								var strImpPagado = fila.cells[9].children(0).value;
								var ultimoRecibo = fila.cells[7].children(0);
								setValue('hidUltimoRecibo',ultimoRecibo);
							
								if(chk.checked && strImpPagado==''){
									fila.cells[9].children(0).value = '';
								}

								if(!chk.checked){
									fila.cells[9].children(0).value = '';
								}
							}
						} else {
							//actualizaTotalesRecibos();	
							TotalRecibosSoles = getValue('txtTotalRecibosSoles');
							decimales = (TotalRecibosSoles.toString().substring(TotalRecibosSoles.toString().indexOf('.') + 2))*1;				
							if ((decimales*1 > 0))
							{
								TotalRecibosSoles = Math.round((TotalRecibosSoles - (decimales*1)/100)*100)/100;
							}
							/*if (decimales*1 > 5)
							{
								TotalRecibosSoles = Math.round((TotalRecibosSoles - (decimales*1-5)/100)*100)/100;
							}*/
							setValue('txtMonto1',TotalRecibosSoles);
							setValue('txtTotalRecibosSoles',TotalRecibosSoles);
							calculaVuelto();
												
						}
						
					} else { //Inactiva check
						setEnabled(idTxtActivar, true,'clsInputEnable');
						val = getValueHTML(idTxtDeuda);
						if ((CodServ == "103") || (document.frmPrincipal.ChkMontoPagar.checked))
						{
							setFocus(idTxtActivar,true);
						}
						else
						{
							var ContGrilla = 0;
							for (var i = 1; i < dgDocumentosPago.rows.length; i++){
								var fila = dgDocumentosPago.rows[i];
								var chk = fila.cells[8].children(0);
								
								if (chk.checked){
									ContGrilla = ContGrilla + 1;
									var ultimoRecibo = getValueHTML(fila.cells[3].children(0).id);
									setValue('hidUltimoRecibo',ultimoRecibo);
									var montoAsignar = 0;
									montoAsignar = parseFloat(fila.cells[7].children(0).innerHTML,10);
									if(montoAsignar < 0){
										setEnabled(idTxtActivar, false,'clsInputDisable');
										alert('Recibo no se ajusta');
										chk.checked=false;
										setFocus(idTxtActivar,true);
										return;										
									}
								}
							}
							
							Monto1 = getValue('txtMonto1');								
							setValue('txtTotalRecibosSoles',Monto1);
							setValue('txtRecibidoPen',Monto1);
							setFocus(idTxtActivar,true);
							calculaVuelto();
						}
					}
				}
			}
			
			function actualizaControlesTipoDocumento(obj)
			{ //onchange
				if (obj != null) { 
					//---					
					if (obj.value == getValue('hidFPCodigoCheque')) 
						setVisible("divBancoGirador01", true);						
					else 
					{
						setValue('ddlBancoGirador01', '');
						setVisible("divBancoGirador01", false);
					}
					//---
					if (obj.value == '') {	//cboTipDocumentoN
						setValue('txtDoc1','');
						setValue('txtMonto1','');
					}
					else {
						val = getValue(obj.id);
						if (val == getValue('hidFPCodigoEfectivo')) 
						{							
							setValue('txtDoc1','');
							
							//--Activamos controles
							setEnabled('txtRecibidoPen', true,'clsInputEnable');
							setEnabled('txtRecibidoUsd', true,'clsInputEnable');
							//--recalculamos total de recibos
							ReactualizaTotalesRecibos();
						}
						else
						{
							//--Inactivamos controles y asignamos valor Cero
							setValue('txtRecibidoPen','0.00');
							setValue('txtRecibidoUsd','0.00');				
							setEnabled('txtRecibidoPen', false,'clsInputDisable');
							setEnabled('txtRecibidoUsd', false,'clsInputDisable');
						}
					}		
					//nhuaringa auto
					var sumapas;
					if (!document.frmPrincipal.ChkMontoPagar.checked)
					{
						if (obj.value == 'ZEFE00')
						{
							actualizaTotalesRecibos();
							for (var i = dgDocumentosPago.rows.length - 1; i > 0; i--){
							var fila = dgDocumentosPago.rows[i];				
							var chk = fila.cells[8].children(0);
							if (chk.checked){											
								Monto1 = getValue('txtMonto1');	
								TotalRecibosSoles = getValue('txtTotalRecibosSoles');
								Monto = TotalRecibosSoles*1 - Monto1*1;
								val = fila.cells[7].children(0).innerHTML*1 - Monto;
								val = val.toFixed(2);
								fila.cells[9].children(0).value = val; 
								actualizaTotalesRecibos();											
							}						
						}
						}
						else
						{
							
							for (var i = dgDocumentosPago.rows.length - 1; i > 0; i--){
							var fila = dgDocumentosPago.rows[i];				
							var chk = fila.cells[8].children(0);
							if (chk.checked){						
								sumapas = fila.cells[7].children(0).value;		
								fila.cells[9].children(0).value = fila.cells[7].children(0).innerHTML;
							}
							}	
							setValue('txtMonto1',sumapas);
							actualizaTotalesRecibos();			
						}				
					}					
					//--					
					calculaVuelto();
				}
			}
									
			function actualizaMontoPago(obj, valMin, incMin)
			{ //onblur						
					if(document.frmPrincipal.ChkMontoPagar.checked)	//nhuaringa auto		
					{
						var msg='';
						if ((obj != null) && (obj.value!='')) {
								if (isNaN(obj.value))
									msg='No es un número válido.';
								else {	
									var val = (obj.value*1).toFixed(2);
									if (incMin) {
										if (val < valMin) 
											msg ='El monto debe ser mayor o igual a ' + valMin + '.';
										else
											setValue(obj.id, val);	
									}	
									else {
										if ((val <= valMin) && (valMin*1 != 0)) //nhuaringa auto
										{
											msg ='El monto debe ser mayor a ' + valMin + '.';
										}											
										else
											setValue(obj.id, val);	
									}
								}
							}
						//--
						if (msg != '') {
							alert(msg);
							setFocusSelect(obj.id);
							return false;
						}						
						//--
						return true;
					}				
			}
			
			
			function validaMontosExcedentesParciales(flag){
				var nmTbl = 'dgDocumentosPago';
				var tbl = document.getElementById(nmTbl); 
				var band = false;
				//LLAMA A ESTE MEDOTO CUANDO SE INGRESA MANUALMEMTE UN MONTO
				if (tbl != null) {	
					var nmcheck;
					var val;						
					var valDeuda;
					var idMontoPago;
					var idDeuda;
					var valPago;
			
					for (i=2; i<=tbl.rows.length; i++){
						nmcheck = nmTbl + '__ctl' + i + '_' + 'chbxDOC_Seleccionar';   
						val = getValueCheck(nmcheck);						
						if (val) {	
							//--
							idMontoPago = nmTbl + '__ctl' + i + '_' + 'txtDOC_MontoPagar';	
							idDeuda = nmTbl + '__ctl' + i + '_' + 'lblDOC_Deuda';
							/*alert(idMontoPago);
							alert(idDeuda);*/
							valPago = getValue(idMontoPago);
							valDeuda = getValueHTML(idDeuda);
							
							valPago = parseFloat(valPago,10);	
							valDeuda = parseFloat(valDeuda,10);												
							if(flag == true){							
								if(valPago > valDeuda){								
									return true;
								}								
							}else{								
								if(valDeuda > valPago){

									return true;
								}							
							}								
																				
						} /*val*/
					} /*for*/
				}	
				return false;
			}
			
			//Proy-31949 Ini
			function actualizaMontoPagoRecibo(idColMontoPago, idTipoMon, idMontoDeuda, idcheck)
			{//en onblur 				
				var hidFilaPOS = getValue('HidFila1');
				var txtMonto1 = getValue('txtMonto1');
				txtMonto1 = parseFloat(txtMonto1,10);
					
				/*gcastillo- inicio*/
				var validColMontoPago;
				var validMontoDeuda;
				var val;
				var band = true;
				
				validColMontoPago = getValue(idColMontoPago);
				validColMontoPago = parseFloat(validColMontoPago,10);
				validMontoDeuda = getValueHTML(idMontoDeuda);
				validMontoDeuda = parseFloat(validMontoDeuda,10);	
				val = getValueCheck(idcheck);
					
				if(hidFilaPOS==''){
					if(validMontoDeuda > validColMontoPago){
					if(validaMontosExcedentesParciales(true)){
						setValueCheck(idcheck,false);	
						activarInactivarIngresoMonto(idcheck, idColMontoPago, idMontoDeuda);
						alert('No se pueden ingresar montos parciales');
						band = false;
					}
				}
				
				if(validMontoDeuda < validColMontoPago){
					if(validaMontosExcedentesParciales(false)){
						setValueCheck(idcheck,false);	
						activarInactivarIngresoMonto(idcheck, idColMontoPago, idMontoDeuda);
						alert('No se pueden ingresar montos excedentes');
						band = false;
					}
				}

				if(band == true){
				if (val) {
					var valPago = getValue(idColMontoPago);
					if (valPago != '') { //validation is before to save.	
						valPago = parseFloat(valPago,10);
						if (isNaN(valPago)) {
							setFocusSelect(idColMontoPago);
							alert('Ingrese un número válido.');
						}
						else if  (valPago<=0) {
						    if (validMontoDeuda >= 0.5)
						    {
								setFocusSelect(idColMontoPago);
								alert('El Monto de Pago debe ser mayor a 0.');
						    }	
						}	
						else {
							valDeuda = getValueHTML(idMontoDeuda);
							valDeuda = parseFloat(valDeuda,10);
							//alert('pausa');
							setValue(idColMontoPago, valPago.toFixed(2));
							actualizaTotalesRecibos();
							
							Monto1 = getValue('txtMonto1');
							setValue('txtTotalRecibosSoles',Monto1);
							setValue('txtRecibidoPen',Monto1);
						}
					}
					}
				}
				} else {
					var decImpTotPOS = 0;
					var decImpPagPOS = 0;
					var strImpPagoFocus = getValue(idColMontoPago);
				
					for (var i = dgDocumentosPago.rows.length - 1; i > 0; i--){
						var fila = dgDocumentosPago.rows[i];				
						var chk = fila.cells[8].children(0);
						var strImpPagado = fila.cells[9].children(0).value;
						
						if(chk.checked){
							if(strImpPagado!=''){
								decImpTotPOS = decImpTotPOS + (strImpPagado*1);
							}
						}
					}
					
					decImpPagPOS = txtMonto1*1;
					setValue('txtTotalRecibosSoles',decImpTotPOS.toFixed(2));
					
					if(strImpPagoFocus!=''){
						setValue(idColMontoPago,(getValue(idColMontoPago)*1).toFixed(2));
					}else{
						setValue(idColMontoPago,'0.00');
			}

					if(decImpPagPOS!=decImpTotPOS){
						alert('Monto total debe ser igual al monto pagado');
						//document.getElementById(idColMontoPago).focus();
			}

					calculaVuelto();
				}				
			}
			//Proy-31949 Fin

			function actualizaTotalesRecibos()
			{
				ReactualizaTotalesRecibos();
				//--Calculo de Vuelto o Faltante
				calculaVuelto();
			}
			
			function ReactualizaTotalesRecibos()
			{
				var nmTbl = 'dgDocumentosPago';
				var tbl = document.getElementById(nmTbl);
				
				var totalSoles= 0; //parseFloat(getValue('hiddTotal'),10); //comentado jmt
				
				//var totalSoles = parseFloat(getValue('hiddTotal'),10);
				//alert(getValue('hiddTotal'));
				
				var totalDolares = 0;
				
				//RedondearNumero('hidPagina');
				var totalPorPagina = parseFloat(getValue('hidPagina'),10);
				var totalPagina = parseFloat(getValue('hiddTotal'),10);

		 		if (tbl != null) {
					var nmcheck;
					var val;
					var valDeuda;
					var idTipoMon;
					var idMontoDeuda;
					var valPago=0;
					var totalHid=0

					for (i=2; i<=tbl.rows.length; i++)	{
						nmcheck = nmTbl + '__ctl' + i + '_' + 'chbxDOC_Seleccionar';
						val = getValueCheck(nmcheck);
						if (val) {	
							//--
							idMontoPago = nmTbl + '__ctl' + i + '_' + 'txtDOC_MontoPagar';
							valPago = getValue(idMontoPago);
							if (valPago != '') {
								valPago = parseFloat(valPago,10);
								//--
								idTipoMoneda = nmTbl + '__ctl' + i + '_' + 'lblDOC_TipoMoneda';	
								valTipoMoneda = getValueHTML(idTipoMoneda);	
								//--calcula totales
								
								//INICIO JTN
								//totalSoles = totalSoles + valPago;
								totalHid = totalHid + valPago;
								if (valTipoMoneda=='PEN')
									totalSoles = totalSoles + valPago;
								else
									totalDolares = totalDolares + valPago;
								//FIN JTN
							}//if
						} /*val*/
					} /*for*/
				} /**/
				

				var tc = getValueHTML('lblTC')*1;
				var totalActual = getValueHTML('txtMonto1')*1;
				total = totalSoles + totalDolares*tc;
				var totalPaginacion = 0;
				
				totalPaginacion =  totalPagina - totalPorPagina;

					if(getValue('hidCodigoServicio')=='103'){
						//totalPorPagina = total;
						totalPorPagina = totalPorPagina*tc;
						totalPaginacion = totalPaginacion * tc
					}
					//alert('TOTAL: ' + total + '\ntotalPaginacion: ' + totalPaginacion);
					total = total + parseFloat(totalPaginacion,10);
					
					
				if(getValue('hidCodigoServicio')=='103'){
					totalDolares = total / tc;
				}
				
				setValue('txtTotalRecibosDolares', totalDolares.toFixed(2));
				//--
				setValue('txtTotalRecibosSoles', total.toFixed(2));
				//hiddTotal

				//nhuaringa auto
				var CodServ = getValue('hidCodigoServicio');
				var decimales;
				var val_fp;
				var codEfec = getValue('hidFPCodigoEfectivo');
				val_fp = getValue('cboTipDocumento1');
				
				if ((CodServ == "103") && ((val_fp == codEfec) ))
				{
					total = (total*1).toFixed(2);
					total = total*1;
					decimales = (total.toString().substring(total.toString().indexOf('.') + 2))*1;	
					if ((decimales*1 > 0))
					{
						total = Math.round((total - (decimales*1)/100)*100)/100;
					}
					/*if (decimales*1 > 5)
					{
						total = Math.round((total - (decimales*1-5)/100)*100)/100;
					}*/
					total = (total*1).toFixed(2);
					//setValue('txtTotalRecibosSoles', total);
					//setValue('txtMonto1',total);
				}
				else
				{
					setValue('txtTotalRecibosSoles', total.toFixed(2));
					setValue('txtMonto1', total.toFixed(2));
				}
				
				
				/**************************************************************************/
				var tipoMoneda = document.getElementById('lblTipoMonedaDeuda');

                total = total*1;
           		if(total.toFixed == 'undefined'){ 
				 
				 setValue('txtMonto1', total.toFixed(2));
				 setValue('txtRecibidoPen', total.toFixed(2));
				}
				else{
					setValue('txtMonto1', total.toFixed(2));
					setValue('txtRecibidoPen', total.toFixed(2));
					setValue('txtTotalRecibosSoles', getValue('txtMonto1'));
				}
				//alert('PRIMERO REDONDEA EL VALOR DE MONTO A PAGAR');
				
				RedondearEfectivoFijos('txtMonto1','cboTipDocumento1');

				if(totalHid<1){					
					if(getValue('hiddPaginaSinAjuste')>0){
					    
						setValue('txtTotalRecibosSoles', getValue('txtMonto1'));
						setValue('txtRecibidoPen', getValue('txtMonto1'))
					}
					else{
					    
						setValue('txtTotalRecibosSoles', getValue('hiddTotal'));
						setValue('txtRecibidoPen', getValue('hiddTotal'));
					}
				}
				
				setValue('txtTotalRecibosSoles', getValue('txtMonto1'));
				setValue('txtRecibidoPen', getValue('txtMonto1'))
				
				//Final - LGZ
				//setValue('txtRecibidoPen', total.toFixed(2));
				//setValue('txtRecibidoPen', total);
				
				
				//RedondearEfectivoFijos('txtRecibidoPen');
				
			}				
							
			function esValidoFilaFormaPago(id_fp, id_doc, id_mon, idBco01)
			{
				val_fp = getValue(id_fp);
				val_doc = getValue(id_doc);
				val_mon  = getValue(id_mon);				
					
					
				//--valida 1er campo: forma de pago	
				if ((val_fp == '') && ((val_doc != '')  || (val_mon != ''))) {	
					alert('La Forma de Pago es un dato requerido.');	
					setFocus(id_fp);
					return false;	
				}
				//--valida 2do campo:  El Nro. de Tarjeta/Cheque
				cod_efe = getValue('hidFPCodigoEfectivo');				
				if (val_fp == cod_efe) {
					if (val_doc != '') {
						alert('El Nro. de Tarjeta/Cheque no es necesario.');
						setFocusSelect(id_doc);
						return false;	
					}				
				}
				else {//Nro Tarjeta/Cheque
					if (val_doc == '') {
						alert('El Nro. de Tarjeta/Cheque es un dato requerido');
						setFocus(id_doc);
						return false;	
					}	
					else {
				
						if (val_fp.length >= 4)  
							val_fp = val_fp.substr(0,4); //quita 2 últimos caracteres
						//---	
						if (frmPrincipal.txtTarjCred.value.indexOf(val_fp) >= 0) {
							if (frmPrincipal.txtBIN.value.indexOf(val_doc.substr(0,4)) < 0 ) {
								if (!confirm('El prefijo de la tarjeta no se encuentra registrado. ¿Desea Continuar?')) {
									setFocus(id_doc);
									return false;
								}
							}
						} 
						else {				
							cod_chk = getValue('hidFPCodigoCheque');
							val_fp = getValue(id_fp); 
							if (val_fp != cod_chk) {
								alert('El tipo de tarjeta/cheque no existe para el cliente.');
								setFocus(id_fp);
								return false;
							}
						}
					}/*--val_doc--*/
				}
				
				//-- valida 3er campo: Monto
				msg = '';
				if (val_mon == '') 
					msg = 'El monto es un dato requerido.';
				else {
					val_mon = val_mon*1;
					if (isNaN(val_mon))
						msg = 'El monto debe ser un valor numérico.';
					else {
						if (val_mon <= 0) 
							msg = 'El monto debe ser mayor a cero.';
					}
				}
				//---4to campo
				if (val_fp == getValue('hidFPCodigoCheque')) {
					val_bco01  = getValue(idBco01);	
					if (val_bco01 == '') 
						msg = 'El Banco Girador es un dato requerido';					
						id_mon = idBco01;	/**/	
				}
				//--
				if (msg!='') {
					alert(msg);
					setFocus(id_mon);
					return false;	
				}
				//--		
				return true;
			}					
			
			function validarRecibos()
			{
				var nmTbl = 'dgDocumentosPago';
				var tbl = document.getElementById(nmTbl); 					
				var totalSelec = 0;
				if (tbl != null)
				{	
					var nmcheck;
					var val;						
					var valDeuda;
					var idTipoMon;
					var idMontoDeuda;
					var valPago;
					var totalSoles = 0;
					var totalDolares = 0;
					var totalPaginacion = parseFloat(getValue('hiddTotal'),10);
					for (i=2; i<=tbl.rows.length; i++)
					{
						nmcheck = nmTbl + '__ctl' + i + '_' + 'chbxDOC_Seleccionar';   
						val = getValueCheck(nmcheck);						
						if (val) {	
							totalSelec++;
							//--
							idMontoPago = nmTbl + '__ctl' + i + '_' + 'txtDOC_MontoPagar';	
							valPago = getValue(idMontoPago);
							
							if (valPago == '')
							{
								setFocusSelect(idMontoPago);
								alert('El Monto de Pago es obligatorio para los recibos seleccionados.');
								return false;
							}
							//--
							idTipoMoneda = nmTbl + '__ctl' + i + '_' + 'lblDOC_TipoMoneda';	
							valTipoMoneda = getValueHTML(idTipoMoneda);										
							//--
							idMontoDeuda = nmTbl + '__ctl' + i + '_' + 'lblDOC_Deuda';	
							valDeuda = getValueHTML(idMontoDeuda);								
							//--calcula totales
							if (valTipoMoneda=='PEN')
								totalSoles = totalSoles + valPago;
							else
								totalDolares = totalDolares + valPago;
						} /*val*/
					}
					//--
					if(totalPaginacion>0){
						return true;
					}
					else{
						if (totalSelec<=0) {
							alert('No ha seleccionado por lo menos un recibo de pago.');
							return false;					
						}
						else {
							return true;
						}
					}
					//--
				}
				else
					return false;
							
			}	
			
			
			
			function f_CalculoDeudaSeleccionada(){
			
				var nmTbl = 'dgDocumentosPago';
				var tbl = document.getElementById(nmTbl); 					
				var totalSelec = 0;
				var totalDeuda = 0;
				if (tbl != null)
				{	
					var nmcheck;
					var val;						
					var valDeuda;
					var idTipoMon;
					var idMontoDeuda;
					var valPago;					
									
					for (i=2; i<=tbl.rows.length; i++)
					{
						nmcheck = nmTbl + '__ctl' + i + '_' + 'chbxDOC_Seleccionar';   
						val = getValueCheck(nmcheck);						
						if (val) {	
							totalSelec++;
							//--
							idMontoDeuda = nmTbl + '__ctl' + i + '_' + 'lblDOC_Deuda';	
							valDeuda = getValueHTML(idMontoDeuda);	
							valDeuda = parseFloat(valDeuda);
							totalDeuda = totalDeuda + valDeuda;
						}
					}
				}		
						return totalDeuda;
			
			}
			
			
			function MontoPagarRestriccion(sumatoriaTotal,idMonto)
			{	
				if(sumatoriaTotal>0){
					valorMonto = document.getElementById(idMonto);				
					
					if(valorMonto.value > sumatoriaTotal){
						alert("El Monto a Pagar no puede ser mayor al Total");
						document.getElementById(idMonto).value = sumatoriaTotal.toFixed(2);
					}
				}						
			}
			
			
			function f_pagoPorExcedente(monto_excedente)
			{
				//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
				var nmTbl = 'dgDocumentosPago';
				var tbl = document.getElementById(nmTbl); 					
				
				if (tbl != null)
				{	
					var nmcheck;
					var val;						
					var valDeuda;
					var idTipoMon;
					var idMontoDeuda;
					var valPago;
					var totalSoles = 0;
					for (i=2; i<=tbl.rows.length; i++)
					{
						nmcheck = nmTbl + '__ctl' + i + '_' + 'chbxDOC_Seleccionar';   
						val = getValueCheck(nmcheck);
												
						if (!val) {	
							
							idMontoDeuda = nmTbl + '__ctl' + i + '_' + 'lblDOC_Deuda';	
							valDeuda = getValueHTML(idMontoDeuda);	
							valDeuda =  parseFloat(valDeuda,10);
							
							if(monto_excedente < valDeuda && monto_excedente != 0.00)
							{
							
								idMontoPago = nmTbl + '__ctl' + i + '_' + 'txtDOC_MontoPagar';
								///
								/// COMENTADO POR ALINEACION 03.01.2014
								/// setValue(idMontoPago,monto_excedente);
								///
								setValueCheck(nmcheck,true);
								monto_excedente = 0.00;
							}
							else 
							{
											
									if(monto_excedente != 0.00){
										idMontoPago = nmTbl + '__ctl' + i + '_' + 'txtDOC_MontoPagar';
										///
										/// COMENTADO POR ALINEACION 03.01.2014
										/// setValue(idMontoPago,valDeuda);
										///
										setValueCheck(nmcheck,true);	
										monto_excedente = monto_excedente - valDeuda;
										monto_excedente = monto_excedente.toFixed(2);
									}
							}
					
						} 
						else
						{
							
							idMontoDeuda = nmTbl + '__ctl' + i + '_' + 'lblDOC_Deuda';	
							valDeuda = getValueHTML(idMontoDeuda);	
							valDeuda = parseFloat(valDeuda);
							valDeuda = valDeuda.toFixed(2);
							idMontoPago = nmTbl + '__ctl' + i + '_' + 'txtDOC_MontoPagar';
							///
							/// COMENTADO POR ALINEACION 03.01.2014
							/// setValue(idMontoPago,valDeuda);
							///
						}
						
					}
				}						
			}	
			
		function f_ValidarGrabar() {
				blnClosingMod = false; //Proy-31949
				var montoPagar = $("#txtTotalRecibosSoles").val()*1;
				var montoPOS = $("#txtMonto1").val()*1;
				
				if(montoPagar != montoPOS)
				{				
					alert("Monto total debe ser igual al monto pagado");
					return false;
				}
				
            //var valorFlag = "#C#";  // es cliente corporativo
            var nombreCliente = document.getElementById("txtNombreCli_old").value.substring(0, 3);
            //alert(document.getElementById("txtNombreCliente").value.substring(0,3));
			//alert(nombreCliente); si nombre cliente es #C# (corporativo)se permite pagar recibos sin la validacion de documento mas reciente
            if (nombreCliente == '') { //realiza restriccion si no es cliente corporativo

                var nmTbl = 'dgDocumentosPago';
                var tbl = document.getElementById(nmTbl);

                if (tbl != null) {
                    var nmcheck;
                    var val;
                    var cancheck = 0;
                    var numReg = 0;
                    var c;
                    
                    ///
                    /// AGREGADO POR ALINEACION 03.01.2014
                    var valMontoPagar=0;//POR BORRAR
                    ///
                    
                    
                    /*********************************************************************/
                    // obtiene la cantidad total de registros con check
                    for (i = 2; i <= tbl.rows.length; i++) {
                        nmcheck = nmTbl + '__ctl' + i + '_' + 'chbxDOC_Seleccionar';
                        val = getValueCheck(nmcheck);
                        if (val) {
                            cancheck = cancheck + 1;
                        }
                    }
                    /*********************************************************************/

                    for (i = 2; i <= tbl.rows.length; i++) {
                        nmcheck = nmTbl + '__ctl' + i + '_' + 'chbxDOC_Seleccionar';
                        val = getValueCheck(nmcheck);
                        numReg = numReg + 1;

                        if (val) {
                            //obtener el valor de la deuda de la grilla							
                            valDeuda = parseFloat(tbl.rows[(i - 1)].cells['7'].innerText);
                            //obtener el valor del monto a pagar		
                            idMontoPago = nmTbl + '__ctl' + i + '_' + 'txtDOC_MontoPagar';
                            valMontoPagar = parseFloat(getValue(idMontoPago));
                            if (i < (cancheck + 1)) { // si el contador es menor a la cantidad total de registros con check
                                if (valDeuda != valMontoPagar) { //si la deuda y el monto a pagar son diferentes									
                                    //alert("Verificar el registro número " + numReg + ": El Monto a Pagar y la Deuda son diferentes.\n" + "El Monto a Pagar sólo puede ser diferente a la Deuda cuando es el último registro con check activado");
                                    alert("Pagos parciales incorrectos, el pago parcial sólo es permitido en el documento más reciente.");
                                    return false; //termina funcion y devuelve accion al usuario									
                                } /*else{ //si la deuda y el monto a pagar son iguales
									alert("Cant. check: " + cancheck + " => Valores iguales => " + "check en el contador => " + i + " => Deuda: " + valDeuda + " => Monto Pagar: " + valMontoPagar);
								}*/
                            } else if (i == (cancheck + 1)) { // si el contador es igual a la cantidad total de registros con check
                                //alert("Cant. check: " + cancheck + " => Ultimo registro - no se hace nada " + "check en el contador => " + i + " => Deuda: " + valDeuda + " => Monto Pagar: " + valMontoPagar);
                            }
                            c = i; //registro de la grilla segun el recorrido
                            //alert("registro con check => " + numReg);
                        }
                    }

                    //alert("El ultimo registro de la grilla es la posicion con check => " + c);

                    for (i = 2; i <= c; i++) {
                        nmcheck = nmTbl + '__ctl' + i + '_' + 'chbxDOC_Seleccionar';
                        val = getValueCheck(nmcheck);
                        if (val == false) {
                            //alert("El check no está activado en el registro número " + (i-1) + "\n" + "Por favor es obligatorio que active el check");
                            alert("Por favor seleccione los documentos más antiguos.");
                            return false;
                        }
                    }
                }
            }//if nombreCliente

                /*******************************************************************************/
                var lblTC;
                if (getValue('hdnEjecutado') == 'R') {
                    alert('La transacción se está ejecutando.');
                    return false;
                }
                if (!esValidoFilaFormaPago('cboTipDocumento1', 'txtDoc1', 'txtMonto1', 'ddlBancoGirador01'))
                    return false;
                if (!validarRecibos())
                    return false;

                //---
                var msg = '';
                var result = false;
                
                ///
                /// AGRAGADO POR ALINEACION 03.01.2014
                var val_pg;
				var val_re;
				var val_deuda;
				///


                val_pg = getValue('txtMonto1');
                val_re = getValue('txtTotalRecibosSoles');

                val_pg = parseFloat(val_pg, 10);
                val_re = parseFloat(val_re, 10);

                val_deuda = getValue('txtValorDeuda');
                val_deuda = parseFloat(val_deuda, 10);

                if (getValueHTML('lblTipoMonedaDeuda') == '$') {
                    lblTC = getValueHTML('lblTC') * 1;

                    lblTC = parseFloat(lblTC, 10);
                    val_deuda = val_deuda * lblTC;
                    val_deuda = val_deuda.toFixed(2);

                }


                if ((val_pg <= 0) || (val_pg != val_re)) {
                    msg = 'El monto total de Pagos debe coincidir con el monto total de\nlos Documentos a pagar.';
                }
                else {
                    if (getValue('txtVuelto') * 1 < 0)
                        msg = 'El importe recibido no puede ser menor al importe a pagar.';
                }
                
                ///
                /// COMENTADO POR ALINEACION 03.01.2014
                /// if (val_pg > val_deuda) {
                ///     msg = 'El monto total de Pagos no debe exceder al Valor de la Deuda.';
                /// }
                ///
                
                if (msg == '') {
					
					///
					/// AGREGADO POR ALINEACION 03.01.2014
					var deuda_seleccionada;
					///
                    deuda_seleccionada = f_CalculoDeudaSeleccionada();
                    deuda_seleccionada = deuda_seleccionada.toFixed(2);
                    if (getValueHTML('lblTipoMonedaDeuda') == '$') {
                        lblTC = getValueHTML('lblTC') * 1;
                        lblTC = parseFloat(lblTC, 10);
                        val_pg = val_pg / lblTC;
                    }
                    //gcastillo - fin - calculo monto de lblDeuda seleccionada
                    //cambiado por jymmyt SI ES CLIENTE CORPORATIVO #C# 
                    if(nombreCliente==''){
                         if (val_pg > deuda_seleccionada) {
							
							///
							/// AGRAGADO POR ALINEACION 03.01.2014
							var monto_excedente;
							///
							
							monto_excedente = val_pg - deuda_seleccionada;
							monto_excedente = monto_excedente.toFixed(2);
							
							///
							/// COMENTADO POR ALINEACION 03.01.2014
							/// result = f_pagoPorExcedente(monto_excedente);
							///
						}
                    }

                    //gcastillo - fin - pago con excedente a recibos antiguos
                    document.getElementById("spnBotones").style.display = "none";
                    document.getElementById("spnTitulo").style.display = "block";
                    setValue('hdnEjecutado', 'R');
                    f_validaCajaCerrada();//INICIATIVA-318
                    return true;
                }
                else {
                    alert(msg);
                    f_validaCajaCerrada();//INICIATIVA-318
                    return false;
                }
				


            /*******************************************************************************/
            ///
            /// COMENTADO POR ALINEACION 03.01.2014
            ///
            /*
            var lblTC;
            if (getValue('hdnEjecutado') == 'R') {
                alert('La transacción se está ejecutando.');
                return false;
            }
            if (!esValidoFilaFormaPago('cboTipDocumento1', 'txtDoc1', 'txtMonto1', 'ddlBancoGirador01'))
                return false;
            if (!validarRecibos())
                return false;

            //---
            var msg = '';
            var result = false;


            val_pg = getValue('txtMonto1');
            val_re = getValue('txtTotalRecibosSoles');

            val_pg = parseFloat(val_pg, 10);
            val_re = parseFloat(val_re, 10);

            val_deuda = getValue('txtValorDeuda');
            val_deuda = parseFloat(val_deuda, 10);



            if (getValueHTML('lblTipoMonedaDeuda') == '$') {

                lblTC = getValueHTML('lblTC') * 1;

                lblTC = parseFloat(lblTC, 10);
                val_deuda = val_deuda * lblTC;
                val_deuda = val_deuda.toFixed(2);

            }



            if ((val_pg <= 0) || (val_pg != val_re)) {
                msg = 'El monto total de Pagos debe coincidir con el monto total de\nlos Documentos a pagar.';
            }
            else {
                if (getValue('txtVuelto') * 1 < 0)
                    msg = 'El importe recibido no puede ser menor al importe a pagar.';
            }

            if (val_pg > val_deuda) {
                msg = 'El monto total de Pagos no debe exceder al Valor de la Deuda.';
            }

            if (msg == '') {
                deuda_seleccionada = f_CalculoDeudaSeleccionada();
                deuda_seleccionada = deuda_seleccionada.toFixed(2);
                if (getValueHTML('lblTipoMonedaDeuda') == '$') {
                    lblTC = getValueHTML('lblTC') * 1;
                    lblTC = parseFloat(lblTC, 10);
                    val_pg = val_pg / lblTC;
                }
                //gcastillo - fin - calculo monto de lblDeuda seleccionada
                if (val_pg > deuda_seleccionada) {
                    monto_excedente = val_pg - deuda_seleccionada;
                    monto_excedente = monto_excedente.toFixed(2);
                    result = f_pagoPorExcedente(monto_excedente);
                }
                //gcastillo - fin - pago con excedente a recibos antiguos
                document.getElementById("spnBotones").style.display = "none";
                document.getElementById("spnTitulo").style.display = "block";
                setValue('hdnEjecutado', 'R');
                return true;
            }
            else {
                alert(msg);
                return false;
            }*/
        }	
			
		function formatNameCorp() {
            var valorFlag = "#C#";  // es cliente corporativo
            var nombreCliente = document.getElementById("txtNombreCliente").value.substring(0, 3);

            if (nombreCliente == valorFlag) { //realiza restriccion si no es cliente corporativo

                document.getElementById("txtNombreCli_old").value = document.getElementById("txtNombreCliente").value

                var parteNombreCliente = document.getElementById("txtNombreCliente").value.substring(3);
                document.getElementById("txtNombreCliente").value = parteNombreCliente;

                //////////////////////////////////////////////
                //para la tabla
                var nmTbl = 'dgDocumentosPago';
                var tbl = document.getElementById(nmTbl);

                if (tbl != null) {
                    //var razonSocial;
                    //var idRazonSocial;
                    var razonSocial_old = '';
                    var razonSocial_new = '';
                    var frag = '';

                    for (i = 1; i < tbl.rows.length; i++) {
                        razonSocial_old = dgDocumentosPago.rows[i].cells[1].children(0).innerHTML;
                        razonSocial_new = dgDocumentosPago.rows[i].cells[1].children(0).innerHTML.substring(3);
                        frag = dgDocumentosPago.rows[i].cells[1].children(0).innerHTML.substring(0, 3);

                        if (frag == valorFlag) {
                            dgDocumentosPago.rows[i].cells[1].children(0).innerHTML = razonSocial_new
                        }

                        //alert(razonSocial_old + ' == ' + razonSocial_new + ' == ' + frag);							
                        //dgDocumentosPago.rows[i].cells[3].children(0).innerHTML = '789=> ' + i;
                    }
                }
                ///////////////////////////////////////////////////
            }

        }

			//nhauringa auto						
			function enableDisable(bEnable, textBoxID)
			{
				document.getElementById(textBoxID).disabled = !bEnable;
				columnEnableDisable(bEnable);	
				setValue('txtMonto1','');
				if (bEnable == true)
				{
					document.getElementById(textBoxID).focus();
					document.getElementById("lblSoloing").style.display="block";
					//setVisible('lblSoloing',true);	
					
				}
				else
				{
					document.getElementById("lblSoloing").style.display="none";
					//setVisible('lblSoloing',false);	
				}
				
			}
			
			function columnEnableDisable(bEnable)
			{							
				for (var i = 1; i < dgDocumentosPago.rows.length; i++){
						var fila = dgDocumentosPago.rows[i];				
						var chk = fila.cells[8].children(0);
						var txt = fila.cells[9].children(0);					
						if (chk.checked){
							chk.disabled = false;
							chk.click();						
						}	
						//chk.disabled = bEnable; --cambio nh
						chk.disabled = false;
						//txt.disabled = bEnable; --cambio nh
						txt.disabled = false;
				}
			}
			//nhuariga auto
			function CalcularPagoGrilla()
			{				
				if (document.frmPrincipal.ChkMontoPagar.checked)
				{
					var Monto;
					var MontoGrilla = 0.00;
					Monto = getValue('txtMonto1');
					for (var j = 1; j < dgDocumentosPago.rows.length; j++){
						MontoGrilla = MontoGrilla*1 + (dgDocumentosPago.rows[j].cells[7].children(0).innerHTML)*1;
					}					
					if (Monto == '' || Monto*1 == 0)
					{
						Monto = '';
						deshabilitarGrilla();
					}
					else /// if (Monto > MontoGrilla) /// COMENTADO POR ALINEACION 03.01.2014
					{
						///
						/// COMENTADO POR ALINEACION 03.01.2014
						/// alert('El monto ingresado excede el Monto total de la deuda');
						/// actualizaTotalesRecibos();
						/// return;
					/// } else
					/// {
						///
						CalculoGrillaCorrecta(Monto);
						actualizaTotalesRecibos();	
						CalculoGrillaCorrecta(Monto);
					}					
					actualizaTotalesRecibos();								
				}				
			}
			
			function CalculoGrillaCorrecta(Monto)
			{
				Monto = getValue('txtMonto1');
				
				///
				/// AGREGADO POR ALINEACION 03.01.2014
				var flagSuperaMonto= false;
				///
				
				deshabilitarGrilla();						
				for (var i = 1; i < dgDocumentosPago.rows.length; i++){
				var fila = dgDocumentosPago.rows[i];				
				var deudaRecibo = fila.cells[7].children(0);
				var chk = fila.cells[8].children(0);						
				var txt = fila.cells[9].children(0);
				
				///
				/// AGREGAD0 POR ALINEACION	03.01.2014
				flagSuperaMonto = (Monto < deudaRecibo.innerHTML*1)?true:false;
				///
							
				if ((i==1) && (Monto < deudaRecibo.innerHTML*1))
				{
					deshabilitarGrilla();
					chk.disabled = false;
					chk.click();
					//chk.disabled = true; -- cambio nh
					chk.disabled = false;
					txt.value = (Monto*1).toFixed(2);
					break;
				}else
				{
					chk.disabled = false;
					chk.click();
					//chk.disabled = true; -- cambio nh
					chk.disabled = false;
					//txt.disabled = true; -- cambio nh
					txt.disabled = false;
					
					///
					/// AGREGADO POR ALINEACION 03.01.2014
					var iaux = i+1;
					///
					if(flagSuperaMonto){
					
						if (Monto < deudaRecibo.innerHTML*1)
						{
							txt.value = (Monto*1).toFixed(2);
							break;	
						}
						else
						{
							Monto = Monto - deudaRecibo.innerHTML*1;
						}
					}
					else{
							var iaux = i+1;
							if(iaux >= dgDocumentosPago.rows.length)
							{
								txt.value = (Monto*1).toFixed(2);
								break;	
							}
							else
							{
								Monto = Monto - deudaRecibo.innerHTML*1;
							}
					}
					/// FIN CAMBIO ALINEACION
					///
				}	
				}
			}
			
			function deshabilitarGrilla()
			{
				for (var i = 1; i < dgDocumentosPago.rows.length; i++){
						var fila = dgDocumentosPago.rows[i];
						var chk = fila.cells[8].children(0);
						chk.disabled = false;
						if (chk.checked){
							chk.click();
						}
						//chk.disabled = true; --cambio nh
						chk.disabled = false;
				}
			}
			
			function VisibleInvisibleCheck()
			{			
				var CodServ = getValue('hidCodigoServicio');	
					if (CodServ == "103")
					{
						setVisible('ChkMontoPagar',false);	
						document.getElementById('txtMonto1').disabled = false;
					}			
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
			
			function FN_ValidateOperation(){
				var blnValidare=true;
				var intCountCheck = $('.radios:checked').length;
				if (intCountCheck == 0) {
					alert('Debe seleccionar una cuenta');
					blnValidare = false;
				}
				return blnValidare;
			}
			
			$(document).on('click','#LnkPos1',function(){
				f_EnvioPOS(1,'Envio POS');
				return false;
			});
			$(document).on('click','#LnkDelPos1',function(){
				f_EnvioPOS(1,'Eliminar POS');
				return false;
			});
			$(document).on('click','#LnkPrintPos1',function(){
				f_EnvioPOS(1,'Imprimir POS')
				return false;
			});
			$(document).on('click','#btnGrabar',function(){
				return f_ValidarGrabar();
			});
			//Proy-31949 Fin
			
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
			//INICIATIVA-318 FIN


		</script>
	</HEAD>
	<body onunload="Javascript:history.go(1);" onload="Javascript:history.go(1);RedondearFijos();VisibleInvisibleCheck();formatNameCorp();f_datos_POS();"
		bottomMargin="1" leftMargin="8" rightMargin="8" topMargin="0">
		<form id="frmPrincipal" method="post" runat="server">
			<div style="MARGIN: 8px" id="divContenido">
				<div style="MARGIN: 0px auto; WIDTH: 750px" id="divCuerpo">
					<div style="BORDER-RIGHT: #336699 1px solid; PADDING-RIGHT: 8px; BORDER-TOP: #336699 1px solid; PADDING-LEFT: 8px; PADDING-BOTTOM: 8px; BORDER-LEFT: #336699 1px solid; WIDTH: 100%; PADDING-TOP: 8px; BORDER-BOTTOM: #336699 1px solid">
						<div style="WIDTH: 100%; HEIGHT: 28px" id="divTituloGeneral"><span style="WIDTH: 100%; TEXT-ALIGN: center" class="TituloRConsulta">Recaudación&nbsp;Clientes 
								Fijo y Páginas&nbsp;- Búsqueda de Documentos</span>
						</div>
						<div style="WIDTH: 100%" id="divDatosCliente">
							<table style="WIDTH: 100%" id="tabDatos" class="Arial12b">
								<TR>
									<TD style="HEIGHT: 27px" class="Arial12br">Datos Cliente</TD>
									<TD style="HEIGHT: 27px"></TD>
									<TD style="HEIGHT: 27px"></TD>
									<TD style="HEIGHT: 27px" class="Arial12br" align="right">Tipo de Cambio: S/ &nbsp;
										<asp:label id="lblTC" runat="server" Font-Bold="True"></asp:label></TD>
								</TR>
								<tr>
									<td>Doc. Identidad:</td>
									<td><input style="WIDTH: 109px; HEIGHT: 17px" id="txtIdentificadorCliente" class="clsInputDisable"
											readOnly maxLength="30" size="12" name="txtIdentificadorCliente" runat="server"></td>
									<td>Nombre Cliente:</td>
									<td><input style="WIDTH: 316px; HEIGHT: 17px" id="txtNombreCliente" class="clsInputDisable"
											tabIndex="1" readOnly size="47" name="txtNombreCliente" runat="server"></td>
								</tr>
								<tr>
									<td>Cantidad Documentos:</td>
									<td><input id="txtNumeroDocumentos" class="clsInputDisable" tabIndex="2" readOnly size="3"
											name="txtNumeroDocumentos" runat="server"></td>
									<td>Valor Deuda:&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
										<asp:label id="lblTipoMonedaDeuda" runat="server" Width="13px">S/ </asp:label></td>
									<td><input style="WIDTH: 120px; HEIGHT: 17px" id="txtValorDeuda" class="clsInputDisable" tabIndex="3"
											readOnly size="14" name="txtValorDeuda" runat="server"></td>
								</tr>
							</table>
						</div>
						<div style="WIDTH: 100%" id="tblPagos">
							<table style="WIDTH: 100%" class="Arial12b" cellPadding="3">
								<colgroup>
									<!--PROY-27440 INI-->
									<col align="center" width="200">
									<col align="center" width="250">
									<col align="center" width="300">
									<!--PROY-27440 FIN-->
								</colgroup>
								<thead>
									<tr>
										<td class="Arial12br" align="left">Pagos</td>
										<td class="Arial12br" align="left"></td>
										<td class="Arial12br" vAlign="middle" align="center"><asp:label style="DISPLAY: none" id="lblSoloing" runat="server" Width="170px">Solo Ingrese en este campo.</asp:label></td>
									</tr>
									<tr align="center">
										<td class="ColumnHeader">Forma de Pago</td>
										<td class="ColumnHeader">Nro. Tarjeta/Nro Cheque</td>
										<td class="ColumnHeader">Monto a Pagar (S/)</td>
									</tr>
								</thead>
								<tbody>
									<tr class="RowEven">
										<td style="HEIGHT: 20px">
											<!--PROY-27440 INI-->
											<asp:dropdownlist id="cboTipDocumento1" tabIndex="6" runat="server" Width="200px" onchange="javascript:actualizaControlesTipoDocumento(this);&#13;&#10;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;RedondearEfectivoFijos('txtMonto1','cboTipDocumento1');f_bloqueo_fila(this,1);"
												CssClass="clsSelectEnable"></asp:dropdownlist>
											<!--PROY-27440 FIN-->
										</td>
										<td style="HEIGHT: 20px"><asp:textbox id="txtDoc1" tabIndex="7" onkeypress="javascript:return onkeypressSoloNumeros(event)"
												runat="server" Width="147px" CssClass="clsInputEnable" MaxLength="16"></asp:textbox></td>
										<td style="HEIGHT: 20px">
											<asp:textbox onblur="javascript:return actualizaMontoPago(this,0,false);" id="txtMonto1" tabIndex="8"
												onkeypress="javascript:ValidaNumeroTelmex(this);" runat="server" Width="147px" onchange="javascript:CalcularPagoGrilla();javascript:calculaVuelto()"
												CssClass="clsInputEnable" MaxLength="18" Enabled="False"></asp:textbox>
											<input id="ChkMontoPagar" onclick="enableDisable(this.checked, 'txtMonto1')" type="checkbox">
											<!--PROY-27440 INI-->
                                                                                        <A  style="DISPLAY: none" id="LnkPos1" runat="server"><IMG style="CURSOR: hand" id="icoTranPos1" border="0" alt="Envio POS" src="../images/send-icon.png"  runat="server"> </A>
											<!--PROY-31949 INI-->
											<a style="DISPLAY: none" id="LnkPrintPos1"><IMG style="CURSOR: hand" id="icoPrintPOS1" border="0" alt="Imprimir" src="../images/print-icon.png"> </a>
											<!--PROY-31949 INI-->
                                                                                        <A  style="DISPLAY: none" id="LnkDelPos1"><IMG style="CURSOR: hand" id="icoDelPos1" border="0" alt="Eliminar POS" src="../images/delete-icon.png" > </A>
                                                                                        <input id="HidTipPOS1" type="hidden" name="HidTipPOS1" runat="server"> 
											<!--PROY-27440 FIN-->
										</td>
										<%--nhuaringa auto--%>
									</tr>
									<TR>
										<TD colSpan="3">
											<div style="DISPLAY: none" id="divBancoGirador01" align="center" runat="server"><label style="WIDTH: 100px" class="ColumnHeader" for="ddlBancoGirador01">Banco 
													Girador:</label><asp:dropdownlist id="ddlBancoGirador01" tabIndex="6" runat="server" Width="350px" CssClass="clsSelectEnable"></asp:dropdownlist>
											</div>
										</TD>
									</TR>
								</tbody>
							</table>
						</div>
						<div style="WIDTH: 100%" id="divDocumentos">
							<table style="WIDTH: 100%" class="Arial12b" cellPadding="3">
								<thead>
									<tr>
										<!--td class="Arial12br" colSpan="7">Documentos</td-->
										<td style="HEIGHT: 20px" class="Arial12br" colSpan="7"><asp:label id="totalRecibos" CssClass="Arial12br" Runat="server">Documentos</asp:label></td>
									</tr>
								</thead>
								<tbody>
									<tr>
										<td colSpan="7">
											<div style="BORDER-RIGHT: #95b7f3 1px solid; BORDER-TOP: #95b7f3 1px solid; OVERFLOW: auto; BORDER-LEFT: #95b7f3 1px solid; WIDTH: 100%; BORDER-BOTTOM: #95b7f3 1px solid"
												id="divDocumentosPago"><asp:datagrid id="dgDocumentosPago" tabIndex="17" runat="server" Width="100%" CssClass="Arial12b"
													AutoGenerateColumns="False" BorderWidth="1px" HeaderStyle-CssClass="ColumnHeader" ItemStyle-Height="25px" BorderColor="#393939"
													CellPadding="0">
													<AlternatingItemStyle CssClass="RowOdd"></AlternatingItemStyle>
													<ItemStyle Height="25px" CssClass="RowEven"></ItemStyle>
													<HeaderStyle CssClass="ColumnHeader"></HeaderStyle>
													<Columns>
														<asp:TemplateColumn HeaderText="Cuenta">
															<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
															<ItemTemplate>
																<asp:Label id="lblDOC_Cuenta" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "NUMERO_CUENTA") %>'>
																</asp:Label>
																<INPUT id="hidDOC_CodServicio" style="WIDTH: 8px" type="hidden" name="hidDOC_CodServicio"
																	runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "CODIGO_SERVICIO") %>'>
																<INPUT id="hidDOC_DescServicio" style="WIDTH: 8px" type="hidden" name="hidDOC_DescServicio"
																	runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "DESC_SERVICIO") %>'>
																<INPUT id="hidDOC_CodTipoDoc" style="WIDTH: 8px" type="hidden" name="hidDOC_CodTipoDoc"
																	runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "CODIGO_TIPODOC") %>'>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="Raz&#243;n Social">
															<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
															<ItemTemplate>
																<asp:Label ID="lblDOC_RazonSocial" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "RAZON_SOCIAL") %>'>
																</asp:Label>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="Tel&#233;fono&lt;br&gt;referencia">
															<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
															<ItemStyle HorizontalAlign="Center"></ItemStyle>
															<ItemTemplate>
																<asp:Label ID="lblDOC_TelefonoRef" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TELEFONO_REFERENCIA") %>'>
																</asp:Label>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="Recibo">
															<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
															<ItemTemplate>
																<asp:Label ID="lblDOC_Recibo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "NUMERO_RECIBO") %>'>
																</asp:Label>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="Fecha&lt;br&gt;Emisi&#243;n">
															<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
															<ItemStyle HorizontalAlign="Center"></ItemStyle>
															<ItemTemplate>
																<asp:Label ID="lblDOC_FechaEmision" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FECHA_EMISION") %>'>
																</asp:Label>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="Fecha&lt;br&gt;Vencimiento">
															<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
															<ItemStyle HorizontalAlign="Center"></ItemStyle>
															<ItemTemplate>
																<asp:Label ID="lblDOC_FechaVencimiento" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FECHA_VENCIMIENTO") %>'>
																</asp:Label>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="Moneda">
															<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
															<ItemStyle HorizontalAlign="Center"></ItemStyle>
															<ItemTemplate>
																<asp:Label ID="lblDOC_TipoMoneda" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TIPO_MONEDA") %>'>
																</asp:Label>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="Deuda">
															<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
															<ItemStyle HorizontalAlign="Right"></ItemStyle>
															<ItemTemplate>
																<asp:Label ID="lblDOC_Deuda" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DEUDA") %>'>
																</asp:Label>
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn HeaderText="Seleccionar">
															<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
															<ItemStyle HorizontalAlign="Center"></ItemStyle>
															<ItemTemplate>
																<INPUT id="chbxDOC_Seleccionar" style="TEXT-ALIGN: center" type="checkbox" name="chbxDOC_Seleccionar"
																	runat="server">
															</ItemTemplate>
														</asp:TemplateColumn>
														<asp:TemplateColumn>
															<HeaderStyle HorizontalAlign="Center" Width="80px"></HeaderStyle>
															<ItemStyle HorizontalAlign="Right"></ItemStyle>
															<HeaderTemplate>
																Monto a Pagar (
																<asp:Label id="lblDOCSimboloTipoMoneda" runat="server" Width="13px">S/ </asp:Label>)
															</HeaderTemplate>
															<ItemTemplate>
																<asp:TextBox onkeypress="javascript:ValidaNumeroTelmex(this);" id="txtDOC_MontoPagar" runat="server"
																	Width="80px" CssClass="clsInputDisable" MaxLength="10" ReadOnly="True"></asp:TextBox>
															</ItemTemplate>
														</asp:TemplateColumn>
													</Columns>
												</asp:datagrid></div>
											<div style="BORDER-RIGHT: #95b7f3 1px solid; BORDER-TOP: #95b7f3 1px solid; BORDER-LEFT: #95b7f3 1px solid; WIDTH: 100%; BORDER-BOTTOM: #95b7f3 1px solid; TEXT-ALIGN: right"
												id="divPieTotales"><asp:panel id="pnlDOCTotalDolares" Runat="server"><SPAN style="WIDTH: 80px">Total</SPAN><SPAN style="WIDTH: 20px">$</SPAN>
													<asp:textbox id="txtTotalRecibosDolares" tabIndex="18" runat="server" Width="80px" CssClass="clsInputDisable"
														MaxLength="10" ReadOnly="True"></asp:textbox>
												</asp:panel>
												<div id="divDOCTotalSoles"><span style="WIDTH: 80px">Total </span><span style="WIDTH: 20px">
														S/</span>
													<asp:textbox id="txtTotalRecibosSoles" tabIndex="18" runat="server" Width="80px" CssClass="clsInputDisable"
														MaxLength="10" ReadOnly="True"></asp:textbox></div>
											</div>
											<div style="BORDER-RIGHT: #336699 1px solid; BORDER-TOP: #336699 1px solid; BORDER-LEFT: #336699 1px solid; BORDER-BOTTOM: #336699 1px solid"
												id="paginacionWraper">
												<div style="LEFT: 280px; MARGIN: 0px auto; WIDTH: 182px; POSITION: relative" id="paginacion"><asp:button id="primeroButton" runat="server" CssClass="BotonOptm" Text="|<"></asp:button><asp:button id="anteriorButton" runat="server" CssClass="BotonOptm" Text="<<"></asp:button><asp:label id="paginacionLabel" Width="95px" CssClass="labelPaginacion" Runat="server">Página {0} de {1}</asp:label><asp:button id="siguienteButton" runat="server" CssClass="BotonOptm" Text=">>"></asp:button><asp:button id="ultimoButton" runat="server" CssClass="BotonOptm" Text=">|"></asp:button></div>
											</div>
										</td>
									<!--PROY-27440 INI-->
									<tr>
										<td colSpan="7" align="center">
											<asp:label id="lblEnvioPos" runat="server" CssClass="TituloRConsulta"></asp:label>
										</td>
									</tr>
									<!--PROY-27440 FIN-->
									</tr>
								</tbody>
							</table>
						</div>
					</div>
					<span style="WIDTH: 100%; HEIGHT: 1px"></span>
					<div style="BORDER-RIGHT: #336699 1px solid; PADDING-RIGHT: 8px; BORDER-TOP: #336699 1px solid; PADDING-LEFT: 8px; PADDING-BOTTOM: 8px; BORDER-LEFT: #336699 1px solid; WIDTH: 100%; PADDING-TOP: 8px; BORDER-BOTTOM: #336699 1px solid; HEIGHT: 25px"
						id="divImportes">
						<div style="MARGIN-LEFT: 30px; MARGIN-RIGHT: 30px; HEIGHT: 40px" class="Arial12B"><span style="WIDTH: 200px">Importe 
								Recibido Soles:</span>
							<asp:textbox onblur="javascript:return actualizaMontoPago(this,0,true);" id="txtRecibidoPen"
								tabIndex="19" onkeypress="javascript:ValidaNumeroTelmex(this);" runat="server" Width="120px"
								onchange="javascript:calculaVuelto();" CssClass="clsInputEnable"></asp:textbox><span style="WIDTH: 160px; TEXT-ALIGN: right" id="spnVuelto">Vuelto:&nbsp;&nbsp; 
								S/</span>
							<asp:textbox id="txtVuelto" tabIndex="21" runat="server" Width="120px" CssClass="clsInputDisable"
								ReadOnly="True"></asp:textbox><br>
							<span style="WIDTH: 200px">Importe Recibido Dólares:</span>
							<asp:textbox onblur="javascript:return actualizaMontoPago(this,0,true);" id="txtRecibidoUsd"
								tabIndex="20" onkeypress="javascript:ValidaNumeroTelmex(this);" runat="server" Width="120px"
								onchange="javascript:calculaVuelto();" CssClass="clsInputEnable"></asp:textbox></div>
					</div>
					<span style="WIDTH: 100%; HEIGHT: 0px"></span>
					<div style="WIDTH: 100%; TEXT-ALIGN: center" id="divComandos">
						<div style="BORDER-RIGHT: #336699 1px solid; BORDER-TOP: #336699 1px solid; PADDING-BOTTOM: 8px; BORDER-LEFT: #336699 1px solid; WIDTH: 400px; PADDING-TOP: 8px; BORDER-BOTTOM: #336699 1px solid; TEXT-ALIGN: center"><span style="DISPLAY: block; TEXT-ALIGN: center" id="spnBotones" runat="server"><asp:button id="btnGrabar" tabIndex="22" runat="server" Width="100px" CssClass="BotonOptm" Text="Grabar"></asp:button>&nbsp;
								<asp:button id="btnCancelar" tabIndex="23" runat="server" Width="100px" CssClass="BotonOptm"
									Text="Cancelar"></asp:button></span><span style="DISPLAY: none" id="spnTitulo" runat="server"><span style="WIDTH: 28px; TEXT-ALIGN: center" class="TituloRConsulta">El&nbsp;pago&nbsp;se&nbsp;está&nbsp;procesando</span>
							</span>
						</div>
					</div>
				</div>
				<div id="divPie"><input style="WIDTH: 8px" id="hdnMensaje" type="hidden" name="hdnMensaje" runat="server">
					<asp:textbox id="txttotalOculto" tabIndex="21" runat="server" Width="1px"></asp:textbox><input style="WIDTH: 8px" id="hdnPuntoDeVenta" type="hidden" name="hdnPuntoDeVenta" runat="server">
					<input style="WIDTH: 8px" id="hdnUsuario" type="hidden" name="hdnUsuario" runat="server">
					<input style="WIDTH: 8px" id="hdnBinAdquiriente" type="hidden" name="hdnBinAdquiriente"
						runat="server"> <input style="WIDTH: 8px" id="hdnCodComercio" type="hidden" name="hdnCodComercio" runat="server">
					<input style="WIDTH: 8px" id="hidCanal" type="hidden" name="hidCanal" runat="server">
					<input style="WIDTH: 8px" id="hidNumeroTrace" type="hidden" name="hidNumeroTrace" runat="server">
					<input style="WIDTH: 8px" id="hdnRutaLog" type="hidden" name="hdnRutaLog" runat="server">
					<input style="WIDTH: 8px" id="hdnDetalleLog" size="1" type="hidden" name="hdnDetalleLog"
						runat="server"> <input style="WIDTH: 8px" id="hidTipoIdentificador" type="hidden" name="hidTipoIdentificador"
						runat="server"> <input style="WIDTH: 8px" id="hidCodigoServicio" type="hidden" name="hidCodigoServicio"
						runat="server"> <input style="WIDTH: 8px" id="hidIdentificador" type="hidden" name="hidIdentificador" runat="server">
					<input style="WIDTH: 8px" id="hdnEjecutado" value="I" type="hidden" name="hdnEjecutado"
						runat="server"> <INPUT style="WIDTH: 8px" id="hidFPCodigoEfectivo" type="hidden" name="hidFPCodigoEfectivo"
						runat="server"> <INPUT style="WIDTH: 8px" id="hidFPCodigoCheque" type="hidden" name="hidFPCodigoCheque"
						runat="server"> <INPUT style="WIDTH: 8px" id="hiddTotal" value="0" type="hidden" name="hiddTotal" runat="server">
					<INPUT style="WIDTH: 8px" id="hidPagina" value="0" type="hidden" name="hidPagina" runat="server"
						size="1"> <INPUT style="WIDTH: 16px; HEIGHT: 22px" id="hiddPaginaSinAjuste" size="1" type="hidden"
						runat="server" NAME="hiddPaginaSinAjuste"> 
					<!-- Atributos de la Página -->
					<p style="DISPLAY: none"><asp:textbox id="txtTarjCred" runat="server" Width="8px"></asp:textbox><asp:textbox id="txtBIN" runat="server" Width="8px"></asp:textbox><asp:checkbox id="chkDocumentosConsultar" runat="server" Visible="False"></asp:checkbox><asp:textbox id="txtNumeroDocumento" runat="server" Width="8px">0</asp:textbox><INPUT style="WIDTH: 48px; HEIGHT: 22px" id="hidUltimoRecibo" size="2" type="hidden" runat="server"
							NAME="hidUltimoRecibo"><INPUT style="WIDTH: 48px; HEIGHT: 22px" id="hiddTotalOriginal" size="2" type="hidden"
							name="Hidden1" runat="server"></p>
				</div>
			</div>
			<div style="DISPLAY: none"><asp:textbox id="txtNombreCli_old" runat="server" Width="147px" CssClass="clsInputEnable" MaxLength="16"></asp:textbox></div>
			<!--PROY-27440 INI--><input id="HidPtoVenta" type="hidden" name="HidPtoVenta" runat="server">
			<input id="HidIntAutPos" type="hidden" name="HidIntAutPos" runat="server"> <input id="HidGrabAuto" type="hidden" name="HidGrabAuto" runat="server">
			<input id="HidCodOpera" type="hidden" name="HidCodOpera" runat="server"> <input id="HidDesOpera" type="hidden" name="HidDesOpera" runat="server">
			<input id="HidTipoOpera" type="hidden" name="HidTipoOpera" runat="server"> <input id="HidTipoTarjeta" type="hidden" name="HidTipoTarjeta" runat="server">
			<input id="HidTipoPago" type="hidden" name="HidTipoPago" runat="server"> <input id="HidEstTrans" type="hidden" name="HidEstTrans" runat="server">
			<input id="HidTipoPOS" type="hidden" name="HidTipoPOS" runat="server"> <input id="HidTipoTran" type="hidden" name="HidTipoTran" runat="server">
			<input id="HidFila1" type="hidden" name="HidFila1" runat="server"> <input id="HidFila2" type="hidden" name="HidFila2" runat="server">
			<input id="HidFila3" type="hidden" name="HidFila3" runat="server"> <input id="HidTipoMoneda" type="hidden" name="HidTipoMoneda" runat="server">
			<input id="HidTransMC" type="hidden" name="HidTransMC" runat="server"> <input id="HidMonedaMC" type="hidden" name="HidMonedaMC" runat="server">
			<input id="HidApliPOS" type="hidden" name="HidApliPOS" runat="server"> <input id="HidMonedaVisa" type="hidden" name="HidMonedaVisa" runat="server">
			<input id="HidDatoPosVisa" type="hidden" name="HidDatoPosVisa" runat="server"> <input id="HidDatoPosMC" type="hidden" name="HidDatoPosMC" runat="server">
			<input id="HidDatoAuditPos" type="hidden" name="HidDatoAuditPos" runat="server">
			<input id="HidIdCabez" type="hidden" name="HidIdCabez" runat="server"> <input id="hidF1" type="hidden" name="hidF1" runat="server">
			<!--PROY-27440 FIN-->
			<!--PROY-31949 INI-->
                        <input id="hdnFlagIntAutPos" type="hidden" name="hdnFlagIntAutPos" runat="server"> 
                        <input id="HidNumIntentosPago" type="hidden" name="HidNumIntentosPago" runat="server">
                        <input id="HidNumIntentosAnular" type="hidden" name="HidNumIntentosAnular" runat="server"> 
                        <input id="HidMsjErrorNumIntentos" type="hidden" name="HidMsjErrorNumIntentos" runat="server"> 
                        <input id="HidMsjErrorTimeOut" type="hidden" name="HidMsjErrorTimeOut" runat="server">
                        <input id="HidMsjNumIntentosPago" type="hidden" name="HidMsjNumIntentosPago" runat="server"> 
                        <input id="HidFlagCajaCerrada" type="hidden" name="HidFlagCajaCerrada" runat="server"> 
                        <input id="HidMsjCajaCerrada" type="hidden" name="HidMsjCajaCerrada" runat="server"> 
                        <input id="HidMedioPagoPermitidas" type="hidden" name="HidMedioPagoPermitidas" runat="server">
                        <!--PROY-31949 FIN--></form>
	</body>
</HTML>
