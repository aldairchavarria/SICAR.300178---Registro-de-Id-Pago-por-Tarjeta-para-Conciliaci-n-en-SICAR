using System;
using System.Collections;
using System.Configuration;
using COM_SIC_Procesa_Pagos.DataPowerRest ;
using COM_SIC_Procesa_Pagos.GestionaRecaudacionRest;

namespace COM_SIC_Procesa_Pagos.BWCobroAnticipadoInstalacion
{
	/// <summary>
	/// Summary description for BWCobroAnticipadoInstalacion.
	/// </summary>
	public class BWCobroAnticipadoInstalacion
	{
		public BWCobroAnticipadoInstalacion()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public ActualizaPAGenericResponse ActualizarPagoAnticipado(object objRequest)
		{
			object oActualizarPagoAnticipado ;

			ActualizaPAGenericRequest objActualizaPAGenericRequest = (ActualizaPAGenericRequest)objRequest;
			oActualizarPagoAnticipado=ActualizarPagosAnticipado(objActualizaPAGenericRequest,typeof(ActualizaPAGenericResponse),"cons_UrlCobroAnticipadoActualizaRes");
			ActualizaPAGenericResponse objActualizaPAGenericResponse=(ActualizaPAGenericResponse)oActualizarPagoAnticipado;

			return objActualizaPAGenericResponse;
          
		}

		public object ActualizarPagosAnticipado(ActualizaPAGenericRequest objActualizaPAGenericRequest, Type objActualizaPAGenericResponse, string rutaOSB)
		{
			Hashtable paramHeaders = new Hashtable();
			CredencialesDPRest objCredencialesDPRest= new CredencialesDPRest();
			Hashtable paramHeader = new Hashtable();
			paramHeader.Add("idTransaccion", DateTime.Now.ToString("YYYYMMDDHHMISSMS"));
			paramHeader.Add("msgId", DateTime.Now.ToString("YYYYMMDDHHMISSMS"));
			paramHeader.Add("userId", ConfigurationSettings.AppSettings["strCodAPlicativoClaveWeb"]);
			paramHeader.Add("timestamp", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"));		
			paramHeader.Add("aplicacion", "SISACT");	

			objCredencialesDPRest.usuario=ConfigurationSettings.AppSettings["strUserWSCobroAnticipadoCAI"].ToString();
			objCredencialesDPRest.clave=ConfigurationSettings.AppSettings["strPassWSCobroAnticipadoCAI"].ToString();
			objCredencialesDPRest.urlServicio=ConfigurationSettings.AppSettings["cons_UrlCobroAnticipadoActualizaRes"].ToString();
			objCredencialesDPRest.timeOutServicio=ConfigurationSettings.AppSettings["TimeOut_PA"].ToString();

			return	RestService.PostInvoque_Generic(objCredencialesDPRest, paramHeader, objActualizaPAGenericRequest,typeof(ActualizaPAGenericResponse));			
		
		}

		public ConsultaPAGenericResponse ConsultarPagosAnticipados(object objRequest)
		{
			object oConsultarPagoAnticipado ;

			ConsultaPAGenericRequest objConsultaPAGenericRequest = (ConsultaPAGenericRequest)objRequest;
			oConsultarPagoAnticipado=ConsultarPagosAnticipado(objConsultaPAGenericRequest,typeof(ConsultaPAGenericResponse),"cons_UrlCobroAnticipadoConsultaRes");
			ConsultaPAGenericResponse objConsultaPAGenericResponse=(ConsultaPAGenericResponse)oConsultarPagoAnticipado;

			return objConsultaPAGenericResponse;
          
		}

		public object ConsultarPagosAnticipado(ConsultaPAGenericRequest objConsultaPAGenericRequest, Type objConsultaPAGenericResponse, string rutaOSB)
		{
			CredencialesDPRest objCredencialesDPRest= new CredencialesDPRest();
			Hashtable paramHeader = new Hashtable();
			paramHeader.Add("idTransaccion", DateTime.Now.ToString("YYYYMMDDHHMISSMS"));
			paramHeader.Add("msgId", DateTime.Now.ToString("YYYYMMDDHHMISSMS"));
			paramHeader.Add("userId", ConfigurationSettings.AppSettings["strCodAPlicativoClaveWeb"]);
			paramHeader.Add("timestamp", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"));
			paramHeader.Add("aplicacion", "SISACT");	

			objCredencialesDPRest.usuario=ConfigurationSettings.AppSettings["strUserWSCobroAnticipadoCAI"].ToString();
			objCredencialesDPRest.clave=ConfigurationSettings.AppSettings["strPassWSCobroAnticipadoCAI"].ToString();
			objCredencialesDPRest.urlServicio=ConfigurationSettings.AppSettings["cons_UrlCobroAnticipadoConsultaRes"].ToString();
			objCredencialesDPRest.timeOutServicio=ConfigurationSettings.AppSettings["TimeOut_PA"].ToString();


			return	RestService.PostInvoque_Generic(objCredencialesDPRest, paramHeader, objConsultaPAGenericRequest,typeof(ConsultaPAGenericResponse));					
		
		}
	}
}
