using System;
using System.Text;

namespace COM_SIC_Procesa_Pagos.BWCobroAnticipadoInstalacion
{
	public class ConsultaPAMessageResponse
	{
		private DataPowerRest.HeadersResponse _Header;
		private ConsultaPAResponseBody _Body;

		public DataPowerRest.HeadersResponse Header
		{
			set{_Header = value;}
			get{return _Header;}
		}

		public ConsultaPAResponseBody Body
		{
			set{_Body = value;}
			get{return _Body;}
		}
	}
}
