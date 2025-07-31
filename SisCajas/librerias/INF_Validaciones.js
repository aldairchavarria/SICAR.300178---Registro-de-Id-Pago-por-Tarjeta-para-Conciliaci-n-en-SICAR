/**************************************************************
 LTrim: Returns a String containing a copy of a specified 
        string without leading spaces 

 Parameters:
      String = The required string argument is any valid 
               string expression. If string contains null, 
               false is returned

 Returns: String
***************************************************************/
function LTrim(String)
{
	var i = 0;
	var j = String.length - 1;

	if (String == null)
		return (false);

	for (i = 0; i < String.length; i++)
	{
		if (String.substr(i, 1) != ' ' &&
		    String.substr(i, 1) != '\t')
			break;
	}

	if (i <= j)
		return (String.substr(i, (j+1)-i));
	else
		return ('');
}

/**************************************************************
 RTrim: Returns a String containing a copy of a specified 
        string without trailing spaces 

 Parameters:
      String = The required string argument is any valid 
               string expression. If string contains null, 
               false is returned

 Returns: String
***************************************************************/
function RTrim(String)
{
	var i = 0;
	var j = String.length - 1;

	if (String == null)
		return (false);

	for(j = String.length - 1; j >= 0; j--)
	{
		if (String.substr(j, 1) != ' ' &&
			String.substr(j, 1) != '\t')
		break;
	}

	if (i <= j)
		return (String.substr(i, (j+1)-i));
	else
		return ('');
}

/**************************************************************
 RTrim: Returns a String containing a copy of a specified 
        string without both leading and trailing spaces 

 Parameters:
      String = The required string argument is any valid 
               string expression. If string contains null, 
               false is returned

 Returns: String
***************************************************************/
function Trim(String)
{
	if (String == null)
		return (false);

	return RTrim(LTrim(String));
}


// Telefono - only numbers.
function checkTelefono(strng,campo) {
var error = "";
if (strng == "") {
   alert("Por favor complete su " + campo);
   return false;
}
    var illegalChars = /[^0-9]/; // allow numbers
    if (illegalChars.test(strng)) {
		alert("Su " + campo + " contiene caracteres no permitidos.");
		return false;
    }
	return true;
}


// NumeroDocumento - only numbers.
function checkNumeroDNI(strng,campo) {
var error = "";
if (strng == "") {
   alert("Por favor complete su " + campo);
   return false;
}
    var illegalChars = /[^0-9]/; // allow numbers
    if (illegalChars.test(strng)) {
		alert("Su " + campo + " contiene caracteres no permitidos.");
		return false;
    }
	return true;
}
// allow only letters and comas 
function checkNombres(strng,campo) {
var error = "";
if (strng == "") {
   alert("Por favor complete el " + campo + ".");
   return false;    
}
    var illegalChars = /[^A-Za-zñÑáéíóúÜüÁÉÍÓÚàèìòùÀÈÌÒÙ' ]/; // allow only letters and comas 
    if (illegalChars.test(strng)){
      alert("El " + campo + " contiene caracteres no permitidos.");
      return false;    
    } 
    else if (!((strng.search(/[a-zñ]+/)>-1) || (strng.search(/[A-ZÑ]+/)>-1))) {
	   alert("El " + campo + " deberá contener como mínimo una letra.");
	   return false;
    }
    else if (strng.length < 2) {
	   alert("El " + campo + " deberá tener una longitud de 2 como mínimo.");
	   return false;		
    }
	return true;    
}
// allow only letters and numbers 
function checkNumDoc(strng,campo) {
var error = "";
if (strng == "") {
   alert("Por favor complete el " + campo + ".");
   return false;    
}
    var illegalChars = /[^A-Za-z0-9ñÑ]/; // allow only letters and numbers 
    //var illegalChars = "/[^A-Za-z0-9" + String.fromCharCode(164) + String.fromCharCode(165) + "]/"; // allow only letters and numbers 
    if (illegalChars.test(strng)) {
      alert("El " + campo + " contiene caracteres no permitidos.");
      return false;    
    } 
	return true;    
}
// allow only numbers 
function checkNumembers(strng,campo) {
var error = "";
if (strng == "") {
   alert("Por favor complete el " + campo + ".");
   return false;    
}
    var illegalChars = /[^0-9]/; // allow only numbers 
    if (illegalChars.test(strng)) {
      alert("El " + campo + " contiene caracteres no permitidos.");
      return false;    
    } 
	return true;    
}                
// username - uc, lc, and underscore only.
function checkUsername(strng,campo) {
var error = "";
if (strng == "") {
   alert("Por favor complete su " + campo);
   return false;
}
    var illegalChars = /[^A-Za-z0-9_-]/; // allow letters, numbers, and underscores
    if (illegalChars.test(strng)) {
		alert("Su " + campo + " contiene caracteres no permitidos.");
		return false;
    }
    else if (!((strng.search(/[a-z]+/)>-1) || (strng.search(/[A-Z]+/)>-1) || (strng.search(/[0-9]+/))>-1)) {
	   alert(campo + " debera contener como minimo una letra o numero.");
	   return false;
    }    
return true;
}       

// password - uppercase, lowercase, and numeral
function checkPassword (strng,campo) {
var error = "";
if (strng == "") {
   alert("Por favor complete su " + campo + ".");
   return false;    
}
    var illegalChars = /[^A-Za-z0-9_-]/; // allow letters, and numbers
    if (illegalChars.test(strng)) {
      alert(campo + " contiene caracteres no permitidos.");
      return false;    
    }
    else if (!((strng.search(/[a-z]+/)>-1) || (strng.search(/[A-Z]+/)>-1) || (strng.search(/[0-9]+/))>-1)) {
	   alert(campo + " debera contener como minimo una letra o numero.");
	   return false;
    }    
return true;    
}    
function check_date(field){
var checkstr = "0123456789";
var DateField = field;
var Datevalue = "";
var DateTemp = "";
var seperator = "/";
var day;
var month;
var year;
var leap = 0;
var err = 0;
var i;
   err = 0;
   DateValue = DateField.value;
   /* Delete all chars except 0..9 */
   for (i = 0; i < DateValue.length; i++) {
	  if (checkstr.indexOf(DateValue.substr(i,1)) >= 0) {
	     DateTemp = DateTemp + DateValue.substr(i,1);
	  }
   }
   DateValue = DateTemp;
   /* Always change date to 8 digits - string*/
   /* if year is entered as 2-digit / always assume 20xx */
   if (DateValue.length == 6) {
      DateValue = DateValue.substr(0,4) + '20' + DateValue.substr(4,2); }
   if (DateValue.length != 8) {
      err = 19;
      return false;
   }
   /* year is wrong if year = 0000 */
   year = DateValue.substr(4,4);
   if (year == 0) {
      err = 20;
      return false;
   }
   /* Validation of month*/
   month = DateValue.substr(2,2);
   if ((month < 1) || (month > 12)) {
      err = 21;
      return false;
   }
   /* Validation of day*/
   day = DateValue.substr(0,2);
   if (day < 1) {
     err = 22;
     return false;
   }
   /* Validation leap-year / february / day */
   if ((year % 4 == 0) || (year % 100 == 0) || (year % 400 == 0)) {
      leap = 1;
   }
   if ((month == 2) && (leap == 1) && (day > 29)) {
      err = 23;
      return false;
   }
   if ((month == 2) && (leap != 1) && (day > 28)) {
      err = 24;
      return false;
   }
   /* Validation of other months */
   if ((day > 31) && ((month == "01") || (month == "03") || (month == "05") || (month == "07") || (month == "08") || (month == "10") || (month == "12"))) {
      err = 25;
      return false;
   }
   if ((day > 30) && ((month == "04") || (month == "06") || (month == "09") || (month == "11"))) {
      err = 26;
      return false;
   }
   /* if 00 ist entered, no error, deleting the entry */
   if ((day == 0) && (month == 0) && (year == 00)) {
      err = 0; day = ""; month = ""; year = ""; seperator = "";
   }
   /* if no error, write the completed date to Input-Field (e.g. 13.12.2001) */
   if (err == 0) {
      DateField.value = day + seperator + month + seperator + year;
      return true;
   }
   /* Error-message if err != 0 */
   else {
      //alert("Date is incorrect!");
      DateField.select();
	  DateField.focus();
	  return false;
   }
}


function validarnroINFOCORP(txtControl)
{
	if(Trim(txtControl.value) == "")
	{
		alert("Por favor complete su Número de Operación");
		txtControl.value = "" ;
		txtControl.focus();
		return false;
	}
	txtControl.value == Trim(txtControl.value);
	if (!checkNumeroDNI(txtControl.value,"Número de Operación"))
	{
		txtControl.focus();
		return false;		
	}
	if(txtControl.value.length < 15)
	{
		alert("El Número de Operación debe tener 15 digitos");
		txtControl.focus();
		return false;
	}	
	return true;
}
function validarnroDNI(txtControl)
{
	if(Trim(txtControl.value) == "")
	{
		alert("Por favor complete su Número de DNI ");
		txtControl.value = "" ;
		txtControl.focus();
		return false;
	}
	txtControl.value == Trim(txtControl.value);
	if (!checkNumeroDNI(txtControl.value,"Número DNI"))
	{
		txtControl.focus();
		return false;		
	}
	if(txtControl.value.length != 8)
	{
		alert("El Número de DNI debe tener 8 digitos");
		txtControl.focus();
		return false;
	}	
	return true;
}
function validarApellido(txtcontrol,nombreCampo){
	if(Trim(txtcontrol.value) == "")
	{
		alert("Por favor complete su " + nombreCampo);
		txtcontrol.focus();
		return false;
	}
	txtcontrol.value == Trim(txtcontrol.value);
	if (!checkNombres(txtcontrol.value,nombreCampo))
	{
		txtcontrol.focus();
		return false;		
	}
	return true;	
}
function validarTipoDocumento(){
	if (document.all.cboTipoDocumento[document.all.cboTipoDocumento.selectedIndex].value==0){
		alert("Por favor seleccione un Tipo de Documento");
		document.all.cboTipoDocumento.focus();
		return false;	
	}
	return true;
}
function validarNumeroDocumento()
{
	if(Trim(document.frmPrincipal.txtNumDoc.value) == "")
	{
		alert("Por favor complete su Número de Documento ");
		document.frmPrincipal.txtNumDoc.focus();
		return false;
	}
	document.frmPrincipal.txtNumDoc.value == Trim(document.frmPrincipal.txtNumDoc.value);
	if (!checkNumeroDNI(document.frmPrincipal.txtNumDoc.value,"Número de Documento"))
	{
		document.frmPrincipal.txtNumDoc.focus();
		return false;		
	}
	if(document.frmPrincipal.txtNumDoc.value.length < 5)
	{
		alert("El Número de Documento debe tener 5 digitos");
		document.frmPrincipal.txtNumDoc.focus();
		return false;
	}	
	return true;
}
function validarNumeroDocumentoExt()
{
	if(Trim(document.frmPrincipal.txtNumDoc.value) == "")
	{
		alert("Por favor complete su Número de Documento ");
		document.frmPrincipal.txtNumDoc.focus();
		return false;
	}
	document.frmPrincipal.txtNumDoc.value == Trim(document.frmPrincipal.txtNumDoc.value);
	if (!checkNumDoc(document.frmPrincipal.txtNumDoc.value,"Número de Documento"))
	{
		document.frmPrincipal.txtNumDoc.focus();
		return false;		
	}
	if(document.frmPrincipal.txtNumDoc.value.length < 5)
	{
		alert("El Número de Documento debe tener 5 digitos");
		document.frmPrincipal.txtNumDoc.focus();
		return false;
	}	
	return true;
}

function validarTipoCambio()
{
	if(document.frmMain.txtTipodeCambio.value == "")
	{
		alert("Por favor complete el Tipo de Cambio");
		document.frmMain.txtTipodeCambio.focus();
		return false;
	}	
	if(!validaTipoCambioInfocorp(document.frmMain.txtTipodeCambio.value))
	{
		alert("Ingrese correctamente el Tipo de Cambio .\nFormato: [###.####] ");
		document.frmMain.txtTipodeCambio.focus();
		return false;
	}	
	return true;
}
function validaFechas()
{
	if(!(check_date(document.frmMain.txtFechaNacimiento))){
		alert("Debe escribir una 'Fecha de Nacimiento' valida en el formato (dd/mm/AAAA)");
		document.frmMain.txtFechaNacimiento.focus();
		return false;		
	}
	if(!(DiferenciaFechas(document.frmMain.txtFechaNacimiento))){
		alert("Debe ser mayor a 18 años");
		document.frmMain.txtFechaNacimiento.focus();
		return false;		
	}	
	return true;
}

//DEBE SER MAYOR A 18 AÑOS
function DiferenciaFechas (txtControl) {
	var mydate=new Date()
	var dia = mydate.getDate() 
	var mes = parseInt(mydate.getMonth()) + 1 
	var ano = mydate.getFullYear() 
	var CadenaFechaActual = dia + "/" + mes + "/" + ano 
	//Obtiene los datos del formulario
	CadenaFecha1 = CadenaFechaActual
	CadenaFecha2 = txtControl.value
	//Obtiene dia, mes y año
	var fecha1 = new fecha( CadenaFecha1 )   
	var fecha2 = new fecha( CadenaFecha2 )
	//Obtiene objetos Date
	var miFecha1 = new Date( fecha1.anio, fecha1.mes, fecha1.dia )
	var miFecha2 = new Date( fecha2.anio, fecha2.mes, fecha2.dia )

	var auxEDAD = parseInt(fecha1.anio - fecha2.anio);
	if(auxEDAD < 18){
		//es menor de edad
	    return false;
	}
	else{
	   if(auxEDAD == 18 && parseInt(fecha2.mes) > parseInt(fecha1.mes))
	      return false;
	   else{
	     if(auxEDAD == 18 && parseInt(fecha2.mes) == parseInt(fecha1.mes) && parseInt(fecha2.dia) > parseInt(fecha1.dia)){
	        //es menor de edad
			return false;
	     }
	   }
	 }
	return true;
}

function fecha( cadena ) {
   //Separador para la introduccion de las fechas
   var separador = "/"
   //Separa por dia, mes y año
   if ( cadena.indexOf( separador ) != -1 ) {
        var posi1 = 0
        var posi2 = cadena.indexOf( separador, posi1 + 1 )
        var posi3 = cadena.indexOf( separador, posi2 + 1 )
        this.dia = cadena.substring( posi1, posi2 )
        this.mes = cadena.substring( posi2 + 1, posi3 )
        this.anio = cadena.substring( posi3 + 1, cadena.length )
   } else {
        this.dia = 0
        this.mes = 0
        this.anio = 0   
   }
}


function f_validaEdad(strng,campo) {
	var illegalChars = /[^0-9]/; // allow only numbers
	var sCad;

	eval( "sCad=" + strng+ ".value");
	sCad=Trim(sCad);

	if (sCad == "") {
		return true;
	}

	if (illegalChars.test(sCad)) {
	  alert("El campo " + campo + " contiene caracteres no permitidos.");
	  eval(strng+".focus()");
	  return false;
	}

	if ((sCad < 18) || (sCad > 99)) {
		alert("La edad debe ser entre 18 y 99 años");
		eval(strng+".focus()");
		return false;
	}
	return true;
}

function f_validaLineaCredito(strng,campo) {
	var illegalChars = /[^0-9]/; // allow only numbers
	var sCad;

	eval( "sCad=" + strng+ ".value");
	sCad=Trim(sCad);

	if (sCad == "") {
		return true;
	}

	if (illegalChars.test(sCad)) {
	  alert("El campo " + campo + " contiene caracteres no permitidos.");
	  eval(strng+".focus()");
	  return false;
	}
	return true;

}