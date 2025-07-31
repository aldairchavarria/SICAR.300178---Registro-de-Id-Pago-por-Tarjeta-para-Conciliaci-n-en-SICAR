using System;

namespace COM_SIC_Procesa_Pagos.GestionaRecaudacionRest
{
	[Serializable]
	public class GestionaRecaudacionResponse
	{
		private DataPowerRest.BEAuditResponse  _auditResponse;
		private listaFormaPagoType _listaFormaPagoType;
		private listaArqueoCaja _listaArqueoCaja;
		private listaDevolucionSaldoBean _listaDevolucionSaldoBean; 

		public DataPowerRest.BEAuditResponse auditResponse
		{
			set{_auditResponse= value;}
			get{ return _auditResponse;}
		}

		public listaFormaPagoType ListaFormaPagoType
		{
			set{_listaFormaPagoType= value;}
			get{ return _listaFormaPagoType;}
		}
		public listaArqueoCaja listaArqueoCaja
		{
			set{_listaArqueoCaja= value;}
			get{ return _listaArqueoCaja;}
		}
		public listaDevolucionSaldoBean listaDevolucionSaldoBean
		{
			set{_listaDevolucionSaldoBean= value;}
			get{ return _listaDevolucionSaldoBean;}
		}
		public GestionaRecaudacionResponse()
		{
			_auditResponse= new DataPowerRest.BEAuditResponse();
			_listaFormaPagoType= new listaFormaPagoType();
			_listaArqueoCaja= new listaArqueoCaja();
			_listaDevolucionSaldoBean= new listaDevolucionSaldoBean();
		}
	}
}
