using System;
using System.Text;
using System.Collections;
using System.Configuration;
using COM_SIC_Entidades.DataPowerRest;
using COM_SIC_Activaciones;

using COM_SIC_Entidades.DataPowerRest.HeadersGenerics;
using COM_SIC_Entidades.DataPowerRest.RestReferences;

using COM_SIC_Entidades.DataPowerRest.MethodsRest.GeneraFacturaElectronica;

namespace COM_SIC_Entidades.DataPowerRest.RestReferences.GenerarFacturaElectronica
{
	/// <summary>
	/// Summary description for RestGenerarFacturaElectronica.
	/// </summary>
	public class RestGenerarFacturaElectronica
	{
		public RestGenerarFacturaElectronica()
		{
			//
			// TODO: Add constructor logic here
			//

		}
		public GeneraFacturaElectronicaResponse GeneraFacturaElectronica(GeneraFacturaElectronicaRequest objRequest, BEAuditoriaRequest objBEAuditoriaRequest)
		{
			string nameArchivo = "logGeneraFacturaElectronica" + "_" + string.Format("{0:dd-MM-yyyy}", DateTime.Now) + ".txt";
			SICAR_Log objFileLog = new SICAR_Log();

			object oResponse;

			GeneraFacturaElectronicaResponse objResp = new GeneraFacturaElectronicaResponse();
			try
			{
				Hashtable paramHeader = new Hashtable();
				paramHeader.Add("idAplicacion", ConfigurationSettings.AppSettings["system_ConsultaClave"]);
				paramHeader.Add("usrApp", ConfigurationSettings.AppSettings["system_ConsultaClave"]);
				paramHeader.Add("timestamp", objBEAuditoriaRequest.timestamp);
				paramHeader.Add("msgId", objBEAuditoriaRequest.idTransaccion);
				paramHeader.Add("userId", objBEAuditoriaRequest.userId);
				paramHeader.Add("idTransaccion", objBEAuditoriaRequest.idTransaccion);

				string strWSIP = Funciones.CheckStr(ConfigurationSettings.AppSettings["ProcesarPagosDelivery_wsip"]);
				string strUserEncrypted = Funciones.CheckStr(ConfigurationSettings.AppSettings["GenerarComprobanteElectronico_User"]);
				string strPassEncrypted = Funciones.CheckStr(ConfigurationSettings.AppSettings["GenerarComprobanteElectronico_Pass"]);
				string strTimeout = Funciones.CheckStr(ConfigurationSettings.AppSettings["GenerarComprobanteElectronico_Timeout"]);
				string strnombreProy = "GeneraFacturaElectronicaCloud";

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

				oResponse = PostInvoqueDP.PostInvoque("urlGenerarComprobanteElectronico", paramHeader, objRequest, objBEAuditoriaRequest.userId, objBEAuditoriaRequest.ipApplication, strnombreProy, strWSIP, strUserEncrypted, strPassEncrypted, strTimeout, typeof(GeneraFacturaElectronicaResponse));
				objResp = (GeneraFacturaElectronicaResponse)oResponse;
				return objResp;
			}
			catch (Exception ex)
			{
				objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} : {1}","Excepcion metodo GeneraFacturaElectronicaCloud",objBEAuditoriaRequest.applicationCodeWS));
				objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} : {1}","Excepcion metodo GeneraFacturaElectronicaCloud",Funciones.CheckStr(ex.Message)));
				objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} : {1}","Excepcion metodo GeneraFacturaElectronicaCloud",Funciones.CheckStr(ex.StackTrace)));
				throw;
			}

		}
	}
}
