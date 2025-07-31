/**E75810*/

/*-- Funciones para setear en y obtener datos desde controles --*/
function setValue(id,v){
	var c = document.getElementById(id);
	if (c != null ) c.value = v;
}

function setValueHTML(id,v){
	var c = document.getElementById(id);	
	if (c != null ) 
	try{
		c.innerHTML = v;
	}catch(ex){}
}

function setValueInnerText(id,v){
	var c = document.getElementById(id);	
	if (c != null ) c.innerText = v;	
}

function setValueCheck(id,v){
	var c = document.getElementById(id);
	if (c != null && c.type.toUpperCase()=='CHECKBOX') c.checked = v;
}

function setValueCombo(id,v)
{
	var c = document.getElementById(id);
	if (c != null ) {
		var ind = getIndexByValue(id,v);
		c.selectedIndex = ind;	
	}
}

function getValue(id){
	var c = document.getElementById(id);
	if (c != null ) return Trim(c.value);
	return '';
}

function getValueHTML(id){
	var c = document.getElementById(id);
	if (c != null ) return Trim(c.innerHTML);
	return '';
}

function getText(id){
	var c = document.getElementById(id);
	if (c != null )return c.options[c.selectedIndex].text;
	return '';
}

function getValueCheck(id)
{
	var c = document.getElementById(id);
	if (c != null && (c.type == 'CHECKBOX' ||  c.type =='checkbox' || c.type=='radio') ) 
		return c.checked;
	else 
		return false;
	
}


function setFocus(id){
	var c = document.getElementById(id);
	if (c != null )
		if (c.disabled == false && isVisible(id)==true )
			c.focus();
}
function setFocusSelect(id){
	var c = document.getElementById(id);
	if (c != null )
		if (c.disabled == false && isVisible(id)==true ){
			c.select();
			c.focus();
		}
}
function isVisible(id){
	var c = document.getElementById(id);
	if (c == null ) return false;		
	if (c.style.display == '')
		return true; 
	else
		return false;
}


function setEnabled(id,habilitar,classname){
	var c = document.getElementById(id);
	if (c==null) return;
	if (c.type == 'text')
		if (habilitar == true) 
			c.readOnly = false;
		else
			c.readOnly = true;
	else
		if (habilitar == true)
			c.disabled =  false;
		else
			c.disabled = true;	
	if (classname != '') c.className = classname;
}


/*------------*/
function Trim(String)
{
	if (String == null)
		return (false);

	return RTrim(LTrim(String));
}

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

function onkeypressSoloNumeros(e)
{
	var key = (e ? e.keyCode || e.which : window.event.keyCode);
	return (key <= 12 || (key >= 48 && key <= 57));
}

function setVisible(id,visible){
	var c = document.getElementById(id);
	if (c == null ) return;	
	if (visible == true)	
		c.style.display = '';
	else
		c.style.display = 'none';
	
}


/**********************/

function ValidarCampo(id, nm)
{	
	if(getValue(id)==''){
		alert(nm + ' es un dato requerido');
		setFocus(id);
		return false;
	}
	else
		return true;
}

function ValidarCampoHidden(id, nm)
{	
	if(getValue(id)==''){
		alert(nm + ' es un dato requerido');
		return false;
	}
	else
		return true;
}



//E75810
function ValidaNumeroTelmex(obj)
{
	var valor_retorno=true;
	var KeyAscii = window.event.keyCode;
	if (KeyAscii<=13) return;	
	
	if ( !(KeyAscii==46 | (KeyAscii >= 48 && KeyAscii<=57) ) | (KeyAscii==46 && obj.value.indexOf(".")>=0) ){		
		window.event.keyCode = 0;
		valor_retorno=false;
	}	
	else
	{	
		if (obj.value.indexOf(".")>=0 ){		
			if (KeyAscii!=46  && obj.value.substring(obj.value.indexOf(".")+1,obj.value.length).length>1)
				window.event.keyCode = 0;	
		}
		valor_retorno=false;
	}
	return valor_retorno;
}



/**FIN**/