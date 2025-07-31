using System;

namespace COM_SIC_Procesa_Pagos.RegistraPedidoRecargaRest
{
	[Serializable]
	public class MessageResponseRegistraPedidoRecargaStatus
	{
		private BodyResponseRegistraPedidoRecargaRest _responseStatus;


		public BodyResponseRegistraPedidoRecargaRest responseStatus
		{
			set{_responseStatus= value;}
			get{ return _responseStatus;}
		}

		public MessageResponseRegistraPedidoRecargaStatus()
		{
			
			_responseStatus= new BodyResponseRegistraPedidoRecargaRest();
			
		}
	}
}
