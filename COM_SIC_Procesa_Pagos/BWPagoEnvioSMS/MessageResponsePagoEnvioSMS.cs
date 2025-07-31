using System;

namespace COM_SIC_Procesa_Pagos.BWPagoEnvioSMS
{//INICIATIVA 995-INI
	[Serializable]
	public class MessageResponsePagoEnvioSMS
	{
		private DataPowerRest.HeadersResponse _header;
		private BodyResponsePagoEnvioSMS _body;

		public DataPowerRest.HeadersResponse Header
		{
			set { _header = value; }
			get { return _header; }
		}

		public BodyResponsePagoEnvioSMS Body
		{
			set { _body = value; }
			get { return _body; }
		}

		public MessageResponsePagoEnvioSMS()
		{
			this.Header = new DataPowerRest.HeadersResponse();
			this.Body = new BodyResponsePagoEnvioSMS();
		}
	}
}
