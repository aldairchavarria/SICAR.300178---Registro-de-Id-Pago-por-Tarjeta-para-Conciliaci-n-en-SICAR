using System;

namespace COM_SIC_Procesa_Pagos.RegistraPedidoRecargaRest
{
	[Serializable] 
	public class BodyRequestRegistrarRecargaPendiente
	{

		private string _nroPedido;  
		private string _nroPedidoAlta;
		private string _descTipoOperacion;
		private string _codAdquiriente;
		private string _canal;
		private string _moneda;
		private string _datosUsuario;
		private string _nroTerminal;
		private string _trace;
		private string _montoRecarga;
		private string _nroTelefono;
		private string _producto;
		private string _codFormato;
		private string _nroComercio;
		private string _nombreComercio;
		private string _procesador;
		private string _telco;
		private string _tipoComprador;
		private string _codCadena;
		private string _nroReferencia;
		private string _usuarioRegistro;

		public string nroPedido
		{
			set{_nroPedido= value;}
			get{ return _nroPedido;}
		}

		public string nroPedidoAlta
		{
			set{_nroPedidoAlta= value;}
			get{ return _nroPedidoAlta;}
		}

		public string descTipoOperacion
		{
			set{_descTipoOperacion= value;}
			get{ return _descTipoOperacion;}
		}

		public string codAdquiriente
		{
			set{_codAdquiriente= value;}
			get{ return _codAdquiriente;}
		}

		public string canal
		{
			set{_canal= value;}
			get{ return _canal;}
		}

		public string moneda
		{
			set{_moneda= value;}
			get{ return _moneda;}
		}
		public string datosUsuario
		{
			set{_datosUsuario= value;}
			get{ return _datosUsuario;}
		}
		public string nroTerminal
		{
			set{_nroTerminal= value;}
			get{ return _nroTerminal;}
		}
		public string trace
		{
			set{_trace= value;}
			get{ return _trace;}
		}
		public string montoRecarga
		{
			set{_montoRecarga= value;}
			get{ return _montoRecarga;}
		}
		public string nroTelefono
		{
			set{_nroTelefono= value;}
			get{ return _nroTelefono;}
		}

		public string producto
		{
			set{_producto= value;}
			get{ return _producto;}
		}

		public string codFormato
		{
			set{_codFormato= value;}
			get{ return _codFormato;}
		}
		public string nroComercio
		{
			set{_nroComercio= value;}
			get{ return _nroComercio;}
		}

		public string nombreComercio
		{
			set{_nombreComercio= value;}
			get{ return _nombreComercio;}
		}

		public string procesador
		{
			set{_procesador= value;}
			get{ return _procesador;}
		}

		public string telco
		{
			set{_telco= value;}
			get{ return _telco;}
		}

		public string tipoComprador
		{
			set{_tipoComprador= value;}
			get{ return _tipoComprador;}
		}
		
		public string codCadena
		{
			set{_codCadena= value;}
			get{ return _codCadena;}
		}

		public string nroReferencia
		{
			set{_nroReferencia= value;}
			get{ return _nroReferencia;}
		}

		public string usuarioRegistro
		{
			set{_usuarioRegistro= value;}
			get{ return _usuarioRegistro;}
		}




		public BodyRequestRegistrarRecargaPendiente()
		{
			
		}
		
	}
}
