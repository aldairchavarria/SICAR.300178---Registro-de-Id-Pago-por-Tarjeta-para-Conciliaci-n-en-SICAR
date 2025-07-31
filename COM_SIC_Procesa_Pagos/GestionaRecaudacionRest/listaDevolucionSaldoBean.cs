using System;

namespace COM_SIC_Procesa_Pagos.GestionaRecaudacionRest
{
	/// <summary>
	/// Summary description for listaDevolucionSaldoBean.
	/// </summary>
	[Serializable]
	public class listaDevolucionSaldoBean
	{

		private string _devolvCodigo;
		private string _devolvProducto;
		private string _devolvCuenta;
		private string _devolvMonto;
		private string _devolvTipDoc;
		private string _devolvNroDoc;
		private string _devolvFecha;
		private string _devolvEstado;
		private string _devolvCodOficina;
		private string _devolvOficina;
		private string _devolvUsuarioCrea;
		private string _devolvUsuAct;
		private string _devolvFechaCrea;
		private string _devolvFechaAct;
		private string _devolvNombre;
		private string _devolvApePat;
		private string _devolvApeMat;
		private string _devolvBioHit;

		public string devolvCodigo
		{
			set{_devolvCodigo = value;}
			get{return _devolvCodigo;}
		}

		public string devolvProducto
		{
			set{_devolvProducto = value;}
			get{return _devolvProducto;}
		}	
	
		public string devolvCuenta
		{
			set{_devolvCuenta = value;}
			get{return _devolvCuenta;}
		}	

		public string devolvMonto
		{
			set{_devolvMonto = value;}
			get{return _devolvMonto;}
		}	

		public string devolvTipDoc
		{
			set{_devolvTipDoc = value;}
			get{return _devolvTipDoc;}
		}	

		public string devolvNroDoc
		{
			set{_devolvNroDoc = value;}
			get{return _devolvNroDoc;}
		}	

		public string devolvFecha
		{
			set{_devolvFecha = value;}
			get{return _devolvFecha;}
		}	

		public string devolvEstado
		{
			set{_devolvEstado = value;}
			get{return _devolvEstado;}
		}	

		public string devolvCodOficina
		{
			set{_devolvCodOficina = value;}
			get{return _devolvCodOficina;}
		}	

		public string devolvOficina
		{
			set{_devolvOficina = value;}
			get{return _devolvOficina;}
		}	

		public string devolvUsuarioCrea
		{
			set{_devolvUsuarioCrea = value;}
			get{return _devolvUsuarioCrea;}
		}	

		public string devolvUsuAct
		{
			set{_devolvUsuAct = value;}
			get{return _devolvUsuAct;}
		}	

		public string devolvFechaCrea
		{
			set{_devolvFechaCrea = value;}
			get{return _devolvFechaCrea;}
		}	

		public string devolvFechaAct
		{
			set{_devolvFechaAct = value;}
			get{return _devolvFechaAct;}
		}	

		public string devolvNombre
		{
			set{_devolvNombre = value;}
			get{return _devolvNombre;}
		}	

		public string devolvApePat
		{
			set{_devolvApePat = value;}
			get{return _devolvApePat;}
		}	

		public string devolvApeMat
		{
			set{_devolvApeMat = value;}
			get{return _devolvApeMat;}
		}	
		public string devolvBioHit
		{
			set{_devolvBioHit = value;}
			get{return _devolvBioHit;}
		}	





		public listaDevolucionSaldoBean()
		{
		}
	}
}
