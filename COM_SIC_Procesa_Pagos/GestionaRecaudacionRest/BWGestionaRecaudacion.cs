using System;
using System.Collections;
using System.Configuration;
using COM_SIC_Procesa_Pagos.DataPowerRest ;
using COM_SIC_Procesa_Pagos.GestionaRecaudacionRest;


namespace COM_SIC_Procesa_Pagos.GestionaRecaudacionRest
{

	public class BWGestionaRecaudacion
	{
		public BWGestionaRecaudacion()
		{
			
		}

		public GestionaRecaudacionesResponse ConsultaFormaPagos(object objRequest, AuditoriaEWS objAuditoriaRequest)
		{
			object oConsultaFormaPago ;

           GestionaRecaudacionRequest objGestionaRecaudacionRequest = (GestionaRecaudacionRequest)objRequest;
		   oConsultaFormaPago=ConsultarFormaPago(objGestionaRecaudacionRequest,typeof(GestionaRecaudacionesResponse),"urlConsultarFormaPagoRestWS",objAuditoriaRequest);
			GestionaRecaudacionesResponse objGestionaRecaudacionesResponse=(GestionaRecaudacionesResponse)oConsultaFormaPago;

			return objGestionaRecaudacionesResponse;
          
		}

		public object ConsultarFormaPago(GestionaRecaudacionRequest objGestionaRecaudacionRequest, Type objGestionaRecaudacionesResponse, string rutaOSB, AuditoriaEWS objAuditoriaRequest)
		{
			Hashtable paramHeaders = new Hashtable();
			
			paramHeaders.Add("idTransaccion", objAuditoriaRequest.IDTRANSACCION);
			paramHeaders.Add("ipAplicacion", objAuditoriaRequest.IPAPLICACION);
			paramHeaders.Add("idTransaccionNegocio", objAuditoriaRequest.idTransaccionNegocio);
			paramHeaders.Add("usuarioAplicacion", objAuditoriaRequest.USRAPP);
			paramHeaders.Add("applicationCodeWS", objAuditoriaRequest.applicationCodeWS);
			paramHeaders.Add("applicationCode", objAuditoriaRequest.APLICACION);
			paramHeaders.Add("nombreAplicacion", objAuditoriaRequest.USRAPP);
			paramHeaders.Add("timestamp",objAuditoriaRequest.USRAPP);
			paramHeaders.Add("msgid", objAuditoriaRequest.USRAPP);
			paramHeaders.Add("userId", objAuditoriaRequest.userId);


  			return	RestService.GetInvoque(rutaOSB, paramHeaders, objGestionaRecaudacionRequest, objGestionaRecaudacionesResponse);					
		
		}



		public GestionaRecaudacionesResponse ActualizarFormaPagos(object objRequest, AuditoriaEWS objAuditoriaRequest)
		{
			object oConsultaFormaPago ;

			GestionaRecaudacionRequest objGestionaRecaudacionRequest = (GestionaRecaudacionRequest)objRequest;
			oConsultaFormaPago=ActualizarFormaPago(objGestionaRecaudacionRequest,typeof(GestionaRecaudacionesResponse),"urlActualizaFormaPagoRestWS",objAuditoriaRequest);
			GestionaRecaudacionesResponse objGestionaRecaudacionesResponse=(GestionaRecaudacionesResponse)oConsultaFormaPago;

			return objGestionaRecaudacionesResponse;
          
		}

		public object ActualizarFormaPago(GestionaRecaudacionRequest objGestionaRecaudacionRequest, Type objGestionaRecaudacionesResponse, string rutaOSB, AuditoriaEWS objAuditoriaRequest)
		{
			Hashtable paramHeaders = new Hashtable();
			
			paramHeaders.Add("idTransaccion", objAuditoriaRequest.IDTRANSACCION);
			paramHeaders.Add("ipAplicacion", objAuditoriaRequest.IPAPLICACION);
			paramHeaders.Add("idTransaccionNegocio", objAuditoriaRequest.idTransaccionNegocio);
			paramHeaders.Add("usuarioAplicacion", objAuditoriaRequest.USRAPP);
			paramHeaders.Add("applicationCodeWS", objAuditoriaRequest.applicationCodeWS);
			paramHeaders.Add("applicationCode", objAuditoriaRequest.APLICACION);
			paramHeaders.Add("nombreAplicacion", objAuditoriaRequest.USRAPP);
			paramHeaders.Add("timestamp",objAuditoriaRequest.USRAPP);
			paramHeaders.Add("msgid", objAuditoriaRequest.USRAPP);
			paramHeaders.Add("userId", objAuditoriaRequest.userId);


			return	RestService.PostInvoque(rutaOSB, paramHeaders, objGestionaRecaudacionRequest, objGestionaRecaudacionesResponse);					
		
		}

		public GestionaRecaudacionesResponse ConsultarArqueoCajas(object objRequest, AuditoriaEWS objAuditoriaRequest)
		{
			object oConsultaFormaPago ;

			GestionaRecaudacionRequest objGestionaRecaudacionRequest = (GestionaRecaudacionRequest)objRequest;
			oConsultaFormaPago=ConsultarArqueoCaja(objGestionaRecaudacionRequest,typeof(GestionaRecaudacionesResponse),"urlConsultarArqueoCajaRestWS",objAuditoriaRequest);
			GestionaRecaudacionesResponse objGestionaRecaudacionesResponse=(GestionaRecaudacionesResponse)oConsultaFormaPago;

			return objGestionaRecaudacionesResponse;
          
		}

		public object ConsultarArqueoCaja(GestionaRecaudacionRequest objGestionaRecaudacionRequest, Type objGestionaRecaudacionesResponse, string rutaOSB, AuditoriaEWS objAuditoriaRequest)
		{
			Hashtable paramHeaders = new Hashtable();

			paramHeaders.Add("idTransaccion", objAuditoriaRequest.IDTRANSACCION);
			paramHeaders.Add("ipAplicacion", objAuditoriaRequest.IPAPLICACION);
			paramHeaders.Add("idTransaccionNegocio", objAuditoriaRequest.idTransaccionNegocio);
			paramHeaders.Add("usuarioAplicacion", objAuditoriaRequest.USRAPP);
			paramHeaders.Add("applicationCodeWS", objAuditoriaRequest.applicationCodeWS);
			paramHeaders.Add("applicationCode", objAuditoriaRequest.APLICACION);
			paramHeaders.Add("nombreAplicacion", objAuditoriaRequest.USRAPP);
			paramHeaders.Add("timestamp",objAuditoriaRequest.USRAPP);
			paramHeaders.Add("msgid", objAuditoriaRequest.USRAPP);
			paramHeaders.Add("userId", objAuditoriaRequest.userId);

			return	RestService.PostInvoque(rutaOSB, paramHeaders, objGestionaRecaudacionRequest, objGestionaRecaudacionesResponse);					

		}

	        public GestionaRecaudacionesResponse RegistrarArqueoCaja(object objRequest, AuditoriaEWS objAuditoriaRequest)
		{
			object oRegistrarArqueoCaja ;

			GestionaRecaudacionRequest objGestionaRecaudacionRequest = (GestionaRecaudacionRequest)objRequest;
			oRegistrarArqueoCaja = RegistrarArqueoCaja(objGestionaRecaudacionRequest,typeof(GestionaRecaudacionesResponse),"urlRegistrarArqueoCajaRestWS",objAuditoriaRequest);
			GestionaRecaudacionesResponse objGestionaRecaudacionesResponse=(GestionaRecaudacionesResponse)oRegistrarArqueoCaja;

			return objGestionaRecaudacionesResponse;
		}

		public object RegistrarArqueoCaja(GestionaRecaudacionRequest objGestionaRecaudacionRequest, Type objGestionaRecaudacionesResponse, string rutaOSB, AuditoriaEWS objAuditoriaRequest)
		{
			Hashtable paramHeaders = new Hashtable();
          
                        paramHeaders.Add("idTransaccion", objAuditoriaRequest.IDTRANSACCION);
			paramHeaders.Add("ipAplicacion", objAuditoriaRequest.IPAPLICACION);
			paramHeaders.Add("idTransaccionNegocio", objAuditoriaRequest.idTransaccionNegocio);
			paramHeaders.Add("usuarioAplicacion", objAuditoriaRequest.USRAPP);
			paramHeaders.Add("applicationCodeWS", objAuditoriaRequest.applicationCodeWS);
			paramHeaders.Add("applicationCode", objAuditoriaRequest.APLICACION);
			paramHeaders.Add("nombreAplicacion", objAuditoriaRequest.USRAPP);
			paramHeaders.Add("timestamp",objAuditoriaRequest.USRAPP);
			paramHeaders.Add("msgid", objAuditoriaRequest.USRAPP);
			paramHeaders.Add("userId", objAuditoriaRequest.userId);


			return	RestService.PostInvoque(rutaOSB, paramHeaders, objGestionaRecaudacionRequest, objGestionaRecaudacionesResponse);
		}

		public GestionaRecaudacionesResponse obtenerDevolucionSaldo(object objRequest, AuditoriaEWS objAuditoriaRequest)
		{
			object oobtenerDevolucionSaldo ;

			GestionaRecaudacionRequest objGestionaRecaudacionRequest = (GestionaRecaudacionRequest)objRequest;
			oobtenerDevolucionSaldo=obtenerDevolucionSaldo(objGestionaRecaudacionRequest,typeof(GestionaRecaudacionesResponse),"urlobtenerDevolucionSaldoRestWS",objAuditoriaRequest);
			GestionaRecaudacionesResponse objGestionaRecaudacionesResponse=(GestionaRecaudacionesResponse)oobtenerDevolucionSaldo;

			return objGestionaRecaudacionesResponse;
          
		}

		public object obtenerDevolucionSaldo(GestionaRecaudacionRequest objGestionaRecaudacionRequest, Type objGestionaRecaudacionesResponse, string rutaOSB, AuditoriaEWS objAuditoriaRequest)
		{
			Hashtable paramHeaders = new Hashtable();
			
			paramHeaders.Add("idTransaccion", objAuditoriaRequest.IDTRANSACCION);
			paramHeaders.Add("ipAplicacion", objAuditoriaRequest.IPAPLICACION);
			paramHeaders.Add("idTransaccionNegocio", objAuditoriaRequest.idTransaccionNegocio);
			paramHeaders.Add("usuarioAplicacion", objAuditoriaRequest.USRAPP);
			paramHeaders.Add("applicationCodeWS", objAuditoriaRequest.applicationCodeWS);
			paramHeaders.Add("applicationCode", objAuditoriaRequest.APLICACION);
			paramHeaders.Add("nombreAplicacion", objAuditoriaRequest.USRAPP);
			paramHeaders.Add("timestamp",objAuditoriaRequest.USRAPP);
			paramHeaders.Add("msgid", objAuditoriaRequest.USRAPP);
			paramHeaders.Add("userId", objAuditoriaRequest.userId);


			return	RestService.PostInvoque(rutaOSB, paramHeaders, objGestionaRecaudacionRequest, objGestionaRecaudacionesResponse);					
		
		}


		public GestionaRecaudacionesResponse RegistrarDevolucionSaldo(object objRequest, AuditoriaEWS objAuditoriaRequest)
		{
			object oRegistrarDevolucionSaldo;

			GestionaRecaudacionRequest objGestionaRecaudacionRequest = (GestionaRecaudacionRequest)objRequest;
			oRegistrarDevolucionSaldo = RegistrarDevolucionSaldo(objGestionaRecaudacionRequest,typeof(GestionaRecaudacionesResponse),"urlRegistrarDevolucionSaldoRestWS",objAuditoriaRequest);
			GestionaRecaudacionesResponse objGestionaRecaudacionesResponse=(GestionaRecaudacionesResponse)oRegistrarDevolucionSaldo;

			return objGestionaRecaudacionesResponse;
		}

		public object RegistrarDevolucionSaldo(GestionaRecaudacionRequest objGestionaRecaudacionRequest, Type objGestionaRecaudacionesResponse, string rutaOSB, AuditoriaEWS objAuditoriaRequest)
		{
			Hashtable paramHeaders = new Hashtable();
          
			paramHeaders.Add("idTransaccion", objAuditoriaRequest.IDTRANSACCION);
			paramHeaders.Add("ipAplicacion", objAuditoriaRequest.IPAPLICACION);
			paramHeaders.Add("idTransaccionNegocio", objAuditoriaRequest.idTransaccionNegocio);
			paramHeaders.Add("usuarioAplicacion", objAuditoriaRequest.USRAPP);
			paramHeaders.Add("applicationCodeWS", objAuditoriaRequest.applicationCodeWS);
			paramHeaders.Add("applicationCode", objAuditoriaRequest.APLICACION);
			paramHeaders.Add("nombreAplicacion", objAuditoriaRequest.USRAPP);
			paramHeaders.Add("timestamp",objAuditoriaRequest.USRAPP);
			paramHeaders.Add("msgid", objAuditoriaRequest.USRAPP);
			paramHeaders.Add("userId", objAuditoriaRequest.userId);


			return	RestService.PostInvoque(rutaOSB, paramHeaders, objGestionaRecaudacionRequest, objGestionaRecaudacionesResponse);
		}

		public GestionaRecaudacionesResponse ValidarDevolucionSaldo(object objRequest, AuditoriaEWS objAuditoriaRequest)
		{
			object oValidarDevolucionSaldo;

			GestionaRecaudacionRequest objGestionaRecaudacionRequest = (GestionaRecaudacionRequest)objRequest;
			oValidarDevolucionSaldo = ValidarDevolucionSaldo(objGestionaRecaudacionRequest,typeof(GestionaRecaudacionesResponse),"urlValidarDevolucionSaldoRestWS",objAuditoriaRequest);
			GestionaRecaudacionesResponse objGestionaRecaudacionesResponse=(GestionaRecaudacionesResponse)oValidarDevolucionSaldo;

			return objGestionaRecaudacionesResponse;
		}

		public object ValidarDevolucionSaldo(GestionaRecaudacionRequest objGestionaRecaudacionRequest, Type objGestionaRecaudacionesResponse, string rutaOSB, AuditoriaEWS objAuditoriaRequest)
		{
			Hashtable paramHeaders = new Hashtable();
          
			paramHeaders.Add("idTransaccion", objAuditoriaRequest.IDTRANSACCION);
			paramHeaders.Add("ipAplicacion", objAuditoriaRequest.IPAPLICACION);
			paramHeaders.Add("idTransaccionNegocio", objAuditoriaRequest.idTransaccionNegocio);
			paramHeaders.Add("usuarioAplicacion", objAuditoriaRequest.USRAPP);
			paramHeaders.Add("applicationCodeWS", objAuditoriaRequest.applicationCodeWS);
			paramHeaders.Add("applicationCode", objAuditoriaRequest.APLICACION);
			paramHeaders.Add("nombreAplicacion", objAuditoriaRequest.USRAPP);
			paramHeaders.Add("timestamp",objAuditoriaRequest.USRAPP);
			paramHeaders.Add("msgid", objAuditoriaRequest.USRAPP);
			paramHeaders.Add("userId", objAuditoriaRequest.userId);


			return	RestService.PostInvoque(rutaOSB, paramHeaders, objGestionaRecaudacionRequest, objGestionaRecaudacionesResponse);
		}

		//INICIATIVA - 529 INI
		public GestionaRecaudacionesResponse ActualizarTipoPagos(object objRequest, AuditoriaEWS objAuditoriaRequest)
		{
			object oConsultaTipoPago ;

			GestionaRecaudacionRequest objGestionaRecaudacionRequest = (GestionaRecaudacionRequest)objRequest;
			oConsultaTipoPago=ActualizarTipoPago(objGestionaRecaudacionRequest,typeof(GestionaRecaudacionesResponse),"urlActualizaTipoPagoRestWS",objAuditoriaRequest);
			GestionaRecaudacionesResponse objGestionaRecaudacionesResponse=(GestionaRecaudacionesResponse)oConsultaTipoPago;

			return objGestionaRecaudacionesResponse;
          
		}

		public object ActualizarTipoPago(GestionaRecaudacionRequest objGestionaRecaudacionRequest, Type objGestionaRecaudacionesResponse, string rutaOSB, AuditoriaEWS objAuditoriaRequest)
		{
			Hashtable paramHeaders = new Hashtable();
			
			paramHeaders.Add("idTransaccion", objAuditoriaRequest.IDTRANSACCION);
			paramHeaders.Add("ipAplicacion", objAuditoriaRequest.IPAPLICACION);
			paramHeaders.Add("idTransaccionNegocio", objAuditoriaRequest.idTransaccionNegocio);
			paramHeaders.Add("usuarioAplicacion", objAuditoriaRequest.USRAPP);
			paramHeaders.Add("applicationCodeWS", objAuditoriaRequest.applicationCodeWS);
			paramHeaders.Add("applicationCode", objAuditoriaRequest.APLICACION);
			paramHeaders.Add("nombreAplicacion", objAuditoriaRequest.USRAPP);
			paramHeaders.Add("timestamp",objAuditoriaRequest.USRAPP);
			paramHeaders.Add("msgid", objAuditoriaRequest.USRAPP);
			paramHeaders.Add("userId", objAuditoriaRequest.userId);


			return	RestService.PostInvoque(rutaOSB, paramHeaders, objGestionaRecaudacionRequest, objGestionaRecaudacionesResponse);					
		
		}
		//INICIATIVA - 529 FIN
	}
}
