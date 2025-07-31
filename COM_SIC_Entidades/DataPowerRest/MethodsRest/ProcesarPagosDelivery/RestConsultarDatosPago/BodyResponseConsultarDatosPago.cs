using System;
using System.Collections;
using COM_SIC_Entidades.DataPowerRest.MethodsRest.ProcesarPagosDelivery;
using COM_SIC_Entidades.DataPowerRest.HeadersGenerics;

namespace COM_SIC_Entidades.DataPowerRest.ProcesarPagosDelivery.RestConsultarDatosPago
{
	[Serializable]
	public class BodyResponseConsultarDatosPago
	{
		private BEAuditResponse _auditResponse;
		private BEDetalleDatosPago[] _datosTransPagDlv;
		private BEListaOpcional _listaOpcional;

		public BEAuditResponse auditResponse {get{ return _auditResponse;} set{_auditResponse = value;}}
		public BEDetalleDatosPago[] datosTransPagDlv {get{ return _datosTransPagDlv;} set{_datosTransPagDlv = value;}}
		public BEListaOpcional listaOpcional {get{ return _listaOpcional;} set{_listaOpcional = value;}}

		public BodyResponseConsultarDatosPago()
		{
			_auditResponse = new BEAuditResponse();
			_listaOpcional = new BEListaOpcional();
		}
	}
}
