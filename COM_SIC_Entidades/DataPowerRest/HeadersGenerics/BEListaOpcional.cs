using System;

namespace COM_SIC_Entidades.DataPowerRest.HeadersGenerics
{
	[Serializable]
	public class BEListaOpcional
	{

		private string _campo;
		private string _valor;


		public string campo { get { return _campo; } set { _campo = value; } }

		public string valor { get { return _valor; } set { _valor = value; } }

		public BEListaOpcional()
		{
			_campo = string.Empty;
			_valor = string.Empty;
		}
		
	}
}
