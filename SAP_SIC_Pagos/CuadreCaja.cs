using System;
using System.Runtime.Serialization;
namespace SAP_SIC_Pagos
{
	/// <summary>
	/// Summary description for CuadreCaja.
	/// </summary>
	[Serializable()] //PROY-140126
	public class CuadreCaja
	{
		private int _CAJA_CODIGO;
		private string _CAJA_DESCRIPCION;
		private string _CAJA_MONTO;
		public CuadreCaja(){}

		public int CAJA_CODIGO
		{
			set{_CAJA_CODIGO = value;}
			get{ return _CAJA_CODIGO;}
		}
		public string CAJA_DESCRIPCION
		{
			set{_CAJA_DESCRIPCION = value;}
			get{ return _CAJA_DESCRIPCION;}
		}
		public string CAJA_MONTO
		{
			set{_CAJA_MONTO = value;}
			get{ return _CAJA_MONTO;}
		}
	}
}
