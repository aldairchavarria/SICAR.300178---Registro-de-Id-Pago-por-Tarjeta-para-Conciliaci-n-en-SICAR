using System;

namespace COM_SIC_Entidades.DataPowerRest.ProcesarPagosDelivery.RestConsultarDatosPago
{
	[Serializable]
	public class BodyRequestConsultarDatosPago
	{
		private BECabeceraDatosPago _consultarDatosPagoRequest;

		public BECabeceraDatosPago consultarDatosPagoRequest { get { return _consultarDatosPagoRequest; } set { _consultarDatosPagoRequest = value; } }

		public BodyRequestConsultarDatosPago()
		{
			_consultarDatosPagoRequest = new BECabeceraDatosPago();
		}
	}
}
