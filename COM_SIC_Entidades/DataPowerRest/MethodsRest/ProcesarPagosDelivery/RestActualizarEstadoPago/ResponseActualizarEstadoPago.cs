using System;

namespace COM_SIC_Entidades.DataPowerRest.MethodsRest.ProcesarPagosDelivery.RestActualizarEstadoPago
{
	[Serializable]
	public class ResponseActualizarEstadoPago
	{
		private MessageResponseActualizarEstadoPago _MessageResponse;

		public MessageResponseActualizarEstadoPago MessageResponse { get { return _MessageResponse; } set { _MessageResponse = value; } }

		public ResponseActualizarEstadoPago()
		{
			_MessageResponse = new MessageResponseActualizarEstadoPago();
		}
	}
}
