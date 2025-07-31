function f_data_IpCaja(objCaja){
	var soapMSG ='<soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/" xmlns:prin="http://principal.wsServer.claro.org/" xmlns:pet="http://com/tecnocom/PeticionInformacionCaja">' +
	'<soapenv:Header/>' +
	'<soapenv:Body>' +
	'<prin:peticionInformacionCaja>' +
	'<pet:AuditRequest>' +
	'<pet:IdTransaccion>' + objCaja.IdTransaccion + '</pet:IdTransaccion>' +
	'<pet:IpApplicacion>' + objCaja.IpApplicacion + '</pet:IpApplicacion>' +
	'<pet:NombreAplicacion>' + objCaja.NombreAplicacion + '</pet:NombreAplicacion>' +
	'<pet:UsuarioAplicacion>' + objCaja.UsuarioAplicacion + '</pet:UsuarioAplicacion>' +
	'</pet:AuditRequest>' +
	'<pet:TipoOperacion>' + objCaja.TipoOperacion + '</pet:TipoOperacion>' +
	'</prin:peticionInformacionCaja>' +
	'</soapenv:Body>' +
	'</soapenv:Envelope>';
	
	return soapMSG;		
}


function f_data_VisaNet(objOperaVisaNet) {
  var soapMSG = '<soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/" xmlns:prin="http://principal.wsServer.claro.org/" xmlns:pet="http://com/tecnocom/PeticionOperacionVisaNet">' +
		'<soapenv:Header/> ' +
		'<soapenv:Body>' + '<prin:peticionOperacionVisaNet>' +
		'<pet:AuditRequest>' + 
			'<pet:IdTransaccion>' + objOperaVisaNet.IdTransaccion + '</pet:IdTransaccion>' +
			'<pet:IpApplicacion>' + objOperaVisaNet.IpApplicacion + '</pet:IpApplicacion>' +
			'<pet:NombreAplicacion>' + objOperaVisaNet.NombreAplicacion + '</pet:NombreAplicacion>' +
			'<pet:UsuarioAplicacion>' + objOperaVisaNet.UsuarioAplicacion + '</pet:UsuarioAplicacion>' +
		'</pet:AuditRequest>' + 		
		'<pet:TipoOperacion>' + objOperaVisaNet.TipoOperacion + '</pet:TipoOperacion>' +            
		'<pet:Transaccion>' +
			'<pet:TipoMoneda>' + objOperaVisaNet.TipoMoneda + '</pet:TipoMoneda>' +
			'<pet:Monto>' + objOperaVisaNet.Monto + '</pet:Monto>' +
			'<pet:CodigoTienda>' + objOperaVisaNet.CodigoTienda + '</pet:CodigoTienda>' +
			'<pet:CodigoCaja>' + objOperaVisaNet.CodigoCaja + '</pet:CodigoCaja>' +
		'</pet:Transaccion>' +
    '<pet:PosServicio>' +
			'<pet:Empresa>' + objOperaVisaNet.Empresa + '</pet:Empresa>' +
			'<pet:Funcion>' + objOperaVisaNet.Funcion + '</pet:Funcion>' +
			'<pet:TipoPS>' + objOperaVisaNet.TipoPS + '</pet:TipoPS>' +
			'<pet:CapturaTarjeta>' + objOperaVisaNet.CapturaTarjeta + '</pet:CapturaTarjeta>' +
			'<pet:Cuotas>' + objOperaVisaNet.Cuotas + '</pet:Cuotas>' +
			'<pet:Diferido>' + objOperaVisaNet.Diferido + '</pet:Diferido>' +
			'<pet:Campos>' +
				'<!--Zero or more repetitions:-->' +
				'<pet:Campo>' +
					'<pet:Nombre>' + objOperaVisaNet.Nombre + '</pet:Nombre>' +
					'<pet:Valor>' + objOperaVisaNet.Valor + '</pet:Valor>' +
				'</pet:Campo>' +
			'</pet:Campos>' +
		'</pet:PosServicio>' +
	'</prin:peticionOperacionVisaNet>' +
	'</soapenv:Body>' +
  '</soapenv:Envelope>';
  return soapMSG;
}

function f_data_MC(objOperacionMC) {

  var soapMSG = '<soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/" xmlns:prin="http://principal.wsServer.claro.org/" xmlns:pet="http://com/tecnocom/PeticionOperacionMC">' +
               '<soapenv:Header/>' +
               '<soapenv:Body>' +
               '<prin:peticionOperacionMC>' +               
                  '<pet:AuditRequest>' +
										'<pet:IdTransaccion>' + objOperacionMC.IdTransaccion + '</pet:IdTransaccion>' +
										'<pet:IpApplicacion>' + objOperacionMC.IpApplicacion + '</pet:IpApplicacion>' +
										'<pet:NombreAplicacion>' + objOperacionMC.NombreAplicacion + '</pet:NombreAplicacion>' +
										'<pet:UsuarioAplicacion>' + objOperacionMC.UsuarioAplicacion + '</pet:UsuarioAplicacion>' +
									'</pet:AuditRequest>' +
									                  
                  '<pet:Aplicacion>' + objOperacionMC.Aplicacion + '</pet:Aplicacion>' +
                  '<pet:Transaccion>' + objOperacionMC.Transaccion + '</pet:Transaccion>' +
                  '<pet:Monto>' + objOperacionMC.Monto + '</pet:Monto>' +
                  '<pet:TipoMoneda>' + objOperacionMC.TipoMoneda + '</pet:TipoMoneda>' +
                  '<pet:DataAdicional>' + objOperacionMC.DataAdicional + '</pet:DataAdicional>' +
                  '<pet:CodigoServicio>' + objOperacionMC.CodigoServicio + '</pet:CodigoServicio>' +
                  '<pet:Dni>' + objOperacionMC.Dni + '</pet:Dni>' +
                  '<pet:Ruc>' + objOperacionMC.Ruc + '</pet:Ruc>' +
                  '<pet:ClaveComercio>' + objOperacionMC.ClaveComercio + '</pet:ClaveComercio>' +
                  
                  '<pet:Operaciones>' +
										'<!--Zero or more repetitions:-->' +
                        '<pet:Operacion>' +
                          '<pet:Producto>' + objOperacionMC.Producto + '</pet:Producto>' +
                          '<pet:Monto>' + objOperacionMC.OpeMonto + '</pet:Monto>' +
												'</pet:Operacion>' +
                  '</pet:Operaciones>' +
									'<pet:DatosAdicionales>' +									
										'<!--Zero or more repetitions:-->' +
										'<pet:DatoAdicional>' +
											'<pet:Nombre>' + objOperacionMC.Nombre + '</pet:Nombre>' +
                      '<pet:Valor>' + objOperacionMC.Valor + '</pet:Valor>' +
											'</pet:DatoAdicional>' +
									'</pet:DatosAdicionales>' +
               '</prin:peticionOperacionMC>' +
						'</soapenv:Body>' +
					'</soapenv:Envelope>';
  return soapMSG;
}


function f_data_TransacPago(objOpeTran) {
  var soapMSG = '<soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/" xmlns:typ="http://claro.com.pe/eai/ws/fact/enviotransacpagospos/types" xmlns:bas="http://claro.com.pe/eai/ws/baseschema">' +
   '<soapenv:Header/>' +
   '<soapenv:Body>' +
      '<typ:guardarTransacRequest>' +
         '<typ:auditRequest>' +
            '<bas:idTransaccion>' + objOpeTran.idTransaccion + '</bas:idTransaccion>' +
            '<bas:ipAplicacion>' + objOpeTran.ipAplicacion + '</bas:ipAplicacion>' +
            '<bas:nombreAplicacion>' + objOpeTran.nombreAplicacion + '</bas:nombreAplicacion>' +
            '<bas:usuarioAplicacion>' + objOpeTran.usuarioAplicacion + '</bas:usuarioAplicacion>' +
         '</typ:auditRequest>' +
         '<typ:idTransaccion>' + objOpeTran.idTransaccion2 + '</typ:idTransaccion>' +
         '<typ:codVenta>' + objOpeTran.codVenta + '</typ:codVenta>' +
         '<typ:nroTienda>' + objOpeTran.nroTienda + '</typ:nroTienda>' +
         '<typ:nroCaja>' + objOpeTran.nroCaja + '</typ:nroCaja>' +
         '<typ:nroReferencia>' + objOpeTran.nroReferencia + '</typ:nroReferencia>' +
         '<typ:nroAprobacion>' + objOpeTran.nroAprobacion + '</typ:nroAprobacion>' +
         '<typ:codOperacion>' + objOpeTran.codOperacion + '</typ:codOperacion>' +
         '<typ:desOperacion>' + objOpeTran.desOperacion + '</typ:desOperacion>' +
         '<typ:tipoOperacion>' + objOpeTran.tipoOperacion + '</typ:tipoOperacion>' +
         '<typ:montoOperacion>' + objOpeTran.montoOperacion + '</typ:montoOperacion>' +
         '<typ:monedaOperacion>' + objOpeTran.monedaOperacion + '</typ:monedaOperacion>' +
         '<typ:fechaTransaccion>' + objOpeTran.fechaTransaccion + '</typ:fechaTransaccion>' +
         '<typ:fechaRegistro>' + objOpeTran.fechaRegistro + '</typ:fechaRegistro>' +
         '<typ:nroTarjeta>' + objOpeTran.nroTarjeta + '</typ:nroTarjeta>' +
         '<typ:fecExpiracion>' + objOpeTran.fecExpiracion + '</typ:fecExpiracion>' +
         '<typ:codCajero>' + objOpeTran.codCajero + '</typ:codCajero>' +
         '<typ:codAnulador>' + objOpeTran.codAnulador + '</typ:codAnulador>' +
         '<typ:flagAnulacion>' + objOpeTran.flagAnulacion + '</typ:flagAnulacion>' +
         '<typ:nombreCliente>' + objOpeTran.nombreCliente + '</typ:nombreCliente>' +
         '<typ:ipCaja>' + objOpeTran.ipCaja + '</typ:ipCaja>' +
         '<typ:idAnulacion>' + objOpeTran.idAnulacion + '</typ:idAnulacion>' +
         '<typ:codEstablecimiento>' + objOpeTran.codEstablecimiento + '</typ:codEstablecimiento>' +
         '<typ:tipoTarjeta>' + objOpeTran.tipoTarjeta + '</typ:tipoTarjeta>' +
         '<typ:ipCliente>' + objOpeTran.ipCliente + '</typ:ipCliente>' +
         '<typ:ipServidor>' + objOpeTran.ipServidor + '</typ:ipServidor>' +
         '<typ:nombrePcCliente>' + objOpeTran.nombrePcCliente + '</typ:nombrePcCliente>' +
         '<typ:nombrePcServidor>' + objOpeTran.nombrePcServidor + '</typ:nombrePcServidor>' +
         '<typ:impresionVoucher>' + objOpeTran.impresionVoucher + '</typ:impresionVoucher>' +
         '<typ:usuarioRed>' + objOpeTran.usuarioRed + '</typ:usuarioRed>' +
         '<typ:tipoPago>' + objOpeTran.tipoPago + '</typ:tipoPago>' +
         '<typ:numPedido>' + objOpeTran.numPedido + '</typ:numPedido>' +
         '<typ:estadoTransaccion>' + objOpeTran.estadoTransaccion + '</typ:estadoTransaccion>' +
         '<typ:codRespTransaccion>' + objOpeTran.codRespTransaccion + '</typ:codRespTransaccion>' +
         '<typ:codAprobTransaccion>' + objOpeTran.codAprobTransaccion + '</typ:codAprobTransaccion>' +
         '<typ:descTransaccion>' + objOpeTran.descTransaccion + '</typ:descTransaccion>' +
         '<typ:numVoucher>' + objOpeTran.numVoucher + '</typ:numVoucher>' +
         '<typ:numSeriePos>' + objOpeTran.numSeriePos + '</typ:numSeriePos>' +
         '<typ:nombreEquipoPos>' + objOpeTran.nombreEquipoPos + '</typ:nombreEquipoPos>' +
         '<typ:numTransaccion>' + objOpeTran.numTransaccion + '</typ:numTransaccion>' +
         '<typ:tipoPos>' + objOpeTran.tipoPos + '</typ:tipoPos>' +
         '<typ:tipoTransaccion>' + objOpeTran.tipoTransaccion + '</typ:tipoTransaccion>' +
         '<typ:fechaTransaccionPos>' + objOpeTran.fechaTransaccionPos + '</typ:fechaTransaccionPos>' +
         '<typ:horaTransaccionPos>' + objOpeTran.horaTransaccionPos + '</typ:horaTransaccionPos>' +
         '<typ:nroRegistro>' + objOpeTran.nroRegistro + '</typ:nroRegistro>' +
         '<typ:listaRequestOpcional>' +
            '<!--1 or more repetitions:-->' +
            '<bas:objetoOpcional campo="?" valor="?"/>' +
         '</typ:listaRequestOpcional>' +
      '</typ:guardarTransacRequest>' +
   '</soapenv:Body>' +
'</soapenv:Envelope>';
  return soapMSG;
}


function f_data_TransacPagoUpdate(objOpeTran) {
  var soapMSG = '<soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/" xmlns:typ="http://claro.com.pe/eai/ws/fact/enviotransacpagospos/types" xmlns:bas="http://claro.com.pe/eai/ws/baseschema">' +
   '<soapenv:Header/>' +
   '<soapenv:Body>' +
      '<typ:actualizarTransacRequest>' +
         '<typ:auditRequest>' +
            '<bas:idTransaccion>' + objOpeTran.idTransaccion + '</bas:idTransaccion>' +
            '<bas:ipAplicacion>' + objOpeTran.ipAplicacion + '</bas:ipAplicacion>' +
            '<bas:nombreAplicacion>' + objOpeTran.nombreAplicacion + '</bas:nombreAplicacion>' +
            '<bas:usuarioAplicacion>' + objOpeTran.usuarioAplicacion + '</bas:usuarioAplicacion>' +
         '</typ:auditRequest>' +
         '<typ:idTransaccion>' + objOpeTran.idTransaccion2 + '</typ:idTransaccion>' +
         '<typ:monedaOperacion>' + objOpeTran.monedaOperacion + '</typ:monedaOperacion>' +
         '<typ:montoOperacion>' + objOpeTran.montoOperacion + ' </typ:montoOperacion>' +
         '<typ:nroRegistro>' + objOpeTran.nroRegistro + '</typ:nroRegistro>' +
         '<typ:numVoucher>' + objOpeTran.numVoucher + '</typ:numVoucher>' +
         '<typ:numAutTransaccion>' + objOpeTran.numAutTransaccion + '</typ:numAutTransaccion>' +
         '<typ:codRespTransaccion>' + objOpeTran.codRespTransaccion + '</typ:codRespTransaccion>' +
         '<typ:descTransaccion>' + objOpeTran.descTransaccion + '</typ:descTransaccion>' +
         '<typ:codAprobTransaccion>' + objOpeTran.codAprobTransaccion + '</typ:codAprobTransaccion>' +
         '<typ:tipoPOS>' + objOpeTran.tipoPos + '</typ:tipoPOS>' +
         '<typ:nroTarjeta>' + objOpeTran.nroTarjeta + '</typ:nroTarjeta>' +
         '<typ:fechaTransaccion>' + objOpeTran.fechaTransaccion + '</typ:fechaTransaccion>' +
         '<typ:horaTransaccion>' + objOpeTran.horaTransaccion + '</typ:horaTransaccion>' +         
         '<typ:fechaExpiracion>' + objOpeTran.fechaExpiracion + '</typ:fechaExpiracion>' +
         '<typ:nomCliente>' + objOpeTran.nomCliente + '</typ:nomCliente>' +
         '<typ:impVoucher>' + objOpeTran.impVoucher + '</typ:impVoucher>' +
         '<typ:seriePOS>' + objOpeTran.seriePOS + '</typ:seriePOS>' +         
         '<typ:nomEquipoPOS>' + objOpeTran.nomEquipoPOS + '</typ:nomEquipoPOS>' +
         '<typ:estadoTransaccion>' + objOpeTran.estadoTransaccion + '</typ:estadoTransaccion>' +
         '<typ:listaRequestOpcional>' +
            '<!--1 or more repetitions:-->' +
            '<bas:objetoOpcional campo="?" valor="?"/>' +
         '</typ:listaRequestOpcional>' +
      '</typ:actualizarTransacRequest>' +
   '</soapenv:Body>' +
  '</soapenv:Envelope>';
  return soapMSG;
}

function pad(n, len) {
	return (new Array(len + 1).join('0') + n).slice(-len);
}

function formatDate(date) {
  var year = date.getFullYear(),
  month = date.getMonth() + 1, // months are zero indexed
  day = date.getDate(),
  hour = date.getHours(),
  minute = date.getMinutes(),
  second = date.getSeconds(),
  hourFormatted = hour % 12 || 12, // hour returned in 24 hour format
  minuteFormatted = minute < 10 ? "0" + minute : minute;

  return year + "" + pad(month, 2) + "" + pad(day, 2) + "" + hourFormatted + "" + minuteFormatted + "" + second;

}

function FormatDateDdMmYyy(date)
{
	var today = date;	
	var dd = today.getDate(); 
	var mm = today.getMonth()+1;//January is 0! 
	var yyyy = today.getFullYear(); 
	if(dd<10){dd='0'+dd} 
	if(mm<10){mm='0'+mm} 
	
	var varFecha= dd + '/' + mm + '/' + yyyy;
	return varFecha;	
}

function FormatHour(date)
{
	var time = date;
	var varHour= pad(time.getHours(),2) + ":" + pad(time.getMinutes(),2) + ":" + pad(time.getSeconds(),2);
	return varHour;
}
    