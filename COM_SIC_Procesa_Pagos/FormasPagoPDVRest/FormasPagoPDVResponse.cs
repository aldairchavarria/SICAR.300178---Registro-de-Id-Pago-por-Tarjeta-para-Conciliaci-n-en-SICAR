using System;
//INI-1019 - YGP
namespace COM_SIC_Procesa_Pagos.FormasPagoPDVRest
{
	[Serializable]
	public class FormasPagoPDVResponse
	{
		private MessageResponseFormasPagoPDV _MessageResponse;

		public MessageResponseFormasPagoPDV MessageResponse
		{
			set{ _MessageResponse = value; }
			get{ return _MessageResponse; }
		}

		public FormasPagoPDVResponse()
		{
			this.MessageResponse = new MessageResponseFormasPagoPDV();
		}
	}
}
