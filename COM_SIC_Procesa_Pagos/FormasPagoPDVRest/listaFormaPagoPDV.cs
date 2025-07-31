using System;
//INI-1019 - YGP - CREACION DE NUEVA CLASE Y SUS PROPIEDADES
namespace COM_SIC_Procesa_Pagos.FormasPagoPDVRest
{
	[Serializable]
	public class listaFormaPagoPDV
	{
		private int _id;
		private string _codigoPDV ;
		private string _descOficinaVenta;
		private string _ccins;
		private string _descMedioPago;
		private string _estado;
		private string _estadoMedioPago;

		public int id
		{
			set { _id = value; }
			get { return _id; }
		}

		public string codigoPDV
		{
			set { _codigoPDV = value; }
			get { return _codigoPDV; }
		}

		public string descOficinaVenta
		{
			set { _descOficinaVenta = value; }
			get { return _descOficinaVenta; }
		}

		public string ccins
		{
			set { _ccins = value; }
			get { return _ccins; }
		}

		public string descMedioPago
		{
			set { _descMedioPago = value; }
			get { return _descMedioPago; }
		}

		public string estado
		{
			set { _estado = value; }
			get { return _estado; }
		}

		public string estadoMedioPago
		{
			set { _estadoMedioPago = value; }
			get { return _estadoMedioPago; }
		}

		public listaFormaPagoPDV()
		{
		}
	}
}
