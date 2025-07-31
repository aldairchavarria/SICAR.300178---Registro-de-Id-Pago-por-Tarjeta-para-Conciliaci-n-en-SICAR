using System;

namespace COM_SIC_Entidades.DataPowerRest.ProcesarPagosDelivery.RestConsultarDatosPago
{
	[Serializable]
	public class RequestConsultarDatosPago
	{
		private MessageRequestConsultarDatosPago _MessageRequest;

		public MessageRequestConsultarDatosPago MessageRequest { get { return _MessageRequest; } set { _MessageRequest = value; } }

		public RequestConsultarDatosPago()
		{
			_MessageRequest = new MessageRequestConsultarDatosPago();
		}
	}
}
