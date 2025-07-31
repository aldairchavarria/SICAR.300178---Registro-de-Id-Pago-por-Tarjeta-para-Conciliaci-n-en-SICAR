<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ventaArticulos.aspx.vb" Inherits="SisCajas.ventaArticulos"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ventaArticulos</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK rel="styleSheet" type="text/css" href="../estilos/est_General.css">
		<script language="JavaScript" src="../librerias/date-picker.js"></script>
		<script language="JavaScript" src="../librerias/Lib_FuncValidacion.js"></script>
		<script language="JavaScript" src="../librerias/msrsclient.js"></script>
		<script language="JavaScript" src="../Scripts/security.js"></script><!-- INC - PBI000002160737 -->
		<script language="javascript">
		
        var serverURL = "ifrm_Combos.aspx"; //"http://localhost/SampleRemoteScriptingWeb/MyServerSidePage.aspx";
        var blnResult;
        var strHTML;
        var strSel;

function textCounter(obj) {
	var maximo;
	switch (parseInt(document.frmPrincipal.cboTipDocumento.options[document.frmPrincipal.cboTipDocumento.selectedIndex].value)) {
		case 1 :
			maximo = 8;
			break;
		case 2 :
			maximo = 10;
			break;
		case 3 :
			maximo = 10;
			break;
		
		case 4 : maximo = <%=configurationsettings.appsettings("gIntCarneExtranjeriaMax")%>;	break;
		
		case 6 :
			maximo = 11;
			break;
		case 7 :
			maximo = 10;
			break;
		default :
			maximo = 15;
			break;
	}
	if (obj.value.length > maximo) // if too long...trim it!
		obj.value = obj.value.substring(0, maximo); // otherwise, update 'characters left' counter
}

function f_ValidarDI(){
	if (!ValidaCombo('document.frmPrincipal.cboTipDocumento','el campo tipo de Doc. de Identidad ',false)) return false;
	switch (parseInt(document.frmPrincipal.cboTipDocumento.options[document.frmPrincipal.cboTipDocumento.selectedIndex].value)) {
		case 1 :
			if (!ValidaDNI('document.frmPrincipal.txtNumDocumento','el campo número de Doc. de Identidad ',false)) return false;
			break;
		case 2 :
			if (!ValidaFP('document.frmPrincipal.txtNumDocumento','el campo número de Doc. de Identidad ',false)) return false;
			break;
		case 3 :
			if (!ValidaFA('document.frmPrincipal.txtNumDocumento','el campo número de Doc. de Identidad ',false)) return false;
			break;
		case 4 :
			if (!ValidaEXTR('document.frmPrincipal.txtNumDocumento','el campo número de Doc. de Identidad ',false)) return false;
			break;
		case 6 :
			if (!ValidaRUC('document.frmPrincipal.txtNumDocumento','el campo número de Doc. de Identidad ',false)) return false;
			break;
		case 7 :
			if (!ValidaPAS('document.frmPrincipal.txtNumDocumento','el campo número de Doc. de Identidad ',false)) return false;
			break;
		default :
			if (!ValidaAlfanumerico('document.frmPrincipal.txtNumDocumento','el campo número de Doc. de Identidad ',false)) return false;
			break;
	}
	return true;
}      

function f_CambiaVend()
{
  // Los datos extraidos del vendedor no tienen el dato telefono.
  //if (document.frmPrincipal.cboSelectVend.value != "" )
  //   document.frmPrincipal.txtTelfV.value = document.frmPrincipal.cboSelectVend.value.split("#")[1];
  //else
     document.frmPrincipal.txtTelfV.value = "";   
}
     
function f_CambiaCamp()
{
		//alert('consulta campañas');
		//alert(document.frmPrincipal.cboArti.value.substring(0,18));
		//alert(document.frmPrincipal.cboCamp.value);
        msrsPOST = true;
        //RSExecute(serverURL,"CargaListaPre",document.frmPrincipal.cboCamp.value,document.frmPrincipal.cboArti.value.substring(0,10),cb,cbError,"X");
		RSExecute(serverURL,"CargaListaPre",document.frmPrincipal.cboCamp.value,document.frmPrincipal.cboArti.value.substring(0,18),cb,cbError,"X");
		document.frmPrincipal.hidIDCampana.value = document.frmPrincipal.cboCamp.value;
		document.frmPrincipal.hidCampana.value = document.frmPrincipal.cboCamp[document.frmPrincipal.cboCamp.selectedIndex].text;
}

function f_CambiaLista()
{
	msrsPOST = true;
        RSExecute(serverURL,"CargaPlanTarif",document.frmPrincipal.cboLPre.value,cb2,cbError,"X");
	document.frmPrincipal.hidIDListaPrecio.value = document.frmPrincipal.cboLPre.value;
	document.frmPrincipal.hidListaPrecio.value = document.frmPrincipal.cboLPre[document.frmPrincipal.cboLPre.selectedIndex].text;
}

function f_cambiaplan()
{
	document.frmPrincipal.hidIDPlanTarifario.value = document.frmPrincipal.cboPlanT.value;
	document.frmPrincipal.hidPlanTarifario.value = document.frmPrincipal.cboPlanT[document.frmPrincipal.cboPlanT.selectedIndex].text;
}

function f_CambiaMotivo()
{
  msrsPOST = true;
  RSExecute(serverURL,"CargaCentro",document.frmPrincipal.cboMotivoPedido.value,cb5,cbError,"X");
  f_CambiaCodComer();
}

function f_CambiaCodComer(){
    var cadena,i;
	cadena = document.frmPrincipal.cboMotivoPedido.value;
	if (cadena=="T23"){
	   document.getElementById("dcodcomer").style.display = "block";
	}
	else{
	   document.getElementById("dcodcomer").style.display = "none";
	}
}

function f_CambiaClasePed()
{
   var cadena;
   if (document.frmPrincipal.cboTipDocumento.value == "6")
      cadena = document.frmPrincipal.cboClasePedido.value;
   else
      cadena = document.frmPrincipal.cboClasePedido1.value;
      
   if ((cadena=="ZTGR")||(cadena=="ZDTR")||(cadena=="ZPFR")) {
     document.getElementById("mped4").style.display = "block";
     document.getElementById("mped5").style.display = "block";
     document.getElementById("mped6").style.display = "none";
     document.frmPrincipal.cboVPlazo.value = "";
   }
   else
   {
     document.getElementById("mped4").style.display = "none";
     document.getElementById("mped5").style.display = "none";
     
     document.frmPrincipal.cboCentroCosto.value = "";
	 document.frmPrincipal.cboMotivoPedido.value = "";
	 document.getElementById("mped2").style.display = "block";
	 
	 if ((cadena=="ZPBR")||(cadena=="ZPDB")||(cadena=="ZPDF")||(cadena=="ZPER")||(cadena=="ZPFP")||(cadena=="ZPPA")||(cadena=="ZPRE")||(cadena=="ZPVR")||
				(cadena=="ZPBRX")||(cadena=="ZPDBX")||(cadena=="ZPDFX")||(cadena=="ZPERX")||(cadena=="ZPFPX")||(cadena=="ZPPAX")||(cadena=="ZPREX")||(cadena=="ZPVRX")) {
				//MOSTRAR VENTA EN CUOTAS
				document.getElementById("mped6").style.display = "block";
	}else{
	     		document.getElementById("mped6").style.display = "none";
	}
    
    document.getElementById("dcodcomer").style.display = "none";
	document.frmPrincipal.txtcodcomer.value = ""; 
     
   }
   
   f_CambiaCodComer();
}

function f_NroTelef()
{
   var cboIMEIArt = document.getElementById("cboIMEIArt");
  if ( cboIMEIArt != null)
  {
    //if (cboIMEIArt.value.substring(11,21) != "0000000000" )
     if (cboIMEIArt.value.substring(19) != "0000000000" )
      //document.frmPrincipal.txtNTelf.value = cboIMEIArt.value.substring(11,21); //modificado JCR
      //alert("valor cboIMEI:" + cboIMEIArt.value);
      //alert(cboIMEIArt.value.substring(19));
      //document.frmPrincipal.txtNTelf.value = cboIMEIArt.value.substring(11,26);
      document.frmPrincipal.txtNTelf.value = cboIMEIArt.value.substring(19);
   // else
   //   document.frmPrincipal.txtNTelf.value = "";
    document.frmPrincipal.hidIMEI.value =  cboIMEIArt.options[cboIMEIArt.selectedIndex].text; 
  }  

}

function f_CambiaArti()
{  
	//alert('f_CambiaArti()');
	//alert(document.frmPrincipal.cboArti.value);
	
	if (document.frmPrincipal.cboArti.value.substring(0,18) != "")
	{
		document.frmPrincipal.txtCodArti.value = document.frmPrincipal.cboArti.value.substring(0,18);	
	}
	else
	{
		document.frmPrincipal.cboArti.focus();
	}
  
	cadena=document.frmPrincipal.txtCodArti.value; 
	
	//alert(document.frmPrincipal.cboArti.value.substr(19));
  
	//activar o desactivar campo de telefono
	//if (cadena.substr(0,2) == "RC")			//cuando es una RC es una tarjeta
	if (document.frmPrincipal.cboArti.value.substr(19) == "TV0005" || document.frmPrincipal.cboArti.value.substr(19) == "TV0003")
	{
		 
		document.frmPrincipal.txtNTelf.value = "";
		document.frmPrincipal.txtNTelf.readOnly = true;
		document.getElementById("txtNTelf").className = "clsInputDisable";
		
		//xi! document.getElementById("tdSerie").innerHTML = "<input name=txtIMEIArt type=text  class=clsInputEnable  id=txtIMEIArt  value='' size=43 maxlength=18>"
		
		if(document.frmPrincipal.txtCant.value == "")
		{ 
			document.frmPrincipal.txtCant.value = "1";
		}
		
		document.frmPrincipal.txtCant.readOnly = true;
		document.getElementById("txtCant").className = "clsInputDisable";
		 
		
		//IDG :voy a cargar la campaña por defecto para las tarjetas de recarga: tipo material: TV0005
		if (document.frmPrincipal.cboArti.value.substr(19) == "TV0005")
		{
			//alert('asigna campaña por defecto.');
			for (i=0;i<document.frmPrincipal.cboCamp.length;i++)
			{
				if (document.frmPrincipal.cboCamp.options[i].value == "NDEF")
				{
					document.frmPrincipal.cboCamp.selectedIndex = i
				}   
			}
		}
		 // ******************* FDG
		
		
	} 
	else{
		document.getElementById("txtNTelf").className = "clsInputEnable";
		document.frmPrincipal.txtNTelf.readOnly = false;
		msrsPOST = true;			
		
		//alert('RSExecute');
		//RSExecute(serverURL,"CargaIMEIS",document.frmPrincipal.cboArti.value.substring(0,18),document.frmPrincipal.txtNTelf.value,cb3,cbError,"X");
		if (cadena!= "")
		{
			//alert('llama RSExecute')
			//alert(cadena);
			RSExecute(serverURL,"CargaIMEIS",cadena,document.frmPrincipal.txtNTelf.value,cb3,cbError,"X");
		}
		
		//comentado por JTN HABILITAR LA OPCION DE PAGO CON RUC
		/*if (cadena.substr(0,7) == "SERECVI") {
			document.frmPrincipal.cboTipDocumento.value = 1;
			document.frmPrincipal.cboTipDocumento.disabled = true;
			document.getElementById("cboTipDocumento").className = "clsSelectDisable";
			
			document.frmPrincipal.txtNumDocumento.value = '00012006';
			document.frmPrincipal.txtNumDocumento.readOnly = true;
			document.getElementById("txtNumDocumento").className = "clsInputDisable";
		}
		else{
			document.frmPrincipal.cboTipDocumento.disabled = false;
			document.getElementById("cboTipDocumento").className = "clsSelectEnable";
			document.frmPrincipal.txtNumDocumento.readOnly = false;
			document.getElementById("txtNumDocumento").className = "clsInputEnable";
		}*/
	}
}

function f_CambiaTxtArti()
{
  
  var blnFound = false;
  for (i=0;i<document.frmPrincipal.cboArti.length;i++)
  {
	if (document.frmPrincipal.cboArti.options[i].value.substring(0,18) == document.frmPrincipal.txtCodArti.value)
    {
       document.frmPrincipal.cboArti.selectedIndex = i
       blnFound = true;
    }   
  }
  if (!blnFound)
    document.frmPrincipal.cboArti.selectedIndex = 0;
  
  f_CambiaArti();		//descomengar pruebas serie
  
 /*
 if (document.getElementById("txtIMEIArt") != null)
  {
	if (!document.frmPrincipal.txtIMEIArt.readOnly)
		f_CambiaArti();
	else
	{
		document.frmPrincipal.txtNTelf.value = "";
		document.frmPrincipal.txtNTelf.readOnly = true;
		document.getElementById("txtNTelf").className = "clsInputDisable";
	}   
  }
  else 
     f_CambiaArti(); 
  */
}

function f_CambiaTipDoc()
{
  document.frmPrincipal.txtNumDocumento.value = "";
  f_CambiaTipoDocVenta();
}

function f_CambiaTipoDocVenta() {
		var cadena,i;
		cadena = document.frmPrincipal.cboTipDocumento.value;

		if (cadena!="06") {
			document.getElementById("divNat").style.display = "none";
			document.getElementById("divJur").style.display = "block";
		}
		else {
			document.getElementById("divNat").style.display = "block";
			document.getElementById("divJur").style.display = "none";
		}

	}

function cb(co) {

if (co.return_value) {
			document.getElementById("tdLista").innerHTML = co.return_value;
			document.frmPrincipal.cboLPre.value = strSel;
		}
  
  }
  
function cb2(co) {

if (co.return_value) {
			document.getElementById("tdPlanT").innerHTML = co.return_value;
		}
  
  }
    
function cb3(co) {

	if (co.return_value) {
		strHTML = co.return_value;
		//INI-936 - SMB - Se creo la variable "parametro" y se modifico la condicion
		 var parametro = '<%=configurationsettings.appsettings("MATERIAL_RV")%>';
		
		if(parametro.indexOf(document.frmPrincipal.cboArti.value.substring(0,18)) === -1)
		{	 
			document.getElementById("txtCant").className = "clsInputDisable";
			document.frmPrincipal.txtCant.readOnly = true;
			document.frmPrincipal.txtCant.value = "1";
		}else
		{
			document.getElementById("txtCant").className = "clsInputEnable";
			document.frmPrincipal.txtCant.readOnly = false;
			document.frmPrincipal.txtCant.value = "1";
		}
		
		
		
		//if (f_CheckChip(document.frmPrincipal.cboArti.value.substring(11,20)))
		if (f_CheckChip(document.frmPrincipal.cboArti.value.substring(19)))
		{
			document.getElementById("tdSerie").innerHTML = strHTML;
		}
		else
		{
			//if (f_CheckPack(document.frmPrincipal.cboArti.value.substring(11,20)))
			if (f_CheckPack(document.frmPrincipal.cboArti.value.substring(19)))
			{
				document.getElementById("tdSerie").innerHTML = strHTML;
			}
			else
			{
				document.getElementById("tdSerie").innerHTML = "<input name=txtIMEIArt type=text  class=clsInputEnable  id=txtIMEIArt value='' size=43 maxlength=18>"
			}
	
		}
  
		 if (document.frmPrincipal.cboArti.value.substring(19)=="TS0003" || document.frmPrincipal.cboArti.value.substring(19)=="TS0004"){
				 
				document.frmPrincipal.txtIMEIArt.value = "";
				document.frmPrincipal.txtIMEIArt.readOnly = true;
				document.getElementById("txtIMEIArt").className = "clsInputDisable";
		}
  
		if (document.frmPrincipal.cboArti.value.substring(21,26) == "")
		{
			if (document.getElementById("txtIMEIArt") !=null )
			{
				 
				document.frmPrincipal.txtIMEIArt.value = "";
				document.frmPrincipal.txtIMEIArt.readOnly = true;
				document.getElementById("txtIMEIArt").className = "clsInputDisable";
			} 
			document.getElementById("txtCant").className = "clsInputEnable";
			document.frmPrincipal.txtCant.readOnly = false;
			document.frmPrincipal.txtCant.value = "1";
	 
		}     
		if(cadena == '<%=configurationsettings.appsettings("strCodArticuloBonoAnualTFI")%>'){
			document.frmPrincipal.txtCant.value = "1";
			document.getElementById("txtCant").className = "clsInputDisable";	
			document.frmPrincipal.txtCant.readOnly = true;
		}
	}	
  
}
  
function cb4(co) {

if (co.return_value) {
			blnResult = co.return_value;
		}
  
  }
  
function cb5(co) {

if (co.return_value) {
			document.getElementById("tdCentroC").innerHTML = co.return_value;
		}
  
  }  
  
  function cbError(co) {
		alert("Error callback fired.");
		if (co.message) {
			alert("Context:" + co.context + "\nError: " + co.message);
		}
	  }
	  
	  
function f_CheckChip(sndstr) {
	//var cadena, cad2, ix;
	//cadena = document.frmPrincipal.hldChips.value;
	//alert('<%=configurationsettings.appsettings("constChips")%>');	
	//if(<%=configurationsettings.appsettings("constChips")%>==sndstr){
	if('<%=configurationsettings.appsettings("constChips")%>'==sndstr){
		return true;	
	}else{
		return false;
	}
	
	//if (cadena.indexOf(";"+sndstr+";")>=0) {
	//	return true;
	//} else {
	//	return false;
	//}
}

function f_CheckPack(sndstr) {
	//var cadena;
	//cadena = document.frmPrincipal.hldPacks.value;
	//if (cadena.indexOf(";" + sndstr + ";") >= 0) {
	//	return true;
	//} else {
	//	return false;
	//}
	
	if('<%=configurationsettings.appsettings("constPacks")%>'==sndstr){
		return true;	
	}else{
		return false;
	}
}

function f_CheckServ(sndstr) {
	var cadena;
	cadena = document.frmPrincipal.hldServ.value;
	if (cadena.indexOf(";"+sndstr+";")>=0) {
		return true;
	} else {
		return false;
	}
}


function f_VerificarG() {

    if (document.frmPrincipal.cboTipDocumento.value=="6")
	  if (!ValidaCombo('document.frmPrincipal.cboClasePedido','el campo Clase de Pedido ',false)) return false;
	else
	  if (!ValidaCombo('document.frmPrincipal.cboClasePedido1','el campo Clase de Pedido ',false)) return false;
	  
	if (!ValidaFechaA('document.frmPrincipal.txtFechaPrecioVenta',false)) return false;
	if (document.frmPrincipal.cboMotivoPedido.value=="T23"){
	   if (!ValidaDigitosB('document.frmPrincipal.txtcodcomer','el campo Codigo Comercial ',5,false)) return false;
	}
	if (!ValidaCombo('document.frmPrincipal.cboSelectVend','el campo Vendedor ',false)) return false;
	if (!ValidaNumero('document.frmPrincipal.txtTelfV','el campo Telefono del Vendedor ',true)) return false;
	var cadena
		cadena = document.getElementById("mped4").style.display
		cadena2 = document.frmPrincipal.cboMotivoPedido.value;
		if (cadena == "block"){
			if (cadena2 == ""){
				alert('el campo Motivo de Pedido es obligatorio');
				document.frmPrincipal.cboMotivoPedido.focus();
				return false;
			}
		}
	//if (!ValidaCombo('document.frmPrincipal.cboCentroCosto','el campo Centro de Costos ',false)) return false;
	return true;
}

function f_Valida()
{
   if (f_ValidarDI()) {
      if (f_VerificarG())
      {
        document.frmPrincipal.hidVerif.value = "1"
        return true;
      }  
      else
      {
        document.frmPrincipal.hidVerif.value = "0"
        return false;
      }
   }

}

function f_Telefono(strTelefono,strTipo)
{
  var blnSeguir = true;
  var tabla = document.getElementById("dgDetalle")
  var trs = tabla.getElementsByTagName("tr");
  for (i=0;i<trs.length;i++)
  {
    var tds = trs[i].getElementsByTagName("td");
    if (tds.length > 0)
      if (tds[12].innerText == strTelefono && tds[2].innerText.substring(0,2) == strTipo)
      {
         if (strTipo == "PB")
           strSubMen = "Pack";
         else 
           if (strTipo == "PS")
              strSubMen = "Chip"; 
           else
              return true;
                 
         alert('Ya existe un ' + strSubMen + ' Asignado al Telefono');
         blnSeguir = false;
      }   
    
  }
  return blnSeguir
}

function f_VerSerie(strSerie)
{
  var blnSeguir = true;
  var tabla = document.getElementById("dgDetalle")
  var trs = tabla.getElementsByTagName("tr");
  for (i=0;i<trs.length;i++)
  {
    var tds = trs[i].getElementsByTagName("td");
    if (tds.length > 0)
      if (tds[5].innerText == strSerie)
      {
         alert('Ya existe un Producto en el Detalle con esta serie');
         blnSeguir = false;
      }   
    
  }
  return blnSeguir
}

function f_RecargaVirtual()
{
  var tabla = document.getElementById("dgDetalle")
  var trs = tabla.getElementsByTagName("tr");
  
  if (document.frmPrincipal.cboArti.value.substring(11,20) == "A10130011" && trs.length > 1)
  {
    alert("En el Detalle de la venta solo debe haber una Recarga Virtual");
    return false;
  }
  
  for (i=0;i<trs.length;i++)
  {
    var tds = trs[i].getElementsByTagName("td");
    if (tds.length > 0)
      if (tds[2].innerText.substring(0,5) == "SEREC" || tds[2].innerText.substring(0,5) == "SEREV")
      {
         alert("En el Detalle de la venta solo debe haber una Recarga Virtual");
         return false;
      }   
    
  }
  return true;
}

function f_Agrega()
{
	document.frmPrincipal.hidIDListaPrecio.value = document.frmPrincipal.cboLPre.value;
	document.frmPrincipal.hidListaPrecio.value = document.frmPrincipal.cboLPre[document.frmPrincipal.cboLPre.selectedIndex].text;
	document.frmPrincipal.hidIDPlanTarifario.value = document.frmPrincipal.cboPlanT.value;
	document.frmPrincipal.hidPlanTarifario.value = document.frmPrincipal.cboPlanT[document.frmPrincipal.cboPlanT.selectedIndex].text;
   document.frmPrincipal.hidVerif.value = "0";
   event.returnValue = false; //no ejecuta el submit
	      
   if (!f_RecargaVirtual())
      return false;
   
   if (!f_Telefono(document.frmPrincipal.txtNTelf.value,document.frmPrincipal.txtCodArti.value.substring(0,2)))
      return false;
   
   var cboIMEIArt = document.getElementById("cboIMEIArt");
   if ( cboIMEIArt != null)
   {
     if (!ValidaCombo('document.frmPrincipal.cboIMEIArt','el campo Articulo ',false)) return false;
     if (!f_VerSerie(document.frmPrincipal.cboIMEIArt.value)) return false;
   }  
   else
   {
     if(document.frmPrincipal.txtIMEIArt.className == "clsInputEnable" && '<%=Session("strTrama")%>' == "")
     {
       if(!ValidaAlfanumerico('document.frmPrincipal.txtIMEIArt','el campo Articulo ',false)) return false
       if (!f_VerSerie(document.frmPrincipal.txtIMEIArt.value)) return false;
     }  
   }
   
   if(!ValidaAlfanumerico('document.frmPrincipal.txtCodArti','el campo Articulo ',false)) return false
   if(!ValidaAlfanumerico('document.frmPrincipal.txtCant','el campo Cantidad ',false)) return false
   
   if (document.frmPrincipal.txtNTelf.className == "clsInputEnable")
     if (!ValidaAlfanumerico('document.frmPrincipal.txtNTelf','el campo Telefono ',false)) return false;
     
   if (!ValidaCombo('document.frmPrincipal.cboCamp','el campo Campañas ',false)) return false;
   if (!ValidaCombo('document.frmPrincipal.cboLPre','el campo Lista de Precios ',false)) return false;
   if (!ValidaCombo('document.frmPrincipal.cboPlanT','el campo Plan Tarifario ',false)) return false;
   cadena = document.getElementById("mped5").style.display
		cadena2 = document.frmPrincipal.cboCentroCosto.value;
		if (cadena == "block"){
			if (cadena2 == ""){
				alert('el campo Centro de Costos es obligatorio');
				document.frmPrincipal.cboCentroCosto.focus();
				return false;
			}
		}
   
    cadena = document.frmPrincipal.cboClasePedido.value;
	cadena2 = document.frmPrincipal.cboMotivoPedido.value;
	if ((cadena=="ZTGR")||(cadena=="ZDTR")||(cadena=="ZPFR")) {
	// (cadena == "block"){
		if (cadena2 == ""){
			alert('el campo Motivo de Pedido es obligatorio');
			document.frmPrincipal.cboMotivoPedido.focus();
			return false;
		}
	}
	if (parseInt(document.frmPrincipal.txtCant.value) < 1){
		alert("El campo Cantidad debe ser mayor o igual a uno");
		return false;
	}
	event.returnValue = true;
	document.frmPrincipal.hidVerif.value = "1";
	return true;
   
}

function f_AgregaGraba()
{
  document.frmPrincipal.hidVerif.value = "0";
  if (f_Agrega())
    if (f_Valida())
       document.frmPrincipal.hidVerif.value = "1";  
    else
       event.returnValue = false; 
  else
     event.returnValue = false;       
}


function f_Series()
{
  event.returnValue =false;
	//if (document.frmPrincipal.txtCodArti.value.substr(0,2)=="RC") KEY
	//alert(document.frmPrincipal.cboArti.value.substr(19));
  if (document.frmPrincipal.cboArti.value.substr(19) == "TV0005" || document.frmPrincipal.cboArti.value.substr(19) == "TV0003")
    event.returnValue =true;
  else
  {
    alert("Venta por series esta disponible para productos de Recarga Fisica");
    return;
  }    
     
}

function abrirPopClaroClub()
{
	
 if (f_Valida()){

		var sTipoDoc = document.frmPrincipal.cboTipDocumento.options[document.frmPrincipal.cboTipDocumento.selectedIndex].value;
		var nroDoc = window.document.frmPrincipal.txtNumDocumento.value;
		var sTipoAcc = '<%=configurationsettings.appsettings("constTipAccCC_Canje")%>';//"0:Consulta | 1:Consulta/Canje" 
        var sTipoClien = '<%=configurationsettings.appsettings("constTipoClientePOSTPAGO")%>';//2:PostPago | 3:Prepago | 6:DTH | 7:HFC
		var url = '<%= configurationsettings.appsettings("popupClaroPuntos")%>'; 
		var opciones = "dialogHeight: 400px; dialogWidth: 700px; edge: Raised; center:Yes; help: No; resizable=no; status: No";
		var vRetorno; 
		var vtemp; 
        param = '?sTipoDoc=' + sTipoDoc + '&nroDoc=' + nroDoc + '&sTipoAcc=' + sTipoAcc + '&sTipoClien=' + sTipoClien;
		url = url + param; 		
		//window.open(url, '', opciones);
        vRetorno = window.showModalDialog(url, '', opciones);
		if (vRetorno != 'undefined' && vRetorno != null) { 		
		 if (vRetorno.indexOf(',') > -1) { 
		    var arrayRespuesta;			
		    arrayRespuesta = vRetorno.split(','); 
		    var accion = arrayRespuesta[0]; 
		    var cantClaroPunto = arrayRespuesta[1];
		    var cantSoles = arrayRespuesta[2];
		    var mensaje = arrayRespuesta[3]; 				
		   if(accion==1)  
		   { 	   
		     if(parseFloat(cantClaroPunto) > 0 && parseFloat(cantSoles)>0)
		      { 
		       document.frmPrincipal.txtDescuentoCC.value = cantSoles;		      
		       return true;
		      } 
		   } 
		 }
		}
	}
	else
	{
		alert("Debe de ingresar el Tipo y Numero de documento");
		return false;
	} 
}


		</script>
	</HEAD>
	<body leftMargin="0" topMargin="0" marginwidth="0" marginheight="0">
		<form id="frmPrincipal" method="post" name="frmPrincipal" runat="server">
			<input id="hidIMEI" type="hidden" name="hidIMEI"><input id="hidVerif" type="hidden" name="hidVerif">
			<table border="0" cellSpacing="0" cellPadding="0" width="850">
				<tr>
					<td vAlign="top" width="10">&nbsp;</td>
					<td vAlign="top" rowSpan="2" width="820">
						<table class="tabla_borde" cellSpacing="0" cellPadding="0" width="790" align="center">
							<tr>
								<td align="center">
									<table border="0" cellSpacing="0" cellPadding="0" width="810" align="center">
										<tr>
											<td class="TituloRConsulta" height="30" colSpan="4" align="center">Venta Rápida</td>
										</tr>
									</table>
									<table border="0" cellSpacing="0" cellPadding="0" width="810" align="center">
										<tr>
											<td>
												<table class="Arial12B" border="0" cellSpacing="2" cellPadding="0" width="80%" align="center">
													<tr>
														<td>&nbsp;&nbsp;&nbsp;Tipo de Doc. Identidad :</td>
														<td>&nbsp;
															<asp:dropdownlist id="cboTipDocumento" runat="server" CssClass="clsSelectEnable"></asp:dropdownlist></td>
														<td>&nbsp;&nbsp;&nbsp;Nro. de Doc. Identidad :</td>
														<td><asp:textbox id="txtNumDocumento" runat="server" CssClass="clsInputEnable" MaxLength="15"></asp:textbox></td>
													</tr>
												</table>
											</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td height="4"></td>
							</tr>
						</table>
						<table>
							<tr>
								<td height="4"></td>
							</tr>
						</table>
						<table class="tabla_borde" cellSpacing="0" cellPadding="0" width="790" align="center">
							<tr>
								<td align="center">
									<table border="0" cellSpacing="0" cellPadding="0" width="810" align="center">
										<tr>
											<td>
												<table class="Arial12B" border="0" cellSpacing="2" cellPadding="0" width="100%" align="center">
													<tr>
														<td height="4"></td>
													</tr>
													<tr>
														<td height="18">
															<table border="0" cellSpacing="1" cellPadding="0" align="center">
																<tr class="Arial12b" bgColor="white">
																	<td class="TituloRConsulta" align="center">Datos de la Venta</td>
																</tr>
															</table>
														</td>
													</tr>
													<tr>
														<td height="3"></td>
													</tr>
												</table>
												<table class="Arial12B" border="0" cellSpacing="2" cellPadding="0" width="100%" align="center">
													<tr>
														<td width="25">&nbsp;</td>
														<td width="170">&nbsp;&nbsp;&nbsp;Tipo de Doc. Venta (*):</td>
														<td width="155">
															<div id="divNat" class="dropcont"><asp:dropdownlist id="cboClasePedido" runat="server" CssClass="clsSelectEnable" BackColor="White"></asp:dropdownlist></div>
															<div id="divJur" class="dropcont"><asp:dropdownlist id="cboClasePedido1" runat="server" CssClass="clsSelectEnable" BackColor="White"></asp:dropdownlist></div>
														</td>
														<td width="25">&nbsp;</td>
														<td width="170">&nbsp;&nbsp;&nbsp;Fecha de Venta (*):</td>
														<td align="left"><asp:textbox id="txtFechaPrecioVenta" runat="server" CssClass="clsInputEnable" MaxLength="10"></asp:textbox>&nbsp;
															<A onmouseover="window.status='Date Picker';return true;" onmouseout="window.status='';return true;"
																href="javascript:show_calendar('frmPrincipal.txtFechaPrecioVenta');"><IMG border="0" src="../../images/botones/btn_Calendario.gif"></A>
														</td>
													</tr>
												</table>
												<div id="mped4" class="dropcont">
													<table class="Arial12B" border="0" cellSpacing="2" cellPadding="0" width="100%" align="center">
														<tr>
															<td width="25">&nbsp;</td>
															<td width="170">&nbsp;&nbsp;&nbsp;Motivo de Pedido :</td>
															<td width="155"><asp:dropdownlist id="cboMotivoPedido" runat="server" CssClass="clsSelectEnable"></asp:dropdownlist></td>
															<td colSpan="3">&nbsp;</td>
														</tr>
													</table>
												</div>
												<div id="dcodcomer" class="dropcont">
													<table class="Arial12B" border="0" cellSpacing="2" cellPadding="0" width="100%" align="center">
														<tr>
															<td width="25">&nbsp;</td>
															<td width="170">&nbsp;&nbsp;&nbsp;Codigo Comercial :</td>
															<td width="170"><asp:textbox id="txtcodcomer" runat="server" CssClass="clsInputEnable" MaxLength="12"></asp:textbox></td>
															<td colSpan="3">&nbsp;</td>
														</tr>
													</table>
												</div>
												<table class="Arial12B" border="0" cellSpacing="2" cellPadding="0" width="100%" align="center">
													<tr>
														<td style="HEIGHT: 20px">&nbsp;</td>
														<td style="HEIGHT: 20px" class="Arial12b">&nbsp;&nbsp;&nbsp;Vendedor (*):</td>
														<td style="HEIGHT: 20px" colSpan="4"><asp:dropdownlist id="cboSelectVend" runat="server" CssClass="clsSelectEnable3"></asp:dropdownlist></td>
													</tr>
													<tr>
														<td width="25">&nbsp;</td>
														<td class="Arial12b" width="170">&nbsp;&nbsp;&nbsp;Telf. Vendedor :</td>
														<td width="155"><asp:textbox id="txtTelfV" runat="server" CssClass="clsInputEnable" MaxLength="12"></asp:textbox></td>
														<td width="25">&nbsp;</td>
														<td class="Arial12b" width="170">&nbsp;&nbsp;&nbsp;Moneda (*):</td>
														<td><input id="cboMoneda" class="clsInputDisable" tabIndex="8" value="Local" readOnly maxLength="12"
																size="30" name="cboMoneda"></td>
													</tr>
												</table>
												<!--*** PMO - CUOTAS EN VENTA RAPIDA - INICIO ***-->
												<!--*** FECHA: 21/11/2005 - AUTOR: GGT-->
												<div id="mped6" class="dropcont">
													<table class="Arial12B" border="0" cellSpacing="2" cellPadding="0" width="100%" align="center">
														<tr>
															<td width="25">&nbsp;</td>
															<td class="Arial12b" width="170">&nbsp;&nbsp;&nbsp;Venta a Plazos :</td>
															<td width="155"><asp:dropdownlist id="cboVPlazo" runat="server" CssClass="clsSelectEnable"></asp:dropdownlist></td>
															<td colSpan="3">&nbsp;</td>
														</tr>
													</table>
												</div>
												<!--*** PMO - CUOTAS EN VENTA RAPIDA - FIN ***-->
												<!--------------------->
												<!--<table class="Arial12B" cellSpacing="2" cellPadding="0" width="100%" align="center" border="0">
													<tr>
														<td width="5">&nbsp;</td>
														<td class="Arial12b" width="150">&nbsp;&nbsp;&nbsp;Transferencia Gratuita :</td>
														<td width="155"><input id="chkTG" type="checkbox" CHECKED name="id=chkTG">
														</td>
													</tr>
												</table> -->
												<div id="mped2" class="dropcont">
													<table class="Arial12B" border="0" cellSpacing="2" cellPadding="0" width="100%" align="center">
														<tr>
															<td width="354"></td>
															<td colSpan="3"></td>
														</tr>
													</table>
												</div>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<table>
							<tr>
								<td height="4"></td>
							</tr>
						</table>
						<input id="hidIDCampana" value="0" type="hidden" runat="server"> <input id="hidIDListaPrecio" value="0" type="hidden" runat="server">
						<input id="hidIDPlanTarifario" value="0" type="hidden" runat="server"> <input id="hidCampana" value="0" type="hidden" runat="server">
						<input id="hidListaPrecio" value="0" type="hidden" runat="server"> <input id="hidPlanTarifario" value="0" type="hidden" runat="server">
						<table class="tabla_borde" cellSpacing="0" cellPadding="0" width="790" align="center">
							<!--aui va añadir articulo-->
							<tr>
								<td align="center">
									<table border="0" cellSpacing="0" cellPadding="0" width="810" align="center">
										<tr>
											<td class="TituloRConsulta" height="30" colSpan="4" align="center"><B>Nuevo Artículo</B></td>
										</tr>
									</table>
									<table border="0" cellSpacing="0" cellPadding="0" width="810" align="center">
										<tr>
											<td>
												<table border="0" cellSpacing="0" borderColor="#d0d8f0" cellPadding="0" width="780" bgColor="#ffffff"
													align="center">
													<tr>
														<td>
															<table border="0" cellSpacing="2" cellPadding="0" width="100%">
																<tr>
																	<td width="10">&nbsp;</td>
																	<td class="Arial12b" width="130">&nbsp;&nbsp;&nbsp;Cod. de Artículo :
																	</td>
																	<td class="Arial12b" width="200"><asp:textbox id="txtCodArti" runat="server" CssClass="clsInputEnable" MaxLength="18" Width="198px"></asp:textbox></td>
																	<td width="10">&nbsp;</td>
																	<td class="Arial12b" colSpan="2"><asp:dropdownlist id="cboArti" runat="server" CssClass="clsSelectEnable4"></asp:dropdownlist></td>
																</tr>
															</table>
															<div id="cboIMEIA">
																<table border="0" cellSpacing="2" cellPadding="0" width="100%">
																	<tr>
																		<td width="10">&nbsp;</td>
																		<td class="Arial12b" width="130">&nbsp;&nbsp;&nbsp;Serie de Artículo :
																		</td>
																		<td id="tdSerie" class="Arial12b" width="200"><input id="txtIMEIArt" class="clsInputEnable" runat="server" tabIndex="13" maxLength="18"
																				size="43" name="txtIMEIArt"></td>
																		<td width="10">&nbsp;</td>
																		<td class="Arial12b" width="200">&nbsp;&nbsp;&nbsp;Cantidad :
																		</td>
																		<td class="Arial12b"><input id="txtCant" class="clsInputDisable" tabIndex="0" readOnly maxLength="5" size="6"
																				name="txtCant" runat="server"></td>
																	</tr>
																</table>
															</div>
															<table border="0" cellSpacing="2" cellPadding="0" width="100%">
																<tr>
																	<td style="HEIGHT: 2px" width="10">&nbsp;</td>
																	<td style="HEIGHT: 2px" class="Arial12b" width="130">&nbsp;&nbsp;&nbsp;Nro. 
																		Teléfono :
																	</td>
																	<td style="HEIGHT: 2px" class="Arial12b" width="200"><asp:textbox id="txtNTelf" runat="server" CssClass="clsInputEnable" MaxLength="15" Width="198px"></asp:textbox></td>
																	<td style="HEIGHT: 2px" width="10">&nbsp;</td>
																	<td style="HEIGHT: 2px" class="Arial12b" width="200">&nbsp;&nbsp;&nbsp;Campañas :
																	</td>
																	<td style="HEIGHT: 2px" class="Arial12b"><asp:dropdownlist id="cboCamp" runat="server" CssClass="clsSelectEnable5"></asp:dropdownlist></td>
																</tr>
																<tr>
																	<td width="10">&nbsp;</td>
																	<td class="Arial12b" width="130">&nbsp;&nbsp;&nbsp;Lista Precios :
																	</td>
																	<td id="tdLista" class="Arial12b" width="200"><asp:dropdownlist id="cboLPre" runat="server" CssClass="clsSelectEnable5"></asp:dropdownlist></td>
																	<td width="10">&nbsp;</td>
																	<td class="Arial12b" width="200">&nbsp;&nbsp;&nbsp;Plan Tarifario :
																	</td>
																	<td id="tdPlanT" class="Arial12b"><asp:dropdownlist id="cboPlanT" runat="server" CssClass="clsSelectEnable5"></asp:dropdownlist></td>
																</tr>
															</table>
															<table>
																<tr>
																	<td height="5"></td>
																</tr>
															</table>
															<div id="mped5" class="dropcont">
																<table class="Arial12B" border="0" cellSpacing="2" cellPadding="0" width="100%" align="center">
																	<tr>
																		<td width="10">&nbsp;</td>
																		<td width="130">&nbsp;&nbsp;&nbsp;Centro Costos :</td>
																		<td id="tdCentroC" width="200"><asp:dropdownlist id="cboCentroCosto" runat="server" CssClass="clsSelectEnable5"></asp:dropdownlist></td>
																		<td colSpan="2">&nbsp;</td>
																	</tr>
																</table>
															</div>
															<!--Datos de la tabla--></td>
													</tr>
												</table>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<table>
							<tr>
								<td height="5"></td>
							</tr>
						</table>
						<div id="BMenu">
							<table class="Arial12B" cellSpacing="2" cellPadding="0" width="200" align="left" style="DISPLAY:none">
								<tr>
									<td>&nbsp;&nbsp;Dscto. Claro Club S/:&nbsp;&nbsp;<INPUT class="clsInputDisable" id="txtDescuentoCC" style="WIDTH: 64px; HEIGHT: 24px" tabIndex="0"
											readOnly maxLength="5" size="5" name="txtDescuentoCC" runat="server" value="0.00"></td>
								</tr>
							</table>
							<table class="tabla_borde" cellSpacing="0" cellPadding="4" width="400" align="center">
								<tr>
									<!--
									<td align="center"><input class="BotonOptm" id="button1" style="WIDTH: 100px" onclick="javascript:f_Continuar();"
											type="button" value="Continuar" name="btncont"></td> -->
									<td align="center"><asp:button id="btnAgregar" runat="server" CssClass="BotonOptm" Width="100px" Text="Agregar"></asp:button></td>
									<td id="tdBtnGrabar" align="center" runat="server"><asp:button id="btnGrabar" runat="server" CssClass="BotonOptm" Width="100px" Text="Grabar"></asp:button></td>
									<td align="center"><asp:button id="btnSeries" runat="server" CssClass="BotonOptm" Width="100px" Text="Series"></asp:button></td>
									<!--
									<td align="center"><input class="BotonOptm" id="button5" style="WIDTH: 100px" onclick="javascript:f_Cancelar();"
											type="button" value="Cancelar" name="btncancel"></td> -->
									<td align="center"><asp:button id="btnAgregarGrabar" runat="server" CssClass="BotonOptm" Width="100px" Text="Agregar-Grabar"></asp:button></td>
									<td align="center" style="DISPLAY:none"><input class="BotonOptm" onclick="abrirPopClaroClub()" id="btnClaroClub" type="button"
											value="Canje de Puntos ClaroClub" name="btnClaroClub" style="WIDTH: 153px; HEIGHT: 19px">
									</td>
								</tr>
							</table>
						</div>
						<table>
							<tr>
								<td height="5"></td>
							</tr>
						</table>
						<table class="tabla_borde" cellSpacing="0" cellPadding="0" width="790" align="center">
							<tr>
								<td align="center">
									<table border="0" cellSpacing="0" cellPadding="0" width="810" align="center">
										<tr>
											<td class="TituloRConsulta" height="30" colSpan="4" align="center">Detalle de Venta</td>
										</tr>
									</table>
									<table border="0" cellSpacing="0" cellPadding="0" width="810" align="center">
										<tr>
											<td>
												<table border="0" cellSpacing="0" cellPadding="0" width="780" align="center">
													<tr>
														<td>
															<div style="BORDER-RIGHT: 1px; BORDER-TOP: 1px; OVERFLOW-Y: scroll; OVERFLOW-X: scroll; BORDER-LEFT: 1px; WIDTH: 775px; BORDER-BOTTOM: 1px; HEIGHT: 130px; TEXT-ALIGN: center"
																class="frame2"><asp:datagrid id="dgDetalle" runat="server" Width="1000px" BorderColor="White" BorderWidth="1px"
																	CellPadding="1" CellSpacing="1" AutoGenerateColumns="False">
																	<AlternatingItemStyle HorizontalAlign="Center" BackColor="#E9EBEE"></AlternatingItemStyle>
																	<ItemStyle HorizontalAlign="Center" Height="25px" CssClass="Arial11B" BackColor="#DDDEE2"></ItemStyle>
																	<HeaderStyle HorizontalAlign="Center" Height="22px" CssClass="Arial12B"></HeaderStyle>
																	<Columns>
																		<asp:TemplateColumn>
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="200px"></HeaderStyle>
																			<ItemTemplate>
																				<P align="center">
																					<asp:ImageButton id="iBtnDelete" runat="server" ImageUrl="../../images/iconos/ico_Eliminar.gif"></asp:ImageButton></P>
																			</ItemTemplate>
																		</asp:TemplateColumn>
																		<asp:BoundColumn DataField="Pos" HeaderText="Pos">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="200px"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="Articulo" HeaderText="Art&#237;culo">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="200px"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="Cantidad" HeaderText="Cantidad">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="200px"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="Util" HeaderText="Util.">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="200px"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="Serie" HeaderText="Serie">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="200px"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="Denominacion" HeaderText="Denominaci&#243;n">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="200px"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="Valor" HeaderText="Valor">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="200px"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="PorDes" HeaderText="Porc.Des.Adic.(%)">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="200px"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="DesAdic" HeaderText="Des.Adic.">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="200px"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="Campana" HeaderText="Campa&#241;a">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="200px"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="PT" HeaderText="P.T.">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="200px"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="NroTelef" HeaderText="N&#250;mero Telf.">
																			<HeaderStyle BorderWidth="1px" BorderColor="#999999" Width="200px"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn Visible="False" DataField="IGV" HeaderText="IGV"></asp:BoundColumn>
																		<asp:BoundColumn Visible="False" DataField="Lista"></asp:BoundColumn>
																		<asp:BoundColumn Visible="False" DataField="Camp"></asp:BoundColumn>
																		<asp:BoundColumn Visible="False" DataField="PlanT"></asp:BoundColumn>
																		<asp:BoundColumn Visible="False" DataField="CCosto"></asp:BoundColumn>
																		<asp:BoundColumn Visible="False" DataField="GRUPO"></asp:BoundColumn>
																		<asp:BoundColumn Visible="False" DataField="strSeries"></asp:BoundColumn>
																	</Columns>
																</asp:datagrid></div>
														</td>
													</tr>
													<tr class="Arial12B" align="center">
														<td align="right">Total:&nbsp;&nbsp;&nbsp;</td>
													</tr>
												</table>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<script language="JavaScript" type="text/javascript">
<!--
   document.getElementById("mped4").style.display = "none";

	var esNavegador, esIExplorer;

	esNavegador = (navigator.appName == "Netscape") ? true : false;
	esIExplorer = ((navigator.appName.indexOf("Microsoft") != -1) || (navigator.appName.indexOf("MSIE") != -1)) ? true : false;

	function e_mayuscula() {
		if ((event.keyCode>96&&event.keyCode<123)||(event.keyCode==241)||(event.keyCode==250)||(event.keyCode==243)||(event.keyCode==237)||(event.keyCode==233)||(event.keyCode==225))
			event.keyCode=event.keyCode-32;
	}
	function e_minuscula(){
		if (event.keyCode>64&&event.keyCode<91)
			event.keyCode=event.keyCode+32;
	}	
	if (esIExplorer) {
		window.document.frmPrincipal.txtCodArti.onkeypress=e_mayuscula;
		window.document.frmPrincipal.txtNumDocumento.onkeypress=e_mayuscula;
	}
	function e_numero() {
	if ( !( (event.keyCode>=48) && (event.keyCode<=57)) && !(event.keyCode==13) )
		event.keyCode=0;
}

f_CambiaClasePed();
 if (document.frmPrincipal.txtCant.value == '')
 {
  document.frmPrincipal.txtCant.value = "1";
  f_CambiaArti();
  document.frmPrincipal.txtNTelf.value = "";
  document.frmPrincipal.txtNTelf.readOnly = false;
  document.getElementById("txtNTelf").className = "clsInputEnable";
 } 
 else 
 {
   if (document.frmPrincipal.txtCant.className=="clsInputDisable")
   {
	 	
		//IDG
		if (document.getElementById("txtIMEIArt") =='')
		{
			 
			document.frmPrincipal.txtIMEIArt.className = "clsInputDisable"
			document.frmPrincipal.txtIMEIArt.value = "";
			document.frmPrincipal.txtIMEIArt.readOnly = true;
		}
		//FDG
		
		document.frmPrincipal.txtNTelf.value = "";
		document.frmPrincipal.txtNTelf.readOnly = true;
		document.getElementById("txtNTelf").className = "clsInputDisable";
		f_CambiaTxtArti();		 
	}	
 }
f_CambiaTipoDocVenta();
f_CambiaTxtArti();		//aqui me recarga y me esta limpiando la serie inicial.

//-->
			</script>
			<%
		' Generacion de hiddens para las validaciones con javascript

'dim objCOM as new SAP_SIC_Ventas.clsVentas
'dim dsAlgo as System.Data.Dataset
dim i as integer
dim strHidden as string 

'dsAlgo = objCOM.Get_GroupArt()

'***VALIDACIÒN DE CHIPS : ***'
'strHidden="<input type=hidden id=hldChips value=;" & chr(34)
'for i = 0 to dsAlgo.tables(0).rows.count - 1
'  strHidden = strHidden & dsAlgo.tables(0).rows(i).Item(0) & ";"
'next
'strHidden=strHidden & chr(34) & ">"
'response.write(strHidden)

'*** VALIDACIÒN DE PACKS ***'
'strHidden="<input type=hidden id=hldPacks value=;" & chr(34)
'for i = 0 to dsAlgo.tables(1).rows.count - 1
'  strHidden = strHidden & dsAlgo.tables(1).rows(i).Item(0) & ";"
'next
'strHidden=strHidden & chr(34) & ">"
'response.write(strHidden)

'*** VALIDACIÒN DE SERVICIOS ***'
'strHidden="<input type=hidden id=hldServ value=;" & chr(34)
'for i = 0 to dsAlgo.tables(2).rows.count - 1
'  strHidden = strHidden & dsAlgo.tables(2).rows(i).Item(0) & ";"
'next
'strHidden=strHidden & chr(34) & ">" 
'response.write(strHidden)

'********************************************************************************'
	'dim constPacks as string =ConfigurationSettings.AppSettings("constServicios")
	'Dim list As String() = constPacks.Split("|")
	'strHidden="<input type=hidden id=hldServ value=;" & chr(34)
	'For i = 0 To list.Length - 1
	'	 strHidden = strHidden & list(i) & ";"
	'Next
	'strHidden=strHidden & chr(34) & ">" 
	'response.write(strHidden)
'********************************************************************************'

'objCOM = nothing

%>
		</form>
		<iframe id="fraAuxiliar1" height="1" width="1" name="fraAuxiliar1"></iframe><iframe id="fraAuxiliar2" height="1" width="1" name="fraAuxiliar2">
		</iframe><iframe id="fraAuxiliar3" height="1" width="1" name="fraAuxiliar3"></iframe>
	</body>
</HTML>
