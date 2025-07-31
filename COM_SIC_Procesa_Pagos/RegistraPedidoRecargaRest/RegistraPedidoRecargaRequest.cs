using System;

namespace COM_SIC_Procesa_Pagos.RegistraPedidoRecargaRest
{
	[Serializable]
	public class RegistraPedidoRecargaRequest
	{
		private MessageRequestRegistraPedidoRecarga _MessageRequest;


		public MessageRequestRegistraPedidoRecarga MessageRequest
		{
			set{_MessageRequest= value;}
			get{ return _MessageRequest;}
		}

	

		public RegistraPedidoRecargaRequest()
		{
			_MessageRequest= new MessageRequestRegistraPedidoRecarga();
		}
	}
}
