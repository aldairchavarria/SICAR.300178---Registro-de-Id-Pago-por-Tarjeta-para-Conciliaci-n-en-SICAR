using System;
using System.Text;
using System.Runtime.Serialization;

namespace COM_SIC_Procesa_Pagos.BWCobroAnticipadoInstalacion
{
	public class ConsultaPAResponseType
	{
		private ConsultaPAResponseData _responseData;
		private DataPowerRest.ResponseStatus _responseStatus;

		public ConsultaPAResponseData responseData
		{
			set{_responseData = value;}
			get{return _responseData;}
		}

		public DataPowerRest.ResponseStatus responseStatus
		{
			set{_responseStatus = value;}
			get{return _responseStatus;}
		}
	}
}
