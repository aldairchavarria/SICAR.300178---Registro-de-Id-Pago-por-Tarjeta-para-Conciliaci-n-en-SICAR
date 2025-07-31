using System;

namespace COM_SIC_Procesa_Pagos.BWPagoEnvioSMS
{//INICIATIVA 995-INI
	[Serializable]
	public class PagoEnvioSMSRequest
	{
		private MessageRequestPagoEnvioSMS _MessageRequest;

		public MessageRequestPagoEnvioSMS MessageRequest
		{
			set { _MessageRequest = value; }
			get { return _MessageRequest; }
		}

		public PagoEnvioSMSRequest()
		{
			this.MessageRequest = new MessageRequestPagoEnvioSMS();
		}
	}
}
