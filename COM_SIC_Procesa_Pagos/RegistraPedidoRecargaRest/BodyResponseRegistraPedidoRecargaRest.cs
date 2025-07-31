using System;

namespace COM_SIC_Procesa_Pagos.RegistraPedidoRecargaRest
{
	[Serializable]
	public class BodyResponseRegistraPedidoRecargaRest
	{

		private string _codigoRespuesta;
		private string _mensajeRespuesta;
		private string _mensajeError;

		public string codigoRespuesta
		{
			set{_codigoRespuesta= value;}
			get{ return _codigoRespuesta;}
		}

		public string mensajeRespuesta
		{
			set{_mensajeRespuesta= value;}
			get{ return _mensajeRespuesta;}
		}

		public string mensajeError
		{
			set{_mensajeError= value;}
			get{ return _mensajeError;}
		}

		public BodyResponseRegistraPedidoRecargaRest()
		{

		}
	}
}
