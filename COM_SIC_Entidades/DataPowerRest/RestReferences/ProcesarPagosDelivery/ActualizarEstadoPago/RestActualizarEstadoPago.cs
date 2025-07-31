using System;
using System.Text;
using System.Collections;
using System.Configuration;
using COM_SIC_Entidades.DataPowerRest;
using COM_SIC_Activaciones;

using COM_SIC_Entidades.DataPowerRest.HeadersGenerics;
using COM_SIC_Entidades.DataPowerRest.RestReferences;

using COM_SIC_Entidades.DataPowerRest.MethodsRest.ProcesarPagosDelivery.RestActualizarEstadoPago;

namespace COM_SIC_Entidades.DataPowerRest.RestReferences.ProcesarPagosDelivery.ActualizarEstadoPago
{
	public class RestActualizarEstadoPago
	{
		public ResponseActualizarEstadoPago ActualizarEstadoPago(RequestActualizarEstadoPago objRequestRegistrarOmisionPIN, BEAuditoriaRequest objBEAuditoriaRequest)
		{
			string nameArchivo = "log_ActualizarEstadoPago" + "_" + string.Format("{0:dd-MM-yyyy}", DateTime.Now) + ".txt";
			SICAR_Log objFileLog = new SICAR_Log();

			object oResponse;

			ResponseActualizarEstadoPago objResp = new ResponseActualizarEstadoPago();
			try
			{
				Hashtable paramHeader = new Hashtable();
				paramHeader.Add("timestamp", objBEAuditoriaRequest.timestamp);
				paramHeader.Add("userId", objBEAuditoriaRequest.userId);
				paramHeader.Add("msgId", objBEAuditoriaRequest.idTransaccion);
				paramHeader.Add("idTransaccion", objBEAuditoriaRequest.idTransaccion);

				string strWSIP = Funciones.CheckStr(ConfigurationSettings.AppSettings["ProcesarPagosDelivery_wsip"]);
				string strUserEncrypted = Funciones.CheckStr(ConfigurationSettings.AppSettings["ProcesarPagosDelivery_User"]);
				string strPassEncrypted = Funciones.CheckStr(ConfigurationSettings.AppSettings["ProcesarPagosDelivery_Pass"]);
				string strTimeout = Funciones.CheckStr(ConfigurationSettings.AppSettings["ProcesarPagosDelivery_TimeOut"]);
				string strnombreProy = "ProcesarPagosDelivery";

				objFileLog.Log_WriteLog(string.Empty, nameArchivo, "---- Parametros de entrada Inicio : ----");
				objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} : {1}","strnombreProy",strnombreProy));
				objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} : {1}","timestamp",objBEAuditoriaRequest.timestamp));
				objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} : {1}","userId",objBEAuditoriaRequest.userId));
				objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} : {1}","idTransaccion",objBEAuditoriaRequest.idTransaccion));
				objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} : {1}","ipApplication",objBEAuditoriaRequest.ipApplication));
				objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} : {1}","strWSIP",strWSIP));
				objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} : {1}","strUserEncrypted",strUserEncrypted));
				objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} : {1}","strPassEncrypted",strPassEncrypted));
				objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} : {1}", "strTimeout", strTimeout));

				oResponse = PostInvoqueDP.PostInvoque("urlActualizarEstadoPagoDelivery", paramHeader, objRequestRegistrarOmisionPIN, objBEAuditoriaRequest.userId, objBEAuditoriaRequest.ipApplication, strnombreProy, strWSIP, strUserEncrypted, strPassEncrypted, strTimeout, typeof(ResponseActualizarEstadoPago));
				objResp = (ResponseActualizarEstadoPago)oResponse;
				return objResp;
			}
			catch (Exception ex)
			{
				objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} : {1}","Excepcion metodo ActualizarEstadoPago",objBEAuditoriaRequest.applicationCodeWS));
				objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} : {1}","Excepcion metodo ActualizarEstadoPago",Funciones.CheckStr(ex.Message)));
				objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} : {1}","Excepcion metodo ActualizarEstadoPago",Funciones.CheckStr(ex.StackTrace)));
				throw;
			}

		}
	}
}
