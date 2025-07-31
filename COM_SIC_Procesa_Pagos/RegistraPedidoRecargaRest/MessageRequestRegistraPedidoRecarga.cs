using System;

namespace COM_SIC_Procesa_Pagos.RegistraPedidoRecargaRest
{
	[Serializable]
	public class MessageRequestRegistraPedidoRecarga
	{
		private DataPowerRest.HeadersRequest _header;
		private MessageRequestRegistrarPedidoRecargaBody _body;

		public DataPowerRest.HeadersRequest Header
		{
			set{_header= value;}
			get{ return _header;}
		}

		public MessageRequestRegistrarPedidoRecargaBody Body
		{
			set{_body= value;}
			get{ return _body;}
		}

		public MessageRequestRegistraPedidoRecarga()
		{
			_header = new DataPowerRest.HeadersRequest();
			_body= new MessageRequestRegistrarPedidoRecargaBody();
		}
	}
}
