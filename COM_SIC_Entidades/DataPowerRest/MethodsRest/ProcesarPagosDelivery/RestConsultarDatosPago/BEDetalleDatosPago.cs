using System;

namespace COM_SIC_Entidades.DataPowerRest.ProcesarPagosDelivery.RestConsultarDatosPago
{
	[Serializable]
	public class BEDetalleDatosPago
	{

		private string _uuid;
		private string _fechaRegistro;
		private string _pedinNroPedido;
		private string _usuarioPago;
		private string _codMedioPago;
		private string _codPDV;
		private string _linea;
		private string _tipoOperacion;
		private string _tipoVenta;
		private string _tipoDocMot;
		private string _nroDocMot;
		private string _flagTiendaVirtual;
		private string _codEstado;
		private string _codRpta;
		private string _msjRpta;
		private string _nodoSicar;
		private string _flagReintentar;
		private string _error;
		private string _intentos;
		private string _fechaActualizacion;
		private string _codMedioPagora; 
        //PROY-140582 - INI
		private string _flagPm;
		//PROY-140582 - FIN
		
		public string uuid { get { return _uuid; } set { _uuid = value; } }
		public string fechaRegistro { get { return _fechaRegistro; } set { _fechaRegistro = value; } }
		public string pedinNroPedido { get { return _pedinNroPedido; } set { _pedinNroPedido = value; } }
		public string usuarioPago { get { return _usuarioPago; } set { _usuarioPago = value; } }
		public string codMedioPago { get { return _codMedioPago; } set { _codMedioPago = value; } }
		public string codPDV { get { return _codPDV; } set { _codPDV = value; } }
		public string linea { get { return _linea; } set { _linea = value; } }
		public string tipoOperacion { get { return _tipoOperacion; } set { _tipoOperacion = value; } }
		public string tipoVenta { get { return _tipoVenta; } set { _tipoVenta = value; } }
		public string tipoDocMot { get { return _tipoDocMot; } set { _tipoDocMot = value; } }
		public string nroDocMot { get { return _nroDocMot; } set { _nroDocMot = value; } }
		public string flagTiendaVirtual { get { return _flagTiendaVirtual; } set { _flagTiendaVirtual = value; } }
		public string codEstado { get { return _codEstado; } set { _codEstado = value; } }
		public string codRpta { get { return _codRpta; } set { _codRpta = value; } }
		public string msjRpta { get { return _msjRpta; } set { _msjRpta = value; } }
		public string nodoSicar { get { return _nodoSicar; } set { _nodoSicar = value; } }
		public string flagReintentar { get { return _flagReintentar; } set { _flagReintentar = value; } }
		public string error { get { return _error; } set { _error = value; } }
		public string intentos { get { return _intentos; } set { _intentos = value; } }
		public string fechaActualizacion { get { return _fechaActualizacion; } set { _fechaActualizacion = value; } }
		public string codMedioPagora  { get { return _codMedioPagora; } set { _codMedioPagora = value; } }
        //PROY-140582 - INI
		public string flagPm { get { return _flagPm; } set { _flagPm = value; }}
		//PROY-140582 - FIN
		

		public BEDetalleDatosPago()
		{
			_uuid = string.Empty;
			_fechaRegistro = string.Empty;
			_pedinNroPedido = string.Empty;
			_usuarioPago = string.Empty;
			_codMedioPago = string.Empty;
			_codPDV = string.Empty;
			_linea = string.Empty;
			_tipoOperacion = string.Empty;
			_tipoVenta = string.Empty;
			_tipoDocMot = string.Empty;
			_nroDocMot = string.Empty;
			_flagTiendaVirtual = string.Empty;
			_codEstado = string.Empty;
			_codRpta = string.Empty;
			_msjRpta = string.Empty;
			_nodoSicar = string.Empty;
			_fechaActualizacion = string.Empty;
			_error = string.Empty;
			_intentos = string.Empty;
			_flagReintentar = string.Empty;
			_codMedioPagora = string.Empty;
			//PROY-140582 - INI
			_flagPm = string.Empty;
			//PROY-140582 - FIN
		}
	}
}
