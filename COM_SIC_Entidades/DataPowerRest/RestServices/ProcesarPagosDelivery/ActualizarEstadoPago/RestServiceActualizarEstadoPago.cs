using System;
using System.Text;
using COM_SIC_Activaciones;
using System.Configuration;
using System.Collections;
//using COM_SIC_Entidades.DataPowerRest.ProcesarPagosDelivery. ActualizarEstadoPago.RestActualizarEstadoPago;
using COM_SIC_Entidades.DataPowerRest.MethodsRest.ProcesarPagosDelivery.RestActualizarEstadoPago;
using COM_SIC_Entidades.DataPowerRest.HeadersGenerics;

namespace COM_SIC_Entidades.DataPowerRest.RestServices.ProcesarPagosDelivery.ActualizarEstadoPago
{
	/// <summary>
	/// Summary description for RestServiceActualizarEstadoPago.
	/// </summary>
	public class RestServiceActualizarEstadoPago
	{
		string nameArchivo = "log_ActualizarEstadoPago" + "_" + string.Format("{0:dd-MM-yyyy}", DateTime.Now) + ".txt";
		public bool ActualizarEstadoPago(string ipServer, string usuario_id, string ipcliente, BECabeceraEstadoPago  objCabecera)
		{
			SICAR_Log objFileLog = new SICAR_Log();
			 objFileLog.Log_WriteLog(string.Empty, nameArchivo, "PROY-140533 - IDEA-141814 - Desarrollo automatización de delivery programado - Automatización del pago");

			bool rpta = false;
			try
			{
				string codigoRespuestaServidor = string.Empty;
				string mensajeRespuestaServidor = string.Empty;

				 objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} --> {1}", "[PROY-140533 - ActualizarEstadoPago][objCabecera.uuid]", Funciones.CheckStr(objCabecera.uuid)));
				 objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} --> {1}", "[PROY-140533 - ActualizarEstadoPago][objCabecera.codEstado]", Funciones.CheckStr(objCabecera.codEstado)));
				 objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} --> {1}", "[PROY-140533 - ActualizarEstadoPago][objCabecera.codRpta]", Funciones.CheckStr(objCabecera.codRpta)));
				 objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} --> {1}", "[PROY-140533 - ActualizarEstadoPago][objCabecera.msjRpta]", objCabecera.msjRpta));
				 objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} --> {1}", "[PROY-140533 - ActualizarEstadoPago][objCabecera.nodoSicar]", objCabecera.nodoSicar));
				 objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} --> {1}", "[PROY-140533 - ActualizarEstadoPago][objCabecera.codError]", objCabecera.flagReintentar));
				 objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} --> {1}", "[PROY-140533 - ActualizarEstadoPago][objCabecera.error]", objCabecera.error));

				RestReferences.ProcesarPagosDelivery.ActualizarEstadoPago.RestActualizarEstadoPago objRestActualizarEstadoPago = new RestReferences.ProcesarPagosDelivery.ActualizarEstadoPago.RestActualizarEstadoPago();
				BEAuditoriaRequest objBEAuditoriaRequest = new BEAuditoriaRequest();
				RequestActualizarEstadoPago objRequestActualizarEstadoPago = new RequestActualizarEstadoPago();
				ResponseActualizarEstadoPago objResponseActualizarEstadoPago = new ResponseActualizarEstadoPago();
				BECabeceraEstadoPago objCabeceraEstadoPago = new BECabeceraEstadoPago();
				BodyRequestActualizarEstadoPago objBodyRequestActualizarEstadoPago = new BodyRequestActualizarEstadoPago();
				ArrayList lstListaOpcional = new ArrayList();
				COM_SIC_Entidades.DataPowerRest.HeadersGenerics.HeaderRequest objHeaderRequest = new COM_SIC_Entidades.DataPowerRest.HeadersGenerics.HeaderRequest();


				#region Header
				objHeaderRequest.consumer = Funciones.CheckStr(ConfigurationSettings.AppSettings["ProcesarPagosDelivery_consumer"]);
				objHeaderRequest.country = Funciones.CheckStr(ConfigurationSettings.AppSettings["ProcesarPagosDelivery_country"]);
				objHeaderRequest.dispositivo = ipcliente;
				objHeaderRequest.language = Funciones.CheckStr(ConfigurationSettings.AppSettings["ProcesarPagosDelivery_language"]);
				objHeaderRequest.modulo = Funciones.CheckStr(ConfigurationSettings.AppSettings["ProcesarPagosDelivery_modulo"]);
				objHeaderRequest.msgType = Funciones.CheckStr(ConfigurationSettings.AppSettings["ProcesarPagosDelivery_msgtype"]);
				objHeaderRequest.operation = Funciones.CheckStr(ConfigurationSettings.AppSettings["ProcesarPagosDelivery_operation"]);
				objHeaderRequest.pid = DateTime.Now.ToString("yyyyMMddHHmmssfff");
				objHeaderRequest.system = Funciones.CheckStr(ConfigurationSettings.AppSettings["ProcesarPagosDelivery_system"]);
				objHeaderRequest.timestamp = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
				objHeaderRequest.userId = usuario_id;
				objHeaderRequest.wsIp = ConfigurationSettings.AppSettings["ProcesarPagosDelivery_wsip"];

				 objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} --> {1}", "[PROY-140533 - ActualizarEstadoPago][objHeaderRequest.consumer]", Funciones.CheckStr(objHeaderRequest.consumer)));
				 objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} --> {1}", "[PROY-140533 - ActualizarEstadoPago][objHeaderRequest.country]", Funciones.CheckStr(objHeaderRequest.country)));
				 objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} --> {1}", "[PROY-140533 - ActualizarEstadoPago][objHeaderRequest.dispositivo]", Funciones.CheckStr(objHeaderRequest.dispositivo)));
				 objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} --> {1}", "[PROY-140533 - ActualizarEstadoPago][objHeaderRequest.modulo]", Funciones.CheckStr(objHeaderRequest.modulo)));
				 objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} --> {1}", "[PROY-140533 - ActualizarEstadoPago][objHeaderRequest.msgType]", Funciones.CheckStr(objHeaderRequest.msgType)));
				 objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} --> {1}", "[PROY-140533 - ActualizarEstadoPago][objHeaderRequest.operation]", Funciones.CheckStr(objHeaderRequest.operation)));
				 objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} --> {1}", "[PROY-140533 - ActualizarEstadoPago][objHeaderRequest.pid]", Funciones.CheckStr(objHeaderRequest.pid)));
				 objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} --> {1}", "[PROY-140533 - ActualizarEstadoPago][objHeaderRequest.system]", Funciones.CheckStr(objHeaderRequest.system)));
				 objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} --> {1}", "[PROY-140533 - ActualizarEstadoPago][objHeaderRequest.timestamp]", Funciones.CheckStr(objHeaderRequest.timestamp)));
				 objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} --> {1}", "[PROY-140533 - ActualizarEstadoPago][objHeaderRequest.userId]", Funciones.CheckStr(objHeaderRequest.userId)));
				 objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} --> {1}", "[PROY-140533 - ActualizarEstadoPago][objHeaderRequest.wsIp]", Funciones.CheckStr(objHeaderRequest.wsIp)));
				objRequestActualizarEstadoPago.MessageRequest.Header.HeaderRequest = objHeaderRequest;
				#endregion


				#region Body

				objCabeceraEstadoPago.uuid = Funciones.CheckStr(objCabecera.uuid);
				objCabeceraEstadoPago.codEstado =Funciones.CheckStr(objCabecera.codEstado);
				objCabeceraEstadoPago.codRpta = Funciones.CheckStr(objCabecera.codRpta);
				objCabeceraEstadoPago.msjRpta = Funciones.CheckStr(objCabecera.msjRpta);
				objCabeceraEstadoPago.nodoSicar = Funciones.CheckStr(objCabecera.nodoSicar);
				objCabeceraEstadoPago.flagReintentar = Funciones.CheckStr(objCabecera.flagReintentar);
				objCabeceraEstadoPago.error = Funciones.CheckStr(objCabecera.error);

				BEListaOpcional ojListaOpcional = new BEListaOpcional();
				ojListaOpcional.campo = string.Empty;
				ojListaOpcional.valor = string.Empty;
				lstListaOpcional.Add(ojListaOpcional);
				
				objCabeceraEstadoPago.listaOpcional = lstListaOpcional;




				 objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} --> {1}", "[PROY-140533 - ActualizarEstadoPago][objCabeceraEstadoPago.uuid]", Funciones.CheckStr(objCabeceraEstadoPago.uuid)));
				 objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} --> {1}", "[PROY-140533 - ActualizarEstadoPago][objCabeceraEstadoPagoC.codEstado]", Funciones.CheckStr(objCabeceraEstadoPago.codEstado)));
				 objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} --> {1}", "[PROY-140533 - ActualizarEstadoPago][objCabeceraEstadoPago.codRpta]", Funciones.CheckStr(objCabeceraEstadoPago.codRpta)));
				 objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} --> {1}", "[PROY-140533 - ActualizarEstadoPago][objCabeceraEstadoPago.msjRpta]", Funciones.CheckStr(objCabeceraEstadoPago.msjRpta)));
				 objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} --> {1}", "[PROY-140533 - ActualizarEstadoPago][objCabeceraEstadoPago.nodoSicar]", Funciones.CheckStr(objCabeceraEstadoPago.nodoSicar)));
				 objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} --> {1}", "[PROY-140533 - ActualizarEstadoPago][objCabeceraEstadoPago.codError]", Funciones.CheckStr(objCabeceraEstadoPago.flagReintentar)));
				objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} --> {1}", "[PROY-140533 - ActualizarEstadoPago][objCabeceraEstadoPago.error]", Funciones.CheckStr(objCabeceraEstadoPago.error)));

				objBodyRequestActualizarEstadoPago.actualizarEstadoPagoRequest = objCabeceraEstadoPago;
				objRequestActualizarEstadoPago.MessageRequest.Body = objBodyRequestActualizarEstadoPago;
				#endregion


				#region Auditoria
				objBEAuditoriaRequest.idTransaccion = DateTime.Now.ToString("yyyyMMddHHmmssfff");
				objBEAuditoriaRequest.timestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"); 
				objBEAuditoriaRequest.userId = Funciones.CheckStr(ConfigurationSettings.AppSettings["system_ConsultaClave"]);
				objBEAuditoriaRequest.msgId = Funciones.CheckStr(ConfigurationSettings.AppSettings["system_ConsultaClave"]);
				objBEAuditoriaRequest.accept = "application/json";
				objBEAuditoriaRequest.ipApplication = ipServer;
				#endregion

				#region Response
				objResponseActualizarEstadoPago = objRestActualizarEstadoPago.ActualizarEstadoPago(objRequestActualizarEstadoPago, objBEAuditoriaRequest);

				codigoRespuestaServidor = objResponseActualizarEstadoPago.MessageResponse.Body.auditResponse.codigoRespuesta;
				mensajeRespuestaServidor = objResponseActualizarEstadoPago.MessageResponse.Body.auditResponse.mensajeRespuesta;

				 objFileLog.Log_WriteLog(string.Empty, nameArchivo, "[PROY-140533 - ActualizarEstadoPago] -> objResponseActualizarEstadoPago.codigoRespuestaServidor : " + codigoRespuestaServidor);
				 objFileLog.Log_WriteLog(string.Empty, nameArchivo, "[PROY-140533 - ActualizarEstadoPago] -> objResponseActualizarEstadoPago.mensajeRespuestaServidor : " + mensajeRespuestaServidor);
				#endregion

				if (codigoRespuestaServidor == "0")
				{
					rpta = true;
				}

			}
			catch (Exception ex)
			{
				 objFileLog.Log_WriteLog(string.Empty, nameArchivo, string.Format("{0} => [{1}|{2}]", "[PROY-140533 - ActualizarEstadoPago][Error]", ex.Message, ex.StackTrace));
			}
			 objFileLog.Log_WriteLog(string.Empty, nameArchivo, "PROY-140533 - IDEA-141814 - Desarrollo automatización de delivery programado - Automatización del pago");

			return rpta;
		}
	}
}
