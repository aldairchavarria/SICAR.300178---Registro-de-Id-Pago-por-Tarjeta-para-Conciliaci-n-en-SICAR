using System;

namespace COM_SIC_Procesa_Pagos.DataPowerRest
{
	public class HeadersRequest
	{
		private HeaderRequest _headerRequest ;
		
		public HeaderRequest HeaderRequest
		{
			set{_headerRequest= value;}
			get{ return _headerRequest;}
		}

		public HeadersRequest()
		{
		_headerRequest= new HeaderRequest();
		}
	}
}
