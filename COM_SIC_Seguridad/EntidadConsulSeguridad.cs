using System;

namespace COM_SIC_Seguridad
{
	/// <summary>
	/// Descripción breve de EntidadConsulSeguridad.
	/// </summary>
	[Serializable]
	public class EntidadConsulSeguridad
	{
		private string _USUACCOD;
		private string _PERFCCOD;
		private string _USUACCODVENSAP;
		private string _APLICCOD;
		private string _OPCCODPAD;
		private string _OPCICCOD;
		private string _OPCICNIVPAD;
		private string _OPCICNIV;
		private string _OPCICDES;
		private string _OPCICABREV;
		private string _OPCICNOMPAG;
		private string _OPCICNUMORD;
		private string _OPCICD1;

		private string _descripcion2;

		public EntidadConsulSeguridad()
		{
			//
			// TODO: agregar aquí la lógica del constructor
			//
		}

		public string USUACCOD
		{
			set { _USUACCOD = value; }
			get { return _USUACCOD; }

		}
		public string PERFCCOD
		{
			set { _PERFCCOD = value; }
			get { return _PERFCCOD; }
		}
		public string USUACCODVENSAP
		{
			set { _USUACCODVENSAP = value; }
			get { return _USUACCODVENSAP; }
		}
		public string APLICCOD
		{
			set { _APLICCOD = value; }
			get { return _APLICCOD; }
		}
		public string OPCCODPAD
		{
			set { _OPCCODPAD = value; }
			get { return _OPCCODPAD; }
		}
		public string OPCICCOD
		{
			set { _OPCICCOD = value; }
			get { return _OPCICCOD; }
		}
		public string OPCICNIVPAD
		{
			set { _OPCICNIVPAD = value; }
			get { return _OPCICNIVPAD; }
		}
		public string OPCICNIV
		{
			set { _OPCICNIV = value; }
			get { return _OPCICNIV; }
		}
		public string OPCICDES
		{
			set { _OPCICDES = value; }
			get { return _OPCICDES; }
		}
		public string OPCICABREV
		{
			set { _OPCICABREV = value; }
			get { return _OPCICABREV; }
		}
		public string OPCICNOMPAG
		{
			set { _OPCICNOMPAG = value; }
			get { return _OPCICNOMPAG; }
		}
		public string OPCICNUMORD
		{
			set { _OPCICNUMORD = value; }
			get { return _OPCICNUMORD; }
		}
		public string OPCICD1
		{
			set { _OPCICD1 = value; }
			get { return _OPCICD1; }
		}

		public string Descripcion2
		{
			set{_descripcion2= value;}
			get{ return _descripcion2;}
		}
	}
}
