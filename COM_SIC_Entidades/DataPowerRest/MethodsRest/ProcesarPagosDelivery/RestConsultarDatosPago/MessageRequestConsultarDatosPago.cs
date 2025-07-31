using System;

namespace COM_SIC_Entidades.DataPowerRest.ProcesarPagosDelivery.RestConsultarDatosPago
{
	[Serializable]
	public class MessageRequestConsultarDatosPago
	{
		private DataPowerRest.HeadersGenerics.HeadersRequest _Header;
		private BodyRequestConsultarDatosPago _Body;

		public DataPowerRest.HeadersGenerics.HeadersRequest Header { get { return _Header; } set { _Header = value; } }

		public BodyRequestConsultarDatosPago Body { get { return _Body; } set { _Body = value; } }


		public MessageRequestConsultarDatosPago()
		{
			_Header = new DataPowerRest.HeadersGenerics.HeadersRequest();
			_Body = new BodyRequestConsultarDatosPago();
		}
	}
}
