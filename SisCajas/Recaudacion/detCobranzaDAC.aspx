<%@ Page Language="vb" AutoEventWireup="false" Codebehind="detCobranzaDAC.aspx.vb" Inherits="SisCajas.detCobranzaDAC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>detCobranzaDAC</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../estilos/est_General.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="../librerias/date-picker.js"></script>
		<script language="JavaScript" src="../librerias/Lib_FuncValidacion.js"></script>
		<script src="../librerias/Lib_Redondeo.js" type="text/javascript"></script>
		<!--PROY-27440 INI-->
		<script language="JavaScript" src="../librerias/msrsclient.js"></script>
                <script language="JavaScript" src="../librerias/jquery-1.8.2.min.js"></script> <!--PROY-31949-->
		<script language="JavaScript" src="../Scripts/form.js"></script>
		<script language="JavaScript" src="../Scripts/xml2json.js"></script>
		<script language="JavaScript" src="../Scripts/operacionPOS.js"></script>
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<!--PROY-27440 FIN-->
		<script language="javascript">
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
			var varNameMonto;
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
	var varNameCboBanco;
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
				document.getElementById('txtDoc' + i).value = varTarjeta;
				document.getElementById('cboTipDocumento' + i).selectedIndex = varComboIndex;
				document.getElementById('txtMonto' + i).disabled = true;
				document.getElementById('txtDoc' + i).disabled = true;
				document.getElementById('cboTipDocumento' + i).disabled = true;
				document.getElementById('cboBanco'+i).disabled=true;//Proy-31949
				
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
		var sel = document.getElementById(varNameCboBanco);
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
                              //document.getElementById(varNameLinkPOS).style.visibility = "visible";
                              //document.getElementById(varNameImgPOS).style.visibility = "visible";						
                              //document.getElementById(varNameLinkDelPOS).style.visibility = "visible";
                              //document.getElementById(varNameImgDelPOS).style.visibility = "visible";
				document.getElementById(varNameCboBanco).disabled = true;
				document.getElementById(varNameTdoc).value  = ""; 
				sel.selectedIndex = -1;
				
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
				/*validacion IAPOS CNH*/							
				document.getElementById(varNameTxtDoc).value = "";
				document.getElementById(varNameTxtDoc).disabled = false;
                                //Proy-31949 Inicio
                                document.getElementById(varNameMonto).value="";
                                document.getElementById(varNameMonto).disabled=false;
                                //document.getElementById(varNameLinkPOS).style.visibility = "hidden";
                                //document.getElementById(varNameImgPOS).style.visibility = "hidden";				
                                //document.getElementById(varNameLinkDelPOS).style.visibility = "hidden";
                                //document.getElementById(varNameImgDelPOS).style.visibility = "hidden";
				document.getElementById(varNameCboBanco).disabled = false;
				varNameCboBanco.selectedIndex = -1;
				
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
                      document.getElementById(varNameMonto).value="";
                      document.getElementById(varNameMonto).disabled=false;
                      //document.getElementById(varNameLinkPOS).style.visibility = "hidden";
                      //document.getElementById(varNameImgPOS).style.visibility = "hidden";				
                      //document.getElementById(varNameLinkDelPOS).style.visibility = "hidden";
                      //document.getElementById(varNameImgDelPOS).style.visibility = "hidden";
			document.getElementById(varNameCboBanco).disabled = false;
			varNameCboBanco.selectedIndex = -1;
                      
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
		//document.getElementById(varNameLinkPOS).style.visibility = "hidden";
		//document.getElementById(varNameImgPOS).style.visibility = "hidden";				
		//document.getElementById(varNameLinkDelPOS).style.visibility = "hidden";
		//document.getElementById(varNameImgDelPOS).style.visibility = "hidden";
		$('#'+varNameLinkPOS).fadeOut('fast');
		$('#'+varNameLinkDelPOS).fadeOut('fast');
		$(varLnkPrintPos).fadeOut('fast');
		//Proy-31949 Fin
		
		for (var i=1; i<3; i ++)
		{
			document.getElementById("cboBanco" + i.ToString()).disabled = true;
		}
	 }
	
	 function f_ConsultaFP()
	 {
		for(i=1; i<=3; i++)
		{
			eval("vv=document.frmPrincipal.cboTipDocumento"+i+".value")
			if (vv !="")
			{
				if(i==1){document.frmPrincipal.hidF1.value=vv}
				if(i==2){document.frmPrincipal.hidF2.value=vv}
				if(i==3){document.frmPrincipal.hidF3.value=vv}
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
				varNameTdoc = "txtDoc1";
				varNameCboBanco = "cboBanco1";
				//CNH
				varNameTxtDoc = "txtDoc1";
						varLnkPrintPos='#LnkPrintPos1';//Proy-31949
						varNameImgPrint='#icoPrintPOS1';//Proy-31949
						ContPago1 = 1;//Proy-31949
						varNameMonto='txtMonto1';//Proy-31949
			break;
			case 2:
						varNameLinkPOS="LnkPos2";
						varNameImgPOS="icoTranPos2";
						varNameLinkDelPOS="LnkDelPos2";
						varNameImgDelPOS="icoDelPos2";
				varNameTipoPOS = "HidTipPOS2";
				varNameTdoc = "txtDoc2";
				varNameCboBanco = "cboBanco2";
				//CNH
				varNameTxtDoc = "txtDoc2";
						varLnkPrintPos='#LnkPrintPos2';//Proy-31949
						varNameImgPrint='#icoPrintPOS2';//Proy-31949						
						ContPago2 = 1;//Proy-31949
						varNameMonto='txtMonto2';//Proy-31949
			break;
			case 3:
						varNameLinkPOS="LnkPos3";
						varNameImgPOS="icoTranPos3";
						varNameLinkDelPOS="LnkDelPos3";
						varNameImgDelPOS="icoDelPos3";
				varNameTipoPOS = "HidTipPOS3";
				varNameTdoc = "txtDoc3";
				varNameCboBanco = "cboBanco3";
				//CNH
				varNameTxtDoc = "txtDoc3";
						varLnkPrintPos='#LnkPrintPos3';//Proy-31949
						varNameImgPrint='#icoPrintPOS3';//Proy-31949
						ContPago3 = 1;//Proy-31949
						varNameMonto='txtMonto3';//Proy-31949
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
	
	function f_activar_fila(intFila,bolEnable)
	{
		document.getElementById("lblEnvioPos").innerHTML  = "";
		document.getElementById("txtMonto" + intFila).disabled = bolEnable;
		//document.getElementById("txtDoc" + intFila).disabled = bolEnable;
		//CNH
		document.getElementById("txtDoc" + intFila).disabled = true;
		document.getElementById("cboTipDocumento" + intFila).disabled = bolEnable;
              document.getElementById("cboBanco" + intFila).disabled = bolEnable;
		
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
		varCodOpe='';
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
		
				var varNameDelFila; 
				var varNameTipoDoc; 
		var vaNameLink;
		varNameTipoPOS='';
		varNameDoc=''
		
		switch (intFila)
		{
			case 1:
				varNameLinkPOS="LnkPos1";varNameImgPOS="icoTranPos1";
				varNameLinkDelPOS="LnkDelPos1";varNameImgDelPOS="icoDelPos1";
				//varNameDelFila="ImgEliminar1";
				varNameTipoDoc="cboTipDocumento1";
				varNameDoc="txtDoc1";
				varNameMonto="txtMonto1";
				//vaNameLink="IdLink1";
				varNameTipoPOS = "HidTipPOS1";
                                varLnkPrintPos='#LnkPrintPos1';//Proy-31949
                                varNameImgPrint='#icoPrintPOS1';//Proy-31949
                                varNameCboBanco = "cboBanco1";
			break;
			case 2:
				varNameLinkPOS="LnkPos2";varNameImgPOS="icoTranPos2";
				varNameLinkDelPOS="LnkDelPos2";varNameImgDelPOS="icoDelPos2";
				//varNameDelFila="ImgEliminar2";
				varNameTipoDoc="cboTipDocumento2";
				varNameDoc="txtDoc2";
				varNameMonto="txtMonto2";
				//vaNameLink="IdLink2";
				varNameTipoPOS = "HidTipPOS2";
                                varLnkPrintPos='#LnkPrintPos2';//Proy-31949
                                varNameImgPrint='#icoPrintPOS2';//Proy-31949
                                varNameCboBanco = "cboBanco2";
			break;
			case 3:
				varNameLinkPOS="LnkPos3";varNameImgPOS="icoTranPos3";
				varNameLinkDelPOS="LnkDelPos3";varNameImgDelPOS="icoDelPos3";
				//varNameDelFila="ImgEliminar3";
				varNameTipoDoc="cboTipDocumento3";
				varNameDoc="txtDoc3";
				varNameMonto="txtMonto3";
				//vaNameLink="IdLink3";
				varNameTipoPOS = "HidTipPOS3";
                                varLnkPrintPos='#LnkPrintPos3';//Proy-31949
                                varNameImgPrint='#icoPrintPOS3';//Proy-31949
                                varNameCboBanco = "cboBanco3";
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
		//document.getElementById(vaNameLink).disabled = true;
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
		
				var idRecaudacion = document.getElementById("txtIdentificadorCliente").value;//Proy-31949
		var varNroPedido = '';
		var varNroTelefono = '';
		varNroPedido = '';//document.getElementById("hidDocSap").value; 
				varNroTelefono = document.getElementById("txtIdentificadorCliente").value;//Proy-31949
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
					'|TipoPago=' + document.getElementById("HidTipoPago").value +
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
				}catch(err)
		{
		}	
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
			alert(err.description);
					document.getElementById("lblEnvioPos").innerHTML  = "";
					f_activar_fila(varIntFila,false);
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
			var varNomCliente = document.getElementById("txtNombreCliente").value;
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
			alert(err.description);
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
					var varNroPedido = document.getElementById("txtIdentificadorCliente").value;//Proy-31949 
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
						document.getElementById(varNameCboBanco).disabled=true;
						
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
						document.getElementById("txtDoc" + varIntFila).disabled = true;
						document.getElementById("HidFila" + varIntFila).value = '';
				document.getElementById("txtDoc" + varIntFila).value = '';
				document.getElementById("HidFila" + varIntFila).value = '';
				varNroTarjeta = '';
				
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
				'|ComboIndex=' + document.getElementById("cboTipDocumento" + varIndex).selectedIndex + 
				'|NroReferncia=' + varNroReferencia;
			}
			
			var varNomCliente = document.getElementById("txtNombreCliente").value;
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
					var varNroPedido = document.getElementById("txtIdentificadorCliente").value;//Proy-31949 
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
						varNroRefVisa = trim1(varNroRefVisa);
						varRespTipTarPos = jsonObj.Envelope.Body.RespuestaPeticionOperacionMC.IdTarjeta; //Proy-31949-Inicio
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
						document.getElementById(varNameCboBanco).disabled=true;
						
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
						document.getElementById("txtDoc" + varIntFila).disabled = true;
				document.getElementById("txtDoc" + varIntFila).value = '';
						document.getElementById("HidFila" + varIntFila).value = '';
				document.getElementById("HidFila" + varIntFila).value = '';
				varNroTarjeta = '';
				
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
						document.getElementById(varNameCboBanco).disabled=false;
						
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
		if (co.message) {
			alert("Context:" + co.context + "\nError: " + co.message);
		}
	}
	//PROY-27440 FIN	

		function f_Grabar(){
				blnClosingMod = false;
		    var blnErr = false;
			if (document.getElementById("txtMonto1") != null)
			{
			if (f_Validar()) {
				CalculoVuelto();
				if (frmPrincipal.txtVuelto.value*1<0){
					alert('El importe recibido no puede ser menor al importe pagado..');
					event.returnValue = false;	
					blnErr = true;		
				}
				//document.frmPrincipal.hidNumFilas.value = f_NumFilas();			
				if (! f_ValidarTarjeta())
				{
					event.returnValue = false;			
					blnErr = true;
				}	
				if (! f_ValidarCheque())
				{
					event.returnValue = false;			
					blnErr = true;
				}
				if 	(!blnErr)
			    {
					//INC000001566216-INICIO
					if(confirm("¿Está seguro de registrar el pago?")){
						document.getElementById("Botones").style.display ="none"
						document.getElementById("divTitulo").style.display ="block"
					}else
						event.returnValue = false;	
					//INC000001566216-FIN
			    }
			}
			else
				event.returnValue = false;
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
			for(i=1; i<4; i++)
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

			for(i=1; i<4; i++)
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
			
		function f_ValidarCheque() {
		var cheque = "ZCHQ";	
			for(i=0; i<document.frmPrincipal.elements.length; i++){			
				if(document.frmPrincipal.elements[i].name.substring(0,15)=="cboTipDocumento"){
					if (document.frmPrincipal.elements[i].value!=""){
					    if (cheque.indexOf(document.frmPrincipal.elements[i].value)>=0) {
							if (document.frmPrincipal.elements[i+1].value==""){
								alert('Debe seleccionar un banco');
								return false;						
							}
							if (document.frmPrincipal.elements[i+2].value==""){
								alert('Debe ingresar el número de cheque');
								return false;						
							}
							
						}
					}
				}
			}				
			return true;
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
					if(document.frmPrincipal.elements[i+3].value!=""){
						if (document.frmPrincipal.elements[i].value==""){
						alert('Debe de seleccionar una forma de pago');
						return false;
						}
					}
					if(document.frmPrincipal.elements[i].value!=""){
						if(document.frmPrincipal.elements[i+3].value==""){
							alert('Debe ingresar un monto de pago');
							return false;
						}
					}
				}
			}				
			return true;
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
		
		function f_Recalcular(obj){
			CalculoVuelto();
		}
		
		function CalculoVuelto(){
			suma = 0.0;
					
			for(i=1; i<4; i++){
				eval("ss=document.frmPrincipal.txtMonto"+i+".value");
				if(ss!='')
				{	
				  eval("vv=document.frmPrincipal.cboTipDocumento"+i+".value")
			      if (vv == 'ZEFE')
			        suma = suma + (ss*1);	
			   	}
			}			
			suma= Math.round(suma*100)/100;			
			if (frmPrincipal.txtRecibidoPen.value=="")	
				frmPrincipal.txtRecibidoPen.value = "0.00"
			if (frmPrincipal.txtRecibidoUsd.value=="") 
				frmPrincipal.txtRecibidoUsd.value = "0.00"
			var tc = document.all.lblTC.innerText*1;
					
			//var vuelto = Math.round((frmPrincipal.txtRecibidoPen.value * 1 + frmPrincipal.txtRecibidoUsd.value * tc - suma)*100)/100;//aotane 05.08.2013				
			var vuelto = Math.round((frmPrincipal.txtRecibidoPen.value * 1 + frmPrincipal.txtRecibidoUsd.value * tc - suma)*1000)/1000;//aotane 05.08.2013				
			
			//frmPrincipal.txtVuelto.value = vuelto;												
			var vueltoRedondeo = Math.round(RedondeaInmediatoSuperior(vuelto)*100)/100;//aotane 05.08.2013		
			frmPrincipal.txtVuelto.value = vueltoRedondeo;//aotane 05.08.2013	
		}
		
		function f_grillaVisible(){
			document.getElementById("divDocumentos").style.display ="block";
		}
		
		function f_grillaOcultar(){
			document.getElementById("divDocumentos").style.display ="none";
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
				var serverURL =  '../Pos/ProcesoPOS.aspx';
				
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
	<body onload="f_datos_POS();RedondearInicio();">
		<form id="frmPrincipal" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="750" border="0">
				<tr>
					<td>
						<table style="BORDER-RIGHT: #336699 2px solid; BORDER-TOP: #336699 2px solid; BORDER-LEFT: #336699 2px solid; BORDER-BOTTOM: #336699 2px solid"
							cellSpacing="0" cellPadding="5" align="left" border="0">
							<thead>
								<tr>
									<td class="TituloRConsulta" align="center">Cobranza DAC's&nbsp;- Resultados 
										Búsqueda</td>
								</tr>
							</thead>
							<tbody>
								<tr>
									<td>
										<table class="Arial12b" style="WIDTH: 720px; HEIGHT: 52px" cellPadding="3">
											<thead>
												<tr>
													<td class="Arial12br" colSpan="3">Datos Cliente</td>
													<td class="Arial12br" align="right">Tipo de Cambio:
														<asp:label id="lblTC" runat="server" Font-Bold="True"></asp:label></td>
												</tr>
											</thead>
											<tbody>
												<tr>
													<td>Cod Cliente DAC:</td>
													<td><input class="clsInputDisable" id="txtIdentificadorCliente" readOnly maxLength="30" size="16"
															name="txtIdentificadorCliente" runat="server"></td>
													<td>Razón Social DAC:</td>
													<td><input class="clsInputDisable" id="txtNombreCliente" readOnly size="60" name="txtNombreCliente"
															runat="server"></td>
												</tr>
												<TR>
													<TD id="tdDeuda" runat="server">Deuda:</TD>
													<TD><INPUT class="clsInputDisable" id="txtMonto" readOnly maxLength="30" size="16" name="txtIdentificadorCliente"
															runat="server"></TD>
													<TD></TD>
													<TD></TD>
												</TR>
											</tbody>
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
													<td style="HEIGHT: 21px">
														<!--PROY-27440 INI--><asp:dropdownlist id="cboTipDocumento1" tabIndex="6" runat="server" CssClass="clsSelectEnable" onChange="javascript:RedondearEfectivo('txtMonto1','cboTipDocumento1');f_bloqueo_fila(this,1);"
															AutoPostBack="True"></asp:dropdownlist><!--INICIATIVA-318-->
														<!--PROY-27440 FIN--></td>
													<td style="WIDTH: 352px; HEIGHT: 21px" width="352"><asp:dropdownlist id="cboBanco1" runat="server" CssClass="clsSelectEnable"></asp:dropdownlist>&nbsp;&nbsp;
														<asp:textbox id="txtDoc1" tabIndex="7" runat="server" CssClass="clsInputEnable" Width="147px"></asp:textbox></td>
													<td style="HEIGHT: 21px"><asp:textbox id="txtMonto1" onkeyup="javascript:RedondearEfectivo('txtMonto1','cboTipDocumento1');"
															tabIndex="8" runat="server" CssClass="clsInputEnable" Width="147px"></asp:textbox>
														<!--PROY-27440 INI--><A style="DISPLAY: none" id="LnkPos1" runat="server"><IMG style="CURSOR: hand" id="icoTranPos1" border="0" alt="Envio POS" src="../images/send-icon.png"
																runat="server"> </A>
														<!--PROY-31949 INI--><a style="DISPLAY: none" id="LnkPrintPos1"><IMG style="CURSOR: hand" id="icoPrintPOS1" border="0" alt="Imprimir" src="../images/print-icon.png">
														</a>
														<!--PROY-31949 INI--><A style="DISPLAY: none" id="LnkDelPos1"><IMG style="CURSOR: hand" id="icoDelPos1" border="0" alt="Eliminar POS" src="../images/delete-icon.png">
														</A>
														<!--PROY-27440 FIN--></td>
												</tr>
												<tr class="RowOdd">
													<td><asp:dropdownlist id="cboTipDocumento2" tabIndex="9" runat="server" CssClass="clsSelectEnable" onChange="javascript:RedondearEfectivo('txtMonto2','cboTipDocumento2');f_bloqueo_fila(this,2);"
															AutoPostBack="True"></asp:dropdownlist></td><!--INICIATIVA-318-->
													<td style="WIDTH: 352px"><asp:dropdownlist id="cboBanco2" runat="server" CssClass="clsSelectEnable"></asp:dropdownlist>&nbsp;&nbsp;
														<asp:textbox id="txtDoc2" tabIndex="10" runat="server" CssClass="clsInputEnable" Width="147px"></asp:textbox></td>
													<td><asp:textbox id="txtMonto2" onkeyup="javascript:RedondearEfectivo('txtMonto2','cboTipDocumento2');"
															tabIndex="8" runat="server" CssClass="clsInputEnable" Width="147px"></asp:textbox>
														<!--PROY-27440 INI--><A style="DISPLAY: none" id="LnkPos2"><IMG style="CURSOR: hand" id="icoTranPos2" border="0" alt="Envio POS" src="../images/send-icon.png">
														</A>
														<!--PROY-31949 INI--><a style="DISPLAY: none" id="LnkPrintPos2"><IMG style="CURSOR: hand" id="icoPrintPOS2" border="0" alt="Imprimir" src="../images/print-icon.png">
														</a>
														<!--PROY-31949 INI--><A style="DISPLAY: none" id="LnkDelPos2"><IMG style="CURSOR: hand" id="icoDelPos2" border="0" alt="Eliminar POS" src="../images/delete-icon.png">
														</A>
														<!--PROY-27440 FIN--></td>
												</tr>
												<tr class="RowEven">
													<td><asp:dropdownlist id="cboTipDocumento3" tabIndex="12" runat="server" CssClass="clsSelectEnable" onChange="javascript:RedondearEfectivo('txtMonto3','cboTipDocumento3');f_bloqueo_fila(this,3);"
															AutoPostBack="True"></asp:dropdownlist></td><!--INICIATIVA-318-->
													<td style="WIDTH: 352px"><asp:dropdownlist id="cboBanco3" runat="server" CssClass="clsSelectEnable"></asp:dropdownlist>&nbsp;&nbsp;
														<asp:textbox id="txtDoc3" tabIndex="13" runat="server" CssClass="clsInputEnable" Width="147px"></asp:textbox></td>
													<td><asp:textbox id="txtMonto3" onkeyup="javascript:RedondearEfectivo('txtMonto3','cboTipDocumento3');"
															tabIndex="8" runat="server" CssClass="clsInputEnable" Width="147px"></asp:textbox>
														<!--PROY-27440 INI--><A style="DISPLAY: none" id="LnkPos3"><IMG style="CURSOR: hand" id="icoTranPos3" border="0" alt="Envio POS" src="../images/send-icon.png">
														</A>
														<!--PROY-31949 INI--><a style="DISPLAY: none" id="LnkPrintPos3"><IMG style="CURSOR: hand" id="icoPrintPOS3" border="0" alt="Imprimir" src="../images/print-icon.png">
														</a>
														<!--PROY-31949 INI--><A style="DISPLAY: none" id="LnkDelPos3"><IMG style="CURSOR: hand" id="icoDelPos3" border="0" alt="Eliminar POS" src="../images/delete-icon.png">
														</A>
														<!--PROY-27440 FIN--></td>
												</tr>
											</tbody>
										</table>
										<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
											<tr>
												<td align="center" height="10">
													<!--PROY-27440 INI--><asp:label id="lblEnvioPos" runat="server" CssClass="TituloRConsulta"></asp:label>
													<!--PROY-27440 FIN--></td>
											</tr>
										</table>
									</td>
								</tr>
							</tbody>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
							<tr>
								<td>
									<div id="divDocumentos" style="WIDTH: 100%" runat="server">
										<table class="Arial12b" style="WIDTH: 100%" cellPadding="3">
											<thead>
												<tr>
													<td class="Arial12br" style="HEIGHT: 20px" colSpan="7"><asp:label id="totalRecibos" CssClass="Arial12br" Runat="server">Documentos</asp:label></td>
												</tr>
											</thead>
											<tbody>
												<tr>
													<td colSpan="7">
														<div id="divDocumentosPago" style="BORDER-RIGHT: #95b7f3 1px solid; BORDER-TOP: #95b7f3 1px solid; OVERFLOW: auto; BORDER-LEFT: #95b7f3 1px solid; WIDTH: 100%; BORDER-BOTTOM: #95b7f3 1px solid"><asp:datagrid id="dgDocumentosPago" tabIndex="17" runat="server" CssClass="Arial12b" Width="100%"
																CellPadding="0" BorderColor="#393939" ItemStyle-Height="25px" HeaderStyle-CssClass="ColumnHeader" BorderWidth="1px" AutoGenerateColumns="False">
																<AlternatingItemStyle CssClass="RowOdd"></AlternatingItemStyle>
																<ItemStyle Height="25px" CssClass="RowEven"></ItemStyle>
																<HeaderStyle CssClass="ColumnHeader"></HeaderStyle>
																<Columns>
																	<asp:TemplateColumn HeaderText="TipoDocumento">
																		<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
																		<ItemTemplate>
																			<asp:Label id="lblTipoDoc" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TIPO_DOCUMENTO") %>'>
																			</asp:Label>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Nro Documento">
																		<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
																		<ItemTemplate>
																			<asp:Label ID="lblNroDoc" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "NRO_DOCUMENTO") %>'>
																			</asp:Label>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Descripcion Servicio">
																		<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Left"></ItemStyle>
																		<ItemTemplate>
																			<asp:Label ID="lblServicio" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DESCRIPCION_DOCUMENTO") %>'>
																			</asp:Label>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Fecha&lt;br&gt;Emisi&#243;n">
																		<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Center"></ItemStyle>
																		<ItemTemplate>
																			<asp:Label ID="lblDOC_FechaEmision" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FEC_EMISION") %>'>
																			</asp:Label>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Fecha&lt;br&gt;Vencimiento">
																		<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Center"></ItemStyle>
																		<ItemTemplate>
																			<asp:Label ID="lblDOC_FechaVencimiento" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FEC_VENC") %>'>
																			</asp:Label>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Total">
																		<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
																		<ItemStyle HorizontalAlign="Right"></ItemStyle>
																		<ItemTemplate>
																			<asp:Label ID="lblImporte" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "IMPORTE") %>'>
																			</asp:Label>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																</Columns>
															</asp:datagrid></div>
													</td>
												</tr>
											</tbody>
										</table>
									</div>
									<DIV></DIV>
								</td>
							</tr>
						</table>
						<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="100%" align="center"
							border="1">
							<tr>
								<td>
									<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
										<tr>
											<td vAlign="top" width="10"></td>
											<td vAlign="middle" width="100%">
												<table class="Arial12B" cellSpacing="2" cellPadding="0" width="90%" align="center" border="0">
													<tr>
														<td width="190">Importe Recibido Soles:</td>
														<td><asp:textbox id="txtRecibidoPen" tabIndex="10" runat="server" CssClass="clsInputEnable"></asp:textbox></td>
														<td>Vuelto:</td>
														<td><asp:textbox id="txtVuelto" runat="server" CssClass="clsInputDisable" ReadOnly="True"></asp:textbox></td>
													</tr>
													<tr>
														<td width="190">Importe Recibido Dolares:</td>
														<td><asp:textbox id="txtRecibidoUsd" tabIndex="10" runat="server" CssClass="clsInputEnable"></asp:textbox></td>
														<td></td>
														<td></td>
													</tr>
												</table>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
							<tr>
								<td height="10"></td>
							</tr>
						</table>
						<div id="Botones">
							<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="400" align="center"
								border="1">
								<tr>
									<td align="center">
										<table cellSpacing="2" cellPadding="0" border="0">
											<tr>
												<td align="center" width="28"></td>
												<td align="center" width="60"><asp:button id="btnGrabar" runat="server" CssClass="BotonOptm" Width="100px" Text="Grabar"></asp:button></td>
												<td align="center" width="28"></td>
												<td align="center" width="60"><asp:button id="btnCancelar" runat="server" CssClass="BotonOptm" Width="100px" Text="Cancelar"></asp:button></td>
												<td align="center" width="28"></td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
						</div>
						<div id="divTitulo" style="DISPLAY: none">
							<table cellSpacing="2" cellPadding="0" width="400" align="center" border="0">
								<tr>
									<td class="TituloRConsulta" align="center" width="28">El&nbsp;pago&nbsp;se&nbsp;está&nbsp;procesando</td>
								</tr>
							</table>
						</div>
					</td>
				</tr>
			</table>
			<p style="DISPLAY: none"><asp:textbox id="txtTarjCred" runat="server"></asp:textbox><asp:textbox id="txtBIN" runat="server"></asp:textbox></p>
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
			<input id="HidIdCabez" type="hidden" name="HidIdCabez" runat="server"> <input id="HidTipPOS1" type="hidden" name="HidTipPOS1" runat="server">
			<input id="HidTipPOS2" type="hidden" name="HidTipPOS2" runat="server"> <input id="HidTipPOS3" type="hidden" name="HidTipPOS3" runat="server">
			<input id="hidF1" type="hidden" name="hidF1" runat="server"> <input id="hidF2" type="hidden" name="hidF2" runat="server">
			<input id="hidF3" type="hidden" name="hidF3" runat="server"> 
			<!--PROY-27440 FIN-->
                        <!--PROY-31949 INI--><input id="hidResponseDoc" type="hidden" name="hidResponseDoc">
			<input id="hdnMsgMayor" type="hidden" name="hdnMsgMayor" runat="server">
                        <input id="hdnMsgMenor" type="hidden" name="hdnMsgMenor" runat="server">
			<input id="HidNumIntentosPago" type="hidden" name="HidNumIntentosPago" runat="server">
			<input id="HidNumIntentosAnular" type="hidden" name="HidNumIntentosAnular" runat="server">
			<input id="HidMsjErrorNumIntentos" type="hidden" name="HidMsjErrorNumIntentos" runat="server">
			<input id="HidMsjErrorTimeOut" type="hidden" name="HidMsjErrorTimeOut" runat="server">
			<input id="HidMsjNumIntentosPago" type="hidden" name="HidMsjNumIntentosPago" runat="server">
			<input id="HidFlagCajaCerrada" type="hidden" name="HidFlagCajaCerrada" runat="server">
			<input id="HidMsjCajaCerrada" type="hidden" name="HidMsjCajaCerrada" runat="server">
			<input id="HidMedioPagoPermitidas" type="hidden" name="HidMedioPagoPermitidas" runat="server">
			<!--INICIATIVA-318 INI-->
                        <input id="hddEnvioAutorizador" type="hidden" name="hddEnvioAutorizador" runat="server">
			<input id="hddComboAutorizador" type="hidden" name="hddComboAutorizador" runat="server">
			<input id="hddcboFormaPago" type="hidden" name="hddcboFormaPago" runat="server">
                        <!--INICIATIVA-318 FIN-->			
			<!--PROY-31949 FIN--></form>
	</body>
</HTML>
