using System;

namespace COM_SIC_Procesa_Pagos.GestionaRecaudacionRest
{
	[Serializable] 
	public class BodyRequestGestionaRecaudacion
	{

		private string _variableClob;
		private string _fechaInicio;
		private string _fechaFin;
		private string _datosClob;
			
		private string _devolvCodigo;
		private string _devolvMonto;
		private string _devolvEstado;
		private string _devolvUsuario;
		private string _accion;

		private string _idTransaccion;
		private string _codigoAplicacion;
		private string _tipoServicio;
		private string _clienteNroCuenta;
		private string _usuarioAplicaion;

		//INICIATIVA - 529 INI
		private string _nroTransaccion;
		private string _rowId;
		private string _viaPago;
		private string _descViaPago;
		private string _nroCheque;
		//INICIATIVA - 529 FIN

		public string variableClob
		{
			set{_variableClob= value;}
			get{ return _variableClob;}
		}

		public string fechaInicio
		{
			set{_fechaInicio= value;}
			get{ return _fechaInicio;}
		}

		public string fechaFin
		{
			set{_fechaFin= value;}
			get{ return _fechaFin;}
		}

	        public string datosClob
		{
			set{_datosClob= value;}
			get{ return _datosClob;}
		}

		public string devolvCodigo
		{
			set{_devolvCodigo= value;}
			get{ return _devolvCodigo;}
		}
		public string devolvMonto
		{
			set{_devolvMonto= value;}
			get{ return _devolvMonto;}
		}
		public string devolvEstado
		{
			set{_devolvEstado= value;}
			get{ return _devolvEstado;}
		}
		public string devolvUsuario
		{
			set{_devolvUsuario= value;}
			get{ return _devolvUsuario;}
		}
		public string accion
		{
			set{_accion= value;}
			get{ return _accion;}
		}
		public string idTransaccion
		{
			set{_idTransaccion= value;}
			get{ return _idTransaccion;}
		}
		public string codigoAplicacion
		{
			set{_codigoAplicacion= value;}
			get{ return _codigoAplicacion;}
		}
		public string tipoServicio
		{
			set{_tipoServicio= value;}
			get{ return _tipoServicio;}
		}
		public string clienteNroCuenta
		{
			set{_clienteNroCuenta= value;}
			get{ return _clienteNroCuenta;}
		}
		public string usuarioAplicaion
		{
			set{_usuarioAplicaion= value;}
			get{ return _usuarioAplicaion;}
		}

		//INICIATIVA - 529 INI
		public string nroTransaccion
		{
			set{_nroTransaccion= value;}
			get{ return _nroTransaccion;}
		}

		public string rowId
		{
			set{_rowId= value;}
			get{ return _rowId;}
		}

		public string viaPago
		{
			set{_viaPago= value;}
			get{ return _viaPago;}
		}

		public string descViaPago
		{
			set{_descViaPago= value;}
			get{ return _descViaPago;}
		}

		public string nroCheque
		{
			set{_nroCheque= value;}
			
			get{ return _nroCheque;}
		}
		//INICIATIVA - 529 FIN

		public BodyRequestGestionaRecaudacion()
		{
			
		}
		//
	}
}
