using System;

namespace COM_SIC_Procesa_Pagos.BWPagoEnvioSMS
{
	//INICIATIVA 995-INI
	[Serializable]
	public class PagoEnvioSMSResponse
	{
		private MessageResponsePagoEnvioSMS _MessageResponse;

		public MessageResponsePagoEnvioSMS MessageResponse
		{
			set{ _MessageResponse = value; }
			get{ return _MessageResponse; }
		}

		public PagoEnvioSMSResponse()
		{
			this.MessageResponse = new MessageResponsePagoEnvioSMS();
		}
	}
}
