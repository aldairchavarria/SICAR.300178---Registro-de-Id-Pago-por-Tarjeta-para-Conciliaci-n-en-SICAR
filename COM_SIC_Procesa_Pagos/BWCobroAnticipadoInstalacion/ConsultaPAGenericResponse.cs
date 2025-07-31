using System;
using System.Text;


namespace COM_SIC_Procesa_Pagos.BWCobroAnticipadoInstalacion
{
	public class ConsultaPAGenericResponse
	{
		public ConsultaPAMessageResponse _MessageResponse;

		public ConsultaPAMessageResponse MessageResponse
		{
			set{_MessageResponse = value;}
			get{return _MessageResponse;}
		}
	}
}
