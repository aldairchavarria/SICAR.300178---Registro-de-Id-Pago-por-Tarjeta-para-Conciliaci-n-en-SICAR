using System;
using System.Text;

namespace COM_SIC_Entidades.DataPowerRest.HeadersGenerics
{
	[Serializable]
	public class BEAuditResponse
	{
		private string _codigoRespuesta;
		private string _mensajeRespuesta;
		private string _idTransaccion;


		public string codigoRespuesta { get { return _codigoRespuesta; } set { _codigoRespuesta = value; } }
		public string mensajeRespuesta { get { return _mensajeRespuesta; } set { _mensajeRespuesta = value; } }
		public string idTransaccion { get { return _idTransaccion; } set { _idTransaccion = value; } }

		public BEAuditResponse()
		{
			_codigoRespuesta = string.Empty;
			_mensajeRespuesta = string.Empty;
			_idTransaccion = string.Empty;
		}
	}
}
