using System;

namespace COM_SIC_Procesa_Pagos.RegistraPedidoRecargaRest
{
	[Serializable]
	public class MessageResponseRegistraPedidoRecarga
	{

		private DataPowerRest.HeadersResponse _header;
		private MessageResponseRegistraPedidoRecargaBody _body;

		public DataPowerRest.HeadersResponse Header
		{
			set{_header= value;}
			get{ return _header;}
		}

		public MessageResponseRegistraPedidoRecargaBody Body
		{
			set{_body= value;}
			get{ return _body;}
		}

		public MessageResponseRegistraPedidoRecarga()
		{
			_header = new DataPowerRest.HeadersResponse();
			_body = new MessageResponseRegistraPedidoRecargaBody();
		}
	}
}
