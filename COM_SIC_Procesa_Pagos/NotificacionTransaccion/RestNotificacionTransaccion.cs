using System;
using System.Collections;
using System.Text;
using System.Web;
using System.Data;
using System.Configuration;
using COM_SIC_Entidades.DataPowerRest;
using COM_SIC_Activaciones;
using COM_SIC_Entidades.DataPowerRest.HeadersGenerics;
using COM_SIC_Entidades.DataPowerRest.RestReferences;
using COM_SIC_Entidades.claro_post_notificaTransaccion.consultarNotificaciones.Request;
using COM_SIC_Entidades.claro_post_notificaTransaccion.consultarNotificaciones.Response;


namespace COM_SIC_Procesa_Pagos.NotificacionTransaccion
{
	/// <summary>
	/// Summary description for RestNotificacionTransaccion.
	/// </summary>
	public class RestNotificacionTransaccion
	{
		public RestNotificacionTransaccion()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		string nameArchivo = "log_ConsultarNotificacion" + "_" + string.Format("{0:dd-MM-yyyy}", DateTime.Now) + ".txt";
		SICAR_Log objFileLog = new SICAR_Log();
		public ConsultarNotificaResponse ConsultarNotificacion(ConsultarNotificaRequest objMessageRequest){
			objFileLog.Log_WriteLog(string.Empty, nameArchivo, string.Format("{0}{1}{0}","**************************","INICIO PROY-140846 ConsultarNotificacion "));
		
			ConsultarNotificaResponse objRpta = new ConsultarNotificaResponse();
		
			try
			{
				Hashtable paramHeader = new Hashtable();
		
				string userEncriptado = Funciones.CheckStr(ConfigurationSettings.AppSettings["User_ConsultarNotificacion"]);
				string passEncriptado = Funciones.CheckStr(ConfigurationSettings.AppSettings["Pass_ConsultarNotificacion"]);
				string idTransaccion = DateTime.Now.ToString("yyyyMMddHHmmssfff");
			
				paramHeader.Add("msgId", idTransaccion);
				paramHeader.Add("timestamp", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"));
				paramHeader.Add("userId", Funciones.CheckStr(ConfigurationSettings.AppSettings["constAplicacion"]));//VALIDAR
				paramHeader.Add("idTransaccion", idTransaccion);
				paramHeader.Add("aplicacion", Funciones.CheckStr("SICAR"));
		

				object oResponse;
				foreach (object valor in paramHeader.Values)
				{
					objFileLog.Log_WriteLog("", nameArchivo, String.Format("{0} --> {1}", "Value {0}", valor));
				}

				BWCobroAnticipadoInstalacion.CredencialesDPRest objCredencialesDPRest= new BWCobroAnticipadoInstalacion.CredencialesDPRest();
				objCredencialesDPRest.usuario = userEncriptado;
				objCredencialesDPRest.clave = passEncriptado;
				objCredencialesDPRest.urlServicio = Funciones.CheckStr(ConfigurationSettings.AppSettings["consUrlConsultarNotificacion"]);
				objCredencialesDPRest.timeOutServicio = Funciones.CheckStr(ConfigurationSettings.AppSettings["Time_Out_ConsultarNotificacion"]);
		
				oResponse = BWCobroAnticipadoInstalacion.RestService.PostInvoque_Generic(objCredencialesDPRest,paramHeader,objMessageRequest, typeof(ConsultarNotificaResponse));
				objRpta = (ConsultarNotificaResponse)oResponse;
			}
			catch (Exception ex)
			{
				objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} : {1}","Excepcion metodo ConsultarNotificacion", Funciones.CheckStr(ex.Message)));
				objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} : {1}","Excepcion metodo ConsultarNotificacion", Funciones.CheckStr(ex.StackTrace)));
			}
		
			objFileLog.Log_WriteLog(string.Empty, nameArchivo, string.Format("{0}{1}{0}","**************************","FIN ConsultarNotificacion"));
			return objRpta;
		
		
		}
		
	}
}
