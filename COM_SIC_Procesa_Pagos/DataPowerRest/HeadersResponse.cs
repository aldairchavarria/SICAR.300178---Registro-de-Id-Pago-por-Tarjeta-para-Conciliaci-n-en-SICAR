using System;

namespace COM_SIC_Procesa_Pagos.DataPowerRest
{
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
}
