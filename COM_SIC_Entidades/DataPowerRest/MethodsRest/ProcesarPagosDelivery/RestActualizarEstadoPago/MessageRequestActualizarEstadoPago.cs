using System;

namespace COM_SIC_Entidades.DataPowerRest.MethodsRest.ProcesarPagosDelivery.RestActualizarEstadoPago
{
	[Serializable]
	public class MessageRequestActualizarEstadoPago
	{
		private DataPowerRest.HeadersGenerics.HeadersRequest _Header;
		private BodyRequestActualizarEstadoPago _Body;

		public DataPowerRest.HeadersGenerics.HeadersRequest Header { get { return _Header; } set { _Header = value; } }

		public BodyRequestActualizarEstadoPago Body { get { return _Body; } set { _Body = value; } }


		public MessageRequestActualizarEstadoPago()
		{
			_Header = new DataPowerRest.HeadersGenerics.HeadersRequest();
			_Body = new BodyRequestActualizarEstadoPago();
		}
	}
}
