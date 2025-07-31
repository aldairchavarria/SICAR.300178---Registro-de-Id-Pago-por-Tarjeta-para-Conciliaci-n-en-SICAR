/****************************************************************************************/
//PARA REDONDEAR LOS NUEMROS DE LOS CAMPOS DE TEXTO DE LA PAGINA
//Regularizacion 20.11.2012
/****************************************************************************************/
function RedondearEfectivo(_id,_idCombo){
	var Textcombo = document.getElementById(_idCombo);
							
	if ( Textcombo.value.indexOf("ZEFE") != (-1) ){;
		RedondearNumero(_id);							
	}	
}//fin... function
//---------------------------------------------------------------------------------------
function RedondearEfectivoFijos(_id,_idCombo){	
    if( trim(document.getElementById('lblTipoMonedaDeuda').innerText) != "$" ){
		var Textcombo = document.getElementById(_idCombo);
		
		if ( Textcombo.value.indexOf("ZEFE") != (-1) ){;
			RedondearNumero(_id);							
		}		
	}	
}//fin... function
//---------------------------------------------------------------------------------------
function RedondearNumero(_id){
	var campo = document.getElementById(_id);
	var caracterBuscado = ".";
	var index = campo.value.indexOf(caracterBuscado);
	var valor;
	var diferencia=0;
										
	if ( index > 0 ){// si existe el caracter "." dentro del numero
		var parteEntera = campo.value.substring(0,index);
		var parteDecimal = campo.value.substring(index+1);
		var primerDigitoDecimal = parteDecimal.substring(0,1);
												
		if( parteDecimal.length == 2 ){//con 2 decimales
			var segundoDigitoDecimal = parteDecimal.substring(1,2);
			var segundoDigitoDecimalNuevo;
													
			if( (parseInt(segundoDigitoDecimal) >= 1) && (parseInt(segundoDigitoDecimal) <= 9) ){// 0 - 9
				segundoDigitoDecimalNuevo = 0;
				diferencia = (parseInt(segundoDigitoDecimal) - parseInt(segundoDigitoDecimalNuevo))/100;
				valor = (parseInt(parteEntera)) + (parseInt(primerDigitoDecimal)/10);
				document.getElementById(_id).value = valor.toFixed(2);													 
			}
												
		
		}//fin... con 2 decimales
												
		return true;
									
	}//fin... si existe el caracter "." dentro del numero
										
}//fin... function
//---------------------------------------------------------------------------------------
function RedondearInicio(){
	RedondearNumero("txtMonto1");
	RedondearNumero("txtMonto2");
	RedondearNumero("txtMonto3");
	RedondearNumero("txtRecibidoPen");
	RedondearNumero("txtRecibidoUsd");	
}
//INICIATIVA 529 INI
function RedondearInicio3(){
	if(document.getElementById('cboTipDocumento1').value== "ZEFE"){
		RedondearNumero("txtMonto1");
		RedondearNumero("txtRecibidoPen");
		RedondearNumero("txtRecibidoUsd");	
	}
	RedondearNumero("txtMonto2");
	RedondearNumero("txtMonto3");
}
//INICIATIVA 529 FIN
//---------------------------------------------------------------------------------------
function RedondearFijos(){

   // trim(document.getElementById('lblTipoMonedaDeuda').innerText)
	if( trim(document.getElementById('lblTipoMonedaDeuda').innerHTML) != "$" ){
	
		RedondearNumero("txtMonto1");	
		RedondearNumero("txtRecibidoPen");
		RedondearNumero("txtRecibidoUsd");
		
	}
}
//---------------------------------------------------------------------------------------
function RedondearInicio2(){
	RedondearNumero("txtMonto1");
	RedondearNumero("txtMonto2");
	RedondearNumero("txtMonto3");
	RedondearNumero("txtRecibidoSoles");
	RedondearNumero("txtRecibidoUsd");	
}
/****************************************************************************************/

/****************************************************************************************/
//PARA RESTRINGIR AL USUARIO QUE NO INGRESE MONTOS MAYOR AL MONTO TOTAL VISUALIZADO
/****************************************************************************************/

function f_CalculoDeuda_Tabla(gv, _columnaMontoTotal) {
    var nmTbl = gv;
    var tbl = document.getElementById(nmTbl);
    var totalDeuda = 0;
	var idMontoPago="";		
	
    if (tbl != null) {
        var valDeuda;
		
		for (i=2; i<=tbl.rows.length; i++){		
            //valDeuda = tbl.rows[i].cells[parseInt(_columnaMontoTotal)].innerHTML;
			//alert("==> fila " + i + " valor => " + valDeuda);			
			idMontoPago = nmTbl + '__ctl' + i + '_' + 'txtDOC_MontoPagar';
			valDeuda = getValue(idMontoPago);			
            totalDeuda = parseFloat(totalDeuda) + parseFloat(valDeuda);
        }
    }	
    return totalDeuda;	
}
//---------------------------------------------------------------------------------------
function f_CalculoDeuda_TablaMiles(gv, _columnaMontoTotal, c) {
    var nmTbl = gv;
    var tbl = document.getElementById(nmTbl);
    var totalDeuda = 0;
	var idMontoPago="";	
	var valDeudaNuevo="";
	var caracter="";	
	
    if (tbl != null) {
        var valDeuda;
		
		for (i=parseInt(c); i<tbl.rows.length; i++){
            valDeuda = tbl.rows[i].cells[parseInt(_columnaMontoTotal)].innerText;			
			valDeudaNuevo="";
			for( var c=0; c<(valDeuda.length); c++ ){
				caracter = valDeuda.charAt(c);				
				if(caracter != ","){
					valDeudaNuevo = valDeudaNuevo + caracter;
				}				
			}			
			valDeuda = parseFloat(valDeudaNuevo);
			totalDeuda = totalDeuda + valDeuda;
        }
    }
    return totalDeuda;	
}
//---------------------------------------------------------------------------------------
//INICIATIVA-318 INI
function f_CalculoDeuda_TablaMiles2(gv, _columnaMontoTotal, c) {
    var nmTbl = gv;
    var tbl = document.getElementById(nmTbl);
    var totalDeuda = 0;
	var idMontoPago="";	
	var valDeudaNuevo="";
	var caracter="";	
	
    if (tbl != null) {
        var valDeuda;
		
		for (i=parseInt(c); i<tbl.rows.length; i++){
			var checkBox = tbl.rows[i].cells[0].childNodes[0];
			if(checkBox.checked){
			    valDeuda = tbl.rows[i].cells[parseInt(_columnaMontoTotal)].innerText;			
				valDeudaNuevo="";
				for( var c=0; c<(valDeuda.length); c++ ){
					caracter = valDeuda.charAt(c);				
					if(caracter != ","){
						valDeudaNuevo = valDeudaNuevo + caracter;
					}				
				}			
				valDeuda = parseFloat(valDeudaNuevo);
				totalDeuda = totalDeuda + valDeuda;
			}
        }
    }
    return totalDeuda;	
}
//---------------------------------------------------------------------------------------
//INICIATIVA-318 FIN
function MontoPagarRestriccion(sumatoriaTotal, idMontoActual, idMonto1, idMonto2, idMonto3)
{
	var monto1 = 0;	
	var monto2 = 0;
	var monto3 = 0;
	
	if( idMonto1 != "" ){ if( document.getElementById(idMonto1).value != "" ){ monto1 = document.getElementById(idMonto1).value; } }	
	if( idMonto2 != "" ){ if( document.getElementById(idMonto2).value != "" ){ monto2 = document.getElementById(idMonto2).value; } }
	if( idMonto3 != "" ){ if( document.getElementById(idMonto3).value != "" ){ monto3 = document.getElementById(idMonto3).value; } }
	
	var totalPagarIngresado = parseFloat(monto1) + parseFloat(monto2) + parseFloat(monto3);
	
	if (totalPagarIngresado > sumatoriaTotal) {
		alert("El Monto a Pagar no puede ser mayor al Total " + sumatoriaTotal.toFixed(2));

		totalPagarIngresado = totalPagarIngresado - parseFloat(document.getElementById(idMontoActual).value);
		
		if( (sumatoriaTotal - totalPagarIngresado) < 0 ) {
			document.getElementById(idMontoActual).value = (0).toFixed(2);
		}else{
			document.getElementById(idMontoActual).value = (sumatoriaTotal - totalPagarIngresado).toFixed(2);
		}		
	}
}
//INICIATIVA-318 INI
function MontoPagarRestriccion2(sumatoriaTotal, idMontoActual, idMonto1, idMonto2, idMonto3)
{
	var monto1 = 0;	
	var monto2 = 0;
	var monto3 = 0;
	
	if( idMonto1 != "" ){ if( document.getElementById(idMonto1).value != "" ){ monto1 = document.getElementById(idMonto1).value; } }	
	if( idMonto2 != "" ){ if( document.getElementById(idMonto2).value != "" ){ monto2 = document.getElementById(idMonto2).value; } }
	if( idMonto3 != "" ){ if( document.getElementById(idMonto3).value != "" ){ monto3 = document.getElementById(idMonto3).value; } }
	
	var totalPagarIngresado = parseFloat(monto1) + parseFloat(monto2) + parseFloat(monto3);
	
	if (totalPagarIngresado > sumatoriaTotal) {
		if(sumatoriaTotal.toFixed(2) == 0){
			alert("Debe seleccionar por lo menos un item.")
		}else{
			alert("El Monto a Pagar no puede ser mayor al Total " + sumatoriaTotal.toFixed(2));
		}
		
		totalPagarIngresado = totalPagarIngresado - parseFloat(document.getElementById(idMontoActual).value);
		
		if( (sumatoriaTotal - totalPagarIngresado) < 0 ) {
			document.getElementById(idMontoActual).value = (0).toFixed(2);
		}else{
			document.getElementById(idMontoActual).value = (sumatoriaTotal - totalPagarIngresado).toFixed(2);
		}		
	}
}
//INICIATIVA-318 FIN
//---------------------------------------------------------------------------------------
function MontoEfectivoRestriccion(sumatoriaTotal, idMontoActual, idMonto1, idMonto2, idMonto3,_idCombo){
	var Textcombo = document.getElementById(_idCombo);
							
	if (Textcombo.value.indexOf("ZEFE") != (-1) ){;
		MontoPagarRestriccion(sumatoriaTotal, idMontoActual, idMonto1, idMonto2, idMonto3);							
	}									
}//fin... function  
//INICIATIVA-318 INI
function MontoEfectivoRestriccion2(sumatoriaTotal, idMontoActual, idMonto1, idMonto2, idMonto3,_idCombo){
	var Textcombo = document.getElementById(_idCombo);
							
	if (Textcombo.value.indexOf("ZEFE") != (-1) ){;
		MontoPagarRestriccion2(sumatoriaTotal, idMontoActual, idMonto1, idMonto2, idMonto3);							
	}else{
		var monto1 = 0;	
		var monto2 = 0;
		var monto3 = 0;
		
		if( idMonto1 != "" ){ if( document.getElementById(idMonto1).value != "" ){ monto1 = document.getElementById(idMonto1).value; } }	
		if( idMonto2 != "" ){ if( document.getElementById(idMonto2).value != "" ){ monto2 = document.getElementById(idMonto2).value; } }
		if( idMonto3 != "" ){ if( document.getElementById(idMonto3).value != "" ){ monto3 = document.getElementById(idMonto3).value; } }
		
		var totalPagarIngresado = parseFloat(monto1) + parseFloat(monto2) + parseFloat(monto3);
		
		if (totalPagarIngresado > sumatoriaTotal) {
			if(sumatoriaTotal.toFixed(2) == 0){
				alert("Debe seleccionar por lo menos un item.");
				document.getElementById(idMontoActual).value = (0).toFixed(2);
			}
		}
	}									
}//fin... function  
//INICIATIVA-318 FIN
//---------------------------------------------------------------------------------------
function MontoEfectivoRestriccionFijos(sumatoriaTotal, idMontoActual, idMonto1, idMonto2, idMonto3,_idCombo){
	if( trim(document.getElementById('lblTipoMonedaDeuda').innerText) != "$" ){
		//alert("soles");
		var Textcombo = document.getElementById(_idCombo);
							
		if (Textcombo.value.indexOf("ZEFE") != (-1) ){;
			MontoPagarRestriccion(sumatoriaTotal, idMontoActual, idMonto1, idMonto2, idMonto3);							
		}
	}										
}//fin... function
/****************************************************************************************/


//17122013
function MontoEfectivoRestriccionSuperaMonto(sumatoriaTotal, idMontoActual, idMonto1, idMonto2, idMonto3,_idCombo){
	var Textcombo = document.getElementById(_idCombo);
							
	if (Textcombo.value.indexOf("ZEFE") != (-1) ){;
		MontoPagarRestriccionSuperaMonto(sumatoriaTotal, idMontoActual, idMonto1, idMonto2, idMonto3);							
	}									
}

function MontoPagarRestriccionSuperaMonto(sumatoriaTotal, idMontoActual, idMonto1, idMonto2, idMonto3)
{
	var monto1 = 0;	
	var monto2 = 0;
	var monto3 = 0;
	
	if( idMonto1 != "" ){ if( document.getElementById(idMonto1).value != "" ){ monto1 = document.getElementById(idMonto1).value; } }	
	if( idMonto2 != "" ){ if( document.getElementById(idMonto2).value != "" ){ monto2 = document.getElementById(idMonto2).value; } }
	if( idMonto3 != "" ){ if( document.getElementById(idMonto3).value != "" ){ monto3 = document.getElementById(idMonto3).value; } }
	
	var totalPagarIngresado = parseFloat(monto1) + parseFloat(monto2) + parseFloat(monto3);
	
		
}

//INICIATIVA-318 INI
function EstablecerMonto_Pagar(monto, idMonto, _idCombo)
{
	var Textcombo = document.getElementById(_idCombo);
	var fpago = "ZBUY;ZCVB;ZNCR;ZCIB;ZEFE";
							
	if (fpago.indexOf(Textcombo.value) == (-1)){
		document.getElementById(idMonto).value = document.getElementById(monto).value;
	}
}

function EstablecerMonto_Pagar2(idMonto, _idCombo){
	var Textcombo = document.getElementById(_idCombo);
	var fpago = "ZBUY;ZCVB;ZNCR;ZCIB;ZEFE";
							
	if (fpago.indexOf(Textcombo.value) == (-1)){
		document.getElementById(idMonto).value = parseFloat(f_CalculoDeuda_TablaMiles2('DGReporte', '7', '1')).toFixed(2);
	}	
}
//INICIATIVA-318 FIN





