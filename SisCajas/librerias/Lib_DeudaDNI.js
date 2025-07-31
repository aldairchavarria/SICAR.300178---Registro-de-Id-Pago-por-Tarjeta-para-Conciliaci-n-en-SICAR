var oLista = new Array();
var PAGINA = 1;
var REGISTROS_POR_PAGINA = getRegistrosPagina();
//INICIATIVA-318 INI
var vComboActivo = 1;
//INICIATIVA-318 FIN

$(document).ready(function(){
	initializeObject();
	
	f_datos_POS();
	
	initializeEvent();
});

//Proy - 31949 Inicio
$(document).on('click','#btnCancelar',function(){
	
	limpiar();
	
	var strIndica = $('#hdnFlagIntAutPos').val();
	
	window.location="RecPagoDocId.aspx";
	
	return false;
});
//Proy - 31949 Fin

var CONTROLLER_DOCUMENTOS = {
            addDocument: function (oParamDoc) {
                oLista.push(oParamDoc);
            },
            readDocument : function(){
				showEfectoCarga();
				var cantDoc=0;
                var cadena = '';
                var metodo = 'Listar';
				var dni = $('#txtDNI').val();
                var url = 'RecPagoDocId_Det.aspx';
                var params = $('form :not(#__VIEWSTATE) > :input').serialize();
                params += '&pMetodo=' + metodo;
				params += '&pdni=' + dni; //Atencion
                oLista = []; //Inicializando el Arreglo
                var oMontoTotal = 0;
                $.ajaxSetup({ cache: false });
                $.get(url, params, function (response) {     

					$('#hidResponseDoc').val(response.split('@')); //Proy-31949
					
					var oResponse = response.split('@');
					var oTipo = oResponse[0]; //Tipo de Mensaje Respuesta (C/I)
					//alert(oTipo);
					
					if(oTipo == 'C'){
					
						var result = oResponse[1].split('|');
						var indice = 1;

						for (var i = 0; i < result.length; i++) {

							var registro = result[i].split(';');

							var cuenta = registro[0];
							var razonSocial = registro[1];
							var telf = registro[2];
							var recibo = registro[3];
							var fecEmision = registro[4];
							var fecVencimiento = registro[5];
							var moneda = registro[6];
							var importe = registro[7];
							var grupo = parseInt(registro[8]);
							var detalle = parseInt(registro[9]);
							
							var customer_id = registro[10];
							var serv_doc = registro[11];
							var serv_tipo = registro[12];
									
							cuenta = cuenta == '' ? grupo : cuenta;

							var objDocumento = new ENT_DOCUMENTOS.Documento(indice,
																			cuenta,
																			razonSocial,
																			telf,
																			recibo,
																			fecEmision,
																			fecVencimiento,
																			moneda,
																			importe,
																			grupo,
																			detalle,
																			customer_id,
																			serv_doc,
																			serv_tipo
																		   );
							CONTROLLER_DOCUMENTOS.addDocument(objDocumento);
							indice++;

							if(detalle==1){
								cantDoc++;
							}else{
								oMontoTotal= parseFloat(oMontoTotal) + parseFloat(importe); 
							}
							removeEfectoCarga();
						}
					}
					else{
						var oMensaje = oTipo.split(';');
						$('#txtCantDoc').val('');
						$('#txtMontoTotalDni').val('');
						$('#tblCuentas tbody tr').next().remove();
						$('#lblTotPaginas').html('Pagina 0 de 0');
						alert(oMensaje[1]);
						removeEfectoCarga();
						return;
					}
					
					//Proy-31949 -- Inicio
					//$('#txtCantDoc').val(cantDoc); 
					$('#txtCantDoc').val(0); 
					//oMontoTotal = parseFloat(oMontoTotal).toFixed(2);
					oMontoTotal = parseFloat(0).toFixed(2);
					$('#txtMontoTotalDni').val(oMontoTotal);
					//Proy-31949 -- Fin
					
                    CONTROLLER_DOCUMENTOS.listDocument(1);

                });
            },
            listDocument: function (attribopen) {
                $('#tblCuentas tbody tr').next().remove();
                var total = oLista.length;
                var currentPage = PAGINA;
                var pageSize = REGISTROS_POR_PAGINA;
                //var totalPage = Math.ceil(parseFloat(total) / parseFloat(pageSize));
                var totalPage = Math.ceil(parseFloat(CONTROLLER_DOCUMENTOS.totalDocument(0)) / parseFloat(pageSize));
                var inicioPagina = (currentPage-1) * pageSize;
                var hastaPagina = parseInt(inicioPagina) + parseInt(pageSize);
				var anadio = 0;
				var contadorPadre = 0;
				var oArrayGrupo = new Array();
				var contadorHijos = 0;
        
				if(total>0){
					$('#tblCuentas tbody tr').next().remove();
					var cadena = '';
					for (var i = 0; i < total; i++) {
					    var index = i;
					    if (inicioPagina == 0) {
					        var detalle = parseInt(oLista[i].detalle);
					        var cuenta = oLista[i].cuenta == '' ? oLista[i].grupo : oLista[i].cuenta;
					        var totalHijosCuenta = CONTROLLER_DOCUMENTOS.totalDetalleCuenta(cuenta);
					        if (detalle == 0) {
					            contadorHijos = 0;
					        }
					        if (index < hastaPagina || anadio < hastaPagina) {

					            var razonSocial = oLista[i].razonSocial;
					            var telf = oLista[i].telf;
					            var recibo = oLista[i].recibo;
					            var fecEmision = oLista[i].fecEmision;
					            var fecVencimiento = oLista[i].fecVencimiento;
					            var moneda = oLista[i].moneda;
					            var importe = oLista[i].importe;
					            var grupo = parseInt(oLista[i].grupo);

					           	document.getElementById('hidNomCliente').value = razonSocial; //27440
                              

					            cadena += dameRegistro(i, cuenta, razonSocial, telf, recibo, fecEmision, fecVencimiento, moneda, importe, grupo, detalle, attribopen);

					            if (detalle == 0) {
					                anadio++;
					                contadorHijos = 0;
					                oArrayGrupo.push(grupo); //Value Init Acordeon
					            }
					            else
					                contadorHijos++;

					        }
					        else {

					            if (anadio == hastaPagina && detalle == 1) {

					                var detalle = parseInt(oLista[i].detalle);
					                var cuenta = oLista[i].cuenta == '' ? oLista[i].grupo : oLista[i].cuenta;
					                var razonSocial = oLista[i].razonSocial;
					                var telf = oLista[i].telf;
					                var recibo = oLista[i].recibo;
					                var fecEmision = oLista[i].fecEmision;
					                var fecVencimiento = oLista[i].fecVencimiento;
					                var moneda = oLista[i].moneda;
					                var importe = oLista[i].importe;
					                var grupo = parseInt(oLista[i].grupo);

                                    document.getElementById('hidNomCliente').value = razonSocial; //27440
                                    
					                cadena += dameRegistro(i, cuenta, razonSocial, telf, recibo, fecEmision, fecVencimiento, moneda, importe, grupo, detalle, attribopen);
									contadorHijos++;
					            }
					            
					            if (contadorHijos == totalHijosCuenta)
									break;
					            
					        }
					    } else {
					        var detalle = parseInt(oLista[i].detalle);
					        if (detalle == 0) {
					            contadorPadre++;
					        }
					        if ((contadorPadre > inicioPagina) && (contadorPadre <= hastaPagina)) {

					            var cuenta = oLista[i].cuenta == '' ? oLista[i].grupo : oLista[i].cuenta;
					            var razonSocial = oLista[i].razonSocial;
					            var telf = oLista[i].telf;
					            var recibo = oLista[i].recibo;
					            var fecEmision = oLista[i].fecEmision;
					            var fecVencimiento = oLista[i].fecVencimiento;
					            var moneda = oLista[i].moneda;
					            var importe = oLista[i].importe;
					            var grupo = parseInt(oLista[i].grupo);

                                document.getElementById('hidNomCliente').value = razonSocial; //27440
                                
					            cadena += dameRegistro(i, cuenta, razonSocial, telf, recibo, fecEmision, fecVencimiento, moneda, importe, grupo, detalle, attribopen);

								//Value Init Acordeon
							    if (detalle == 0) {
									oArrayGrupo.push(grupo);
								}
                        
					        }
					    }

					}
					$('#tblCuentas tbody').append(cadena);
					$('#lblTotPaginas').html('Pagina 1 de ' + totalPage);

					if (attribopen != undefined && attribopen != null) {
						for (var i = 0; i < oArrayGrupo.length; i++) {
							var atribAcordeon = oArrayGrupo[i];
							acordeonTablaOcultaDetalle(atribAcordeon);
						}
					}

                }else{
					alert('No se encuentran documentos');
					return;
                }
                
            },

            totalDocument: function (oDetalle) {
                var total = oLista.length;
                var retorno = 0;
                for (var i = 0; i < total; i++) {
                    if (oDetalle != undefined && oDetalle != null) {
                        var detalleCuenta = parseInt(oLista[i].detalle);
                        if (detalleCuenta == oDetalle) {
                            retorno++;
                        }
                    } else {
                        retorno++;
                    }
                }
                return retorno;
            },

            totalDetalleCuenta: function (oCuenta) {
                var total = oLista.length;
                var retorno = 0;
                for (var i = 0; i < total; i++) {
                    var detalleCuenta = parseInt(oLista[i].detalle);
                    if (detalleCuenta == 1) {
                        var cuenta = oLista[i].cuenta;
                        if (cuenta == oCuenta) {
                            retorno++;
                        }
                    }
                }
                return retorno;
            }

        }

        var ENT_DOCUMENTOS = {
            Documento: function (indice, cuenta, razonSocial, telf, recibo, fecEmision, 
							     fecVencimiento, moneda, importe, grupo, detalle, customer_id, serv_doc, serv_tipo) {
                this.indice = indice;
                this.cuenta = cuenta;
                this.razonSocial = razonSocial;
                this.telf = telf;
                this.recibo = recibo;
                this.fecEmision = fecEmision;
                this.fecVencimiento = fecVencimiento;
                this.moneda = moneda;
                this.importe = importe == '' ? 0 : importe;
                this.grupo = grupo;
                this.detalle = detalle;
                this.customer_id = customer_id;
                this.serv_doc = serv_doc;
                this.serv_tipo = serv_tipo;
            }
        }

function dameRegistro(i, cuenta, razonSocial, telf, recibo, fecEmision, fecVencimiento, moneda, importe, grupo, detalle) {

            var cadena = '';
            cuenta == '' ? i : cuenta;
            var attrib = grupo;
            var claseFila = ' RowEven';
            var claseTD = 'td';
            var radio = "";
            var fila = 'fila';
            var img = '';
            var style = '';

            if (i % 2 == 0)
                claseFila = ' RowOdd';

            if (detalle == 1) {
                fila = fila + grupo;
                claseTD = claseTD + grupo;
                //cadena += "<tr id='tr" + (i + 1) + "' class='" + fila + claseFila + "'  >";
				cadena += "<tr class='" + fila + claseFila + "'  >";   
				style = " &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; \u2514\u2500\u2500" ;
				cuenta = style + cuenta;
            } else {
                radio = "<input type='checkbox' id = " + grupo + " class='radios' attrMoneda=" + moneda + " attrDeuda="+ importe +" attrCuenta=" + (cuenta == '' ? grupo : cuenta) + " />"; //Proy-31949
                //cadena += "<tr id='tr" + (i + 1) + "' class='" + fila + claseFila + "' attrib='" + grupo + "' attribopen='0' >";
                cadena += "<tr id='tr" + grupo + "' class='" + fila + claseFila + "' attrib='" + grupo + "' attribopen='0' >";
                img = "<img id='img" + grupo + "' class='img' attrib='" + grupo + "'  src='../images/ic_plus.png' height='15' width='15' style='margin-top:-13px; float:right; cursor:pointer;' />";
				style = "&nbsp;&nbsp; ";
				cuenta = style + '<b>' + cuenta + '</b>' + img;
				razonSocial = '<b>' + razonSocial + '</b>';
				telf = '<b>' + telf + '</b>';
				moneda = '<b>' + moneda + '</b>';
				importe = '<b>' + importe + '</b>';
            }			
			
            cadena += "<td class='" + claseTD + "' style='border: solid 1px black;'>" + radio + "</td>";
            cadena += "<td class='" + claseTD + "' style='border: solid 1px black; text-align:left;'>" + cuenta +"</td>";
            cadena += "<td class='" + claseTD + "' style='border: solid 1px black;'>" + razonSocial + "</td>";
            cadena += "<td class='" + claseTD + "' style='border: solid 1px black;'>" + telf + "</td>";
            cadena += "<td class='" + claseTD + "' style='border: solid 1px black;'>" + recibo + "</td>";
            cadena += "<td class='" + claseTD + "' style='border: solid 1px black;'>" + fecEmision + "</td>";
            cadena += "<td class='" + claseTD + "' style='border: solid 1px black;'>" + fecVencimiento + "</td>";
            cadena += "<td class='" + claseTD + "' style='border: solid 1px black;'>" + moneda + "</td>";
            cadena += "<td class='" + claseTD + "' style='border: solid 1px black;'>" + importe + "</td>";
            
            cadena += "</tr>"

            return cadena;
        }

function getRegistrosPagina() {
    var retorno = $('#cboPaginado').val();
    return retorno;
}

function acordeonTablaOcultaDetalle(attrib) {
    var attribopen = $('#tr' + attrib).attr('attribopen');
    $('.td' + attrib).css('border', 'solid 0px black');
    attribopen = '1';
    $('.fila' + attrib).hide();
    $('#tr' + attrib).attr('attribopen', attribopen);
}

function showEfectoCarga(){
	$('#divEfecto').removeClass();
	$('#divEfecto').addClass('modal-backdrop fade in');
	$('#spanImage').show();
}
function removeEfectoCarga(){
	$('#divEfecto').removeClass();
	$('#spanImage').hide();
}

function limpiar(){
	for(var i =1; i<4; i++){
		$('#txtDoc'+i).val('');
		$('#txtMonto'+i).val('');
		$('#cboTipDocumento'+i).val('');
	}
    $('#cboTipDocumento1').val('ZEFE');
    $('.radios').prop('checked', false);
    $('#txtRecibidoPen').val('');
    $('#txtRecibidoUsd').val('0.00');
    $('#txtVuelto').val('');
    document.getElementById("tdVuelto").innerText = "Vuelto:";
    enableFormasPago();
    
    //Proy - 31949 - Inicio
    $('#txtCantDoc').val(0);
    $('#txtMontoTotalDni').val('0.00');
    $('#LnkPos1').fadeOut('fast');
    $('#LnkPrintPos1').fadeOut('fast');
    $('#LnkDelPos1').fadeOut('fast');
    $('#LnkPos2').fadeOut('fast');
    $('#LnkPrintPos2').fadeOut('fast');
    $('#LnkDelPos2').fadeOut('fast');
    $('#LnkPos3').fadeOut('fast');
    $('#LnkPrintPos3').fadeOut('fast');
    $('#LnkDelPos3').fadeOut('fast');
    //Proy - 31949 - Fin
    
	vComboActivo = 1; //INICIATIVA-318
}

function setWidth(){
	var el_width = $('#tblCuentas').width();
	$('#tblBotones').width(el_width);
}

function dameFecha(fecha){
            var anio = String(fecha).substr(6, 4);
            var mes = String(fecha).substr(3, 2);
            var dia = String(fecha).substr(0, 2);
            var retorno = anio + mes + dia;
            return retorno;
}

function seteaDetalle(cuenta, moneda) {
            var total = oLista.length;
            var cadena = '';
            if (total > 0) {
                for (var i = 0; i < total; i++) {
                    var oDocumento = oLista[i];
                    if(oDocumento.detalle == 1){ //Que solo busque en el detalle
                           if (oDocumento.cuenta == cuenta && oDocumento.moneda == moneda) {
                            var miMoneda = oDocumento.moneda;
                            var miImporte = oDocumento.importe;
                             if (miMoneda == 'USD') {
                                //var tipoCambio = $('#HDTipoCambio').val();
                                var tipoCambio = document.getElementById('HDTipoCambio').value;
                                miImporte = miImporte * parseFloat(tipoCambio);
                            }
                            if(cadena == '')
                                cadena += oDocumento.serv_doc + ';Telefonia celul;604;'+ oDocumento.serv_doc + ';' + 
                                          oDocumento.recibo + ';' + 
                                          dameFecha(oDocumento.fecEmision) + ';' + 
                                          dameFecha(oDocumento.fecVencimiento) + ';' +
                                          miImporte + ';' +
                                          oDocumento.customer_id + ';' +
                                          oDocumento.serv_tipo + ';' +
                                          oDocumento.telf;
                            else
                                cadena += ',' + oDocumento.serv_doc + ';Telefonia celul;604;'+ oDocumento.serv_doc +';' +
                                            oDocumento.recibo + ';' +
                                            dameFecha(oDocumento.fecEmision) + ';' +
                                            dameFecha(oDocumento.fecVencimiento) + ';' +
                                            miImporte + ';' +
                                            oDocumento.customer_id + ';' +
                                            oDocumento.serv_tipo + ';' +
											oDocumento.telf;
                        }
                    }
                }
            }
            return cadena;
}
   
function validateForm(i){
	var combo = $('#cboTipDocumento' + i).val();
	var nro = $('#txtDoc' + i).val();
	var monto = $('#txtMonto' + i).val();
	if(combo != 'ZEFE')
	{
		if(combo != ''){
		
			if(combo == 'ZCHQ'){ //Cheque
				if(nro == ''){
					alert('Debe ingresar el numero de cheque'); 
					$('#txtDoc' + i).focus();		
					return false;
				}
			}else{ //Tarjetas credito
				if(nro == ''){
					alert('Debe ingresar el numero de tarjeta');
														
					 //Proy 27440
					 if ( document.getElementById('HidIntAutPos').value != '1')
				     {
					$('#txtDoc' + i).focus();		
				     }	
				     else
				     {
				       $('#txtMonto' + i).focus();	
				     }	
					 //Proy 27440
					
										
					return false;
				}
			}

			if(monto == ''){
					alert('Debe ingresar el monto');
					$('#txtMonto' + i).focus();		
					return false;
			}else if(parseFloat(monto) == 0){
					alert('El monto no puede ser cero');
					$('#txtMonto' + i).focus();		
					return false;
			}
			
		}
		
	}else{
	
		if(monto == ''){
			alert('Debe ingresar el monto');
			$('#txtMonto' + i).focus();		
			return false;
		}else if(parseFloat(monto) == 0){
			alert('El monto no puede ser cero');
			$('#txtMonto' + i).focus();		
			return false;
		}
		
	}
	return true;
}

function validaFormaPago(){
	var retorno = true;
     for (var i = 1; i < 4; i++) {
		retorno = validateForm(i);
		if(!retorno)
			break;
	}
	if(retorno && validaFaltante() && f_ValidarTarjeta()){
		//INC000001566216-INICIO
		if(confirm("¿Está seguro de registrar el pago?")){
			document.getElementById("divBotones").style.display ="none";
			document.getElementById("divTitulo").style.display ="block";
		}else
			event.returnValue = false;	
		//INC000001566216-FIN
	}else{
		retorno = false;
	}
	return retorno;
}

function validaFaltante(){
	var faltante = $('#txtVuelto').val();
	if(faltante != "" && faltante != null){
		var valor = parseFloat(faltante);
		if(valor<0){
			alert('El importe recibido no puede ser menor al importe pagado');
			return false;
		}
	}
	return true;
}
		
function enableFormasPago(){
	$('#txtDoc1').attr('disabled', true);
	$('#txtDoc2').attr('disabled', true);
	$('#txtMonto2').attr('disabled', true);
	$('#txtDoc3').attr('disabled', true);
	$('#txtMonto3').attr('disabled', true);
}		
		
function limpiarMonto(){
	$('#txtMonto1').val('');
	$('#txtMonto2').val('');
	$('#txtMonto3').val('');
}		

function dameRedondeoMonto(documento, text) {
    var retorno = text;
    if (documento == 'ZEFE') 
    {
        var parteEntera = Math.floor(text).toFixed(2);
        var parteDecimal = (text - parteEntera).toFixed(2);
        parteDecimal = parseFloat(parteDecimal);
        if (parteDecimal > 0) 
        {
            var separador = '.';
                var indexEntero = String(text).indexOf(separador) + 1;
                var restoDecimal = text.substr(indexEntero);
                var primerdecimal = '';
                var segundoDecimal = '';
                switch (restoDecimal.length) 
                {
                    case 1:
                        primerdecimal = restoDecimal.substr(0, 1);
                        parteEntera = parseInt(parteEntera) + separador + (primerdecimal);
                        parteEntera = parseFloat(parteEntera).toFixed(2);
                        break;
                    case 2:
                        primerdecimal = restoDecimal.substr(0, 1);
                        segundoDecimal = restoDecimal.substr(1, 1);
                        if (parseInt(segundoDecimal) >= 1) 
                        {
                            var miDecimal = primerdecimal;
                            parteEntera = parseInt(parteEntera) + separador + (miDecimal);
                        }
                        parteEntera = parseFloat(parteEntera).toFixed(2);
                        break;
                
            }
        }
        retorno = parteEntera;
    }
    return retorno;
}
						     
function initializeObject(){
    document.getElementById('cboPaginado').selectedIndex = 0;
	setWidth();
    //Listar();
    PAGINA = 1;
    REGISTROS_POR_PAGINA = getRegistrosPagina();
    CONTROLLER_DOCUMENTOS.readDocument();    
    enableFormasPago();
}

function initializeEvent() {

	$('#cboTipDocumento1').change(function(){		
		vComboActivo = 1; //INICIATIVA-318
		var combo = $(this).val();
		if(combo == '') //Vacio
		{ 
			vComboActivo = 0; //INICIATIVA-318
			$('#txtDoc1').attr('disabled', true);
			$('#txtMonto1').val('');
			$('#txtDoc1').val('');
			$('#txtMonto1').attr('disabled', true);			
		}
		else if(combo != 'ZEFE') //Tarjetas
		{
		    //Proy 27440
		    if ( document.getElementById('HidIntAutPos').value != '1')
		    {
			$('#txtDoc1').attr('disabled', false);		
			$('#txtDoc1').focus();
		    }
		    else 
		    {
		       $('#txtDoc1').attr('disabled', true);
		       //$('#txtMonto1').focus();
		    }
		    //Proy 27440
					
			$('#txtMonto1').attr('disabled', false);	
			
		}else if(combo == 'ZEFE') //Efectivo
		{ 
			$('#txtDoc1').val('');
			$('#txtDoc1').attr('disabled', true);	
			$('#txtMonto1').attr('disabled', false);	
			$('#txtMonto1').focus();		
		}
		 RedondearEfectivo('txtMonto1','cboTipDocumento1');
		f_Recalcular(this);
	});	
	$('#cboTipDocumento2').change(function(){		
		vComboActivo = 2; //INICIATIVA-318	
		var combo = $(this).val();
		if(combo == '') //Vacio
		{ 
			vComboActivo = 0; //INICIATIVA-318
			$('#txtDoc2').attr('disabled', true);
			$('#txtMonto2').val('');
			$('#txtDoc2').val('');
			$('#txtMonto2').attr('disabled', true);			
		}
		else if(combo != 'ZEFE') //Tarjetas
		{
		    //27440
		    if ( document.getElementById('HidIntAutPos').value != '1')
		    {
			$('#txtDoc2').attr('disabled', false);		
			$('#txtDoc2').focus();
		    }
		    else 
		    {
		       $('#txtDoc2').attr('disabled', true);
		      // $('#txtMonto2').focus();
		    }
		    //27440
		
					
			$('#txtMonto2').attr('disabled', false);	
			
		}else if(combo == 'ZEFE') //Efectivo
		{ 
			$('#txtDoc2').val('');
			$('#txtDoc2').attr('disabled', true);	
			$('#txtMonto2').attr('disabled', false);	
			$('#txtMonto2').focus();		
		}
		 RedondearEfectivo('txtMonto2','cboTipDocumento2');
		f_Recalcular(this);
	});
	$('#cboTipDocumento3').change(function(){		
		vComboActivo = 3; //INICIATIVA-318
		var combo = $(this).val();
		if(combo == '') //Vacio
		{ 
			vComboActivo = 0; //INICIATIVA-318
			$('#txtDoc3').attr('disabled', true);
			$('#txtMonto3').val('');
			$('#txtDoc3').val('');
			$('#txtMonto3').attr('disabled', true);			
		}
		else if(combo != 'ZEFE') //Tarjetas
		{
		    //27440
		    if ( document.getElementById('HidIntAutPos').value != '1')
		    {
			$('#txtDoc3').attr('disabled', false);		
			$('#txtDoc3').focus();
		    }
		    else 
		    {
		       $('#txtDoc3').attr('disabled', true);
		      // $('#txtMonto3').focus();
		    }
		    //27440
				
			$('#txtMonto3').attr('disabled', false);	
			
		}else if(combo == 'ZEFE') //Efectivo
		{ 
			$('#txtDoc3').val('');
			$('#txtDoc3').attr('disabled', true);	
			$('#txtMonto3').attr('disabled', false);	
			$('#txtMonto3').focus();		
		}
		 RedondearEfectivo('txtMonto3','cboTipDocumento3');
		f_Recalcular(this);
	});
	
    $('#btnLimpiar').click(function () {
		limpiar();
        return false;
    });

    $('#btnGrabar').click(function () {
		blnClosingMod = false;
        var countCheck = $('.radios:checked').length;
        if (countCheck == 0) {
            alert('Debe seleccionar una cuenta');
            return false;
        }
        
        //Proy-31949 -- Inicio
        var decMonto=0;
        var decMontoTotal=parseFloat($('#txtMontoTotalDni').val());
        
        for (var i = 1; i < 4; i++) {
			if($('#txtMonto' + i).val()!=''){
				decMonto = decMonto + parseFloat($('#txtMonto' + i).val());
			}
		}
		
		if(decMonto>decMontoTotal){
			alert($('#hdnMsgMayor').val());
			return false;
		}
		//INICIATIVA - 529 INI
		/*if(decMonto<decMontoTotal){  
			alert($('#hdnMsgMenor').val());
			return false;
		}*/
		//INICIATIVA - 529 FIN
        
        //Proy-31949 -- Fin
        
        return validaFormaPago();
    });

    $("body").on("change", ".radios", function () {
        //$('.radios').prop('checked', false); Proy-31949
        //$(this).prop('checked', true); Proy-31949
        var deudaTotal = $(this).attr('attrDeuda');
        var cuenta = $(this).attr('attrCuenta');
        var moneda = $(this).attr('attrMoneda');
        var detalle = seteaDetalle(cuenta, moneda);
        var documento = $('#cboTipDocumento1').val();
		
		//Proy-31949 -- Inicio
		var strResponse = $('#hidResponseDoc').val();
		var strRecibos = $('#hidRecibos').val();
		var arrRows;
		var arrDetalle;
		var arrOperacion;
		var decImporte=0;
		var strMoneda;
		var intContadorHijo=0;
		var blnIndica=$(this).prop('checked');
		var decInCantidad=parseFloat($('#txtCantDoc').val());
		var decInImporte=parseFloat($('#txtMontoTotalDni').val());
		
		if(strResponse!='' && strResponse!=null){
			arrRows=strResponse.split('|');
			
			for(var row=0;row<arrRows.length;row++){
				arrDetalle = arrRows[row].split(';');
				arrOperacion = arrDetalle[0].split(',');
				strMoneda = arrDetalle[6];
				
				if(arrDetalle[12]!='M'){
					if(arrOperacion.length>1){
						if(arrOperacion[1]==cuenta){							
							if (strMoneda != 'PEN') {
								var tipoCambio = $('#hdTipoCambio').val();
								if (tipoCambio > 0) {
									decImporte = parseFloat(arrDetalle[7]) * parseFloat(tipoCambio);
								}
							}else{
								decImporte=parseFloat(arrDetalle[7]);
							}
						}
					}else{
						if(arrOperacion[0]==cuenta){
							if (strMoneda != 'PEN') {
            var tipoCambio = $('#hdTipoCambio').val();
            if (tipoCambio > 0) {
									decImporte = parseFloat(arrDetalle[7]) * parseFloat(tipoCambio);
								}
							}else{
								decImporte=parseFloat(arrDetalle[7]);
							}
						}
					}				
				}else{
					if(arrOperacion[0]==cuenta){
						intContadorHijo=parseFloat(intContadorHijo)+1;
            }
        }
			}
			
			//decImporte = parseFloat(dameRedondeoMonto(documento, decImporte.toString())); //INICIATIVA-318
			
			//INICIATIVA-318 INI
			if(vComboActivo==1){
				decImporte = parseFloat(dameRedondeoMonto($('#cboTipDocumento1').val(), decImporte.toString()));
			}
			if(vComboActivo==2){
				decImporte = parseFloat(dameRedondeoMonto($('#cboTipDocumento2').val(), decImporte.toString()));
			}
			if(vComboActivo==3){
				decImporte = parseFloat(dameRedondeoMonto($('#cboTipDocumento3').val(), decImporte.toString()));
			}
			//INICIATIVA-318 FIN
			
			if(blnIndica==true){
				decInCantidad=decInCantidad+intContadorHijo;
				decInImporte=decInImporte+decImporte;
				strRecibos = strRecibos + ',' + detalle;
			}
			else{
				decInCantidad=decInCantidad-intContadorHijo;
				decInImporte=decInImporte-decImporte;
				strRecibos = strRecibos.replace(',' + detalle,'');
            }
			
			//INICIATIVA-318 INI
			if(vComboActivo==1){
				if($('#cboTipDocumento2').val()!= '' || $('#cboTipDocumento3').val()!= ''){
					$('#txtMonto1').val(decImporte.toFixed(2));
				}else{
					decInImporte = parseFloat(dameRedondeoMonto($('#cboTipDocumento1').val(), decInImporte.toString()));
					$('#txtMonto1').val(decInImporte.toFixed(2));
				}
			}
			if(vComboActivo==2){
				if($('#cboTipDocumento1').val()!= '' || $('#cboTipDocumento3').val()!= ''){
					$('#txtMonto2').val(decImporte.toFixed(2));
				}else{
					decInImporte = parseFloat(dameRedondeoMonto($('#cboTipDocumento2').val(), decInImporte.toString()));
					$('#txtMonto2').val(decInImporte.toFixed(2));
				}
			}
			if(vComboActivo==3){
				if($('#cboTipDocumento1').val()!= '' || $('#cboTipDocumento2').val()!= ''){
					$('#txtMonto3').val(decImporte.toFixed(2));
				}else{
					decInImporte = parseFloat(dameRedondeoMonto($('#cboTipDocumento3').val(), decInImporte.toString()));
					$('#txtMonto3').val(decInImporte.toFixed(2));
				}
			}
			//INICIATIVA-318 FIN		
			
			decInImporte=decInImporte.toFixed(2);
			
			$('#txtCantDoc').val(decInCantidad);
			$('#txtMontoTotalDni').val(decInImporte);
        }
		
		$('#hidRecibos').val(strRecibos);   //Fill Detalle Documentos
		
		//deudaTotal = dameRedondeoMonto(documento, deudaTotal);
		//if (moneda != 'PEN') {
        //    var tipoCambio = $('#hdTipoCambio').val();
        //    if (tipoCambio > 0) {
        //        deudaTotal = parseFloat(deudaTotal) * parseFloat(tipoCambio);
        //    }
        //}
		
        //deudaTotal = parseFloat(deudaTotal).toFixed(2);
        //$('#txtMonto1').val(deudaTotal);//Proy-27440
        if(documento == 'ZEFE' || $('#cboTipDocumento2').val() == 'ZEFE' || $('#cboTipDocumento3').val() == 'ZEFE') //INICIATIVA-318
			//$('#txtRecibidoPen').val(deudaTotal);
			$('#txtRecibidoPen').val(decInImporte);
        //$('#hdDeudaPorCuenta').val(deudaTotal);
        $('#hdDeudaPorCuenta').val(decInImporte);
        $('#txtVuelto').val('');
        //document.getElementById("tdVuelto").innerText = "Vuelto:";
        f_Recalcular();  
        //$('#txtMonto1').focus(); //INICIATIVA-318
		
		//Proy-31949 -- Fin
    });

    $("body").on("click", ".img", function () {
        var attrib = $(this).attr('attrib');
        var attribopen = $('#tr' + attrib).attr('attribopen');
        if (attribopen == '0') { //Cerrado
            $('.td' + attrib).css('border', 'solid 0px black');
            $('#img' + attrib).attr('src', '../images/ic_plus.png');
            attribopen = '1';
        } else { //Abierto
            $('.td' + attrib).css('border', 'solid 1px black');
            $('#img' + attrib).attr('src', '../images/ic_minus.png');
            attribopen = '0';
        }
        $('.fila' + attrib).slideToggle('fast');
        $('#tr' + attrib).attr('attribopen', attribopen);
    });

    $('#cmdBuscar').click(function () {    
        var text = $('#txtDNI').val();
        text = $.trim(text);
        if(text.length == 0)
        {
			alert('El Nro de DNI no puede estar vacío o nulo.');
			return false;
        }else if(text.length != 8)
        {
			alert('El Nro de DNI debe tener 8 digitos');
			return false;
        }      
        PAGINA = 1;
        REGISTROS_POR_PAGINA = getRegistrosPagina();
        limpiar();
        CONTROLLER_DOCUMENTOS.readDocument();
        return false;
    });
    $('#btnPrimero').click(function () {
        PAGINA = 1;
        REGISTROS_POR_PAGINA = getRegistrosPagina();
        CONTROLLER_DOCUMENTOS.listDocument(1);

        //var total = oLista.length;
        var total = CONTROLLER_DOCUMENTOS.totalDocument(0);
        var pageSize = REGISTROS_POR_PAGINA;
        var totalPage = Math.ceil(parseFloat(total) / parseFloat(pageSize));
        $('#lblTotPaginas').html('Pagina ' + PAGINA + ' de ' + totalPage);
    });
    $('#btnAntes').click(function () {
        if (PAGINA > 1) {
            PAGINA = PAGINA - 1;
        }
        REGISTROS_POR_PAGINA = getRegistrosPagina();
        CONTROLLER_DOCUMENTOS.listDocument(1);

        //var total = oLista.length;
        var total = CONTROLLER_DOCUMENTOS.totalDocument(0);
        var pageSize = REGISTROS_POR_PAGINA;
        var totalPage = Math.ceil(parseFloat(total) / parseFloat(pageSize));
        $('#lblTotPaginas').html('Pagina ' + PAGINA + ' de ' + totalPage);
    });
    $('#btnSiguiente').click(function () {
        //var total = oLista.length;
        var total = CONTROLLER_DOCUMENTOS.totalDocument(0);
        REGISTROS_POR_PAGINA = getRegistrosPagina();
        var pageSize = REGISTROS_POR_PAGINA;
        var totalPage = Math.ceil(parseFloat(total) / parseFloat(pageSize));
        if (PAGINA < totalPage) {
            PAGINA = PAGINA + 1;
        }
        CONTROLLER_DOCUMENTOS.listDocument(1);

        $('#lblTotPaginas').html('Pagina ' + PAGINA + ' de ' + totalPage);
    });
    $('#btnUltimo').click(function () {
        //var total = oLista.length;
        var total = CONTROLLER_DOCUMENTOS.totalDocument(0);
        REGISTROS_POR_PAGINA = getRegistrosPagina();
        var pageSize = REGISTROS_POR_PAGINA;
        var totalPage = Math.ceil(parseFloat(total) / parseFloat(pageSize));
        PAGINA = totalPage;
        CONTROLLER_DOCUMENTOS.listDocument(1);

        $('#lblTotPaginas').html('Pagina ' + PAGINA + ' de ' + totalPage);
    });
    $('#cboPaginado').change(function () {
        REGISTROS_POR_PAGINA = getRegistrosPagina();
        CONTROLLER_DOCUMENTOS.listDocument(1);
        var total = CONTROLLER_DOCUMENTOS.totalDocument(0);
        //var total = oLista.length;
        var pageSize = REGISTROS_POR_PAGINA;
        var totalPage = Math.ceil(parseFloat(total) / parseFloat(pageSize));
        $('#lblTotPaginas').html('Pagina ' + PAGINA + ' de ' + totalPage);
    });

    $("#tblCuentas tbody").on("mouseover", "tr", function () {
        $(this).css('background-color', '#f5f5f5');
    });

    $("#tblCuentas tbody").on("mouseout", "tr", function () {
        $(this).css('background-color', '');
    });
	
	$('#txtDNI').on("paste", function (e) {
		var element = this;
		setTimeout(function () {
			var text = $(element).val();
			text = $.trim(text);
			var lonText = text.length;
			var cadena = '';
			if (lonText > 0) {
				for (var i = 0; i < lonText; i++) {
					var num = String(text).substr(i, 1);
					num = $.trim(num);
					if (num != '') {
						var esNumero = isNaN(num);
						if (!esNumero) {
							cadena += num;
						}
					}
				}
			}
			$('#txtDNI').val(cadena);
		}, 100);
	 });	 
	$('#txtMonto1').keyup(function () {
        RedondearEfectivo('txtMonto1','cboTipDocumento1');
    });
	$('#txtMonto2').keyup(function () {
		RedondearEfectivo('txtMonto2','cboTipDocumento2');
    });
    $('#txtMonto3').keyup(function () {
		RedondearEfectivo('txtMonto3','cboTipDocumento3');
    });	 
    $('#txtMonto1, #txtMonto2, #txtMonto3').on('blur', function () {
        f_Recalcular(); 
    });
    
    //INICIATIVA-318 INI
    $('#txtMonto1').click(function () {
        vComboActivo = 1;
    });
    $('#txtMonto2').click(function () {
        vComboActivo = 2;
    });
    $('#txtMonto3').click(function () {
        vComboActivo = 3;
    });
    //INICIATIVA-318 FIN
}