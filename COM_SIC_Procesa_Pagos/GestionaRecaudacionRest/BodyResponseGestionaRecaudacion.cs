using System;

namespace COM_SIC_Procesa_Pagos.GestionaRecaudacionRest
{
	[Serializable]
	public class BodyResponseGestionaRecaudacion
	{

		private string _codigoRespuesta;
		private string _mensajeRespuesta;
		private string _mensajeError;
		private listaFormaPagoType[] _listaFormaPagoType;
		private listaArqueoCaja[] _listaArqueoCaja;
		private listaDevolucionSaldoBean[] _listaDevolucionSaldoBean;
		private string _devolvCodigo;
		private int _saldoFavor;
		private string _estadoCuenta;

		

		public string codigoRespuesta
		{
			set{_codigoRespuesta= value;}
			get{ return _codigoRespuesta;}
		}

		public string mensajeRespuesta
		{
			set{_mensajeRespuesta= value;}
			get{ return _mensajeRespuesta;}
		}

		public string mensajeError
		{
			set{_mensajeError= value;}
			get{ return _mensajeError;}
		}

		public listaFormaPagoType[] listaFormaPagoType
		{
			set{_listaFormaPagoType= value;}
			get{ return _listaFormaPagoType;}
		}

		public listaArqueoCaja[] listaArqueoCaja
		{
			set{_listaArqueoCaja= value;}
			get{ return _listaArqueoCaja;}
		}

		public listaDevolucionSaldoBean[] listaDevolucionSaldoBean
		{
			set{_listaDevolucionSaldoBean= value;}
			get{ return _listaDevolucionSaldoBean;}
		}

		public string devolvCodigo
		{
			set{_devolvCodigo= value;}
			get{ return _devolvCodigo;}
		}
		
		public int saldoFavor
		{
			set{_saldoFavor= value;}
			get{ return _saldoFavor;}
		}

		public string estadoCuenta
		{
			set{_estadoCuenta= value;}
			get{ return _estadoCuenta;}
		}

		public BodyResponseGestionaRecaudacion()
		{
			_listaFormaPagoType= new  listaFormaPagoType[0];
			_listaArqueoCaja= new  listaArqueoCaja[0];
			_listaDevolucionSaldoBean= new  listaDevolucionSaldoBean[0];
		}
	}
}
