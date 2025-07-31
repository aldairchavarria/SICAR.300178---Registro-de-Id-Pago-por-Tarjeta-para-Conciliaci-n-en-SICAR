using System;
//INI-1019 - YGP
namespace COM_SIC_Procesa_Pagos.FormasPagoPDVRest
{
	[Serializable]
	public class BodyResponseFormasPagoPDV
	{
		private string _codigoRespuesta;
		private string _mensajeRespuesta;
		private listaFormaPagoPDV[] _listaFormaPagoPDV;
		private listaOficinasVenta[] _listaOficinasVenta;	
	
		public listaFormaPagoPDV[] listaFormaPagoPDV
		{
			set { _listaFormaPagoPDV = value; }
			get { return _listaFormaPagoPDV; }
		}

		public listaOficinasVenta[] listaOficinasVenta
		{
			set { _listaOficinasVenta = value; }
			get { return _listaOficinasVenta; }
		}

		public string codigoRespuesta
		{
			set { _codigoRespuesta = value; }
			get { return _codigoRespuesta; }
		}

		public string mensajeRespuesta
		{
			set { _mensajeRespuesta = value; }
			get { return _mensajeRespuesta; }
		}

		public BodyResponseFormasPagoPDV()
		{
			this.listaFormaPagoPDV = new listaFormaPagoPDV[0]; 
			this.listaOficinasVenta = new listaOficinasVenta[0];
		}
	}
}
