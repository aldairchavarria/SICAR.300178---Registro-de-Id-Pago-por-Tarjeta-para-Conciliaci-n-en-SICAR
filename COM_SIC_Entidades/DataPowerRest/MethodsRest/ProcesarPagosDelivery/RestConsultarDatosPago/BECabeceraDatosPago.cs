using System;
using System.Collections;
using COM_SIC_Entidades.DataPowerRest.HeadersGenerics;

namespace COM_SIC_Entidades.DataPowerRest.ProcesarPagosDelivery.RestConsultarDatosPago
{
	[Serializable]
	public class BECabeceraDatosPago
	{

		private string _uuid;
		private string _nroPedido;
		private ArrayList _listaOpcional;


		public string uuid { get { return _uuid; } set { _uuid = value; } }

		public string nroPedido { get { return _nroPedido; } set { _nroPedido = value; } }

		public ArrayList listaOpcional  { get { return _listaOpcional; } set { _listaOpcional = value; } }

		public BECabeceraDatosPago()
		{
			_uuid = string.Empty;
			_nroPedido = string.Empty;
			_listaOpcional = new ArrayList();
		}
	}
}
