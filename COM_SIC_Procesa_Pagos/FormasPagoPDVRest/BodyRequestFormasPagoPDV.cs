using System;
//INI-1019 - YGP
namespace COM_SIC_Procesa_Pagos.FormasPagoPDVRest
{
	[Serializable] 
	public class BodyRequestFormasPagoPDV
	{
		private int _id;
		private string _codPDV;
		private string _codCanal;
		private string _ccins;
		private string _codMedioPago;
		private string _usuario;
		private string _estado;

		public int id
		{
			set { _id = value; }
			get { return _id; }
		}

		public string estado
		{
			set { _estado = value; }
			get { return _estado; }
		}
		
		public string codPDV
		{
			set { _codPDV = value; }
			get { return _codPDV; }
		}

		public string codCanal
		{
			set { _codCanal = value; }
			get { return _codCanal; }
		}

		public string ccins
		{
			set { _ccins = value; }
			get { return _ccins; }
		}

		public string codMedioPago
		{
			set { _codMedioPago = value; }
			get { return _codMedioPago; }
		}

		public string usuario
		{
			set { _usuario = value; }
			get { return _usuario; }
		}

		public BodyRequestFormasPagoPDV()
		{	
		}
	}
}
