using System;
//INI-1019- YGP - CREACION DE NUEVA CLASE Y SUS PROPIEDADES
namespace COM_SIC_Procesa_Pagos.FormasPagoPDVRest
{
	[Serializable]
	public class listaOficinasVenta
	{
		private string _codCanal;
		private string _descOficinaVenta;
		private string _codPDV;

		public string codCanal
		{
			set { _codCanal = value; }
			get { return _codCanal; }
		}

		public string descOficinaVenta
		{
			set { _descOficinaVenta = value; }
			get { return _descOficinaVenta; }
		}

		public string  codPDV
		{
			set { _codPDV = value; }
			get { return _codPDV; }
		}

		public listaOficinasVenta()
		{
		}
	}
}
