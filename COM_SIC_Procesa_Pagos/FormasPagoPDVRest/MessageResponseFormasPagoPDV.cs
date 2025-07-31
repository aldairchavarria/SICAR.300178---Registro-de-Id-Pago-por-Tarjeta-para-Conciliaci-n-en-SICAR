using System;
//INI-1019 - YGP
namespace COM_SIC_Procesa_Pagos.FormasPagoPDVRest
{
	[Serializable]
	public class MessageResponseFormasPagoPDV
	{
		private DataPowerRest.HeadersResponse _header;
		private BodyResponseFormasPagoPDV _body;

		public DataPowerRest.HeadersResponse Header
		{
			set { _header = value; }
			get { return _header; }
		}

		public BodyResponseFormasPagoPDV Body
		{
			set { _body = value; }
			get { return _body; }
		}

		public MessageResponseFormasPagoPDV()
		{
			this.Header = new DataPowerRest.HeadersResponse();
			this.Body = new BodyResponseFormasPagoPDV();
		}
	}
}
