using System;

namespace COM_SIC_Procesa_Pagos.DataPowerRest
{
	//INICIATIVA 712 Cobro Anticipado
	/// <summary>
	/// Summary description for ResponseStatus.
	/// </summary>
	public class ResponseStatus
	{
		public ResponseStatus()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		private string _idTransaccion;
		private string _codigoRespuesta;
		private string _mensajeRespuesta;


		public string idTransaccion
		{
			set{_idTransaccion = value;}
			get{return _idTransaccion;}
		}

		public string codigoRespuesta
		{
			set{_codigoRespuesta = value;}
			get{return _codigoRespuesta;}
		}

		public string mensajeRespuesta
		{
			set{_mensajeRespuesta = value;}
			get{return _mensajeRespuesta;}
		}
	}
}
