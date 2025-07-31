using System;

namespace COM_SIC_Entidades.DataPowerRest.ProcesarPagosDelivery.RestConsultarDatosPago
{
	[Serializable]
	public class ResponseConsultarDatosPago
	{
		private MessageResponseConsultarDatosPago _MessageResponse;

		public MessageResponseConsultarDatosPago MessageResponse { get { return _MessageResponse; } set { _MessageResponse = value; } }

		public ResponseConsultarDatosPago()
		{
			_MessageResponse = new MessageResponseConsultarDatosPago();
		}
	}
}
