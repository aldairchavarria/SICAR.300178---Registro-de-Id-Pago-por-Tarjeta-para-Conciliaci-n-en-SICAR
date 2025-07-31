using System;
using System.Text;
using System.Collections;
using COM_SIC_Activaciones;
using System.Configuration;
using COM_SIC_Entidades.DataPowerRest.ProcesarPagosDelivery.RestConsultarDatosPago;
using COM_SIC_Entidades.DataPowerRest.HeadersGenerics;

namespace COM_SIC_Entidades.DataPowerRest.RestServices.ConsultarDatosPago
{
	public class RestServiceConsultarDatosPago
	{
		string nameArchivo = "log_ActualizarEstadoPago" + "_" + string.Format("{0:dd-MM-yyyy}", DateTime.Now) + ".txt";

		public bool ConsultarDatosPago(string strIdentificador,string strPedido, string ipServer, string usuario_id, string ipcliente, BECabeceraDatosPago objCabecera, ref BEDetalleDatosPago detallePago)
		{
			SICAR_Log objFileLog = new SICAR_Log();
			objFileLog.Log_WriteLog(string.Empty, nameArchivo, "PROY-140533 - IDEA-141814 - Desarrollo automatización de delivery programado - Automatización del pago");

			ResponseConsultarDatosPago objResponseConsultarDatosPago = new ResponseConsultarDatosPago();

			bool rpta = false;
			try
			{
				
				string codigoRespuestaServidor = string.Empty;
				string mensajeRespuestaServidor = string.Empty;
				ArrayList listaDetalle = new ArrayList();
				BEDetalleDatosPago[] lstdetallePago = null;

				objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} --> {1}", "[PROY-140533 - ConsultarDatosPago][objCabecera.uuid]", Funciones.CheckStr(objCabecera.uuid)));
				objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} --> {1}", "[PROY-140533 - ConsultarDatosPago][objCabecera.nroPedido]", Funciones.CheckStr(objCabecera.nroPedido)));

				RestReferences.ProcesarPagosDelivery.ConsultarDatosPago.RestConsultarDatosPago objRestConsultarDatosPago = new RestReferences.ProcesarPagosDelivery.ConsultarDatosPago.RestConsultarDatosPago();
				BEAuditoriaRequest objBEAuditoriaRequest = new BEAuditoriaRequest();
				RequestConsultarDatosPago objRequestConsultarDatosPago = new RequestConsultarDatosPago();
				
				BECabeceraDatosPago objCabeceraDatosPago = new BECabeceraDatosPago();
				BodyRequestConsultarDatosPago objBodyRequestConsultarDatosPago = new BodyRequestConsultarDatosPago();
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

				objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} --> {1}", "[PROY-140533 - ConsultarDatosPago][objHeaderRequest.consumer]", Funciones.CheckStr(objHeaderRequest.consumer)));
				objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} --> {1}", "[PROY-140533 - ConsultarDatosPago][objHeaderRequest.country]", Funciones.CheckStr(objHeaderRequest.country)));
				objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} --> {1}", "[PROY-140533 - ConsultarDatosPago][objHeaderRequest.dispositivo]", Funciones.CheckStr(objHeaderRequest.dispositivo)));
				objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} --> {1}", "[PROY-140533 - ConsultarDatosPago][objHeaderRequest.modulo]", Funciones.CheckStr(objHeaderRequest.modulo)));
				objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} --> {1}", "[PROY-140533 - ConsultarDatosPago][objHeaderRequest.msgType]", Funciones.CheckStr(objHeaderRequest.msgType)));
				objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} --> {1}", "[PROY-140533 - ConsultarDatosPago][objHeaderRequest.operation]", Funciones.CheckStr(objHeaderRequest.operation)));
				objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} --> {1}", "[PROY-140533 - ConsultarDatosPago][objHeaderRequest.pid]", Funciones.CheckStr(objHeaderRequest.pid)));
				objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} --> {1}", "[PROY-140533 - ConsultarDatosPago][objHeaderRequest.system]", Funciones.CheckStr(objHeaderRequest.system)));
				objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} --> {1}", "[PROY-140533 - ConsultarDatosPago][objHeaderRequest.timestamp]", Funciones.CheckStr(objHeaderRequest.timestamp)));
				objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} --> {1}", "[PROY-140533 - ConsultarDatosPago][objHeaderRequest.userId]", Funciones.CheckStr(objHeaderRequest.userId)));
				objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} --> {1}", "[PROY-140533 - ConsultarDatosPago][objHeaderRequest.wsIp]", Funciones.CheckStr(objHeaderRequest.wsIp)));
				objRequestConsultarDatosPago.MessageRequest.Header.HeaderRequest = objHeaderRequest;
				#endregion


				#region Body

				objCabeceraDatosPago.uuid = Funciones.CheckStr(objCabecera.uuid);
				objCabeceraDatosPago.nroPedido = Funciones.CheckStr(objCabecera.nroPedido);

				BEListaOpcional ojListaOpcional = new BEListaOpcional();
				ojListaOpcional.campo = string.Empty;
				ojListaOpcional.valor = string.Empty;
				lstListaOpcional.Add(ojListaOpcional);
				
				objCabeceraDatosPago.listaOpcional = lstListaOpcional;




				objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} --> {1}", "[PROY-140533 - ConsultarDatosPago][objCabeceraDatosPago.uuid]", Funciones.CheckStr(objCabeceraDatosPago.uuid)));
				objFileLog.Log_WriteLog(string.Empty, nameArchivo, String.Format("{0} --> {1}", "[PROY-140533 - ConsultarDatosPago][objCabeceraDatosPagoC.codEstado]", Funciones.CheckStr(objCabeceraDatosPago.nroPedido)));

				objBodyRequestConsultarDatosPago.consultarDatosPagoRequest = objCabeceraDatosPago;
				objRequestConsultarDatosPago.MessageRequest.Body = objBodyRequestConsultarDatosPago;
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
				objResponseConsultarDatosPago = objRestConsultarDatosPago.ConsultarDatosPago(objRequestConsultarDatosPago, objBEAuditoriaRequest);

				codigoRespuestaServidor = objResponseConsultarDatosPago.MessageResponse.Body.auditResponse.codigoRespuesta;
				mensajeRespuestaServidor = objResponseConsultarDatosPago.MessageResponse.Body.auditResponse.mensajeRespuesta;
				lstdetallePago = (BEDetalleDatosPago[])objResponseConsultarDatosPago.MessageResponse.Body.datosTransPagDlv;

				objFileLog.Log_WriteLog(string.Empty, nameArchivo, "[PROY-140533 - ConsultarDatosPago] -> objResponseConsultarDatosPago.codigoRespuestaServidor : " + codigoRespuestaServidor);
				objFileLog.Log_WriteLog(string.Empty, nameArchivo, "[PROY-140533 - ConsultarDatosPago] -> objResponseConsultarDatosPago.mensajeRespuestaServidor : " + mensajeRespuestaServidor);
				
				#endregion

				if (codigoRespuestaServidor == "0" && lstdetallePago != null)
				{
					detallePago = lstdetallePago[0];
					rpta = true;
				}

			}
			catch (Exception ex)
			{
				objFileLog.Log_WriteLog(string.Empty, nameArchivo, string.Format("{0} => [{1}|{2}]", "[PROY-140533 - ConsultarDatosPago][Error]", ex.Message, ex.StackTrace));
			}

			objFileLog.Log_WriteLog(string.Empty, nameArchivo, "PROY-140533 - IDEA-141814 - Desarrollo automatización de delivery programado - Automatización del pago");

			return rpta;

		}
	}
}
