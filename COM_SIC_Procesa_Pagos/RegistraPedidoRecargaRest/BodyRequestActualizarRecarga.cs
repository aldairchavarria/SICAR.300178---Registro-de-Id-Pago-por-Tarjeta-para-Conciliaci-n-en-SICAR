using System;

namespace COM_SIC_Procesa_Pagos.RegistraPedidoRecargaRest
{
	[Serializable] 
	public class BodyRequestActualizarRecarga
	{

		private string _nroPedido;
		private string _codSerie;
		private string _nrolinea;

		public string nroPedido
		{
			set{_nroPedido= value;}
			get{ return _nroPedido;}
		}

		public string codSerie
		{
			set{_codSerie= value;}
			get{ return _codSerie;}
		}

		public string nrolinea
		{
			set{_nrolinea= value;}
			get{ return _nrolinea;}
		}
		

		public BodyRequestActualizarRecarga()
		{
		}
		
	}
}
