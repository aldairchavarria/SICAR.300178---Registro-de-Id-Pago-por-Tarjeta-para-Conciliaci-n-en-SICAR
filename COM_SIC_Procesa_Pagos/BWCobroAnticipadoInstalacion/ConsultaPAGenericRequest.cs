using System;
using System.Text;


namespace COM_SIC_Procesa_Pagos.BWCobroAnticipadoInstalacion
{
	public class ConsultaPAGenericRequest
	{
		private ConsultaPAMessageRequest _MessageRequest ;


		public ConsultaPAMessageRequest MessageRequest
		{
			set{_MessageRequest = value;}
			get{return _MessageRequest;}
		}
	}
}
