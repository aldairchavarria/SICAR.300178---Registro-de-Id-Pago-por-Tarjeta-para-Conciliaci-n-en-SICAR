using System;
using AjaxPro;
//using Claro.SisAct.Common;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Net;
using System.Text;
using COM_SIC_Activaciones;
using COM_SIC_Entidades.DataPowerRest.HeadersGenerics;

namespace COM_SIC_Entidades.DataPowerRest.RestReferences
{
	/// <summary>
	/// Summary description for PostInvoqueDP.
	/// </summary>
	public class PostInvoqueDP
	{
		static string nameLog = "Log-InvoqueRestServiceDP";
		
		//private static string strArchivo = "Log_PostInvoque";
		public static object PostInvoque(string name, Hashtable objHeader, object obj, string usuario, string ipServidor, string strnombreProy, string strWSIP, string strUserEncrypted, string strPassEncrypted, string strTimeout, Type objResponse)
		{
			SICAR_Log objFileLog = new SICAR_Log();
			
			try
			{
				
				objFileLog.Log_WriteLog(String.Empty, nameLog, "[INICIO][InvokeRestService]");
				HttpWebRequest request = HttpWebRequest.Create(ConfigurationSettings.AppSettings[name]) as HttpWebRequest;
				request.Method = "POST";
				request.Headers = GetHeadersDP.GetHeaders(objHeader, usuario, ipServidor, strnombreProy, strWSIP, strUserEncrypted, strPassEncrypted);
				request.Accept = "application/json";
				request.ContentType = "application/json";

				string data = JavaScriptSerializer.Serialize(obj);
				objFileLog.Log_WriteLog(String.Empty, nameLog, String.Format("{0}: {1}", "[InvokeRestService][data]", Funciones.CheckStr(data)));
				byte[] byteArray = Encoding.UTF8.GetBytes(data);
				request.Timeout = Funciones.CheckInt(strTimeout);
				request.ContentLength = byteArray.Length;
				Stream dataStream = request.GetRequestStream();
				dataStream.Write(byteArray, 0, byteArray.Length);
				dataStream.Close();
				WebResponse ws = request.GetResponse();

				using (Stream stream = ws.GetResponseStream())
				{
					StreamReader reader = new StreamReader(stream, Encoding.UTF8);
					String responseString = reader.ReadToEnd();
					objFileLog.Log_WriteLog(String.Empty, nameLog, String.Format("{0}: {1}", "[InvokeRestService][responseString]", Funciones.CheckStr(responseString)));
					object result = JavaScriptDeserializer.DeserializeFromJson(responseString, objResponse);
					objFileLog.Log_WriteLog(String.Empty, nameLog, "[FIN][InvokeRestService]");
					return result;
				}
			}
			catch (WebException wex)
			{
			//PROY-140621 Inicio
				objFileLog.Log_WriteLog(String.Empty, nameLog, String.Format("{0} : {1}", "Error WebException description", wex.Message));
				objFileLog.Log_WriteLog(String.Empty, nameLog, String.Format("{0} : {1}", "Error WebException Source", wex.Source));
				objFileLog.Log_WriteLog(String.Empty, nameLog, String.Format("{0} : {1}", "Error WebException StackTrace", wex.StackTrace));

				if (wex.Response != null)
				{
					using (WebResponse WebRespException = wex.Response)
					{
						HttpWebResponse httpResponse = (HttpWebResponse)WebRespException;

						objFileLog.Log_WriteLog(String.Empty, nameLog, String.Format("{0} : {1}", "Error Code", httpResponse.StatusCode.ToString()));
						
                    
						using (Stream data = WebRespException.GetResponseStream())
						using (StreamReader reader = new StreamReader(data))
						{
							string text = reader.ReadToEnd();
							objFileLog.Log_WriteLog(String.Empty, nameLog, "error: " + text);
						}
					}
				}
				return null;
			}
			catch (Exception ex)
			{
				objFileLog.Log_WriteLog(String.Empty, nameLog, String.Format("{0} : {1}", "Error Exception description", ex.Message));
				objFileLog.Log_WriteLog(String.Empty, nameLog, String.Format("{0} : {1}", "Error Exception Source", ex.Source));
				objFileLog.Log_WriteLog(String.Empty, nameLog, String.Format("{0} : {1}", "Error Exception StackTrace", ex.StackTrace));

				return null;

			}
			//PROY-140621 Fin
			
		}
	}
}
