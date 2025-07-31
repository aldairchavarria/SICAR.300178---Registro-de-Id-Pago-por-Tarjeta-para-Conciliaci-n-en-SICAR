using System;

namespace COM_SIC_Procesa_Pagos.BWPagoEnvioSMS
{
	//INICIATIVA 995-INI
	[Serializable]
	public class MessageRequestPagoEnvioSMS
	{
		private DataPowerRest.HeadersRequest _header;
		private BodyRequestPagoEnvioSMS _body;

		public DataPowerRest.HeadersRequest Header
		{
			set { _header = value; }
			get { return _header; }
		}

		public BodyRequestPagoEnvioSMS Body
		{
			set { _body = value; }
			get { return _body; }
		}

		public MessageRequestPagoEnvioSMS()
		{
			this.Header = new DataPowerRest.HeadersRequest();
			this.Body = new BodyRequestPagoEnvioSMS();
		}
	}
}
