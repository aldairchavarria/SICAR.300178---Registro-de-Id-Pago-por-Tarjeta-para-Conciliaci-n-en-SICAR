using System;

namespace COM_SIC_Procesa_Pagos.RegistraPedidoRecargaRest
{
	[Serializable]
	public class RegistraPedidoRecargaResponse
	{
		private DataPowerRest.BEAuditResponse  _auditResponse;

		public DataPowerRest.BEAuditResponse auditResponse
		{
			set{_auditResponse= value;}
			get{ return _auditResponse;}
		}

		public RegistraPedidoRecargaResponse()
		{
			_auditResponse= new DataPowerRest.BEAuditResponse();

		}
	}
}
