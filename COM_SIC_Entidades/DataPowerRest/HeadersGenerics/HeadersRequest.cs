//INICIO PROY-140332 (Try & Buy)
using System;
using System.Runtime.Serialization;

namespace COM_SIC_Entidades.DataPowerRest.HeadersGenerics
{
	/// <summary>
	/// Summary description for HeadersRequest.
	/// </summary>
	[Serializable]
	public class HeadersRequest
	{
		private HeaderRequest _headerRequest;

		public HeaderRequest HeaderRequest
		{
			set{_headerRequest= value;}
			get{ return _headerRequest;}
		}

		public HeadersRequest()
		{
			_headerRequest = new HeaderRequest();
		}
	}
	//FIN PROY-140332 (Try & Buy)
}
