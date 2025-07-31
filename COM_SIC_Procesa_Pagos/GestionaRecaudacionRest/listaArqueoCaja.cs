using System;

namespace COM_SIC_Procesa_Pagos.GestionaRecaudacionRest
{
	
	public class listaArqueoCaja
	{
		
		private string _region ;
		private string _codigo ;
		private string _descripcion;
		private string _diasArqueo;
		private string _diasLaborales;
		private string _efectividad;
		private string _pendiente;

		public string region
		{
			set{_region = value;}
			get{return _region;}
		}

		public string codigo
		{
			set{_codigo = value;}
			get{return _codigo;}
		}

		public string descripcion
		{
			set{_descripcion = value;}
			get{return _descripcion;}
		}

		public string diasArqueo
		{
			set{_diasArqueo = value;}
			get{return _diasArqueo;}
		}

		public string diasLaborales
		{
			set{_diasLaborales = value;}
			get{return _diasLaborales;}
		}
		public string efectividad
		{
			set{_efectividad = value;}
			get{return _efectividad;}
		}
		public string pendiente
		{
			set{_pendiente = value;}
			get{return _pendiente;}
		}

		public listaArqueoCaja()
		{
		}
	}
}
