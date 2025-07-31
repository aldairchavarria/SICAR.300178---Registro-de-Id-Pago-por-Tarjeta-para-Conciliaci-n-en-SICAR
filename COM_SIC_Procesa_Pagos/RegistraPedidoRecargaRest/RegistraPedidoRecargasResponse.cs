using System;

namespace COM_SIC_Procesa_Pagos.RegistraPedidoRecargaRest
{
	[Serializable]
	public class RegistraPedidoRecargasResponse
	{
		private MessageResponseRegistraPedidoRecarga _MessageResponse;

		public MessageResponseRegistraPedidoRecarga MessageResponse
		{
			set{_MessageResponse= value;}
			get{ return _MessageResponse;}
		}

		public RegistraPedidoRecargasResponse()
		{
			_MessageResponse= new MessageResponseRegistraPedidoRecarga();
		}
	}
}
