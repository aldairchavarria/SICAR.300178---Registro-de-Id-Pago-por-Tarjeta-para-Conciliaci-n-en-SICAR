using System;
//INI-1019 - YGP
namespace COM_SIC_Procesa_Pagos.FormasPagoPDVRest
{
	[Serializable]
	public class MessageRequestFormasPagoPDV
	{
		private DataPowerRest.HeadersRequest _header;
		private BodyRequestFormasPagoPDV _body;

		public DataPowerRest.HeadersRequest Header
		{
			set { _header = value; }
			get { return _header; }
		}

		public BodyRequestFormasPagoPDV Body
		{
			set { _body = value; }
			get { return _body; }
		}

		public MessageRequestFormasPagoPDV()
		{
			this.Header = new DataPowerRest.HeadersRequest();
			this.Body = new BodyRequestFormasPagoPDV();
		}
	}
}
