using System;

namespace COM_SIC_Entidades.DataPowerRest.MethodsRest.ProcesarPagosDelivery.RestActualizarEstadoPago
{
	[Serializable]
	public class RequestActualizarEstadoPago
	{
		private MessageRequestActualizarEstadoPago _MessageRequest;

		public MessageRequestActualizarEstadoPago MessageRequest { get { return _MessageRequest; } set { _MessageRequest = value; } }

		public RequestActualizarEstadoPago()
		{
			_MessageRequest = new MessageRequestActualizarEstadoPago();
		}
	}
}
