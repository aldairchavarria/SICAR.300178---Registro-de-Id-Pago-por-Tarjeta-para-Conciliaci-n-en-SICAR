using System;
using System.Collections;

using COM_SIC_Entidades.DataPowerRest.HeadersGenerics;

namespace COM_SIC_Entidades.DataPowerRest.MethodsRest.ProcesarPagosDelivery.RestActualizarEstadoPago
{
	[Serializable]
	public class BECabeceraEstadoPago
	{
		private string _uuid;
		private string _codEstado;
		private string _codRpta;
		private string _msjRpta;
		private string _nodoSicar;
		private string _flagReintentar;
		private string _error;
		private ArrayList _listaOpcional;


		public string uuid { get { return _uuid; } set { _uuid = value; } }

		public string codEstado { get { return _codEstado; } set { _codEstado = value; } }

		public string codRpta { get { return _codRpta; } set { _codRpta = value; } }

		public string msjRpta { get { return _msjRpta; } set { _msjRpta = value; } }

		public string nodoSicar { get { return _nodoSicar; } set { _nodoSicar = value; } }

		public string flagReintentar { get { return _flagReintentar; } set { _flagReintentar = value; } }

		public string error { get { return _error; } set { _error = value; } }

		public ArrayList listaOpcional  { get { return _listaOpcional; } set { _listaOpcional = value; } }

		public BECabeceraEstadoPago()
		{
			_uuid = string.Empty;
			_codEstado = string.Empty;
			_codRpta = string.Empty;
			_msjRpta = string.Empty;
			_nodoSicar = string.Empty;
			_flagReintentar = string.Empty;
			_error = string.Empty;
			_listaOpcional = new ArrayList();
		}
	}
}
