using System;
using System.Text;

namespace COM_SIC_Procesa_Pagos.BWCobroAnticipadoInstalacion
{
	public class ConsultaPAMessageRequest
	{
		private DataPowerRest.HeadersRequest _Header;
		private ConsultaPARequestBody _Body;

		public DataPowerRest.HeadersRequest Header
		{
			set{_Header = value;}
			get{return _Header;}
		}

		public ConsultaPARequestBody Body
		{
			set{_Body = value;}
			get{return _Body;}
		}


	}
}
