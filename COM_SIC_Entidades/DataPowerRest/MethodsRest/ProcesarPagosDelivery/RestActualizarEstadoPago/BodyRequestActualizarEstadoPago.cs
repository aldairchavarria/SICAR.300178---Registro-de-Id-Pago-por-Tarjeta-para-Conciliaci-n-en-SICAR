using System;

namespace COM_SIC_Entidades.DataPowerRest.MethodsRest.ProcesarPagosDelivery.RestActualizarEstadoPago
{
	[Serializable]
	public class BodyRequestActualizarEstadoPago
	{
		private BECabeceraEstadoPago _actualizarEstadoPagoRequest;

		public BECabeceraEstadoPago actualizarEstadoPagoRequest { get { return _actualizarEstadoPagoRequest; } set { _actualizarEstadoPagoRequest = value; } }

		public BodyRequestActualizarEstadoPago()
		{
			_actualizarEstadoPagoRequest = new BECabeceraEstadoPago();
		}
	}
}
