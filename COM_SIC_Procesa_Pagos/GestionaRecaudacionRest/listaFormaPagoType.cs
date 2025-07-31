using System;

namespace COM_SIC_Procesa_Pagos.GestionaRecaudacionRest
{
	[Serializable]
	public class listaFormaPagoType
	{
		private string _codigoTipoPago ;
		private string _descripcionTipoPago;
		private string _estadoTipoPago;
		private string _idTipoPago;

		public string codigoTipoPago
		{
			set{_codigoTipoPago = value;}
			get{return _codigoTipoPago;}
		}

		public string descripcionTipoPago
		{
			set{_descripcionTipoPago = value;}
			get{return _descripcionTipoPago;}
		}

		public string estadoTipoPago
		{
			set{_estadoTipoPago = value;}
			get{return _estadoTipoPago;}
		}

		public string idTipoPago
		{
			set{_idTipoPago = value;}
			get{return _idTipoPago;}
		}

		public listaFormaPagoType()
		{
		}
	}
}
