using System;
//INI-1019 - YGP
namespace COM_SIC_Procesa_Pagos.FormasPagoPDVRest
{
	[Serializable]
	public class FormasPagoPDVRequest
	{
		private MessageRequestFormasPagoPDV _MessageRequest;

		public MessageRequestFormasPagoPDV MessageRequest
		{
			set { _MessageRequest = value; }
			get { return _MessageRequest; }
		}

		public FormasPagoPDVRequest()
		{
			this.MessageRequest = new MessageRequestFormasPagoPDV();
		}
	}
}
