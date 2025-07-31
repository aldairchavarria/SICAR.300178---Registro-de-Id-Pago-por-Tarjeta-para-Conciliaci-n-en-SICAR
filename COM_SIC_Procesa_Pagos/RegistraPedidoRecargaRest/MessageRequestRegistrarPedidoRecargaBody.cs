using System;

namespace COM_SIC_Procesa_Pagos.RegistraPedidoRecargaRest
{
	[Serializable]
	public class MessageRequestRegistrarPedidoRecargaBody
	{
		private BodyRequestActualizarRecarga _actualizarRecargaRequest;
		private BodyRequestRegistrarRecargaPendiente _registrarRecargaPendienteRequest;


		public BodyRequestActualizarRecarga actualizarRecargaRequest
		{
			set{_actualizarRecargaRequest= value;}
			get{ return _actualizarRecargaRequest;}
		}
		public BodyRequestRegistrarRecargaPendiente registrarRecargaPendienteRequest
		{
			set{_registrarRecargaPendienteRequest= value;}
			get{ return _registrarRecargaPendienteRequest;}
		}

		public MessageRequestRegistrarPedidoRecargaBody()
		{
			
			_actualizarRecargaRequest= new BodyRequestActualizarRecarga();
			_registrarRecargaPendienteRequest= new BodyRequestRegistrarRecargaPendiente();
		}
	}
}
