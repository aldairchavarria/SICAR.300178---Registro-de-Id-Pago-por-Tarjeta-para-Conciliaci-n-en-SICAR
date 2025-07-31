using System;
using System.Net;
using System.Collections;
using System.Reflection;
using System.Text;
using System.IO;
using AjaxPro;
using System.Configuration;

namespace COM_SIC_Procesa_Pagos.DataPowerRest
{
	
	public class RestService
	{
		public RestService()
		{

		}

		private static WebHeaderCollection GetHeaders(Hashtable table)
		{
			WebHeaderCollection Headers = new WebHeaderCollection();
			
			foreach (DictionaryEntry entry in table)
			{
				Headers.Add(entry.Key.ToString(), entry.Value!=null? entry.Value.ToString():null);
			}

			string strEncryptedBase64 = DES.GetEncryptedBase64(table);
			

			string sAthorization = "Basic " + strEncryptedBase64;
			if (strEncryptedBase64 != "1" && strEncryptedBase64 != "-1" && strEncryptedBase64 != "-2" && strEncryptedBase64 != "-3")
			{
				Headers.Add("Authorization", sAthorization);
				
			}

			return Headers;
		}
		public static object PostInvoque(string name, Hashtable header, object objRequest, Type objResponse)
		{
			HttpWebRequest request = HttpWebRequest.Create(ConfigurationSettings.AppSettings[name]) as HttpWebRequest;
			request.Method = "POST";
			request.Headers = GetHeaders(header);
			request.Accept = "application/json";
			request.Timeout = Convert.ToInt32(ConfigurationSettings.AppSettings["strRelationPlanTimeout"]);
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

		public static object GetInvoque(string name, Hashtable header, object objRequest, Type objResponse)
		{
			HttpWebRequest request = HttpWebRequest.Create(ConfigurationSettings.AppSettings[name]) as HttpWebRequest;
			request.Method = "GET";
			request.Headers = GetHeaders(header);
			request.Accept = "application/json";
			request.Timeout = Convert.ToInt32(ConfigurationSettings.AppSettings["strRelationPlanTimeout"]);
			request.ContentType = "application/json";			

			WebResponse ws = request.GetResponse();

			using (Stream stream = ws.GetResponseStream())
			{
				StreamReader reader = new StreamReader(stream, Encoding.UTF8);
				string responseString = reader.ReadToEnd();
			
				return JavaScriptDeserializer.DeserializeFromJson(responseString,objResponse) as object;
			}
		}
		
		//INI-1019 - YGP Nuevo metodo POST integrando un nuevo tiempo de espera
		public static object PostInvoque(string name, Hashtable header, object objRequest, Type objResponse, string PlanTimeout)
		{
			HttpWebRequest request = HttpWebRequest.Create(ConfigurationSettings.AppSettings[name]) as HttpWebRequest;
			request.Method = "POST";
			request.Headers = GetHeaders(header);
			request.Accept = "application/json";
			request.Timeout = Convert.ToInt32(ConfigurationSettings.AppSettings[PlanTimeout]);
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
	}
}
