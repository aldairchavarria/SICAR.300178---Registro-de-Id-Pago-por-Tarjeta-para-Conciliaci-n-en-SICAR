using System;
using AjaxPro;
//using Claro.SisAct.Common;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Net;
using System.Text;
using COM_SIC_Activaciones;

namespace COM_SIC_Entidades.DataPowerRest.RestReferences
{
	/// <summary>
	/// Summary description for GetHeadersDP.
	/// </summary>
	public class GetHeadersDP
	{
		public static WebHeaderCollection GetHeaders(Hashtable table, string strusuario, string stripServidor, string strnombreProy, string strWSIP, string strUserEncrypted, string strPassEncrypted)
		{
			
			SICAR_Log objFileLog = new SICAR_Log();
			string nameArchivo = "log_GetHeaders" + "_" + string.Format("{0:dd-MM-yyyy}", DateTime.Now) + ".txt";

			objFileLog.Log_WriteLog(string.Empty, nameArchivo, "INICIO GetHeaders()");

			WebHeaderCollection Headers = new WebHeaderCollection();
			string codigoResultado = string.Empty;
			string mensajeResultado = string.Empty;
			string usuarioAplicacion = string.Empty;
			string clave = string.Empty;
			bool respuesta = false;

			foreach (DictionaryEntry entry in table)
			{
				Headers.Add(entry.Key.ToString(), entry.Value != null ? entry.Value.ToString() : null);
				objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0}  --> {1} = {2}","Headers",entry.Key.ToString(),entry.Value.ToString()));
			}

			objFileLog.Log_WriteLog(string.Empty, nameArchivo, "INICIO SERVICIO DESENCRIPTACION  --> BWConsultaClaves.ConsultaClaveWS");

			AuditoriaEWS sAuditoriastring_ = new AuditoriaEWS();
			
			string ipTransaccion = DateTime.Now.ToString("YYYYMMDDHHMISSMS");
			string wsIp = (strWSIP == string.Empty || strWSIP == null) ? stripServidor : strWSIP;
			string usrAplicacion = strusuario;
			string codigoAplicacion = ConfigurationSettings.AppSettings["constAplicacion"];
			string idAplicacion = ConfigurationSettings.AppSettings["system_ConsultaClave"];
			string usuarioAplicacionEncriptado = strUserEncrypted;
			string claveEncriptada = strPassEncrypted;
			string mensajeError = string.Empty;

			sAuditoriastring_.IDTRANSACCION = ipTransaccion;
			sAuditoriastring_.IPAPLICACION = idAplicacion;
			sAuditoriastring_.APLICACION = codigoAplicacion;
			sAuditoriastring_.USRAPP = strusuario;

			objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0}  --> {1}","wsIp",wsIp));
			objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0}  --> {1}","ipServidor",stripServidor));
			objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0}  --> {1}","usrAplicacion",usrAplicacion));
			objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0}  --> {1}","codigoAplicacion",codigoAplicacion));
			objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0}  --> {1}","idAplicacion",idAplicacion));
			objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0}  --> {1}","ipTransaccion",ipTransaccion));
			objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0}  --> {1}","usuarioAplicacionEncriptado",usuarioAplicacionEncriptado));
			objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0}  --> {1}", "claveEncriptada", claveEncriptada));

			respuesta = new ConsultaClavesNegocio().ejecutarConsultaClave(sAuditoriastring_,
			    codigoAplicacion,
			    usuarioAplicacionEncriptado,
			    claveEncriptada,
			    ref usuarioAplicacion,
			    ref clave,
				ref mensajeError
			    );

			objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0}  --> {1}","respuesta",Funciones.CheckStr(respuesta)));
			objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0}  --> {1}","mensajeError",mensajeError));
			objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0}  --> {1}","Usuario",usuarioAplicacion));
			objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0}  --> {1}", "Clave", clave));

//			usuarioAplicacion = "";
//			clave = "";

			string strEncryptedBase64 = string.Empty;

			if (respuesta)
			{
				objFileLog.Log_WriteLog(string.Empty, nameArchivo, "Ingreso a encodificar user y pass");
				strEncryptedBase64 = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", usuarioAplicacion, clave)));

				if (strEncryptedBase64 != "1" && strEncryptedBase64 != "-1" && strEncryptedBase64 != "-2" && strEncryptedBase64 != "-3")
				{
					Headers.Add("Authorization", String.Format("{0} {1}","Basic",strEncryptedBase64));
				}
			}

			objFileLog.Log_WriteLog(string.Empty, nameArchivo, "FIN GetHeaders()");

			return Headers;
		}
	}
}
