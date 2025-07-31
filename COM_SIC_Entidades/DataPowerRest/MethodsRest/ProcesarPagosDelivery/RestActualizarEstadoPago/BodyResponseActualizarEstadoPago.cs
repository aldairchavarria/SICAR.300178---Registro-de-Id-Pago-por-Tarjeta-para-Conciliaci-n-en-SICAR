using System;
using COM_SIC_Entidades.DataPowerRest.HeadersGenerics;

namespace COM_SIC_Entidades.DataPowerRest.MethodsRest.ProcesarPagosDelivery.RestActualizarEstadoPago
{
	[Serializable]
	public class BodyResponseActualizarEstadoPago
	{
		private BEAuditResponse _auditResponse;
		private BEListaOpcional _listaOpcional;

		public BEAuditResponse auditResponse {get{ return _auditResponse;} set{_auditResponse = value;}}
		public BEListaOpcional listaOpcional {get{ return _listaOpcional;} set{_listaOpcional = value;}}

		public BodyResponseActualizarEstadoPago()
		{
			_auditResponse = new BEAuditResponse();
			_listaOpcional = new BEListaOpcional();
		}
	}
}
