using System;
//using COM_SIC_FacturaElectronica.GeneraFacturaElectronica;
//using COM_SIC_FacturaElectronica.DataPower.Request;
using System.Collections;
using COM_SIC_Entidades.DataPowerRest.HeadersGenerics;
using COM_SIC_Entidades.DataPowerRest.RestReferences.GenerarFacturaElectronica;
using COM_SIC_Entidades.DataPowerRest.MethodsRest.GeneraFacturaElectronica;
using System.Configuration;

namespace COM_SIC_FacturaElectronica
{
	/// <summary>
	/// Clase que se encarga de llamar al facturador cloud
	/// </summary>
	public class FacturaElectronicaControlador
	{
		public FacturaElectronicaControlador()
		{
			//
			// TODO: Add constructor logic here
			//
		}

//sRespuesta = objFE.OnlineGeneration(pRuc,pLogin,pClave,pTrama,int.Parse(pTipoFolacion),true,int.Parse(pTipoRetorno),true);
		public void GenerarFactura(OnlineGenerationRequest pRequest,out string pCodigo,out string pMensaje)
		{
			
			BEAuditoriaRequest oAuditoriaRequest = new BEAuditoriaRequest();

			
			string [] arrHeaderRequest = COM_SIC_Seguridad.ReadKeySettings.Key_HeaderResquest.Split('|');

			GeneraFacturaElectronicaRequest oRequestGeneral = new GeneraFacturaElectronicaRequest();
			HeaderRequest objHeaderRequest = new HeaderRequest();
			objHeaderRequest.consumer = arrHeaderRequest[0];
            objHeaderRequest.country = arrHeaderRequest[1];
            objHeaderRequest.dispositivo = arrHeaderRequest[2];
            objHeaderRequest.language = arrHeaderRequest[3];
            objHeaderRequest.modulo = arrHeaderRequest[4];
            objHeaderRequest.msgType = arrHeaderRequest[5];
            objHeaderRequest.operation = "generarComprobante";
            objHeaderRequest.pid = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            objHeaderRequest.system = arrHeaderRequest[4];
            objHeaderRequest.timestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"); 
            objHeaderRequest.userId = DateTime.Now.ToString("ddMMyyyy");
            objHeaderRequest.wsIp = ConfigurationSettings.AppSettings["ProcesarPagosDelivery_wsip"];
	
			HeadersRequest oHeader = new  HeadersRequest();
			oHeader.HeaderRequest = objHeaderRequest;
			MessageRequest oMessageRequest = new MessageRequest();
			oMessageRequest.Header = oHeader;
			oRequestGeneral.MessageRequest = oMessageRequest;
			Hashtable oListHeaders = new Hashtable();
			

			BodyRequest objBody = new BodyRequest();
			objBody.onlineGeneration = pRequest;
			oMessageRequest.Body = objBody;

			oAuditoriaRequest.idTransaccion = DateTime.Now.ToString("yyyyMMddHHmmssfff");
			oAuditoriaRequest.timestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"); 
			oAuditoriaRequest.userId = ConfigurationSettings.AppSettings["system_ConsultaClave"];
			oAuditoriaRequest.msgId =ConfigurationSettings.AppSettings["system_ConsultaClave"];
//			oAuditoriaRequest.accept = "application/json";
			oAuditoriaRequest.ipApplication = ConfigurationSettings.AppSettings["ProcesarPagosDelivery_wsip"];//ipServer

			RestGenerarFacturaElectronica objRest = new RestGenerarFacturaElectronica();
			GeneraFacturaElectronicaResponse oResponse = objRest.GeneraFacturaElectronica(oRequestGeneral,oAuditoriaRequest);

			pCodigo = oResponse.MessageResponse.Body.codigo;
			pMensaje = oResponse.MessageResponse.Body.mensaje;

		
		}

	}
}
