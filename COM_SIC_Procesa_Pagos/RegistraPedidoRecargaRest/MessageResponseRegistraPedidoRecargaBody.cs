using System;

namespace COM_SIC_Procesa_Pagos.RegistraPedidoRecargaRest
{
	[Serializable]
	public class MessageResponseRegistraPedidoRecargaBody
	{
		private MessageResponseRegistraPedidoRecargaStatus _actualizarRecargaResponseType;
		private MessageResponseRegistraPedidoRecargaStatus _registrarRecargaPendienteResponseType;



		public MessageResponseRegistraPedidoRecargaStatus actualizarRecargaResponseType
		{
			set{_actualizarRecargaResponseType= value;}
			get{ return _actualizarRecargaResponseType;}
		}

		public MessageResponseRegistraPedidoRecargaStatus registrarRecargaPendienteResponseType
		{
			set{_registrarRecargaPendienteResponseType= value;}
			get{ return _registrarRecargaPendienteResponseType;}
		}
		public MessageResponseRegistraPedidoRecargaBody()
		{
			_actualizarRecargaResponseType= new MessageResponseRegistraPedidoRecargaStatus();
			_registrarRecargaPendienteResponseType= new MessageResponseRegistraPedidoRecargaStatus();
		}
	}
}
