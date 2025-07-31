<%@ Page Language="vb" AutoEventWireup="false" Codebehind="poolConsultaPagos.aspx.vb" Inherits="SisCajas.poolConsultaPagos" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Aplicativo PVU</title>
		<meta http-equiv="Pragma" content="no-cache">
		<META http-equiv="Expires" content="Mon, 06 Jan 1990 00:00:01 GMT">
		<LINK href="../estilos/est_General.css" type="text/css" rel="styleSheet">
			<script language="JavaScript" src="../librerias/date-picker.js"></script>
			<script language="JavaScript" src="../librerias/Lib_FuncValidacion.js"></script>
			<script language="JavaScript" src="../librerias/Lib_FuncGenerales.js"></script>
			<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
			<script language="JavaScript">

<!--

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
					case "btnAnularPago":						
						if (!f_AnularPagos()) event.returnValue = false;
						break;
					case "btnAnular":						
						if (!f_Anular()) event.returnValue = false;
						break;
					case "btnDescompensar":						
						if (!validarDescompensar()) event.returnValue = false;
						break;			
					case "btnFormasPago": // PROY-27440 INI 
						if (!f_FormasPago()) event.returnValue = false;
						break; //PROY-27440 FIN
					}
				break;
		}
	}
	
	//AGREGADO POR FFS INICIO
	function validarDescompensar(){
		var strCod = "";
		var strEstado = "";
		var strCodProcesar = "";
		
		var strTipoDoc = "";
		var strCodSunat = "";
		
		for (var i = 1; i < dgPool.rows.length; i++){
				if (dgPool.rows[i].cells[0].children.rbPagos.checked){					
					strCod = dgPool.rows[i].cells[0].children.rbPagos.value;
					strEstado =dgPool.rows[i].cells[15].innerText;	//PROY-30166 - IDEA - 38934 - Se aumento el indice por la columna adicional
					strCodProcesar=dgPool.rows[i].cells[17].children.IdCodigo.value;	//PROY-30166 - IDEA - 38934 - Se aumento el indice por la columna adicional

					strTipoDoc = dgPool.rows[i].cells[0].children.rbPagos.TipoDoc;
					strCodSunat = dgPool.rows[i].cells[6].innerText;	//PROY-30166 - IDEA - 38934 - Se aumento el indice por la columna adicional
					break;
				}
		}
			strCod =trim(strCod);
			strEstado =trim(strEstado.toLowerCase());
			strCodProcesar =trim(strCodProcesar);
			strCodProcesar =trim(strCodProcesar);
			
			setValue('hidParamImpresion',strCod + ';' + strCodProcesar + ';' + strCodSunat + ';' + strTipoDoc);
			
			if (strCod == "") {
					document.frmPrincipal.action="";
					alert("Debe Seleccionar un Documento para Descompensar Pago");					
					return false;
			}
			else
			{					
					strEstado =trim(strEstado.toLowerCase());
					if (strEstado == "procesado")
					{
							// Si esta Procesado Ingresa a Descompensar
							frmPrincipal.txtRbPagos.value = strCod;						
							frmPrincipal.cmdDescompensar.click();
							return true;
					}
					else if(strEstado == "error de proceso"){
						alert('Comuniquese con soporte.\n\nDocumento no puede ser descompensar porque tuvo un error al ser procesado en SAP.');
						return false;
					}
					else
					{
							answer = confirm ("Este Documento requiere ser procesado para continuar con la operación ¿Desea procesar el Documento?");
							
							if (answer) {					
								frmPrincipal.txtRbPagos.value = strCodProcesar;
								frmPrincipal.cmdProcesar.click();
								return true;						
							}
							else{								
								return false;
							}
					}
			}
	}
	//AGREGADO POR FFS FIN 
	
	//function validarDescompensar(){
		//if(verificarPagosProcesado()){
		//	return frmPrincipal.btnDescompensar.click();
			//return true;
		//}
		//else
		//{
					
			//return false;
		//}
	//}	
	
	function f_AnularPagos()
	{
		var strCod = "";
		var strEstado="";
		var strCodProcesar = "";
		for (var i = 1; i < dgPool.rows.length; i++){
					if (dgPool.rows[i].cells[0].children.rbPagos.checked){					
						strCod = dgPool.rows[i].cells[0].children.rbPagos.value;
						strEstado =dgPool.rows[i].cells[15].innerText;	//PROY-30166 - IDEA - 38934 - Se aumento el indice por la columna adicional
						//strCodProcesar=dgPool.rows[i].cells[16].children.IdCodigo.value;
						break;
					}
			}
			
		if (strCod == "") 
		{	
			//event.returnValue = false;
			document.frmPrincipal.action="";
			alert("Debe Seleccionar un Documento para Anular Pago");					
			return false;
		}
		else
		{
			frmPrincipal.txtRbPagos.value = strCod;		
			frmPrincipal.cmdAnularPago.click();							
			return true;
		}	
		
	}
	//CAMBIADO POR FFS INICIO
	//function f_AnularPagos() 
	//{				
	//		var strCod = "";
	//		var strEstado="";
	//		var strCodProcesar = "";
	//		
	//		for (var i = 1; i < dgPool.rows.length; i++){
	//				if (dgPool.rows[i].cells[0].children.rbPagos.checked){					
	//					strCod = dgPool.rows[i].cells[0].children.rbPagos.value;
	//					strEstado =dgPool.rows[i].cells[14].innerText;
	//					strCodProcesar=dgPool.rows[i].cells[16].children.IdCodigo.value;
	//					break;
	//				}
	//		}
	//		
	//		strCod =trim(strCod);
	//		strCodProcesar =trim(strCodProcesar);
	//		
	//		if (strCod == "") {
	//
	//
	//				//event.returnValue = false;
	//				document.frmPrincipal.action="";
	//				alert("Debe Seleccionar un Documento para Anular Pago");					
	//				return false;
	//		}
	//		else
	//		{					
	//				strEstado =trim(strEstado.toLowerCase());					
	//				if (strEstado == "procesado")
	//				{
	//						frmPrincipal.txtRbPagos.value = strCod;		
	//						frmPrincipal.cmdAnularPago.click();							
	//						return true;
	//				}
	//				else
	//				{
	//					alert('Comuniquese con soporte.\n\nDocumento no puede ser anular porque tuvo un error al ser procesado en SAP.');
	//					return false;
	//				}
	//				/*else
	//				{
	//						answer = confirm ("Este Documento requiere ser procesado para continuar con la operación ¿Desea procesar el Documento?");
	//						
	//						if (answer) {					
	//							frmPrincipal.txtRbPagos.value = strCodProcesar;
	//							frmPrincipal.cmdProcesar.click();
	//							return true;						
	//						}
	//						else{								
	//							return false;
	//						}
	//				}*/
	//		}
	//}
	//CAMBIADO POR FFS FIN

	function f_Buscar() {
		if(ValidaFechaA('document.frmPrincipal.txtFecha', true))
		{
			document.frmPrincipal.submit();
		}
	}

	//CAMBIADO POR FFS INICIO
	function f_Anular() {
	
			var strCod = "";			
			var strEstado="";
			var strCodProcesar = "";
			
			
			for (var i = 1; i < dgPool.rows.length; i++){
					if (dgPool.rows[i].cells[0].children.rbPagos.checked){
						strCod = dgPool.rows[i].cells[0].children.rbPagos.value;
						strEstado =dgPool.rows[i].cells[15].innerText;	//PROY-30166 - IDEA - 38934 - Se aumento el indice por la columna adicional
						strCodProcesar=dgPool.rows[i].cells[17].children.IdCodigo.value;	//PROY-30166 - IDEA - 38934 - Se aumento el indice por la columna adicional		
						break;
					}
			}
			strCod =trim(strCod);
			strCodProcesar =trim(strCodProcesar);
			
			answer = confirm ("¿Está seguro de anular el Documento seleccionado?")			
			if (answer) {
				if (strCod == "") {
						//event.returnValue = false;
						document.frmPrincipal.action="";
						alert("Debe Seleccionar un Documento para Anular Pago");					
						return false;
				}
				else
				{		
					strEstado =trim(strEstado.toLowerCase());					
					if (strEstado == "procesado")
					{
							frmPrincipal.txtRbPagos.value = strCod;				
							frmPrincipal.cmdAnular.click();	
							return true;			
					}
					else
					{
						alert('Comuniquese con soporte.\n\nDocumento no puede se anular porque tuvo un error al ser procesado en SAP.');
						return false;
					}
					/*else
					{
							answer = confirm ("Este Documento requiere ser procesado para continuar con la operación ¿Desea procesar el Documento?");
							
							if (answer) {					
								frmPrincipal.txtRbPagos.value = strCodProcesar;
								frmPrincipal.cmdProcesar.click();
								return true;						
							}
							else{								
								return false;
							}
					}*/							
				}
			}
		
	}
	//CAMBIADO POR FFS FIN
	
	function f_TipoDocVenta(){
		var doc,tipo;
		tipo="";
		doc=f_Value();
		if (document.frmPrincipal.AUXi.value==1)
		{	tipo=document.frmPrincipal.txttipo.value;
		}
		else{
			if (document.frmPrincipal.AUXi.value>1){
				for(i=0;i<document.frmPrincipal.AUXi.value;i++)
					if	(document.frmPrincipal.rbPagosC[i].value == doc)
					{	tipo=document.frmPrincipal.txttipo[i].value;
					}
			}
		} 
		return tipo
	}
	
	function f_NumSunat(){
		var doc,tipo;
		tipo="";
		doc=f_Value();
		if (document.frmPrincipal.AUXi.value==1)
		{	tipo=document.frmPrincipal.txttipo.value;
		}
		else{
			if (document.frmPrincipal.AUXi.value>1){
				for(i=0;i<document.frmPrincipal.AUXi.value;i++)
					if	(document.frmPrincipal.rbPagosC[i].value == doc)
					{	tipo=document.frmPrincipal.txtnumsunat[i].value;
					}
			}
		}
		return tipo
	}

	//CAMBIADO POR FFS
	function f_Reasignacion()
	{
		event.returnValue = false;
		
			var strCod = "";			
			var strEstado="";
			var strCodProcesar = "";
			
			for (var i = 1; i < dgPool.rows.length; i++){
					if (dgPool.rows[i].cells[0].children.rbPagos.checked){
						strCod = dgPool.rows[i].cells[0].children.rbPagos.value;
						strEstado =dgPool.rows[i].cells[15].innerText;		//PROY-30166 - IDEA - 38934 - Se aumento el indice por la columna adicional		
						strCodProcesar=dgPool.rows[i].cells[17].children.IdCodigo.value;	//PROY-30166 - IDEA - 38934 - Se aumento el indice por la columna adicional
						break;
					}
			}
			strCod =trim(strCod);
			strCodProcesar =trim(strCodProcesar);
			
			/***************/
			var strCodSAP = "";						
			var strCodProcesar = "";
			var strCodSunat = "";			
			var strTipoDoc = "";
			for (var i = 1; i < dgPool.rows.length; i++){
					if (dgPool.rows[i].cells[0].children.rbPagos.checked){
						strCodSAP = dgPool.rows[i].cells[0].children.rbPagos.value;
						strTipoDoc = dgPool.rows[i].cells[0].children.rbPagos.TipoDoc;
						strCodSunat = dgPool.rows[i].cells[6].innerText;			//PROY-30166 - IDEA - 38934 - Se aumento el indice por la columna adicional
						strCodProcesar=dgPool.rows[i].cells[17].children.IdCodigo.value;	//PROY-30166 - IDEA - 38934 - Se aumento el indice por la columna adicional
						break;
					}
			}			
			setValue('hidParamImpresion',strCodSAP + ';' + strCodProcesar + ';' + strCodSunat + ';' + strTipoDoc);
			/***************/
		   if (f_Boton())
		   {
		   				strEstado =trim(strEstado.toLowerCase());					
						if (strEstado == "procesado")
						{
								frmPrincipal.txtRbPagos.value = strCod;
																
								for (i=1;i<dgPool.rows.length;i++)
								{
									if (dgPool.rows[i].cells[0].children.rbPagos.checked)
									{											
											//CAMBIADO POR FFS INICIO dgPool.rows[i].cells[14].children.FKART.value (ORIGINAL) y el metodo trim()
										if (trim(dgPool.rows[i].cells[16].children.FKART.value) == "<%= configurationsettings.appsettings("cteTIPODOC_DEPOSITOGARANTIA")%>" )	//PROY-30166 - IDEA - 38934 - Se aumento el indice por la columna adicional
										{
											alert('No se puede realizar Reasignación con este documento.');
											return false;
										}
								             
										if (dgPool.rows[i].cells[6].innerText == "" || dgPool.rows[i].cells[6].innerText == "0000000000000000")	//PROY-30166 - IDEA - 38934 - Se aumento el indice por la columna adicional
										{
											alert('Error documento no tiene asignado Número Sunat');
											return false;
										} 
											else
										{
												
											event.returnValue = true;
											return true;
										}
										  
									}
								}					
						}
						else if (strEstado == "error de proceso"){
							alert('Comuniquese con soporte.\n\nDocumento no puede ser Reasignar porque tuvo un error al ser procesado en SAP.');
							return false;
						}
						else
						{
								answer = confirm ("Este Documento requiere ser procesado para continuar con la operación ¿Desea procesar el Documento?");
								
								if (answer) {					
									frmPrincipal.txtRbPagos.value = strCodProcesar;
									frmPrincipal.cmdProcesar.click();									
									return true;						
								}
								else{								
									return false;
								}
						}
		   }
		   else
             return false;
	}

//ejecuta la impresiòn del operacionesImp.aspx
	function Imprimir()
	{		
				var objIframe = document.getElementById("IfrmImpresion");
				window.open(objIframe.contentWindow.location);
				
	
	}
	
	
	//*** se ejecuta cuando se termina de hacer el pago, para la impresiòn " " 22-12-2014.
	function f_Imprimir(){	
		//event.returnValue = false;
		
		//alert('imprimir');
	
	<% Session("PoolPagados") = "1" %>
	
			var strCodSAP = "";			
			var strCodSunat = "";
			var strDepGar = "";
			var strTipoDoc = "";
			var strEstado = "";
			var strCodProcesar = "";
			var strClaseFactura = "";
			
			for (var i = 1; i < dgPool.rows.length; i++){
					if (dgPool.rows[i].cells[0].children.rbPagos.checked){
						strCodSAP = dgPool.rows[i].cells[0].children.rbPagos.value;
						strDepGar = dgPool.rows[i].cells[0].children.rbPagos.DepositoGar;
						strTipoDoc = dgPool.rows[i].cells[0].children.rbPagos.TipoDoc;
						//alert(strTipoDoc);
						strCodSunat = dgPool.rows[i].cells[6].innerText; //PROY-30166 - IDEA - 38934 - Se aumento el indice por la columna adicional
						//**se corrigio:
						strEstado	=dgPool.rows[i].cells[17].innerText; //PROY-140397 -MCKINSEY -> Jordy Sullca RevenixZ - Se aumento el indice por la columna adicional
						//Clase Factura
						strClaseFactura = dgPool.rows[i].cells[5].innerText; //PROY-30166 - IDEA - 38934 - Se aumento el indice por la columna adicional
						
						//****estos dos campos me dan error . " " ****
						//strEstado	=dgPool.rows[i].cells[14].innerText;
						//strCodProcesar=dgPool.rows[i].cells[16].children.IdCodigo.value;
						break;
					}
			}
			
			strCodSAP=trim(strCodSAP);
			strCodProcesar =trim(strCodProcesar);	//Es usado cuando el estado es NO PROCESADO.
			setValue('hidParamImpresion',strCodSAP + ';' + strCodProcesar + ';' + strCodSunat + ';' + strTipoDoc);
			
			//alert(strEstado);
	
			if (strCodSAP==""){
				alert("Seleccione alguna transacción..!!");
				return;
			}
			else //AGREGADO POR FFS
			{	
			
				strEstado =trim(strEstado.toLowerCase());
				
				strClaseFactura = trim(strClaseFactura);
				var strNotaCanje = '<%= ConfigurationSettings.AppSettings("DesClaseNotaCanje")%>';
				if (strClaseFactura == strNotaCanje) 
				{
				
					//PROY-23700-IDEA-29415 - INI 
									var objIframe = document.getElementById("IfrmImpresion");
									
									objIframe.style.visibility = "visible";
									objIframe.style.width = 0;
									objIframe.style.height = 0;
				
									objIframe.src = "OperacionesImp.aspx?codRefer="+ strCodSAP+ "&FactSunat="+strCodSunat+"&Reimpresion=1&TipoDoc="+strTipoDoc;
																			
									return true;
								
                                        //PROY-23700-IDEA-29415 - FIN 
				}
				
				if (strEstado == "error de proceso"){
					alert('Comuniquese con soporte.\n\nDocumento no puede ser imprimir porque tuvo un error al ser procesado en SAP.');
					return false;
				}
				if (strEstado == "procesado")
					{		
								
									//Se procede a Imprimir el Documento
									var objIframe = document.getElementById("IfrmImpresion");
									
									objIframe.style.visibility = "visible";
									objIframe.style.width = 0;
									objIframe.style.height = 0;
									
									//*** llammos a la impresiòn ****//
									if (strTipoDoc=="RENTA ADELANTADA"){
									objIframe.src = "OperacionesImp_DG.aspx?numDepGar="+ strCodSAP;
									}
									else
									{
									objIframe.src = "OperacionesImp.aspx?codRefer="+ strCodSAP+ "&FactSunat="+strCodSunat+"&Reimpresion=1&TipoDoc="+strTipoDoc;
									}
									return true; 
									//****
									
									if (strTipoDoc=="ZFPA" || strTipoDoc=="G/R"){
									}
									else{				
										if (strTipoDoc=="DG") {
											objIframe.src = "OperacionesImp_DG.aspx?numDepGar="+ strDepGar;
										}
										else {					
											objIframe.src = "OperacionesImp.aspx?codRefer="+ strCodSAP+ "&FactSunat="+strCodSunat+"&Reimpresion=1&TipoDoc="+strTipoDoc;
										}				
									}
									return true;
								
					}
					else
					{
							answer = confirm ("Este Documento requiere ser procesado para continuar con la operación ¿Desea procesar el Documento?");
								if (answer) {
									//Como no esta recuperando el strCodProcesar, va a salir un error:
									frmPrincipal.txtRbPagos.value = strCodProcesar;
									frmPrincipal.cmdProcesar.click();
									return true;
								}
								else{
									
									return false;
								}
					}
			}
			
		
     
			
			
			//objIframe.contentWindow.location.replace('OperacionesImp_DG.asp?numDepGar='+ strDepGar);
			
	/*
		var rValores;
		rValores = f_Valores();
		if(document.frmPrincipal.AUXi.value > 0){
			if (rValores[3] > 0){
				var objIframe = document.getElementById("IfrmImpresion");
			
				objIframe.style.visibility = "visible";
				objIframe.style.width = 0;
				objIframe.style.height = 0;
			
		
			if (rValores[4]=="ZFPA" || rValores[4]=="G/R"){
				//alert("ImpGuiaRemision.asp");
				objIframe.contentWindow.location.replace('ImpGuiaRemision.asp?DocSap='+f_Value()+'&strUltNum='+f_NumSunat()+'&strRef='+f_NumSunat()+'&SoloImp=1');
			}
			else{
				if (rValores[4]=="DG") {
					//document.frmPrincipal.txtNRO_DEP_GARANTIA.value  = f_ValueNRO_DEP_GARANTIA();
					//alert("OperacionesImp_DG.asp");
					objIframe.contentWindow.location.replace('OperacionesImp_DG.asp?codRefer=' + f_Value()+ '&FactSunat=' + f_ValueFactSunat() + '&numDepGar='+ f_ValueNRO_DEP_GARANTIA());
					//openwindowscroll('OperacionesImp_DG.asp?codRefer=' + f_Value()+ '&FactSunat=' + f_ValueFactSunat()  + '&numDepGar='+ f_ValueNRO_DEP_GARANTIA() ,'Impresión',500,800,100,100);
				}
				else {
					objIframe.contentWindow.location.replace('OperacionesImp.asp?codRefer=' + f_Value()+ '&FactSunat=' + f_ValueFactSunat() + '&Reimpresion=1&TipoDoc=' + rValores[4]);
					//openwindowscroll('OperacionesImp.asp?codRefer=' + f_Value()+ '&FactSunat=' + f_ValueFactSunat() + '&Reimpresion=1&TipoDoc=' + rValores[4] ,'Impresión',500,800,100,100);
					}
			}
			
				//openwindowscroll('OperacionesImp.asp?codRefer=' + f_Value()+ '&FactSunat=' + f_ValueFactSunat()  ,'Impresión',500,800,100,100);
			}else{
				alert("Debe Seleccionar un Documento a Imprimir");
			}
		}else{
			alert("No Hay Documentos para Imprimir")
		}
	*/
	}	//*** fin del imprimir " " 

	function f_Descompensacion() {
		/*var rValores;
		rValores = f_Valores();
		if(document.frmPrincipal.AUXi.value > 0){
			if (rValores[3] > 0){

				//RRM_DG
				if (rValores[4]=="DG") {
					alert('No se puede realizar Descompensación con este documento.');
					return;
				}
				//RRM_DG
				
				document.frmPrincipal.codRefer.value = rValores[3];
				document.frmPrincipal.codOperacion.value = "12"
				document.frmPrincipal.action='Operaciones.asp';
				document.frmPrincipal.submit();
			}else{
				alert("Debe Seleccionar un Documento para Descompensar");
			}
		}else{
			alert("No Hay Documentos Pagados")
		}*/
	}

	function f_Value() {
		var rval, ff;
		ff = document.frmPrincipal.AUXi.value
		rval="0";
		if(ff == 1){
				if (document.frmPrincipal.rbPagosC.checked) {
				rval = document.frmPrincipal.rbPagosC.value;
				}
		}
		else{
			for (x = 0; x < document.frmPrincipal.rbPagosC.length; x++) {

				if (document.frmPrincipal.rbPagosC[x].checked) {
					rval = document.frmPrincipal.rbPagosC[x].value;
				}
			}
		}
		return rval;
	}

//RRM
	function f_ValuePEDIDO() {
		var rval, ff;
        ff = document.frmPrincipal.AUXi.value;
		rval="xxx";
		if(ff == 1){
				if (document.frmPrincipal.rbPagosC.checked) {
				rval = document.frmPrincipal.PEDIDO.value;
				}
		}
		else{
			for(i=0; i<document.frmPrincipal.PEDIDO.length; i++){
			   if (document.frmPrincipal.rbPagosC[i].checked){
			       rval=document.frmPrincipal.PEDIDO[i].value;
			       break;
			   }
			}
		}
		return rval;
	}
//RRM

//RRM_DG
	function f_ValueNRO_DEP_GARANTIA() {
		var rval, ff;
        ff = document.frmPrincipal.AUXi.value;
		rval="xxx";
		if(ff == 1){
				if (document.frmPrincipal.rbPagosC.checked) {
				rval = document.frmPrincipal.NRO_DEP_GARANTIA.value;
				}
		}
		else{
			for(i=0; i<document.frmPrincipal.NRO_DEP_GARANTIA.length; i++){
			   if (document.frmPrincipal.rbPagosC[i].checked){
			       rval=document.frmPrincipal.NRO_DEP_GARANTIA[i].value;
			       break;
			   }
			}
		}
		return rval;
	}

	function f_ValueNRO_REF_DEP_GAR() {
		var rval, ff;
        ff = document.frmPrincipal.AUXi.value;
		rval="xxx";
		if(ff == 1){
				if (document.frmPrincipal.rbPagosC.checked) {
				rval = document.frmPrincipal.NRO_REF_DEP_GAR.value;
				}
		}
		else{
			for(i=0; i<document.frmPrincipal.NRO_REF_DEP_GAR.length; i++){
			   if (document.frmPrincipal.rbPagosC[i].checked){
			       rval=document.frmPrincipal.NRO_REF_DEP_GAR[i].value;
			       break;
			   }
			}
		}
		return rval;
	}
//RRM_DG

	//INI CLIE_REC 20-10-2005
	function f_ValueNRO_CONTRATO() {

		var rval, ff;
        ff = document.frmPrincipal.AUXi.value;
		rval="xxx";
		if(ff == 1){
				if (document.frmPrincipal.rbPagosC.checked) {
				rval = document.frmPrincipal.NRO_CONTRATO.value;
				}
		}
		else{
			for(i=0; i<document.frmPrincipal.NRO_CONTRATO.length; i++){
			   if (document.frmPrincipal.rbPagosC[i].checked){
			       rval=document.frmPrincipal.NRO_CONTRATO[i].value;
			       break;
			   }
			}
		}
		return rval;
	}
	
	function f_ValueNRO_OPE_INFOCORP() {
		var rval, ff;
        ff = document.frmPrincipal.AUXi.value;
		rval="xxx";
		if(ff == 1){
				if (document.frmPrincipal.rbPagosC.checked) {
				rval = document.frmPrincipal.NRO_OPE_INFOCORP.value;
				}
		}
		else{
			for(i=0; i<document.frmPrincipal.NRO_OPE_INFOCORP.length; i++){
			   if (document.frmPrincipal.rbPagosC[i].checked){
			       rval=document.frmPrincipal.NRO_OPE_INFOCORP[i].value;
			       break;
			   }
			}
		}
		return rval;
	}
	
	function f_ValueCODIGO_APROBACIO() {
		var rval, ff;
        ff = document.frmPrincipal.AUXi.value;
		rval="xxx";
		if(ff == 1){
				if (document.frmPrincipal.rbPagosC.checked) {
				rval = document.frmPrincipal.CODIGO_APROBACIO.value;
				}
		}
		else{
			for(i=0; i<document.frmPrincipal.CODIGO_APROBACIO.length; i++){
			   if (document.frmPrincipal.rbPagosC[i].checked){
			       rval=document.frmPrincipal.CODIGO_APROBACIO[i].value;
			       break;
			   }
			}
		}
		return rval;
	}

	function f_ValueTIPO_ACTIV_CLTE() {
		var rval, ff;
        ff = document.frmPrincipal.AUXi.value;
		rval="xxx";
		if(ff == 1){
				if (document.frmPrincipal.rbPagosC.checked) {
				rval = document.frmPrincipal.TIPO_ACTIV_CLTE.value;
				}
		}
		else{
			for(i=0; i<document.frmPrincipal.TIPO_ACTIV_CLTE.length; i++){
			   if (document.frmPrincipal.rbPagosC[i].checked){
			       rval=document.frmPrincipal.TIPO_ACTIV_CLTE[i].value;
			       break;
			   }
			}
		}
		return rval;
	}
	
	function f_ValueACTIVACION_LINEA() {
		var rval, ff;
        
		ff = document.frmPrincipal.AUXi.value;
		rval="xxx";
		if(ff == 1){
				if (document.frmPrincipal.rbPagosC.checked) {
				rval = document.frmPrincipal.ACTIVACION_LINEA.value;
				}
		}
		else{
			for(i=0; i<document.frmPrincipal.ACTIVACION_LINEA.length; i++){
			   if (document.frmPrincipal.rbPagosC[i].checked){
			       rval=document.frmPrincipal.ACTIVACION_LINEA[i].value;
			       break;
			   }
			}
		
		}
		return rval;
	}
	//FIN CLIE_REC 20-10-2005


	function f_ValueFactSunat() {
		var rval, ff;
		ff = document.frmPrincipal.AUXi.value
		rval="0";
		if(ff == 1){
				if (document.frmPrincipal.rbPagosC.checked) {
				rval = document.frmPrincipal.rbFactSunat.value;
				}
		}
		else{
			for (x = 0; x < document.frmPrincipal.rbFactSunat.length; x++) {

				if (document.frmPrincipal.rbPagosC[x].checked) {
					rval = document.frmPrincipal.rbFactSunat[x].value;
				}
			}
		}
		return rval;
	}
	
	function f_ImpAcuerdo(){
		var rValores;
		rValores = f_Valores();
		if(document.frmPrincipal.AUXi.value > 0){
			if (rValores[3] > 0){
				if (rValores[4]=="ZFPA" || rValores[4]=="G/R"){
					document.frmPrincipal.codRefer.value = rValores[3];
					document.frmPrincipal.TipoOpc.value = "2";
					document.frmPrincipal.action='AcuerdoAlquiler.asp';
					document.frmPrincipal.submit();
				} else {
					alert("Solo se pueden imprimir acuerdos para documentos de alquiler")
				}
			}else{
				alert("Debe Seleccionar un Documento de Pago");
			}
		}else{
			alert("No Hay Documentos por Pagar")
		}
	}	
	
	function f_Valores(){
		var rval = new Array(14);
		var ff;
        ff = document.frmPrincipal.AUXi.value;
		rval[0] = "xxx";
		rval[1] = "xxx";
		rval[2] = "xxx";
		rval[3] = "0";
		rval[4] = "xxx";
		rval[5] = "xxx";
		rval[6] = "xxx";
		rval[7] = "xxx";
		rval[8] = "xxx";
		rval[9] = "xxx";
		rval[10] = "xxx";
		rval[11] = "";
		rval[12] = "";
		rval[13] = "";
		if(ff == 1){
				if (document.frmPrincipal.rbPagosC.checked) {
				rval[0] = document.frmPrincipal.PEDIDO.value;
				rval[1] = "";//document.frmPrincipal.txtacuerdo.value;
				rval[2] = document.frmPrincipal.NRO_DEP_GARANTIA.value;
				rval[3] = document.frmPrincipal.rbPagosC.value;
				rval[4] = document.frmPrincipal.txttipo.value;
				rval[5] = document.frmPrincipal.TIPO_ACTIV_CLTE.value;
				rval[6] = document.frmPrincipal.ACTIVACION_LINEA.value;
				rval[7] = document.frmPrincipal.NRO_CONTRATO.value;
				rval[8] = document.frmPrincipal.NRO_OPE_INFOCORP.value;
				rval[9] = document.frmPrincipal.CODIGO_APROBACIO.value;
				rval[10] = document.frmPrincipal.NRO_REF_DEP_GAR.value;
				rval[11] = "";//document.frmPrincipal.txtflag.value;
				rval[12] = "";//document.frmPrincipal.txtmsgsap.value;
				rval[13] = document.frmPrincipal.txtnumsunat.value
				}
		}
		else{
			for(i=0; i<document.frmPrincipal.rbPagosC.length; i++){
			   if (document.frmPrincipal.rbPagosC[i].checked){
			        rval[0] = document.frmPrincipal.PEDIDO[i].value;
					rval[1] = "";//document.frmPrincipal.txtacuerdo[i].value;
					rval[2] = document.frmPrincipal.NRO_DEP_GARANTIA[i].value;
					rval[3] = document.frmPrincipal.rbPagosC[i].value;
					rval[4] = document.frmPrincipal.txttipo[i].value;
					rval[5] = document.frmPrincipal.TIPO_ACTIV_CLTE[i].value;
					rval[6] = document.frmPrincipal.ACTIVACION_LINEA[i].value;
					rval[7] = document.frmPrincipal.NRO_CONTRATO[i].value;
					rval[8] = document.frmPrincipal.NRO_OPE_INFOCORP[i].value;
					rval[9] = document.frmPrincipal.CODIGO_APROBACIO[i].value;
					rval[10] = document.frmPrincipal.NRO_REF_DEP_GAR[i].value;
					rval[11] = "";//document.frmPrincipal.txtflag[i].value;
					rval[12] = "";//document.frmPrincipal.txtmsgsap[i].value;
					rval[13] = document.frmPrincipal.txtnumsunat[i].value
					
			       break;
			   }
			}
		}
		return rval;
	}
	
	function f_Anterior() {
    var pagina;
    pagina=parseInt(document.frmPrincipal.BuscaPagina.value);
    pagina=pagina-1;
    if (pagina<0){
       pagina=1 ;
    }
    document.frmPrincipal.accionPago.value = "2"
	document.frmPrincipal.action='Operaciones.asp';
	document.frmPrincipal.codOperacion.value   = "01";
	document.frmPrincipal.BuscaPagina.value=pagina;
	document.frmPrincipal.submit();
	}

	function f_Siguiente() {
    var pagina;
    
    pagina=parseInt(document.frmPrincipal.BuscaPagina.value);
    pagina=pagina+1;
    document.frmPrincipal.accionPago.value = "2"
	document.frmPrincipal.action='Operaciones.asp';
	document.frmPrincipal.codOperacion.value  = "01";
	document.frmPrincipal.BuscaPagina.value=pagina;
	document.frmPrincipal.submit();
}
	
function f_Reimpresion()
{

		for (var i = 1; i < dgPool.rows.length; i++){
				if (dgPool.rows[i].cells[0].children.rbPagos.checked){
					strEstado =dgPool.rows[i].cells[15].innerText;	//PROY-30166 - IDEA - 38934 - Se aumento el indice por la columna adicional
					break;
				}
		}
		strEstado =trim(strEstado.toLowerCase());
		if (strEstado == "error de proceso"){
			alert('Comuniquese con soporte.\n\nDocumento no puede ser Re Imprimir porque tuvo un error al ser procesado en SAP.');
			return false;
		}

		var objIframe = document.getElementById("IfrmImpresion");
		objIframe.style.visibility = "hidden";
		objIframe.style.visibility = "visible";
		objIframe.style.width = 0;
		objIframe.style.height = 0;
		objIframe.contentWindow.location.replace('ReimpAnulado.aspx');
}
	
//PROY-27440 INI	
	function f_FormasPago()
		{
			var strCod = "";
			var strEstado="";
			var strCodProcesar = "";
			for (var i = 1; i < dgPool.rows.length; i++){
				if (dgPool.rows[i].cells[0].children.rbPagos.checked){					
					strCod = dgPool.rows[i].cells[0].children.rbPagos.value;
					strEstado =dgPool.rows[i].cells[15].innerText;	//PROY-30166 - IDEA - 38934 - Se aumento el indice por la columna adicional
					//strCodProcesar=dgPool.rows[i].cells[16].children.IdCodigo.value;
					break;
				}
			}
				
			if (strCod == "") 
			{	
				//event.returnValue = false;
				document.frmPrincipal.action="";
				alert("Debe Seleccionar un Documento para ver la Forma de Pago");					
				return false;
			}
			else
			{
				frmPrincipal.txtRbPagos.value = strCod;		
				frmPrincipal.cmdFormaPago.click();							
				return true;
			}	
			
	}
	//PROY-27440 FIN
	
//-->
			</script>
			<meta http-equiv="pragma" content="no-cache">
	</HEAD>
	<body leftMargin="0" topMargin="0" marginheight="0" marginwidth="0">
		<iframe id="IfrmImpresion" style="VISIBILITY: hidden; WIDTH: 0px; HEIGHT: 0px" name="IfrmImpresion"
			src="#"></iframe>
		<form id="frmPrincipal" name="frmPrincipal" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="975" border="0">
				<tr>
					<td vAlign="top" width="10">&nbsp;</td>
					<td vAlign="top" align="center" width="800">
						<table cellSpacing="2" cellPadding="0" width="90%" align="center" border="0">
							<tr>
								<td class="Arial12B" height="10"></td>
							</tr>
						</table>
						<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="100%" align="center"
							border="1" name="Contenedor">
							<tr>
								<td align="center">
									<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
										<tr>
											<td vAlign="top" width="11" height="32"></td>
											<td class="TituloRConsulta" vAlign="top" align="center" width="807" height="32">Consulta 
												de Documentos Pagados</td>
											<td vAlign="top" width="9" height="32"></td>
										</tr>
									</table>
									<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
										<tr>
											<td vAlign="top" width="10"></td>
											<td width="98%">
												<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
													<tr>
														<td>
															<div class="frame2" style="BORDER-RIGHT: 1px; BORDER-TOP: 1px; OVERFLOW-Y: scroll; OVERFLOW-X: scroll; BORDER-LEFT: 1px; WIDTH: 775px; BORDER-BOTTOM: 1px; HEIGHT: 330px; TEXT-ALIGN: center"><asp:datagrid id="dgPool" runat="server" AutoGenerateColumns="False" Height="30px" Width="1200px"
																	CssClass="Arial11B" CellSpacing="1" BorderColor="White">
																	<AlternatingItemStyle HorizontalAlign="Center" Height="30px" BackColor="#DDDEE2"></AlternatingItemStyle>
																	<ItemStyle HorizontalAlign="Center" Height="30px" BackColor="#E9EBEE"></ItemStyle>
																	<HeaderStyle HorizontalAlign="Center" BorderWidth="1px" BorderColor="#999999" CssClass="Arial12B"></HeaderStyle>
																	<Columns>
																		<asp:TemplateColumn HeaderText="Opci&#243;n" ItemStyle-HorizontalAlign="Center">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="5%"></HeaderStyle>
																			<ItemTemplate>
																				<input id=rbPagos type=radio value='<%# DataBinder.Eval(Container,"DataItem.PEDIN_NROPEDIDO")  %>' TipoDoc='<%# DataBinder.Eval(Container,"DataItem.PEDIV_DESCCLASEFACTURA") %>' name=rbPagos  >
																			</ItemTemplate>
																		</asp:TemplateColumn>
																		<asp:BoundColumn DataField="PEDIV_NOMBRECLIENTE" HeaderText="Nombre del Cliente">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="15%"></HeaderStyle>
																			<ItemStyle Height="29px"></ItemStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="INPAN_TOTALDOCUMENTO" HeaderText="Importe" DataFormatString="{0:N2}">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="5%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="PEDIN_SALDO" HeaderText="Saldo" DataFormatString="{0:N2}">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="5%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="PAGON_INICIAL" HeaderText="Cuota Inicial" DataFormatString="{0:N2}">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="5%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="PEDIV_DESCCLASEFACTURA" HeaderText="Tipo">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="10%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="PAGOC_CODSUNAT" HeaderText="Fact. SUNAT">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="10%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="PEDID_FECHADOCUMENTO" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="8%"></HeaderStyle>
																		</asp:BoundColumn>
																		<%--PROY-140397-MCKENSY JBD-INI --%>
																		<asp:BoundColumn DataField="PAGOD_FECHACONTA" HeaderText="Fecha Pago" DataFormatString="{0:dd/MM/yyyy}">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="8%"></HeaderStyle>
																		</asp:BoundColumn>
																		<%--PROY-140397-MCKENSY JBD-FIN --%>
																		<asp:BoundColumn DataField="PEDIN_PEDIDOSAP" HeaderText="Doc. SAP">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="8%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="DEPEV_DESCRIPCIONLP" HeaderText="Utilizaci&#243;n">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="8%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="DEPEN_NROCUOTA" HeaderText="Cuota">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="5%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="PEDIC_MONEDA" HeaderText="Moneda">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="5%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="INPAN_TOTALMERCADERIA" HeaderText="Neto" DataFormatString="{0:N2}">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="5%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="INPAN_TOTALIMPUESTO" HeaderText="Impuesto" DataFormatString="{0:N2}">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="5%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="INPAN_TOTALDOCUMENTO" HeaderText="Pago" DataFormatString="{0:N2}">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="5%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:TemplateColumn HeaderStyle-BackColor="#ffffff" ItemStyle-BackColor="#ffffff" ItemStyle-ForeColor="#ffffff">
																			<HeaderStyle Width="0%" BorderWidth="0" BorderStyle="None"></HeaderStyle>
																			<ItemTemplate>
																				<INPUT id=PEDIN_PEDIDOALTA style="WIDTH: 1px; HEIGHT: 22px" type=hidden size=1 value='<%# DataBinder.Eval(Container,"DataItem.PEDIN_PEDIDOALTA") %>'>
																				<INPUT id=PEDIC_CLASEFACTURA style="WIDTH: 1px; HEIGHT: 22px" type=hidden size=1 value='<%# DataBinder.Eval(Container,"DataItem.PEDIC_CLASEFACTURA") %>'>
																			</ItemTemplate>
																		</asp:TemplateColumn>
																		<asp:TemplateColumn HeaderText="Estado SAP">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="5%" />
																			<ItemTemplate>
																				<asp:Label CssClass='<%# IIf(DataBinder.Eval(Container,"DataItem.ESTADO_SAP").ToLower()="procesado", "estadoItemOK","estadoItemError")%>' Text='<%#DataBinder.Eval(Container,"DataItem.ESTADO_SAP")%>' runat="server" ID="Label1">
																				</asp:Label>
																				</asp:Label>
																			</ItemTemplate>
																			<ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
																				Font-Underline="False" HorizontalAlign="Center" />
																		</asp:TemplateColumn>
																		<asp:BoundColumn DataField="DEPEV_NROTELEFONO" HeaderText="Núm Telefónico">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="9%"></HeaderStyle>
																			<ItemStyle Height="29px"></ItemStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="PEDIN_NROPEDIDO" HeaderText="NROPEDIDO">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="5%"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="PEDIC_TIPOOFICINA" HeaderText="Tipo Oficina">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="5%"></HeaderStyle>
																		</asp:BoundColumn>
																		
																		<%--PROY-140397-MCKINSEY JSQ INICIO--%>
																		<asp:BoundColumn DataField="PEDMC_TIPO_ENTREGA" HeaderText="Tipo Entrega">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="10%"></HeaderStyle>
																		</asp:BoundColumn>
																		<%--PROY-140397-MCKINSEY JSQ FIN--%>
																		
																		<%--<asp:TemplateColumn>
																			<HeaderStyle Width="0%" BorderWidth="0" BorderStyle="None"></HeaderStyle>
																			<ItemStyle ForeColor="White" BackColor="White"></ItemStyle>
																			<ItemTemplate>
																			<asp:BoundColumn DataField="PEDIV_DRAASOCIADO" HeaderText="DRA Asociado">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="5%"></HeaderStyle>
																		</asp:BoundColumn>
																				<INPUT id="IdCodigo" name="IdCodigo" style="WIDTH: 1px; HEIGHT: 22px" type="hidden" size="1" value='<%# DataBinder.Eval(Container,"DataItem.ID_T_TRS_PEDIDO")   %>
																		<%--'>
																			</ItemTemplate>
																		</asp:TemplateColumn>--%>
																		
																		<%--PROY-140397-MCKINSEY JSQ INICIO--%>
																		<asp:TemplateColumn Visible="False">
																			<HeaderStyle BorderWidth="0px" BorderStyle="None" Width="0%"></HeaderStyle>
																			<ItemTemplate>
																				<INPUT id=FLAG_MULTIPUNTO style="WIDTH: 0; HEIGHT: 0" type=hidden size=0 runat="server" value='<%# DataBinder.Eval(Container,"DataItem.FLAG_MULTIPUNTO")%>' NAME="FLAG_MULTIPUNTO">
																			</ItemTemplate>
																		</asp:TemplateColumn>
																		<%--PROY-140397-MCKINSEY JSQ FIN--%>
																		
																		
																	</Columns>
																</asp:datagrid></div>
														</td>
													</tr>
												</table>
											</td>
											<td vAlign="top" align="right" width="14"></td>
										</tr>
									</table>
									<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
										<tr>
											<td height="4"></td>
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
						<table borderColor="#336699" cellSpacing="0" cellPadding="0" width="100%" align="center"
							border="1">
							<tr>
								<td align="center" height="30">
									<table cellSpacing="1" cellPadding="0" align="center" border="0">
										<tr>
											<td align="center" width="52"></td>
											<td align="center" width="4"></td>
											<%--<td width="50" align="center"><INPUT style="WIDTH: 98px" id="btnDescompensar" class="BotonOptm" value="Descompensar"
													type="button" name="btnDescompensar"></td>
											<td width="4" align="center"></td>
											<td width="52" align="center"><INPUT style="WIDTH: 85px" id="btnAnularPago" class="BotonOptm" title="Solo anula pagos, no el documento"
													value="Anular Pago" type="button" name="btnAnularPago"></td>
											<td width="4" align="center"></td>
											<td width="52" align="center"><asp:button id="btnReasignar" runat="server" Width="98px" CssClass="BotonOptm" Text="Reasignación"></asp:button></td>
											<td width="4" align="center"></td>--%>
											<td id="tdReimAnul" align="center" width="52" runat="server"><INPUT class="BotonOptm" id="btnImprimir" title="  Reimpresión  " style="WIDTH: 80px" onclick="f_Imprimir()"
													type="button" value="Imprimir" name="btnImprimir"></td>
											<td id="tdEspReAnul" align="center" width="4" runat="server"></td>
											<td align="center" width="52"><INPUT class="BotonOptm" id="btnAnularPago" title="Solo anula pagos, no el documento" style="WIDTH: 85px"
													type="button" value="Anular Pago" name="btnAnularPago"></td>
											<td align="center" width="4"></td>
											<%--<td width="52" align="center"><INPUT style="WIDTH: 76px" id="btnAnular" class="BotonOptm" title="Anula el pago y el documento"
													value="Anular" type="button" name="btnAnular">
											</td>--%>
											<td align="center" width="4"></td>
											<td vAlign="middle" align="center" width="70"><asp:textbox id="txtFecha" runat="server" Width="68px" CssClass="clsInputEnable"></asp:textbox></td>
											<td align="left" width="30"><A onmouseover="window.status='Date Picker';return true;" onmouseout="window.status='';return true;"
													href="javascript:show_calendar('frmPrincipal.txtFecha');"><IMG src="../../images/botones/btn_Calendario.gif" border="0"></A>
											</td>
											<td align="center" width="52"><INPUT class="BotonOptm" id="btnBuscar" style="WIDTH: 80px" onclick="f_Buscar()" type="button"
													value="Buscar" name="btnBuscar"></td>
											<%--PROY-27440 INI --%>
											<td align="center" width="52"><INPUT class="BotonOptm" id="btnFormasPago" title="Solo se visualiza las Formas de Pago"
											style="WIDTH: 85px" type="button" value="Forma Pago" name="btnFormasPago"></td>
											<%--PROY-27440 FIN --%>
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
					</td>
				</tr>
			</table>
			<script language="JavaScript" type="text/javascript">
var esNavegador, esIExplorer;

esNavegador = (navigator.appName == "Netscape") ? true : false;
esIExplorer = ((navigator.appName.indexOf("Microsoft") != -1) || (navigator.appName.indexOf("MSIE") != -1)) ? true : false;

function f_Boton()
  { var rval = "";
    for(i=0; i<document.frmPrincipal.elements.length; i++){
       if (document.frmPrincipal.elements[i].name.substring(0,7) == "rbPagos")
       {
         if (document.frmPrincipal.elements[i].checked)
         {
           rval = document.frmPrincipal.elements[i].value;
           break;
         }  
       }
     }
    if (rval == "")
    {
      alert('Debe Seleccionar un Documento de Pago');
      return false;
    }
    else
      return true;
  }

			</script>
			<!---->
			<p style="DISPLAY: none"><INPUT class="BotonOptm" id="Procesar" style="WIDTH: 1px" type="button" value="Procesar"
					name="btnProcesar" runat="server"> <INPUT id="txtRbPagos" type="hidden" name="txtRbPagos" runat="server">
				<asp:button id="cmdDescompensar" runat="server" Text="Descompensar"></asp:button><asp:button id="cmdProcesar" runat="server" Text="ProcesarPagos"></asp:button><asp:button id="cmdAnularPago" runat="server" Text="Anular Pagos"></asp:button><asp:button id="cmdAnular" runat="server" Text="Anular"></asp:button><INPUT id="hidParamImpresion" type="hidden" name="hidParamImpresion" runat="server">
				<INPUT class="BotonOptm" id="btnReimAnul" style="WIDTH: 98px" onclick="f_Reimpresion()"
					type="button" value="Reimp. Anulado" name="btnReimAnul" runat="server">
					<!--PROY-27440 INI-->
                      <asp:button id="cmdFormaPago" runat="server" Text="FormaPago"></asp:button>
                      <!--PROY-27440 FIN-->
			</p>
		</form>
	</body>
</HTML>
