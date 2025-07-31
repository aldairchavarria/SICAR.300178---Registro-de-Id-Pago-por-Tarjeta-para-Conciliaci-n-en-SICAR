using System;
using System.Text;

namespace COM_SIC_Procesa_Pagos.BWCobroAnticipadoInstalacion
{
	public class ConsultaPAResponseBody
	{
		private ConsultaPAResponseType _consultaPAResponseType;

		public ConsultaPAResponseType consultaPAResponseType
		{
			set{_consultaPAResponseType = value;}
			get{return _consultaPAResponseType;}
		}
	}
}
