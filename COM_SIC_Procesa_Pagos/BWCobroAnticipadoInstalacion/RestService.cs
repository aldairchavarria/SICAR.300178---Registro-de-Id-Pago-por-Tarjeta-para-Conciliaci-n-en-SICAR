using System;
using System.Net;
using System.Collections;
using System.Reflection;
using System.Text;
using System.IO;
using AjaxPro;
using COM_SIC_Activaciones;
using System.Configuration;

namespace COM_SIC_Procesa_Pagos.BWCobroAnticipadoInstalacion
{
	/// <summary>
	/// Summary description for RestService.
	/// </summary>
	public class RestService
	{
		

		public RestService()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		
		//SICAR_Log objFileLog = new SICAR_Log();

		private static WebHeaderCollection GetHeaders(Hashtable table)
		{
			WebHeaderCollection Headers = new WebHeaderCollection();
			foreach (DictionaryEntry entry in table)
			{
				Headers.Add(entry.Key.ToString(), entry.Value!=null? entry.Value.ToString():null);
			}

			string strEncryptedBase64 = DataPowerRest.DES.GetEncryptedBase64(table);

			if (strEncryptedBase64 != "1" && strEncryptedBase64 != "-1" && strEncryptedBase64 != "-2" && strEncryptedBase64 != "-3")
			{
				Headers.Add("Authorization", "Basic " + strEncryptedBase64);
			}

		
			return Headers;
		}


		public static object PostInvoque(string name, Hashtable header, object objRequest, Type objResponse)
		{
			HttpWebRequest request = HttpWebRequest.Create(ConfigurationSettings.AppSettings[name]) as HttpWebRequest;
			request.Method = "POST";
			request.Headers = GetHeaders(header);
			request.Accept = "application/json";
			request.Timeout = Convert.ToInt32(ConfigurationSettings.AppSettings["TimeOut_TryBuy"]);
			request.ContentType = "application/json";			
			string data = JavaScriptSerializer.Serialize(objRequest);
			byte[] byteArray = Encoding.UTF8.GetBytes(data);
			request.ContentLength = byteArray.Length;
			Stream dataStream = request.GetRequestStream();
			dataStream.Write(byteArray, 0, byteArray.Length);
			dataStream.Close();

			WebResponse ws = request.GetResponse();

			using (Stream stream = ws.GetResponseStream())
			{
				StreamReader reader = new StreamReader(stream, Encoding.UTF8);
				string responseString = reader.ReadToEnd();
				return JavaScriptDeserializer.DeserializeFromJson(responseString,objResponse) as object;
			}
		}
		
		public static object GetInvoque(string name, Hashtable header, ArrayList parametros, Type objResponse)
		{
			string url = ConfigurationSettings.AppSettings[name];
			
			string parametrosConcatenados = ConcatParametros(parametros);
			string urlConParametros = url + "?" + parametrosConcatenados;

			HttpWebRequest request = HttpWebRequest.Create(urlConParametros) as HttpWebRequest;
                
			request.Method = "GET";
			request.Headers = GetHeaders(header); //Para el DataPower.
			request.Accept = "application/json";
			request.Timeout = Convert.ToInt32(ConfigurationSettings.AppSettings["TimeOut_TryBuy"]);
					
			WebResponse ws = request.GetResponse();

			using (Stream stream = ws.GetResponseStream())
			{
				StreamReader reader = new StreamReader(stream, Encoding.UTF8);
				String responseString = reader.ReadToEnd();
				return JavaScriptDeserializer.DeserializeFromJson(responseString, objResponse) as object;
					
			}
		}

			
		private static string ConcatParametros(ArrayList parameters)
		{
			bool FirstParam = true;
			StringBuilder Parametros = null;

			if (parameters != null)
			{
				Parametros = new StringBuilder();
				foreach (string param in parameters)
				{
					Parametros.Append(FirstParam ? "" : "&");
					Parametros.Append(param);
					FirstParam = false;
				}
			}
			return Parametros == null ? string.Empty : Parametros.ToString();
		}
 
		//PROY-140372 INI
		public static object PostInvoque_Generic(CredencialesDPRest objCredencialesDPRest, Hashtable header, object objRequest, Type objResponse)
		{
			SICAR_Log objFileLog = new SICAR_Log();
			string nameArchivo = "log_ProgramarTareaRepo" + "_" + string.Format("{0:dd-MM-yyyy}", DateTime.Now) + ".txt";
			try 
			{
				HttpWebRequest request = HttpWebRequest.Create(objCredencialesDPRest.urlServicio) as HttpWebRequest;
				request.Method = "POST";
				request.Headers = GetHeaders_Generic(header,objCredencialesDPRest);
				request.Accept = "application/json";
				request.Timeout = Convert.ToInt32(objCredencialesDPRest.timeOutServicio);
				request.ContentType = "application/json";			
				string data = JavaScriptSerializer.Serialize(objRequest);
				objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} : {1}","Request: ", Funciones.CheckStr(data)));
				byte[] byteArray = Encoding.UTF8.GetBytes(data);
				request.ContentLength = byteArray.Length;
				Stream dataStream = request.GetRequestStream();
				dataStream.Write(byteArray, 0, byteArray.Length);
				dataStream.Close();

				WebResponse ws = request.GetResponse();

				using (Stream stream = ws.GetResponseStream())
				{
					StreamReader reader = new StreamReader(stream, Encoding.UTF8);
					string responseString = reader.ReadToEnd();
					objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} : {1}","Response: ", Funciones.CheckStr(responseString)));
					return JavaScriptDeserializer.DeserializeFromJson(responseString,objResponse) as object;
				}
			} 
			catch (WebException wex)
			{
				objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} : {1}","Catch Error: ", Funciones.CheckStr(wex.Message)));
				using (WebResponse WebRespException = wex.Response)
				{
					HttpWebResponse httpResponse = (HttpWebResponse)WebRespException;
                    
					using (Stream data = WebRespException.GetResponseStream())
					using (StreamReader reader = new StreamReader(data))
					{
						string text = reader.ReadToEnd();
						objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} : {1}","text Error: ", Funciones.CheckStr(text)));
					}
				}
				return null;

			}
		} 
		
		private static WebHeaderCollection GetHeaders_Generic(Hashtable table,CredencialesDPRest objCredencialesDPRest)
		{
			WebHeaderCollection Headers = new WebHeaderCollection();
			foreach (DictionaryEntry entry in table)
			{
				Headers.Add(entry.Key.ToString(), entry.Value!=null? entry.Value.ToString():null);
			}

			string strEncryptedBase64 = DES.GetEncryptedBase64_Generic(table,objCredencialesDPRest);

			if (strEncryptedBase64 != "1" && strEncryptedBase64 != "-1" && strEncryptedBase64 != "-2" && strEncryptedBase64 != "-3")
			{
				Headers.Add("Authorization", "Basic " + strEncryptedBase64);
			}

		
			return Headers;
		}
		//PROY-140372 FIN


	}
	//PROY-140332 FIN
}
