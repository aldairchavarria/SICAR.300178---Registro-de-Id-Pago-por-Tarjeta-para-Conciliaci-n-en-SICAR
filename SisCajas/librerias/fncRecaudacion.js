/*****************************************************
/* Responsable		: Narciso Lema Ch.
/* Fecha Creación	: 21/10/2005
/* Función			: LaunchMessages
/* Descripción		: Lanza los mensajes que tenga la página.
/*****************************************************/
function LaunchMessages(){
	var mensaje = document.forms[0].elements.namedItem("hdnMensaje").value;
	if (mensaje.length > 0){
		alert(mensaje);
		document.forms[0].elements.namedItem("hdnMensaje").value = "";
	}
}

function e_numero() {
	if ( !( (event.keyCode>=48) && (event.keyCode<=57)) && !(event.keyCode==13) )
		event.keyCode=0;
}

function e_numeroyentrar() {
	e_enviar();
	if ( !( (event.keyCode>=48) && (event.keyCode<=57)) && !(event.keyCode==13) ){
		event.keyCode=0;
	}
}

function e_numero_guion() {
	if ( !( (event.keyCode == 45) ||((event.keyCode>=48) && (event.keyCode<=57))) && !(event.keyCode==13) )
		event.keyCode=0;
}

function e_numero_punto() {
	if ( !( (event.keyCode == 46) ||((event.keyCode>=48) && (event.keyCode<=57))) && !(event.keyCode==13) )
		event.keyCode=0;
}

function Change_TipoIdentificacion(obj)
{
	var valor = obj.options[obj.selectedIndex].value;
	valor = parseInt(valor,10)
	
	switch(valor)
	{
		case 1:
			var txt = new String(document.getElementById("txtIdentificador").value);
			txt = txt.substring(0,10);
			document.getElementById("txtIdentificador").onkeypress = e_numeroyentrar;
			document.getElementById("txtIdentificador").maxLength = 10;			
			document.getElementById("txtIdentificador").value = txt;
			break;
		case 2:
			var txt = new String(document.getElementById("txtIdentificador").value);
			txt = txt.substring(0,12);
			//document.getElementById("txtIdentificador").onkeypress = null;
			document.getElementById("txtIdentificador").onkeypress = e_enviar;
			document.getElementById("txtIdentificador").maxLength = 12;
			document.getElementById("txtIdentificador").value = txt;
			break;
	}
}

function ValEmpty(valor,campo)
{
	if ((valor == null) || (valor.length == 0))
	{
		alert("Debe ingresar " + campo);
		return false;
	}
	return true;
}

function ValAlfaNum(valor,campo,esvacio)
{
	var i, j;
	
	if (!esvacio)
	{
		if (!ValEmpty(valor, campo)) return false; 
	}
		
	for (i = 0; i<valor.length; i++)
	{
		a = valor.substr(i,1);
		j = a.charCodeAt(0);
		if (a != " ")
		{
			if (!(((j>=48)&&(j<=57))||((j>96)&&(j<=123))||((j>64)&&(j<=91))||(j==38)||(j==45)||(j==209)||(j==225)||(j==233)||(j==237)||(j==241)||(j==243)||(j==250)))
			{
				alert("El campo " + campo + " contiene carácteres no válidos.");
				return false;
			}
		}
	}	
	return true;
}

function ValNum(valor,campo)
{
	var i, j;
		
	for (i = 0; i<valor.length; i++)
	{
		a = valor.substr(i,1);
		j = a.charCodeAt(0);
		if (a != " ")
		{
			if (!((j>=48)&&(j<=57)))
			{
				//alert("El campo " + campo + " contiene carácteres no válidos.");
				return false;
			}
		}
	}	
	return true;
}

function ValNumPunto(valor,campo)
{
	var i, j;
		
	for (i = 0; i<valor.length; i++)
	{
		a = valor.substr(i,1);
		j = a.charCodeAt(0);
		if (a != " ")
		{
			if (!( (j==46) || ((j>=48)&&(j<=57))))
			{
				alert("El campo " + campo + " contiene carácteres no válidos.");
				return false;
			}
		}
	}	
	return true;
}

function ValReferencia(valor,campo)
{
	var i, j;
		
	for (i = 0; i<valor.length; i++)
	{
		a = valor.substr(i,1);
		j = a.charCodeAt(0);
		if (a != " ")
		{
			if (!( (j==45) || ((j>=48)&&(j<=57))))
			{
				alert("El campo " + campo + " contiene carácteres no válidos.");
				return false;
			}
		}
	}	
	return true;
}

function ValRecibo(valor, campo)
{
	var cabecera, numero;
	var strValor = new String(valor);
	
	var mensajeError = "Ingresar el número de recibo en el formato correcto."; //"El campo " + campo + " no tiene formato de Recibo.";
	
	try{
		if (strValor.indexOf("-") < 0)
		{
			alert(mensajeError);
			return false;
		}else{
			cabecera = new String(strValor.substring(0,strValor.indexOf("-")));
			numero = new String(strValor.substring(strValor.indexOf("-")+1));
			
			if ((cabecera.length != 4)||(numero.length != 7)){
				alert(mensajeError);
				return false;
			}
			
			if (!ValNum(numero, campo)) return false;
			return true;		
		}
	}catch(ex){
		alert(mensajeError);
		return false;
	}
}

function ValNumDecimales(valor, decimales){
	var ent, dec, strValor;
	strValor = new String(valor);
	
	if ((strValor.indexOf(".") >= 0) && (strValor.indexOf(".") + 1 < strValor.length)){
		ent = strValor.substring(0,strValor.indexOf("."));		
		dec = strValor.substring(strValor.indexOf(".")+1);
		
		if (ent.length == 0) {
			alert("Ingrese un valor entero en el Monto a Pagar.");
			return false;
		}
		if (dec.length > decimales){
			alert("Ingrese solo 2 número decimales en el Monto a Pagar.");
			return false;
		}
	}
	return true;
}