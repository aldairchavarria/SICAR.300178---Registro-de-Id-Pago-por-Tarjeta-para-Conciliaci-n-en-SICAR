using System;
using System.Collections;
using System.Configuration;
using COM_SIC_Procesa_Pagos.DataPowerRest ;
using COM_SIC_Procesa_Pagos.GestionaRecaudacionRest;


namespace COM_SIC_Procesa_Pagos.RegistraPedidoRecargaRest
{

	public class BWRegistraPedidoRecarga
	{
		public BWRegistraPedidoRecarga()
		{
			
		}

		public object RegistraRecargaPendiente(RegistraPedidoRecargaRequest objRegistraPedidoRecargaRequest, Type objRegistraPedidoRecargaResponse, string rutaOSB, AuditoriaEWS objAuditoriaRequest)
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
			paramHeaders.Add("aplicacion", "SICAR");
			


			return	RestService.PostInvoque(rutaOSB, paramHeaders, objRegistraPedidoRecargaRequest, objRegistraPedidoRecargaResponse);					
		}

		public RegistraPedidoRecargasResponse RegistraRecargaPendientes(object objRequest, AuditoriaEWS objAuditoriaRequest)
		{
			object oRegistraPedido ;

			RegistraPedidoRecargaRequest objRegistraPedidoRecargaRequest = (RegistraPedidoRecargaRequest)objRequest;
			oRegistraPedido = RegistraRecargaPendiente(objRegistraPedidoRecargaRequest,typeof(RegistraPedidoRecargasResponse),"urlRegistrarRecargaPendienteRestWS",objAuditoriaRequest);
			RegistraPedidoRecargasResponse objRegistraPedidoRecargasResponse = (RegistraPedidoRecargasResponse)oRegistraPedido;

			return objRegistraPedidoRecargasResponse;

		}

		public object ActualizarRecarga(RegistraPedidoRecargaRequest objRegistraPedidoRecargaRequest, Type objRegistraPedidoRecargaResponse, string rutaOSB, AuditoriaEWS objAuditoriaRequest)
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
			paramHeaders.Add("aplicacion", "SICAR");

			return	RestService.PostInvoque(rutaOSB, paramHeaders, objRegistraPedidoRecargaRequest, objRegistraPedidoRecargaResponse);					
		}

		public RegistraPedidoRecargasResponse ActualizarRecargas(object objRequest, AuditoriaEWS objAuditoriaRequest)
		{
			object oRegistraPedido ;

			RegistraPedidoRecargaRequest objRegistraPedidoRecargaRequest = (RegistraPedidoRecargaRequest)objRequest;
			oRegistraPedido = ActualizarRecarga(objRegistraPedidoRecargaRequest,typeof(RegistraPedidoRecargasResponse),"urlActualizarRecargaRestWS",objAuditoriaRequest);
			RegistraPedidoRecargasResponse objRegistraPedidoRecargasResponse = (RegistraPedidoRecargasResponse)oRegistraPedido;

			return objRegistraPedidoRecargasResponse;

		}



	}
}
