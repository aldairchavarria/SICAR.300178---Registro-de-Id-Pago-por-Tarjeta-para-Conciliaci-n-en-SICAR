using System;

namespace COM_SIC_Entidades.DataPowerRest.MethodsRest.ProcesarPagosDelivery.RestActualizarEstadoPago
{
	[Serializable]
	public class MessageResponseActualizarEstadoPago
	{
		private DataPowerRest.HeadersGenerics.HeadersResponse _Header;
		private BodyResponseActualizarEstadoPago _Body;

		public DataPowerRest.HeadersGenerics.HeadersResponse Header { get { return _Header; } set { _Header = value; } }

		public BodyResponseActualizarEstadoPago Body { get { return _Body; } set { _Body = value; } }

		public MessageResponseActualizarEstadoPago()
		{
			_Header = new DataPowerRest.HeadersGenerics.HeadersResponse();
			_Body = new BodyResponseActualizarEstadoPago();
		}
	}
}
