using System;

namespace COM_SIC_Procesa_Pagos.DataPowerRest
{
	
	public class BEAuditRequest
	{
		private string _idTransaccion;
		private string _ipAplicacion ; 
		private string _nombreAplicacion;
		private string  _usuarioAplicacion;


		public string idTransaccion
		{
			set{_idTransaccion= value;}
			get{ return _idTransaccion;}
		}

		public string ipAplicacion
		{
			set{_ipAplicacion= value;}
			get{ return _ipAplicacion;}
		}

		public string nombreAplicacion
		{
			set{_nombreAplicacion= value;}
			get{ return _nombreAplicacion;}
		}

		public string usuarioAplicacion
		{
			set{_usuarioAplicacion= value;}
			get{ return _usuarioAplicacion;}
		}
				

		public BEAuditRequest()
		{
			
		}
	}
}
