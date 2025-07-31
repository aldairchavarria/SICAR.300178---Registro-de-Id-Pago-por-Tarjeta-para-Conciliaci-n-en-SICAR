using System;

namespace COM_SIC_Entidades.DataPowerRest.ProcesarPagosDelivery.RestConsultarDatosPago
{
	[Serializable]
	public class MessageResponseConsultarDatosPago
	{
		private DataPowerRest.HeadersGenerics.HeadersResponse _Header;
		private BodyResponseConsultarDatosPago _Body;

		public DataPowerRest.HeadersGenerics.HeadersResponse Header { get { return _Header; } set { _Header = value; } }

		public BodyResponseConsultarDatosPago Body { get { return _Body; } set { _Body = value; } }

		public MessageResponseConsultarDatosPago()
		{
			_Header = new DataPowerRest.HeadersGenerics.HeadersResponse();
			_Body = new BodyResponseConsultarDatosPago();
		}
	}
}
