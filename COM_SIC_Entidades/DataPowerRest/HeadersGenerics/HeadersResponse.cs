//INICIO PROY-140332 (Try & Buy)
using System;
using System.Runtime.Serialization;

namespace COM_SIC_Entidades.DataPowerRest.HeadersGenerics
{
	/// <summary>
	/// Summary description for HeadersResponse.
	/// </summary>
	[Serializable]
	public class HeadersResponse
	{
		private HeaderResponse _headerResponse;

		public HeaderResponse HeaderResponse
		{
			set{_headerResponse= value;}
			get{ return _headerResponse;}
		}

		public HeadersResponse()
		{
			_headerResponse = new HeaderResponse();
		}
	}
	//FIN PROY-140332 (Try & Buy)
}
