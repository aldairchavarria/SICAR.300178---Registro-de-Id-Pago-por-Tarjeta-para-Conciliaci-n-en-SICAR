using System;
using System.Collections;
using System.Configuration;
using COM_SIC_Procesa_Pagos.DataPowerRest ;
using COM_SIC_Procesa_Pagos.GestionaRecaudacionRest;
namespace COM_SIC_Procesa_Pagos.BWPagoEnvioSMS
{//INICIATIVA 995-INI
	public class BWPagoEnvioSMS
	{
		public BWPagoEnvioSMS()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public object SendRequest(PagoEnvioSMSRequest objRequest, string urlWebconfig, AuditoriaEWS objAuditoriaRequest)
		{
			Hashtable paramHeaders = getParamHeaders(objAuditoriaRequest);
			return RestService.PostInvoque(urlWebconfig, paramHeaders, objRequest, typeof(PagoEnvioSMSResponse),"strTimeOutPagoEnvioSMS");;
		}

		public PagoEnvioSMSResponse PagoEnvioSMS(PagoEnvioSMSRequest objRequest, AuditoriaEWS objAuditoriaRequest)
		{
			return (PagoEnvioSMSResponse)SendRequest(objRequest, "urlPagoEnvioSMS", objAuditoriaRequest);
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
	}
}
