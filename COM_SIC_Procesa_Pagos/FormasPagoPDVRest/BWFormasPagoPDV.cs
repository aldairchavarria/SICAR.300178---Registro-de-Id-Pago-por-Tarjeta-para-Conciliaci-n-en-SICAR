using System;
using System.Collections;
using System.Configuration;
using COM_SIC_Procesa_Pagos.DataPowerRest ;
using COM_SIC_Procesa_Pagos.GestionaRecaudacionRest;
//INI-1019 - YGP
namespace COM_SIC_Procesa_Pagos.FormasPagoPDVRest
{

	public class BWFormasPagoPDV
	{

		public BWFormasPagoPDV()
		{
		}

		public Hashtable getParamHeaders(AuditoriaEWS objAuditoriaRequest)
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

			return paramHeaders;
		}

		public object SendRequest(FormasPagoPDVRequest objRequest, string urlWebconfig, AuditoriaEWS objAuditoriaRequest)
		{
			Hashtable paramHeaders = getParamHeaders(objAuditoriaRequest);
			return RestService.PostInvoque(urlWebconfig, paramHeaders, objRequest, typeof(FormasPagoPDVResponse),"strTimeOutGenDP");;
		}

		public FormasPagoPDVResponse ConsultarOficinaVenta(FormasPagoPDVRequest objRequest, AuditoriaEWS objAuditoriaRequest)
		{
			return (FormasPagoPDVResponse)SendRequest(objRequest, "urlconsultarOficinasVentaWS", objAuditoriaRequest);
		}

		public FormasPagoPDVResponse ConsultarFormaPagoPDV(FormasPagoPDVRequest objRequest, AuditoriaEWS objAuditoriaRequest)
		{
			return (FormasPagoPDVResponse)SendRequest(objRequest, "urlconsultarFormaPagoPDVWS", objAuditoriaRequest);
		}

		public FormasPagoPDVResponse RegistrarFormaPagoPDV(FormasPagoPDVRequest objRequest, AuditoriaEWS objAuditoriaRequest)
		{
			return (FormasPagoPDVResponse)SendRequest(objRequest, "urlregistrarFormaPagoPDVWS", objAuditoriaRequest);
		}

		public FormasPagoPDVResponse ActualizarFormaPagoPDV(FormasPagoPDVRequest objRequest, AuditoriaEWS objAuditoriaRequest)
		{
			return (FormasPagoPDVResponse)SendRequest(objRequest, "urlactualizarFormaPagoPDVWS", objAuditoriaRequest);
		}
	}
}
